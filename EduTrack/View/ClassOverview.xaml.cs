using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EduTrack.Data;
using EduTrack.Models;
using EduTrack.Utilities;

namespace EduTrack.View
{
    /// <summary>One look at a class: headcounts, attendance, average, today's lessons and what needs a follow-up.</summary>
    public partial class ClassOverview : UserControl
    {
        private const int AbsenceAlert = 3;

        private sealed class TodayRow
        {
            public SessionInfo Session { get; set; }
            public string TimeText => Session.TimeText;
            public string Title => string.IsNullOrWhiteSpace(Session.Title) ? Loc.T("Lesson") : Session.Title;
            public string TeacherName => Session.TeacherName;
            public string ActionText { get; set; }
        }

        private sealed class AttentionRow
        {
            public string Title { get; set; }
            public string Detail { get; set; }
            public string ActionText { get; set; }
            public string Tab { get; set; }
        }

        private sealed class ModuleRow
        {
            public string Name { get; set; }
            public string Teacher { get; set; }
            public string Coefficient { get; set; }
            public string Assessments { get; set; }
            public string Average { get; set; }
        }

        private readonly ClassWorkspace _host;

        public ClassOverview(ClassWorkspace host)
        {
            InitializeComponent();
            _host = host;
            Loaded += async (s, e) => await LoadAsync();
        }

        private async Task LoadAsync()
        {
            var cls = _host.Current;
            if (cls == null) return;
            StudentsValue.Text = cls.StudentCount.ToString();
            TeachersValue.Text = cls.TeacherCount.ToString();
            ModulesValue.Text = cls.ModuleCount.ToString();
            TodayDate.Text = Loc.ShortDate(DateTime.Today);

            try
            {
                double? rate = await ClassData.GetClassRateAsync(cls.ClassID, DateTime.Today.AddDays(-30), DateTime.Today);
                AttendanceValue.Text = rate.HasValue ? Math.Round(rate.Value).ToString("0") + "%" : "–";

                var book = await ClassData.GetGradeBookAsync(cls.ClassID);
                book.ComputeSummary();
                double? average = book.ClassAverage;
                AverageValue.Text = average.HasValue ? average.Value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) : "–";

                var inv = System.Globalization.CultureInfo.InvariantCulture;
                var moduleRows = book.Modules.Select(m =>
                {
                    var averages = book.Students.Select(s => book.ModuleAverage(s.StudentID, m.ModuleID, book.TextOf)).Where(v => v.HasValue).Select(v => v.Value).ToList();
                    return new ModuleRow
                    {
                        Name = m.ModuleName,
                        Teacher = string.IsNullOrWhiteSpace(m.TeacherName) ? Loc.T("No teacher") : m.TeacherName,
                        Coefficient = m.Coefficient.ToString("0.##", inv),
                        Assessments = book.Assessments.Count(a => a.ModuleID == m.ModuleID).ToString(),
                        Average = averages.Count == 0 ? "–" : averages.Average().ToString("0.00", inv)
                    };
                }).ToList();
                ModulesList.ItemsSource = moduleRows;
                ModulesEmpty.Visibility = moduleRows.Count == 0 ? Visibility.Visible : Visibility.Collapsed;

                var sessions = await ClassData.GetSessionsAsync(cls.ClassID, DateTime.Today);
                var today = sessions.Select(s => new TodayRow { Session = s, ActionText = Loc.T(s.Taken ? "Review attendance" : "Take attendance") }).ToList();
                TodayList.ItemsSource = today;
                TodayEmpty.Visibility = today.Count == 0 ? Visibility.Visible : Visibility.Collapsed;

                var attention = new List<AttentionRow>();
                var report = await ClassData.GetReportAsync(cls.ClassID, DateTime.Today.AddDays(-30), DateTime.Today, AbsenceAlert);
                foreach (var r in report.Where(x => x.NeedsAttention).OrderByDescending(x => x.Absent).Take(6))
                    attention.Add(new AttentionRow
                    {
                        Title = r.FullName,
                        Detail = Loc.T(r.Absent + (r.Absent == 1 ? " absence" : " absences")) + " · " + Loc.T(r.PersonType == 'T' ? "Teacher" : "Student"),
                        ActionText = Loc.T("Open report"), Tab = "attendance"
                    });
                foreach (var a in book.Assessments.Where(a => a.Date.HasValue && a.Date.Value.Date <= DateTime.Today || book.MissingScores(a) < book.Students.Count))
                {
                    int missing = book.MissingScores(a);
                    if (missing == 0 || book.Students.Count == 0) continue;
                    var module = book.Modules.FirstOrDefault(m => m.ModuleID == a.ModuleID);
                    attention.Add(new AttentionRow
                    {
                        Title = (module?.ModuleName ?? "") + " · " + a.Title,
                        Detail = Loc.T(missing + (missing == 1 ? " score missing" : " scores missing")),
                        ActionText = Loc.T("Enter scores"), Tab = "grades"
                    });
                }
                AttentionList.ItemsSource = attention.Take(8).ToList();
                AttentionEmpty.Visibility = attention.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("The class could not be loaded.\n" + ex.Message, "Can't load classes", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TakeAttendance_Click(object sender, RoutedEventArgs e)
        {
            var row = (sender as FrameworkElement)?.DataContext as TodayRow;
            _host.PendingDate = DateTime.Today;
            _host.PendingSession = row?.Session;
            _host.Go("attendance");
        }

        private void OpenGrades_Click(object sender, RoutedEventArgs e) => _host.Go("grades");

        private void TakeAttendanceAnyway_Click(object sender, RoutedEventArgs e)
        {
            _host.PendingDate = DateTime.Today;
            _host.PendingSession = null;
            _host.Go("attendance");
        }

        private void Attention_Click(object sender, RoutedEventArgs e)
        {
            var row = (sender as FrameworkElement)?.DataContext as AttentionRow;
            if (row != null) _host.Go(row.Tab);
        }
    }
}
