using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace EduTrack.Models
{
    /// <summary>A class as the workspace lists it, with its headcounts.</summary>
    public class ClassSummary
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public string Major { get; set; }
        public string Description { get; set; }
        public DateTime? Created { get; set; }
        public int StudentCount { get; set; }
        public int TeacherCount { get; set; }
        public int ModuleCount { get; set; }

        /// <summary>"22 students · 2 teachers", already in the chosen language (filled in by the workspace).</summary>
        public string CountsText { get; set; }
    }

    /// <summary>Someone who could be added to a class.</summary>
    public class PersonOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int? CurrentClassId { get; set; }
        public string CurrentClassName { get; set; }
    }

    public class ModuleInfo
    {
        public int ModuleID { get; set; }
        public int ClassID { get; set; }
        public string ModuleName { get; set; }
        public int? TeacherID { get; set; }
        public string TeacherName { get; set; }
        public decimal Coefficient { get; set; } = 1;
        public string Subtitle => (TeacherName ?? "") + (string.IsNullOrEmpty(TeacherName) ? "" : " · ") + "×" + Coefficient.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture);
    }

    /// <summary>One slot of the weekly timetable.</summary>
    public class LessonInfo
    {
        public int LessonID { get; set; }
        public int ClassID { get; set; }
        public int DayOfWeek { get; set; }            // 1 = Monday ... 7 = Sunday
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public int? ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string TeacherName { get; set; }
        public string Title { get; set; }
        public string Room { get; set; }

        public string Label => !string.IsNullOrWhiteSpace(ModuleName) ? ModuleName : (Title ?? "");
        public string TimeText => Start.ToString(@"hh\:mm") + "–" + End.ToString(@"hh\:mm");
    }

    /// <summary>A lesson on a given date: what attendance is attached to.</summary>
    public class SessionInfo
    {
        public int SessionID { get; set; }            // 0 until attendance has been saved once
        public int ClassID { get; set; }
        public int? LessonID { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public int? ModuleID { get; set; }
        public string Title { get; set; }
        public int? TeacherID { get; set; }
        public string TeacherName { get; set; }
        public bool Taken { get; set; }               // attendance already saved

        public string TimeText => Start.ToString(@"hh\:mm") + "–" + End.ToString(@"hh\:mm");
        public string DisplayTitle => string.IsNullOrWhiteSpace(Title) ? EduTrack.Utilities.Loc.T("Lesson") : Title;
        public System.Windows.Visibility TakenVisibility => Taken ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
    }

    /// <summary>Present, Absent, Late or Excused for one person in one lesson.</summary>
    public class AttendanceEntry : INotifyPropertyChanged
    {
        private string _status = "P";
        private string _note;

        public char PersonType { get; set; }          // 'S' student, 'T' teacher
        public string Group => EduTrack.Utilities.Loc.T(PersonType == 'T' ? "Teacher" : "Students");
        public int PersonID { get; set; }
        public string FullName { get; set; }
        public string P_Pic { get; set; }
        public string Initials { get; set; }

        public string Status { get => _status; set { if (_status != value) { _status = value; Raise(); Raise(nameof(NoteVisibility)); Raise(nameof(IsPresent)); Raise(nameof(IsAbsent)); Raise(nameof(IsLate)); Raise(nameof(IsExcused)); } } }
        public string Note { get => _note; set { if (_note != value) { _note = value; Raise(); } } }

                /// <summary>The note box is only shown for people who are not simply present, or when a note already exists.</summary>
        public bool ShowNote => Status != "P" || !string.IsNullOrEmpty(Note);
        public System.Windows.Visibility NoteVisibility => ShowNote ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;

        public bool IsPresent { get => Status == "P"; set { if (value) Status = "P"; else Raise(); } }
        public bool IsAbsent { get => Status == "A"; set { if (value) Status = "A"; else Raise(); } }
        public bool IsLate { get => Status == "L"; set { if (value) Status = "L"; else Raise(); } }
        public bool IsExcused { get => Status == "E"; set { if (value) Status = "E"; else Raise(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void Raise([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    /// <summary>Totals for one person over a date range.</summary>
    public class AttendanceReportRow
    {
        public char PersonType { get; set; }
        public int PersonID { get; set; }
        public string FullName { get; set; }
        public string P_Pic { get; set; }
        public string Initials { get; set; }
        public int Present { get; set; }
        public int Late { get; set; }
        public int Absent { get; set; }
        public int Excused { get; set; }
        public int Total => Present + Late + Absent + Excused;
        /// <summary>Share of recorded lessons attended (present or late), 0 to 100.</summary>
        public double Rate => Total == 0 ? 100 : Math.Round((Present + Late) * 100.0 / Total, 0);
        public string RateText => Total == 0 ? "–" : Rate.ToString("0") + "%";
        public bool NeedsAttention { get; set; }
    }

    public class AssessmentInfo
    {
        public int AssessmentID { get; set; }
        public int ModuleID { get; set; }
        public string Title { get; set; }
        public string Kind { get; set; } = "Exam";
        public decimal Weight { get; set; } = 1;
        public decimal MaxScore { get; set; } = 20;
        public DateTime? Date { get; set; }

        public string Caption => Title + "  (×" + Weight.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + " · /" + MaxScore.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + ")";
    }

    /// <summary>One cell of the grade grid: what the user types for a student and an assessment.</summary>
    public class ScoreCell : INotifyPropertyChanged
    {
        private string _text = "";
        public AssessmentInfo Assessment { get; set; }
        public int StudentID { get; set; }
        public string Original { get; set; } = "";
        public string Text { get => _text; set { if (_text != value) { _text = value ?? ""; Raise(); Edited?.Invoke(this); } } }
        public bool IsDirty => (Text ?? "").Trim() != (Original ?? "");
        public event Action<ScoreCell> Edited;
        public event PropertyChangedEventHandler PropertyChanged;
        private void Raise([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    /// <summary>One student's row in the grade grid or the summary.</summary>
    public class GradeRow : INotifyPropertyChanged
    {
        private double? _average;
        private int _rank;

        public int StudentID { get; set; }
        public string FullName { get; set; }
        public string P_Pic { get; set; }
        public string Initials { get; set; }
        public List<ScoreCell> Cells { get; set; } = new List<ScoreCell>();            // module view (one per assessment)
        public List<string> ModuleAverages { get; set; } = new List<string>();         // summary view (one per module)

        public double? Average { get => _average; set { _average = value; Raise(); Raise(nameof(AverageText)); Raise(nameof(Passing)); Raise(nameof(HasAverage)); } }
        public int Rank { get => _rank; set { _rank = value; Raise(); Raise(nameof(RankText)); } }
        public bool HasAverage => _average.HasValue;
        public bool Passing => _average.HasValue && _average.Value >= 10;
        public string AverageText => _average.HasValue ? _average.Value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) : "–";
        public string RankText => _rank > 0 ? _rank.ToString() : "–";
        public string ResultText => !_average.HasValue ? "–" : EduTrack.Utilities.Loc.T(Passing ? "Passed" : "Below pass mark");

        public event PropertyChangedEventHandler PropertyChanged;
        private void Raise([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    /// <summary>Everything needed for grades of one class, loaded once; averages are computed here.</summary>
    public class GradeBook
    {
        public List<ModuleInfo> Modules { get; set; } = new List<ModuleInfo>();
        public List<AssessmentInfo> Assessments { get; set; } = new List<AssessmentInfo>();
        public List<GradeRow> Students { get; set; } = new List<GradeRow>();
        /// <summary>(assessmentId, studentId) -> raw text as stored ("14.5", "ABS", "").</summary>
        public Dictionary<(int, int), string> Raw { get; set; } = new Dictionary<(int, int), string>();

        public const double PassMark = 10;
        public string TextOf(AssessmentInfo a, int studentId) => Raw.TryGetValue((a.AssessmentID, studentId), out var s) ? s : "";

        /// <summary>Fills each student's module averages, overall average and rank from the stored scores (or from edited cells).</summary>
        public void ComputeSummary(Func<AssessmentInfo, int, string> textOf = null)
        {
            textOf = textOf ?? TextOf;
            foreach (var row in Students)
            {
                var perModule = Modules.Select(m => (average: ModuleAverage(row.StudentID, m.ModuleID, textOf), coefficient: m.Coefficient)).ToList();
                row.ModuleAverages = perModule.Select(x => x.average.HasValue ? x.average.Value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) : "–").ToList();
                row.Average = Overall(perModule);
            }
            Rank(Students);
        }

        public double? ClassAverage
        {
            get
            {
                var values = Students.Where(s => s.Average.HasValue).Select(s => s.Average.Value).ToList();
                return values.Count == 0 ? (double?)null : values.Average();
            }
        }

        /// <summary>For each assessment, how many students still have no score or flag.</summary>
        public int MissingScores(AssessmentInfo a) => Students.Count(s => string.IsNullOrWhiteSpace(TextOf(a, s.StudentID)));

        /// <summary>Score out of 20 for one student and one assessment, or null when empty / absent / excused.</summary>
        public double? Normalised(AssessmentInfo assessment, string text)
        {
            if (assessment == null || string.IsNullOrWhiteSpace(text)) return null;
            if (!decimal.TryParse(text.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out decimal value)) return null;
            if (assessment.MaxScore <= 0) return null;
            return (double)(value / assessment.MaxScore * 20m);
        }

        public double? ModuleAverage(int studentId, int moduleId, Func<AssessmentInfo, int, string> textOf)
        {
            double sum = 0, weights = 0;
            foreach (var a in Assessments.Where(x => x.ModuleID == moduleId))
            {
                double? n = Normalised(a, textOf(a, studentId));
                if (n == null) continue;
                sum += n.Value * (double)a.Weight;
                weights += (double)a.Weight;
            }
            return weights > 0 ? sum / weights : (double?)null;
        }

        public double? Overall(IEnumerable<(double? average, decimal coefficient)> modules)
        {
            double sum = 0, coefs = 0;
            foreach (var m in modules)
            {
                if (m.average == null) continue;
                sum += m.average.Value * (double)m.coefficient;
                coefs += (double)m.coefficient;
            }
            return coefs > 0 ? sum / coefs : (double?)null;
        }

        /// <summary>Ranks students by average (1,2,2,4 for ties); students without an average get no rank.</summary>
        public static void Rank(IEnumerable<GradeRow> rows)
        {
            var ordered = rows.Where(r => r.Average.HasValue).OrderByDescending(r => Math.Round(r.Average.Value, 2)).ToList();
            int rank = 0; double? previous = null;
            for (int i = 0; i < ordered.Count; i++)
            {
                double current = Math.Round(ordered[i].Average.Value, 2);
                if (previous == null || current != previous) rank = i + 1;
                ordered[i].Rank = rank;
                previous = current;
            }
            foreach (var r in rows.Where(r => !r.Average.HasValue)) r.Rank = 0;
        }
    }
}
