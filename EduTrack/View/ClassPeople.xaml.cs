using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using EduTrack.Data;
using EduTrack.Models;
using EduTrack.Utilities;

namespace EduTrack.View
{
    /// <summary>Who is in the class: add and remove students and teachers.</summary>
    public partial class ClassPeople : UserControl
    {
        private readonly ClassWorkspace _host;
        private readonly DispatcherTimer _searchDelay = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(250) };
        private ClassSummary Class => _host.Current;

        public ClassPeople(ClassWorkspace host)
        {
            InitializeComponent();
            _host = host;
            _searchDelay.Tick += async (s, e) => { _searchDelay.Stop(); await LoadStudentsAsync(); };
            Loaded += async (s, e) => { await LoadTeachersAsync(); await LoadStudentsAsync(); await LoadCandidatesAsync(); };
        }

        private async System.Threading.Tasks.Task LoadTeachersAsync()
        {
            if (Class == null) return;
            List<Teacher> teachers = new List<Teacher>();
            try { teachers = await ClassData.GetTeachersAsync(Class.ClassID); }
            catch (Exception ex) { MessageBox.Show("The teachers of this class could not be loaded.\n" + ex.Message, "Can't load teachers", MessageBoxButton.OK, MessageBoxImage.Error); }
            TeachersList.ItemsSource = teachers;
            TeachersEmpty.Visibility = teachers.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            TeachersHeader.Text = Loc.T("Teachers  (" + teachers.Count + ")");
        }

        private async System.Threading.Tasks.Task LoadStudentsAsync()
        {
            if (Class == null) return;
            string search = SearchBox.Text.Trim();
            List<Student> students = new List<Student>();
            try { students = await ClassData.GetStudentsAsync(Class.ClassID, search); }
            catch (Exception ex) { MessageBox.Show("The students of this class could not be loaded.\n" + ex.Message, "Can't load students", MessageBoxButton.OK, MessageBoxImage.Error); }
            StudentsList.ItemsSource = students;
            StudentsEmpty.Text = Loc.T(search.Length > 0 ? "No student matches “" + search + "”" : "No student is in this class yet. Use the Add to class bar above.");
            StudentsEmpty.Visibility = students.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            StudentsHeader.Text = Loc.T("Students  (" + (search.Length > 0 ? students.Count + " found" : students.Count.ToString()) + ")");
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e) { _searchDelay.Stop(); _searchDelay.Start(); }

        private async void AddKind_Changed(object sender, RoutedEventArgs e)
        {
            if (comboBox == null) return;
            await LoadCandidatesAsync();
        }

        private async System.Threading.Tasks.Task LoadCandidatesAsync()
        {
            if (Class == null) return;
            bool teacher = radioTeacher.IsChecked == true;
            List<PersonOption> people = new List<PersonOption>();
            try { people = await ClassData.GetCandidatesAsync(Class.ClassID, teacher); }
            catch (Exception ex) { MessageBox.Show("The list of people could not be loaded.\n" + ex.Message, "Can't load people", MessageBoxButton.OK, MessageBoxImage.Error); }
            foreach (var p in people) if (!string.IsNullOrEmpty(p.Note)) p.Note = Loc.T("currently in") + " " + p.CurrentClassName;
            comboBox.ItemsSource = people;
            comboBox.Tag = Loc.T(people.Count == 0 ? (teacher ? "Every teacher is already in this class" : "Every student is already in this class") : "Choose who to add");
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            var person = comboBox.SelectedItem as PersonOption;
            if (Class == null || person == null)
            {
                MessageBox.Show("Choose who to add first.", "Nothing chosen", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            bool teacher = radioTeacher.IsChecked == true;

            // A student belongs to one class at a time, so adding them here moves them.
            if (!teacher && person.CurrentClassId.HasValue)
            {
                var answer = MessageBox.Show(person.Name + " is in " + person.CurrentClassName + ". Move them to " + Class.ClassName + "?",
                    "Move student", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (answer != MessageBoxResult.Yes) return;
            }

            try
            {
                await ClassData.AddPersonAsync(Class.ClassID, person.Id, teacher);
                MessageBox.Show(person.Name + " was added to " + Class.ClassName + ".");
                comboBox.SelectedItem = null;
                await Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("They could not be added to the class.\n" + ex.Message, "Couldn't add", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void RemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            var student = (sender as FrameworkElement)?.DataContext as Student;
            if (student == null || Class == null) return;
            if (MessageBox.Show("Remove " + student.FullName + " from " + Class.ClassName + "? Their student record is kept.",
                "Remove from class", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            await Change(() => ClassData.RemovePersonAsync(Class.ClassID, student.Id, false));
        }

        private async void RemoveTeacher_Click(object sender, RoutedEventArgs e)
        {
            var teacher = (sender as FrameworkElement)?.DataContext as Teacher;
            if (teacher == null || Class == null) return;
            if (MessageBox.Show("Remove " + teacher.FullName + " from " + Class.ClassName + "? Their teacher record is kept.",
                "Remove from class", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            await Change(() => ClassData.RemovePersonAsync(Class.ClassID, teacher.Id, true));
        }

        private async System.Threading.Tasks.Task Change(Func<System.Threading.Tasks.Task> action)
        {
            try { await action(); await Refresh(); }
            catch (Exception ex) { MessageBox.Show("That change could not be saved.\n" + ex.Message, "Couldn't save", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private async System.Threading.Tasks.Task Refresh()
        {
            await LoadTeachersAsync();
            await LoadStudentsAsync();
            await LoadCandidatesAsync();
            await _host.RefreshCountsAsync();
        }
    }
}
