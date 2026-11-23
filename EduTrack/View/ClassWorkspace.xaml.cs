using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EduTrack.Data;
using EduTrack.Models;
using EduTrack.Utilities;

namespace EduTrack.View
{
    /// <summary>
    /// The Classes section: pick a class on the left, then work on it on the right (Overview, People, Attendance, Grades, Timetable).
    /// The class stays selected while you move between the tabs.
    /// </summary>
    public partial class ClassWorkspace : UserControl, IHub
    {
        private static readonly string LastClassFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EduTrack", "lastclass.txt");

        private List<ClassSummary> _classes = new List<ClassSummary>();
        private bool _loading;
        private string _tab = "overview";

        /// <summary>Set by the Overview to open Attendance on a given date and lesson.</summary>
        public DateTime? PendingDate { get; set; }
        public SessionInfo PendingSession { get; set; }

        public ClassSummary Current => ClassList.SelectedItem as ClassSummary;

        public ClassWorkspace()
        {
            InitializeComponent();
            Loaded += async (s, e) => { if (_classes.Count == 0 && !_loading) await ReloadAsync(ReadLastClass()); };
        }

        // ------------------------------------------------------------ narrow window: the list becomes a drop-down

        private bool _narrow;
        private bool _syncing;

        private void Workspace_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            bool narrow = ActualWidth < 1280;
            if (narrow == _narrow) return;
            _narrow = narrow;
            ListColumn.Width = narrow ? new GridLength(0) : new GridLength(268);
            GapColumn.Width = narrow ? new GridLength(0) : new GridLength(20);
            ListCard.Visibility = narrow ? Visibility.Collapsed : Visibility.Visible;
            ClassCombo.Visibility = narrow && _classes.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ClassCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_syncing || _loading) return;
            if (ClassCombo.SelectedItem is ClassSummary chosen && chosen != Current) ClassList.SelectedItem = chosen;
        }

        // ------------------------------------------------------------ classes

        /// <summary>Reloads the list (and the counts under each name), keeping the same class selected.</summary>
        public async Task ReloadAsync(int? select = null)
        {
            _loading = true;
            try
            {
                int? keep = select ?? Current?.ClassID;
                _classes = await ClassData.GetClassesAsync();
                foreach (var c in _classes) c.CountsText = CountsText(c);
                ClassList.ItemsSource = _classes;
                ClassCombo.ItemsSource = _classes;
                ClassList.SelectedItem = _classes.FirstOrDefault(c => c.ClassID == keep) ?? _classes.FirstOrDefault();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The classes could not be loaded. Check that the database is running.\n" + ex.Message, "Can't load classes", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally { _loading = false; }

            bool any = _classes.Count > 0;
            ClassCombo.Visibility = _narrow && any ? Visibility.Visible : Visibility.Collapsed;
            NoClassesText.Visibility = any ? Visibility.Collapsed : Visibility.Visible;
            Workspace.Visibility = any && FormHost.Visibility != Visibility.Visible ? Visibility.Visible : Visibility.Collapsed;
            IdleState.Visibility = any || FormHost.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            ClassList_SelectionChanged(null, null);
        }

        /// <summary>Updates the numbers under each class name without rebuilding the open tab.</summary>
        public async Task RefreshCountsAsync()
        {
            try
            {
                var fresh = await ClassData.GetClassesAsync();
                foreach (var c in _classes)
                {
                    var f = fresh.FirstOrDefault(x => x.ClassID == c.ClassID);
                    if (f == null) continue;
                    c.StudentCount = f.StudentCount; c.TeacherCount = f.TeacherCount; c.ModuleCount = f.ModuleCount;
                    c.CountsText = CountsText(c);
                }
                _loading = true;
                var keep = ClassList.SelectedItem;
                try { ClassList.Items.Refresh(); ClassList.SelectedItem = keep; } finally { _loading = false; }
            }
            catch { /* the counts are cosmetic */ }
        }

        private static string CountsText(ClassSummary c) =>
            Loc.T(c.StudentCount + (c.StudentCount == 1 ? " student" : " students")) + " · " + Loc.T(c.TeacherCount + (c.TeacherCount == 1 ? " teacher" : " teachers"));

        private void ClassList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_loading) return;
            var current = Current;
            if (current == null) { TabContent.Content = null; return; }
            _syncing = true; ClassCombo.SelectedItem = current; _syncing = false;
            WriteLastClass(current.ClassID);

            ClassTitle.Text = current.ClassName;
            ClassMeta.Text = string.Join(" · ", new[]
            {
                string.IsNullOrWhiteSpace(current.Major) ? null : current.Major,
                current.Created.HasValue ? Loc.T("created " + Loc.ShortDate(current.Created.Value)) : null
            }.Where(x => x != null));
            if (FormHost.Visibility == Visibility.Visible) return;
            ShowTab(_tab);
        }

        private static int? ReadLastClass()
        {
            try { return int.TryParse(File.ReadAllText(LastClassFile).Trim(), out int id) ? id : (int?)null; } catch { return null; }
        }

        private static void WriteLastClass(int id)
        {
            try { Directory.CreateDirectory(Path.GetDirectoryName(LastClassFile)); File.WriteAllText(LastClassFile, id.ToString()); } catch { }
        }

        // ------------------------------------------------------------ tabs

        private void Tab_Checked(object sender, RoutedEventArgs e)
        {
            _tab = (string)((RadioButton)sender).Tag;
            if (Current != null && FormHost.Visibility != Visibility.Visible) ShowTab(_tab);
        }

        /// <summary>Opens a tab by name. Anything a tab needs to know first is set on this workspace beforehand.</summary>
        public void Go(string tab)
        {
            var button = new[] { TabOverview, TabPeople, TabAttendance, TabGrades, TabTimetable }.FirstOrDefault(b => (string)b.Tag == tab) ?? TabOverview;
            if (button.IsChecked == true) ShowTab(tab); else button.IsChecked = true;
        }

        private void ShowTab(string tab)
        {
            CloseForm();
            if (Current == null) return;
            FrameworkElement view;
            switch (tab)
            {
                case "people": view = new ClassPeople(this); break;
                case "attendance": view = new ClassAttendance(this); break;
                case "grades": view = new ClassGrades(this); break;
                case "timetable": view = new ClassTimetable(this); break;
                default: view = new ClassOverview(this); tab = "overview"; break;
            }
            _tab = tab;
            TabContent.Content = view;
        }

        /// <summary>0 = new class, 2 = timetable, 3 = grades, anything else = the overview (used by the Home shortcuts).</summary>
        public void SelectTab(int index)
        {
            if (index == 0) { OpenForm(null); return; }
            Go(index == 2 ? "timetable" : index == 3 ? "grades" : "overview");
        }

        // ------------------------------------------------------------ create, edit, delete

        private void NewClass_Click(object sender, RoutedEventArgs e) => OpenForm(null);

        private void EditClass_Click(object sender, RoutedEventArgs e) => OpenForm(Current);

        private void OpenForm(ClassSummary editing)
        {
            var form = new ClassForm(editing);
            form.Saved += async id => { CloseForm(); await ReloadAsync(id); };
            form.Cancelled += () => { CloseForm(); if (_classes.Count == 0) IdleState.Visibility = Visibility.Visible; };
            FormHost.Content = form;
            FormHost.Visibility = Visibility.Visible;
            Workspace.Visibility = Visibility.Collapsed;
            IdleState.Visibility = Visibility.Collapsed;
        }

        private void CloseForm()
        {
            if (FormHost.Visibility != Visibility.Visible) return;
            FormHost.Visibility = Visibility.Collapsed;
            FormHost.Content = null;
            Workspace.Visibility = _classes.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private async void DeleteClass_Click(object sender, RoutedEventArgs e)
        {
            var current = Current;
            if (current == null) return;
            var answer = MessageBox.Show(
                "Delete the class “" + current.ClassName + "”? Its schedule, modules and grades are deleted too. " +
                "Students and teachers stay in the system, they just won't belong to a class.",
                "Delete class", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (answer != MessageBoxResult.Yes) return;
            try
            {
                await ClassData.DeleteClassAsync(current.ClassID);
                MessageBox.Show("The class was deleted.");
                await ReloadAsync(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The class could not be deleted.\n" + ex.Message, "Couldn't delete", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
