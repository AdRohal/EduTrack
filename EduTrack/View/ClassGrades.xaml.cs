using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using EduTrack.Data;
using EduTrack.Models;
using EduTrack.Utilities;

namespace EduTrack.View
{
    /// <summary>
    /// Grades of one class. A module holds assessments (exams, quizzes...) with their own weight; the module average and the overall
    /// average (by module coefficient) are worked out live. "Summary" shows every module and the ranking.
    /// </summary>
    public partial class ClassGrades : UserControl
    {
        private sealed class RailEntry
        {
            public ModuleInfo Module { get; set; }          // null = the summary
            public string Name { get; set; }
            public string Sub { get; set; }
        }

        private sealed class Chip
        {
            public AssessmentInfo Assessment { get; set; }
            public string Title { get; set; }
            public string Detail { get; set; }
            public string Short { get; set; }
        }

        private sealed class Kind
        {
            public string Value { get; set; }
            public string Display { get; set; }
        }

        private readonly ClassWorkspace _host;
        private GradeBook _book = new GradeBook();
        private List<PersonOption> _teachers = new List<PersonOption>();
        private RailEntry _current;
        private readonly HashSet<ScoreCell> _dirty = new HashSet<ScoreCell>();
        private Dictionary<(int, int), ScoreCell> _cells = new Dictionary<(int, int), ScoreCell>();
        private Dictionary<int, GradeRow> _rowByStudent = new Dictionary<int, GradeRow>();
        private ModuleInfo _editingModule;
        private AssessmentInfo _editingAssessment;
        private bool _suppress;

        public ClassGrades(ClassWorkspace host)
        {
            InitializeComponent();
            _host = host;
            AssessmentKind.DisplayMemberPath = "Display";
            AssessmentKind.ItemsSource = new[] { "Exam", "Quiz", "Homework", "Project", "Other" }.Select(k => new Kind { Value = k, Display = Loc.T(k) }).ToList();
            AssessmentKind.SelectedIndex = 0;
            Loaded += async (s, e) => await LoadAsync();
            Unloaded += async (s, e) => { if (_dirty.Count > 0 && AllValid()) await SaveCoreAsync(false); };
        }

        // ------------------------------------------------------------ loading

        private async Task LoadAsync(int? keepModuleId = null)
        {
            var cls = _host.Current;
            if (cls == null) return;
            try
            {
                _book = await ClassData.GetGradeBookAsync(cls.ClassID);
                var teachers = await ClassData.GetTeachersAsync(cls.ClassID);
                _teachers = new[] { new PersonOption { Id = 0, Name = Loc.T("No teacher") } }.Concat(teachers.Select(t => new PersonOption { Id = t.Id, Name = t.FullName })).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The students could not be loaded.\n" + ex.Message, "Can't load students", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ModuleTeacher.ItemsSource = _teachers;

            var entries = new List<RailEntry>();
            if (_book.Modules.Count > 0)
                entries.Add(new RailEntry { Name = Loc.T("Summary"), Sub = Loc.T("All modules") });
            entries.AddRange(_book.Modules.Select(m => new RailEntry { Module = m, Name = m.ModuleName, Sub = m.Subtitle }));

            _suppress = true;
            Rail.ItemsSource = entries;
            var keep = keepModuleId.HasValue ? entries.FirstOrDefault(x => x.Module?.ModuleID == keepModuleId.Value) : null;
            _current = keep ?? entries.FirstOrDefault();
            Rail.SelectedItem = _current;
            _suppress = false;
            Show();
        }

        private async void Rail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_suppress) return;
            var chosen = Rail.SelectedItem as RailEntry;
            if (chosen == null || chosen == _current) return;
            if (_dirty.Count > 0)
            {
                var answer = MessageBox.Show("You changed grades that are not saved. Save them before you leave?", "Unsaved changes", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (answer == MessageBoxResult.Yes && !(AllValid() && await SaveCoreAsync(false)))
                {
                    _suppress = true; Rail.SelectedItem = _current; _suppress = false;
                    return;
                }
                _dirty.Clear();
            }
            _current = chosen;
            CloseEditors();
            Show();
        }

        private void Show()
        {
            _dirty.Clear();
            UpdateDirtyUi();
            if (_current == null) { ShowNothing(); return; }
            if (_current.Module == null) ShowSummary(); else ShowModule(_current.Module);
        }

        private void SetEmpty(string title, string text)
        {
            EmptyTitle.Text = Loc.T(title); EmptyText.Text = Loc.T(text);
            EmptyState.Visibility = Visibility.Visible;
        }

        private void ShowNothing()
        {
            ModuleHeader.Visibility = Visibility.Collapsed;
            SaveBar.Visibility = Visibility.Collapsed;
            Scores.Columns.Clear(); Scores.ItemsSource = null;
            FooterText.Text = "";
            SetEmpty("No modules yet", "A module is a subject taught to this class (Algebra, English...). Add one with the + button, then enter grades here.");
        }

        // ------------------------------------------------------------ summary of all modules

        private void ShowSummary()
        {
            ModuleHeader.Visibility = Visibility.Collapsed;
            SaveBar.Visibility = Visibility.Collapsed;
            Scores.IsReadOnly = true;
            _book.ComputeSummary();
            var rows = _book.Students.OrderBy(r => r.FullName).ToList();

            Scores.Columns.Clear();
            Scores.Columns.Add(new DataGridTextColumn { Header = Loc.T("Rank"), Binding = new Binding("RankText"), SortMemberPath = "Rank", Width = new DataGridLength(60), ElementStyle = (Style)Resources["ScoreText"] });
            // Names only here: the summary is wide, and an avatar would squeeze the module columns.
            Scores.Columns.Add(new DataGridTextColumn { Header = Loc.T("Student"), Binding = new Binding("FullName"), SortMemberPath = "FullName", MinWidth = 150, Width = new DataGridLength(2, DataGridLengthUnitType.Star), IsReadOnly = true, ElementStyle = NameStyle() });
            for (int i = 0; i < _book.Modules.Count; i++)
                Scores.Columns.Add(new DataGridTextColumn
                {
                    Header = new TextBlock { Text = _book.Modules[i].ModuleName, TextWrapping = TextWrapping.Wrap, TextAlignment = TextAlignment.Center }, Binding = new Binding("ModuleAverages[" + i + "]"), MinWidth = 118,
                    Width = new DataGridLength(1.2, DataGridLengthUnitType.Star), ElementStyle = (Style)Resources["ScoreText"], SortMemberPath = null
                });
            Scores.Columns.Add(new DataGridTextColumn { Header = Loc.T("Average"), Binding = new Binding("AverageText"), SortMemberPath = "Average", MinWidth = 80, Width = new DataGridLength(1.2, DataGridLengthUnitType.Star), ElementStyle = (Style)Resources["ScoreText"] });
            Scores.Columns.Add(ResultColumn());
            Scores.ItemsSource = rows;

            var figures = rows.Where(r => r.Average.HasValue).Select(r => r.Average.Value).ToList();
            if (rows.Count == 0) { SetEmpty("No students in this class yet", "Add students and teachers to this class first, in People."); FooterText.Text = ""; return; }
            EmptyState.Visibility = Visibility.Collapsed;
            if (figures.Count == 0) { FooterText.Text = Loc.T("No score has been entered yet."); return; }
            FooterText.Text = Loc.T("Class average") + " " + figures.Average().ToString("0.00", CultureInfo.InvariantCulture)
                + " · " + Loc.T("Highest") + " " + figures.Max().ToString("0.00", CultureInfo.InvariantCulture)
                + " · " + Loc.T("Lowest") + " " + figures.Min().ToString("0.00", CultureInfo.InvariantCulture)
                + " · " + figures.Count(v => v >= GradeBook.PassMark) + " / " + figures.Count + " " + Loc.T("at or above 10");
        }

        private static Style NameStyle()
        {
            var style = new Style(typeof(TextBlock));
            style.Setters.Add(new Setter(TextBlock.FontWeightProperty, FontWeights.Bold));
            style.Setters.Add(new Setter(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center));
            style.Setters.Add(new Setter(FrameworkElement.MarginProperty, new Thickness(6, 0, 0, 0)));
            style.Setters.Add(new Setter(TextBlock.TextTrimmingProperty, TextTrimming.CharacterEllipsis));
            return style;
        }

        private DataGridColumn StudentColumn(DataGridLength width)
        {
            var template = new DataTemplate();
            var panel = new FrameworkElementFactory(typeof(StackPanel));
            panel.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
            panel.SetValue(FrameworkElement.MarginProperty, new Thickness(6, 2, 6, 2));
            var avatar = new FrameworkElementFactory(typeof(Avatar));
            avatar.SetValue(FrameworkElement.WidthProperty, 30.0);
            avatar.SetValue(FrameworkElement.HeightProperty, 30.0);
            avatar.SetBinding(Avatar.PhotoProperty, new Binding("P_Pic"));
            avatar.SetBinding(Avatar.InitialsProperty, new Binding("Initials"));
            var name = new FrameworkElementFactory(typeof(TextBlock));
            name.SetBinding(TextBlock.TextProperty, new Binding("FullName"));
            name.SetValue(TextBlock.FontWeightProperty, FontWeights.Bold);
            name.SetValue(FrameworkElement.MarginProperty, new Thickness(10, 0, 0, 0));
            name.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);
            name.SetValue(TextBlock.TextTrimmingProperty, TextTrimming.CharacterEllipsis);
            panel.AppendChild(avatar);
            panel.AppendChild(name);
            template.VisualTree = panel;
            return new DataGridTemplateColumn { Header = Loc.T("Student"), CellTemplate = template, SortMemberPath = "FullName", Width = width, MinWidth = 175, IsReadOnly = true };
        }

        private DataGridColumn ResultColumn()
        {
            var style = new Style(typeof(TextBlock), (Style)Resources["ScoreText"]);
            var fail = new DataTrigger { Binding = new Binding("Passing"), Value = false };
            fail.Setters.Add(new Setter(TextBlock.ForegroundProperty, new DynamicResourceExtension("MutedBrush")));
            var pass = new DataTrigger { Binding = new Binding("Passing"), Value = true };
            pass.Setters.Add(new Setter(TextBlock.ForegroundProperty, new DynamicResourceExtension("SuccessBrush")));
            style.Triggers.Add(fail);
            style.Triggers.Add(pass);
            var below = new DataTrigger { Binding = new Binding("HasAverage"), Value = true };
            return new DataGridTextColumn { Header = Loc.T("Result"), Binding = new Binding("ResultText"), MinWidth = 100, Width = new DataGridLength(1.4, DataGridLengthUnitType.Star), ElementStyle = style };
        }

        // ------------------------------------------------------------ one module

        private void ShowModule(ModuleInfo module)
        {
            ModuleHeader.Visibility = Visibility.Visible;
            SaveBar.Visibility = Visibility.Visible;
            Scores.IsReadOnly = false;
            ModuleTitle.Text = module.ModuleName;
            ModuleMeta.Text = string.Join(" · ", new[]
            {
                string.IsNullOrWhiteSpace(module.TeacherName) ? Loc.T("No teacher") : module.TeacherName,
                Loc.T("Coefficient") + " " + module.Coefficient.ToString("0.##", CultureInfo.InvariantCulture)
            });

            var assessments = _book.Assessments.Where(a => a.ModuleID == module.ModuleID).ToList();
            AssessmentChips.ItemsSource = assessments.Select(a => new Chip
            {
                Assessment = a, Title = a.Title, Short = "×" + a.Weight.ToString("0.##", CultureInfo.InvariantCulture) + " · /" + a.MaxScore.ToString("0.##", CultureInfo.InvariantCulture),
                Detail = Loc.T(a.Kind) + " · ×" + a.Weight.ToString("0.##", CultureInfo.InvariantCulture) + " · /" + a.MaxScore.ToString("0.##", CultureInfo.InvariantCulture)
                         + (a.Date.HasValue ? " · " + Loc.ShortDate(a.Date.Value) : "")
            }).ToList();

            var rows = _book.Students.OrderBy(r => r.FullName).ToList();
            _cells = new Dictionary<(int, int), ScoreCell>();
            _rowByStudent = rows.ToDictionary(r => r.StudentID);
            foreach (var row in rows)
            {
                row.Cells = new List<ScoreCell>();
                foreach (var a in assessments)
                {
                    string text = _book.TextOf(a, row.StudentID);
                    var cell = new ScoreCell { Assessment = a, StudentID = row.StudentID, Original = text, Text = text };
                    cell.Edited += Cell_Edited;
                    row.Cells.Add(cell);
                    _cells[(a.AssessmentID, row.StudentID)] = cell;
                }
                RecalculateAverage(row, module.ModuleID);
            }

            Scores.Columns.Clear();
            Scores.Columns.Add(StudentColumn(new DataGridLength(3, DataGridLengthUnitType.Star)));
            for (int i = 0; i < assessments.Count; i++)
            {
                var a = assessments[i];
                Scores.Columns.Add(new DataGridTextColumn
                {
                    Header = a.Title + "\n×" + a.Weight.ToString("0.##", CultureInfo.InvariantCulture) + " · /" + a.MaxScore.ToString("0.##", CultureInfo.InvariantCulture),
                    Binding = new Binding("Cells[" + i + "].Text") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.LostFocus },
                    ElementStyle = (Style)Resources["ScoreText"], EditingElementStyle = (Style)Resources["ScoreEditor"],
                    MinWidth = 100, Width = new DataGridLength(1.2, DataGridLengthUnitType.Star), SortMemberPath = null
                });
            }
            Scores.Columns.Add(new DataGridTextColumn
            {
                Header = Loc.T("Module average"), Binding = new Binding("AverageText"), IsReadOnly = true, SortMemberPath = "Average",
                ElementStyle = (Style)Resources["ScoreText"], MinWidth = 110, Width = new DataGridLength(1.2, DataGridLengthUnitType.Star)
            });
            Scores.ItemsSource = rows;

            if (rows.Count == 0) { SetEmpty("No students in this class yet", "Add students and teachers to this class first, in People."); SaveScoresButton.IsEnabled = false; FooterText.Text = ""; }
            else if (assessments.Count == 0) { SetEmpty("No assessment yet", "Add an exam, quiz or homework above, then enter the scores here."); FooterText.Text = ""; }
            else
            {
                EmptyState.Visibility = Visibility.Collapsed;
                UpdateFooter();
            }
        }

        private void RecalculateAverage(GradeRow row, int moduleId)
        {
            row.Average = _book.ModuleAverage(row.StudentID, moduleId, (a, sid) => _cells.TryGetValue((a.AssessmentID, sid), out var c) ? c.Text : "");
        }

        private void UpdateFooter()
        {
            var values = _rowByStudent.Values.Where(r => r.Average.HasValue).Select(r => r.Average.Value).ToList();
            FooterText.Text = values.Count == 0
                ? Loc.T("Type a score in a cell: a number, or ABS / EXC when the student was absent or excused.")
                : Loc.T("Module average") + " " + values.Average().ToString("0.00", CultureInfo.InvariantCulture) + " · " + values.Count + " / " + _rowByStudent.Count + " " + Loc.T("students graded");
        }

        private void Cell_Edited(ScoreCell cell)
        {
            if (cell.IsDirty) _dirty.Add(cell); else _dirty.Remove(cell);
            if (_rowByStudent.TryGetValue(cell.StudentID, out var row) && _current?.Module != null) RecalculateAverage(row, _current.Module.ModuleID);
            UpdateDirtyUi();
            UpdateFooter();
        }

        private void UpdateDirtyUi()
        {
            DirtyText.Visibility = _dirty.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            SaveScoresButton.IsEnabled = _dirty.Count > 0;
        }

        // ------------------------------------------------------------ saving scores

        private bool AllValid() => _dirty.All(c => ClassData.IsValidScore(c.Text, c.Assessment.MaxScore));

        private async void SaveScores_Click(object sender, RoutedEventArgs e)
        {
            Scores.CommitEdit(DataGridEditingUnit.Row, true);
            var bad = _dirty.FirstOrDefault(c => !ClassData.IsValidScore(c.Text, c.Assessment.MaxScore));
            if (bad != null)
            {
                string who = _rowByStudent.TryGetValue(bad.StudentID, out var r) ? r.FullName : "";
                MessageBox.Show(who + " · " + bad.Assessment.Title + ": " + Loc.T("use a number from 0 to " + bad.Assessment.MaxScore.ToString("0.##", CultureInfo.InvariantCulture) + ", or a short word such as ABS."), "Check the grades", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            await SaveCoreAsync(true);
        }

        private async Task<bool> SaveCoreAsync(bool announce)
        {
            try
            {
                var cells = _dirty.ToList();
                await ClassData.SaveScoresAsync(cells);
                foreach (var c in cells)
                {
                    string text = (c.Text ?? "").Trim();
                    c.Original = text;
                    _book.Raw[(c.Assessment.AssessmentID, c.StudentID)] = text;
                }
                _dirty.Clear();
                UpdateDirtyUi();
                if (announce) MessageBox.Show("Grades saved successfully.");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("The grades could not be saved.\n" + ex.Message, "Couldn't save", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        // ------------------------------------------------------------ modules

        private void CloseEditors()
        {
            ModuleEditor.Visibility = Visibility.Collapsed;
            AssessmentEditor.Visibility = Visibility.Collapsed;
        }

        private void AddModule_Click(object sender, RoutedEventArgs e) => OpenModuleEditor(null);

        private void EditModule_Click(object sender, RoutedEventArgs e) => OpenModuleEditor(_current?.Module);

        private void OpenModuleEditor(ModuleInfo module)
        {
            _editingModule = module;
            AssessmentEditor.Visibility = Visibility.Collapsed;
            ModuleMessage.Text = "";
            ModuleEditorTitle.Text = Loc.T(module == null ? "New module" : "Edit module");
            ModuleName.Text = module?.ModuleName ?? "";
            ModuleCoef.Text = (module?.Coefficient ?? 1).ToString("0.##", CultureInfo.InvariantCulture);
            ModuleTeacher.SelectedItem = _teachers.FirstOrDefault(t => t.Id == (module?.TeacherID ?? 0)) ?? _teachers.FirstOrDefault();
            ModuleEditor.Visibility = Visibility.Visible;
            ModuleName.Focus();
        }

        private void CancelModule_Click(object sender, RoutedEventArgs e) => ModuleEditor.Visibility = Visibility.Collapsed;

        private async void SaveModule_Click(object sender, RoutedEventArgs e)
        {
            string name = ModuleName.Text.Trim();
            if (name.Length == 0) { ModuleMessage.Text = Loc.T("Give the module a name."); ModuleName.Focus(); return; }
            if (!decimal.TryParse(ModuleCoef.Text.Trim().Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal coef) || coef <= 0 || coef > 99)
            { ModuleMessage.Text = Loc.T("The coefficient must be a number above 0."); ModuleCoef.Focus(); return; }
            var teacher = ModuleTeacher.SelectedItem as PersonOption;
            try
            {
                if (_dirty.Count > 0 && AllValid()) await SaveCoreAsync(false);
                int id = await ClassData.SaveModuleAsync(new ModuleInfo
                {
                    ModuleID = _editingModule?.ModuleID ?? 0, ClassID = _host.Current.ClassID, ModuleName = name, Coefficient = coef,
                    TeacherID = teacher != null && teacher.Id > 0 ? teacher.Id : (int?)null
                });
                ModuleEditor.Visibility = Visibility.Collapsed;
                MessageBox.Show(_editingModule == null ? "Module added successfully." : "Module saved successfully.");
                await LoadAsync(id);
                await _host.RefreshCountsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The module could not be added.\n" + ex.Message, "Couldn't add", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteModule_Click(object sender, RoutedEventArgs e)
        {
            var module = _current?.Module;
            if (module == null) return;
            if (MessageBox.Show("Delete the module “" + module.ModuleName + "”? Its assessments and scores are deleted too.", "Delete module", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            try
            {
                _dirty.Clear();
                await ClassData.DeleteModuleAsync(module.ModuleID);
                await LoadAsync();
                await _host.RefreshCountsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("That change could not be saved.\n" + ex.Message, "Couldn't save", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ------------------------------------------------------------ assessments

        private void AddAssessment_Click(object sender, RoutedEventArgs e) => OpenAssessmentEditor(null);

        private void Assessment_Click(object sender, RoutedEventArgs e) => OpenAssessmentEditor(((sender as FrameworkElement)?.DataContext as Chip)?.Assessment);

        private void OpenAssessmentEditor(AssessmentInfo a)
        {
            if (_current?.Module == null) return;
            _editingAssessment = a;
            ModuleEditor.Visibility = Visibility.Collapsed;
            AssessmentMessage.Text = "";
            AssessmentEditorTitle.Text = Loc.T(a == null ? "New assessment" : "Edit assessment");
            AssessmentTitle.Text = a?.Title ?? "";
            AssessmentKind.SelectedItem = ((IEnumerable<Kind>)AssessmentKind.ItemsSource).FirstOrDefault(k => k.Value == (a?.Kind ?? "Exam"));
            AssessmentWeight.Text = (a?.Weight ?? 1).ToString("0.##", CultureInfo.InvariantCulture);
            AssessmentMax.Text = (a?.MaxScore ?? 20).ToString("0.##", CultureInfo.InvariantCulture);
            AssessmentDate.SelectedDate = a?.Date ?? DateTime.Today;
            DeleteAssessmentButton.Visibility = a == null ? Visibility.Collapsed : Visibility.Visible;
            AssessmentEditor.Visibility = Visibility.Visible;
            AssessmentTitle.Focus();
        }

        private void CancelAssessment_Click(object sender, RoutedEventArgs e) => AssessmentEditor.Visibility = Visibility.Collapsed;

        private async void SaveAssessment_Click(object sender, RoutedEventArgs e)
        {
            if (_current?.Module == null) return;
            string title = AssessmentTitle.Text.Trim();
            if (title.Length == 0) { AssessmentMessage.Text = Loc.T("Give the assessment a title."); AssessmentTitle.Focus(); return; }
            if (!decimal.TryParse(AssessmentWeight.Text.Trim().Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal weight) || weight <= 0 || weight > 99)
            { AssessmentMessage.Text = Loc.T("The weight must be a number above 0."); AssessmentWeight.Focus(); return; }
            if (!decimal.TryParse(AssessmentMax.Text.Trim().Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal max) || max <= 0 || max > 1000)
            { AssessmentMessage.Text = Loc.T("The maximum score must be a number above 0."); AssessmentMax.Focus(); return; }

            try
            {
                if (_dirty.Count > 0 && AllValid()) await SaveCoreAsync(false);
                int moduleId = _current.Module.ModuleID;
                await ClassData.SaveAssessmentAsync(new AssessmentInfo
                {
                    AssessmentID = _editingAssessment?.AssessmentID ?? 0, ModuleID = moduleId, Title = title,
                    Kind = (AssessmentKind.SelectedItem as Kind)?.Value ?? "Exam", Weight = weight, MaxScore = max, Date = AssessmentDate.SelectedDate
                });
                AssessmentEditor.Visibility = Visibility.Collapsed;
                await LoadAsync(moduleId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The grades could not be saved.\n" + ex.Message, "Couldn't save", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteAssessment_Click(object sender, RoutedEventArgs e)
        {
            if (_editingAssessment == null || _current?.Module == null) return;
            if (MessageBox.Show("Delete the assessment “" + _editingAssessment.Title + "”? All its scores are deleted too.", "Delete assessment", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            try
            {
                int moduleId = _current.Module.ModuleID;
                _dirty.Clear();
                await ClassData.DeleteAssessmentAsync(_editingAssessment.AssessmentID);
                AssessmentEditor.Visibility = Visibility.Collapsed;
                await LoadAsync(moduleId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("That change could not be saved.\n" + ex.Message, "Couldn't save", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
