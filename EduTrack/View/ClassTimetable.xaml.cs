using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EduTrack.Data;
using EduTrack.Models;
using EduTrack.Utilities;

namespace EduTrack.View
{
    /// <summary>The week of a class: one column per day, one block per lesson. A lesson can be tied to a module, which brings its teacher.</summary>
    public partial class ClassTimetable : UserControl
    {
        private static readonly string[] Days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        private readonly ClassWorkspace _host;
        private List<LessonInfo> _lessons = new List<LessonInfo>();
        private List<ModuleInfo> _modules = new List<ModuleInfo>();
        private LessonInfo _editing;

        public ClassTimetable(ClassWorkspace host)
        {
            InitializeComponent();
            _host = host;

            DayBox.ItemsSource = Days.Select(Loc.T).ToList();
            var times = new List<string>();
            for (int minutes = 7 * 60; minutes <= 21 * 60; minutes += 15) times.Add(TimeSpan.FromMinutes(minutes).ToString(@"hh\:mm"));
            StartBox.ItemsSource = times;
            EndBox.ItemsSource = times;
            Loaded += async (s, e) => await LoadAsync();
        }

        private async Task LoadAsync()
        {
            var cls = _host.Current;
            if (cls == null) return;
            try
            {
                _lessons = await ClassData.GetLessonsAsync(cls.ClassID);
                _modules = await ClassData.GetModulesAsync(cls.ClassID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The schedule could not be loaded.\n" + ex.Message, "Can't load schedule", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ModuleBox.ItemsSource = new[] { new ModuleInfo { ModuleID = 0, ModuleName = Loc.T("No module") } }.Concat(_modules).ToList();
            BuildWeek();
        }

        // ------------------------------------------------------------ the week

        private void BuildWeek()
        {
            WeekGrid.Children.Clear();
            WeekGrid.ColumnDefinitions.Clear();
            WeekGrid.RowDefinitions.Clear();
            WeekGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            WeekGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            int lastDay = _lessons.Any(l => l.DayOfWeek == 7) ? 7 : _lessons.Any(l => l.DayOfWeek == 6) ? 6 : 5;
            for (int day = 1; day <= lastDay; day++)
            {
                WeekGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star), MinWidth = 148 });

                var header = new TextBlock
                {
                    Text = Loc.T(Days[day - 1]), FontWeight = FontWeights.Bold, Margin = new Thickness(8, 4, 8, 10),
                    TextTrimming = TextTrimming.CharacterEllipsis
                };
                header.SetResourceReference(TextBlock.ForegroundProperty, day == ((int)DateTime.Today.DayOfWeek + 6) % 7 + 1 ? "AccentBrush" : "InkBrush");
                Grid.SetColumn(header, day - 1);
                WeekGrid.Children.Add(header);

                var column = new StackPanel { Margin = new Thickness(4, 0, 4, 0) };
                foreach (var lesson in _lessons.Where(l => l.DayOfWeek == day).OrderBy(l => l.Start))
                    column.Children.Add(MakeBlock(lesson));
                Grid.SetColumn(column, day - 1);
                Grid.SetRow(column, 1);
                WeekGrid.Children.Add(column);
            }
            EmptyState.Visibility = _lessons.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private FrameworkElement MakeBlock(LessonInfo lesson)
        {
            var text = new StackPanel();
            text.Children.Add(new TextBlock { Text = lesson.TimeText, FontWeight = FontWeights.Bold, FontSize = 12, FlowDirection = FlowDirection.LeftToRight, HorizontalAlignment = HorizontalAlignment.Left });
            text.Children.Add(new TextBlock { Text = string.IsNullOrWhiteSpace(lesson.Label) ? Loc.T("Lesson") : lesson.Label, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 2, 0, 0) });
            string detail = string.Join(" · ", new[] { lesson.TeacherName, lesson.Room }.Where(x => !string.IsNullOrWhiteSpace(x)));
            if (detail.Length > 0)
            {
                var caption = new TextBlock { Text = detail, FontSize = 12, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 2, 0, 0) };
                caption.SetResourceReference(TextBlock.ForegroundProperty, "MutedBrush");
                text.Children.Add(caption);
            }

            var block = new Button
            {
                Content = text, Tag = lesson, HorizontalContentAlignment = HorizontalAlignment.Stretch, Padding = new Thickness(10, 8, 10, 9),
                Margin = new Thickness(0, 0, 0, 6), Cursor = System.Windows.Input.Cursors.Hand
            };
            block.SetResourceReference(Control.StyleProperty, "GhostButton");
            block.SetResourceReference(Control.BackgroundProperty, "SurfaceAltBrush");
            block.SetResourceReference(Control.ForegroundProperty, "InkBrush");
            block.Click += (s, e) => OpenEditor(lesson);
            return block;
        }

        // ------------------------------------------------------------ editor

        private void AddLesson_Click(object sender, RoutedEventArgs e) => OpenEditor(null);

        private void OpenEditor(LessonInfo lesson)
        {
            _editing = lesson;
            EditorMessage.Text = "";
            EditorTitle.Text = Loc.T(lesson == null ? "New lesson" : "Edit lesson");
            DeleteButton.Visibility = lesson == null ? Visibility.Collapsed : Visibility.Visible;
            DayBox.SelectedIndex = (lesson?.DayOfWeek ?? ((int)DateTime.Today.DayOfWeek + 6) % 7 + 1) - 1;
            StartBox.SelectedItem = (lesson?.Start ?? new TimeSpan(9, 0, 0)).ToString(@"hh\:mm");
            EndBox.SelectedItem = (lesson?.End ?? new TimeSpan(10, 0, 0)).ToString(@"hh\:mm");
            var items = (IEnumerable<ModuleInfo>)ModuleBox.ItemsSource;
            ModuleBox.SelectedItem = items?.FirstOrDefault(m => m.ModuleID == (lesson?.ModuleID ?? 0));
            TitleBox.Text = lesson?.Title ?? "";
            RoomBox.Text = lesson?.Room ?? "";
            Editor.Visibility = Visibility.Visible;
        }

        private void CancelEdit_Click(object sender, RoutedEventArgs e) => Editor.Visibility = Visibility.Collapsed;

        private async void SaveLesson_Click(object sender, RoutedEventArgs e)
        {
            var cls = _host.Current;
            if (cls == null) return;
            if (!TimeSpan.TryParse(StartBox.SelectedItem as string, out TimeSpan start) || !TimeSpan.TryParse(EndBox.SelectedItem as string, out TimeSpan end))
            {
                EditorMessage.Text = Loc.T("Choose the start and end time.");
                return;
            }
            if (end <= start) { EditorMessage.Text = Loc.T("The lesson must end after it starts."); return; }

            int day = DayBox.SelectedIndex + 1;
            var module = ModuleBox.SelectedItem as ModuleInfo;
            int? moduleId = module != null && module.ModuleID > 0 ? module.ModuleID : (int?)null;
            string title = TitleBox.Text.Trim();
            if (moduleId == null && title.Length == 0) { EditorMessage.Text = Loc.T("Choose a module or give the lesson a title."); return; }

            var overlap = _lessons.FirstOrDefault(l => l.DayOfWeek == day && l.LessonID != (_editing?.LessonID ?? 0) && l.Start < end && start < l.End);
            if (overlap != null)
            {
                EditorMessage.Text = Loc.T("This overlaps another lesson on the same day: ") + overlap.Label + " " + overlap.TimeText;
                return;
            }

            try
            {
                await ClassData.SaveLessonAsync(new LessonInfo
                {
                    LessonID = _editing?.LessonID ?? 0, ClassID = cls.ClassID, DayOfWeek = day, Start = start, End = end,
                    ModuleID = moduleId, Title = title.Length == 0 ? null : title, Room = string.IsNullOrWhiteSpace(RoomBox.Text) ? null : RoomBox.Text.Trim()
                });
                Editor.Visibility = Visibility.Collapsed;
                await LoadAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The schedule could not be saved.\n" + ex.Message, "Couldn't save", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteLesson_Click(object sender, RoutedEventArgs e)
        {
            if (_editing == null) return;
            if (MessageBox.Show("Delete this lesson from the timetable? Attendance already taken for it is kept.", "Delete lesson", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            try
            {
                await ClassData.DeleteLessonAsync(_editing.LessonID);
                Editor.Visibility = Visibility.Collapsed;
                await LoadAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("That change could not be saved.\n" + ex.Message, "Couldn't save", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
