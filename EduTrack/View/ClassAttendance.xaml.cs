using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using EduTrack.Data;
using EduTrack.Models;
using EduTrack.Utilities;

namespace EduTrack.View
{
    /// <summary>
    /// Roll call per lesson: choose the day, choose the lesson, mark each teacher and student Present, Absent, Late or Excused.
    /// The Report view adds up the marks per person over a period.
    /// </summary>
    public partial class ClassAttendance : UserControl
    {
        private const int AbsenceAlert = 3;

        private readonly ClassWorkspace _host;
        private List<SessionInfo> _sessions = new List<SessionInfo>();
        private SessionInfo _session;
        private ObservableCollection<AttendanceEntry> _rows = new ObservableCollection<AttendanceEntry>();
        private bool _dirty;
        private bool _suppress;
        private ClassSummary Class => _host.Current;

        public ClassAttendance(ClassWorkspace host)
        {
            InitializeComponent();
            _host = host;

            var times = new List<string>();
            for (int minutes = 7 * 60; minutes <= 21 * 60; minutes += 15) times.Add(TimeSpan.FromMinutes(minutes).ToString(@"hh\:mm"));
            ExtraStart.ItemsSource = times; ExtraEnd.ItemsSource = times;
            ExtraStart.SelectedItem = "09:00"; ExtraEnd.SelectedItem = "10:00";

            _suppress = true;
            DayPicker.SelectedDate = host.PendingDate ?? DateTime.Today;
            FromPicker.SelectedDate = DateTime.Today.AddDays(-30);
            ToPicker.SelectedDate = DateTime.Today;
            _suppress = false;

            Loaded += async (s, e) => await LoadDayAsync(host.PendingSession);
            // Leaving the tab with marks not saved yet would lose them, so they are saved.
            Unloaded += async (s, e) => { if (_dirty && _session != null) await SaveCoreAsync(false); };
        }

        private DateTime Day => (DayPicker.SelectedDate ?? DateTime.Today).Date;

        // ------------------------------------------------------------ the day and its lessons

        private async Task LoadDayAsync(SessionInfo prefer = null)
        {
            if (Class == null) return;
            _host.PendingSession = null; _host.PendingDate = null;
            try { _sessions = await ClassData.GetSessionsAsync(Class.ClassID, Day); }
            catch (Exception ex) { MessageBox.Show("The schedule could not be loaded.\n" + ex.Message, "Can't load schedule", MessageBoxButton.OK, MessageBoxImage.Error); return; }

            foreach (var s in _sessions) Decorate(s);
            _suppress = true;
            SessionList.ItemsSource = _sessions;
            _suppress = false;

            var pick = prefer == null ? null : _sessions.FirstOrDefault(s => s.Start == prefer.Start)
                       ?? _sessions.FirstOrDefault(s => !s.Taken) ?? _sessions.FirstOrDefault();
            if (pick == null) pick = _sessions.FirstOrDefault(s => !s.Taken) ?? _sessions.FirstOrDefault();

            if (pick == null)
            {
                _session = null;
                _rows = new ObservableCollection<AttendanceEntry>();
                RowsList.ItemsSource = null;
                ShowNoLesson(true);
                return;
            }
            ShowNoLesson(false);
            SessionList.SelectedItem = pick;     // loads the rows through SelectionChanged
        }

        private static void Decorate(SessionInfo s)
        {
            // Nothing to compute: the view reads DisplayTitle / TakenVisibility from the model.
        }

        private void ShowNoLesson(bool none)
        {
            NoLesson.Visibility = none ? Visibility.Visible : Visibility.Collapsed;
            RowsList.Visibility = none ? Visibility.Collapsed : Visibility.Visible;
            SaveButton.IsEnabled = !none;
            ClearButton.Visibility = Visibility.Collapsed;
            UpdateCounts();
            SaveState.Text = "";
        }

        private async void SessionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_suppress) return;
            var chosen = SessionList.SelectedItem as SessionInfo;
            if (chosen == null || chosen == _session) return;
            if (!await ConfirmLeaveAsync())
            {
                _suppress = true; SessionList.SelectedItem = _session; _suppress = false;
                return;
            }
            await OpenSessionAsync(chosen);
        }

        private async Task OpenSessionAsync(SessionInfo session)
        {
            _session = session;
            List<AttendanceEntry> rows;
            try { rows = await ClassData.LoadAttendanceAsync(session); }
            catch (Exception ex) { MessageBox.Show("The students could not be loaded.\n" + ex.Message, "Can't load students", MessageBoxButton.OK, MessageBoxImage.Error); return; }

            _rows = new ObservableCollection<AttendanceEntry>(rows);
            foreach (var r in _rows) r.PropertyChanged += Row_Changed;
            var view = new ListCollectionView(_rows);
            view.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
            RowsList.ItemsSource = view;
            _dirty = false;
            ShowNoLesson(false);
            ClearButton.Visibility = session.Taken ? Visibility.Visible : Visibility.Collapsed;
            SaveState.Text = Loc.T(session.Taken ? "Saved" : "Not saved yet. Everyone starts as present.");
            UpdateCounts();
            if (_rows.Count == 0) { NoLessonTitle.Text = Loc.T("Nobody to mark"); NoLessonText.Text = Loc.T("Add students and teachers to this class first, in People."); NoLesson.Visibility = Visibility.Visible; }
        }

        // ------------------------------------------------------------ marking

        private void Row_Changed(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Status" && e.PropertyName != "Note") return;
            _dirty = true;
            SaveState.Text = Loc.T("Changes not saved yet");
            UpdateCounts();
        }

        private void UpdateCounts()
        {
            CountPresent.Text = Loc.T("Present") + " " + _rows.Count(r => r.Status == "P");
            CountAbsent.Text = Loc.T("Absent") + " " + _rows.Count(r => r.Status == "A");
            CountLate.Text = Loc.T("Late") + " " + _rows.Count(r => r.Status == "L");
            CountExcused.Text = Loc.T("Excused") + " " + _rows.Count(r => r.Status == "E");
        }

        private void AllPresent_Click(object sender, RoutedEventArgs e)
        {
            foreach (var r in _rows) r.Status = "P";
        }

        private void RowsList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.FocusedElement is TextBox) return;
            string status = e.Key == Key.P ? "P" : e.Key == Key.A ? "A" : e.Key == Key.L ? "L" : e.Key == Key.E ? "E" : null;
            if (status == null) return;
            var row = RowsList.SelectedItem as AttendanceEntry ?? _rows.FirstOrDefault();
            if (row == null) return;
            row.Status = status;
            int next = _rows.IndexOf(row) + 1;
            if (next < _rows.Count)
            {
                RowsList.SelectedItem = _rows[next];
                RowsList.ScrollIntoView(_rows[next]);
            }
            e.Handled = true;
        }

        // ------------------------------------------------------------ saving

        private async void Save_Click(object sender, RoutedEventArgs e) => await SaveCoreAsync(true);

        private async Task<bool> SaveCoreAsync(bool announce)
        {
            if (_session == null) return false;
            try
            {
                _session.SessionID = await ClassData.SaveAttendanceAsync(_session, _rows);
                _session.Taken = true;
                _dirty = false;
                SaveState.Text = Loc.T("Saved");
                ClearButton.Visibility = Visibility.Visible;
                SessionList.Items.Refresh();
                if (announce) MessageBox.Show("Attendance saved successfully.");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("The attendance could not be saved.\n" + ex.Message, "Couldn't save", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>Asks what to do with marks that are not saved. True means carry on.</summary>
        private async Task<bool> ConfirmLeaveAsync()
        {
            if (!_dirty || _session == null) return true;
            var answer = MessageBox.Show("You changed the attendance of this lesson. Save it before you leave?", "Unsaved changes", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes) return await SaveCoreAsync(false);
            _dirty = false;
            return true;
        }

        private async void ClearRecord_Click(object sender, RoutedEventArgs e)
        {
            if (_session == null || _session.SessionID == 0) return;
            if (MessageBox.Show("Delete the attendance record of this lesson? Everyone goes back to unmarked.", "Delete record", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            try
            {
                await ClassData.DeleteSessionAsync(_session.SessionID);
                _dirty = false; _session = null;
                await LoadDayAsync();
            }
            catch (Exception ex) { MessageBox.Show("That change could not be saved.\n" + ex.Message, "Couldn't save", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        // ------------------------------------------------------------ date

        private async void DayPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_suppress || !IsLoaded) return;
            if (!await ConfirmLeaveAsync()) { _suppress = true; DayPicker.SelectedDate = _session?.Date ?? DateTime.Today; _suppress = false; return; }
            _session = null;
            await LoadDayAsync();
        }

        private void PrevDay_Click(object sender, RoutedEventArgs e) => DayPicker.SelectedDate = Day.AddDays(-1);
        private void NextDay_Click(object sender, RoutedEventArgs e) => DayPicker.SelectedDate = Day.AddDays(1);
        private void Today_Click(object sender, RoutedEventArgs e) => DayPicker.SelectedDate = DateTime.Today;

        // ------------------------------------------------------------ extra lesson

        private void ExtraLesson_Click(object sender, RoutedEventArgs e) =>
            ExtraPanel.Visibility = ExtraPanel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

        private void ExtraCancel_Click(object sender, RoutedEventArgs e) => ExtraPanel.Visibility = Visibility.Collapsed;

        private async void ExtraAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!TimeSpan.TryParse(ExtraStart.SelectedItem as string, out TimeSpan start) || !TimeSpan.TryParse(ExtraEnd.SelectedItem as string, out TimeSpan end) || end <= start)
            {
                MessageBox.Show("The lesson must end after it starts.", "Check this first", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!await ConfirmLeaveAsync()) return;
            var existing = _sessions.FirstOrDefault(s => s.Start == start);
            if (existing == null)
            {
                existing = new SessionInfo
                {
                    ClassID = Class.ClassID, Date = Day, Start = start, End = end,
                    Title = string.IsNullOrWhiteSpace(ExtraTitle.Text) ? Loc.T("Extra lesson") : ExtraTitle.Text.Trim()
                };
                _sessions.Add(existing);
                _sessions = _sessions.OrderBy(s => s.Start).ToList();
                _suppress = true; SessionList.ItemsSource = _sessions; _suppress = false;
            }
            ExtraPanel.Visibility = Visibility.Collapsed;
            ExtraTitle.Clear();
            ShowNoLesson(false);
            _session = null;
            SessionList.SelectedItem = existing;
            await OpenSessionAsync(existing);
        }

        // ------------------------------------------------------------ report

        private async void Mode_Changed(object sender, RoutedEventArgs e)
        {
            if (RollCall == null) return;
            bool report = ModeReport.IsChecked == true;
            if (report && !await ConfirmLeaveAsync()) { ModeRollCall.IsChecked = true; return; }
            RollCall.Visibility = report ? Visibility.Collapsed : Visibility.Visible;
            Report.Visibility = report ? Visibility.Visible : Visibility.Collapsed;
            LessonStrip.Visibility = report ? Visibility.Collapsed : Visibility.Visible;
            DayNav.Visibility = report ? Visibility.Collapsed : Visibility.Visible;
            if (report) await LoadReportAsync();
        }

        private async void ReportFilter_Changed(object sender, RoutedEventArgs e) { if (IsLoaded) await LoadReportAsync(); }

        private async void ReportRange_Changed(object sender, SelectionChangedEventArgs e) { if (!_suppress && IsLoaded) await LoadReportAsync(); }

        private async Task LoadReportAsync()
        {
            if (Class == null || FromPicker.SelectedDate == null || ToPicker.SelectedDate == null) return;
            try
            {
                bool teachers = ReportTeachers.IsChecked == true;
                var all = await ClassData.GetReportAsync(Class.ClassID, FromPicker.SelectedDate.Value, ToPicker.SelectedDate.Value, AbsenceAlert);
                var rows = all.Where(r => (r.PersonType == 'T') == teachers).OrderByDescending(r => r.Absent).ThenBy(r => r.FullName).ToList();
                ReportGrid.ItemsSource = rows;
                ReportEmpty.Visibility = rows.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("The students could not be loaded.\n" + ex.Message, "Can't load students", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
