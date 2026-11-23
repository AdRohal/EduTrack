using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using EduTrack.Data;
using EduTrack.Models;
using EduTrack.Utilities;
using MySql.Data.MySqlClient;

namespace EduTrack.View
{
    /// <summary>Shows who is in a class and lets you add or remove students and teachers.</summary>
    public partial class ShowClasses : UserControl
    {
        private sealed class ClassOption
        {
            public int ClassID { get; set; }
            public string ClassName { get; set; }
            public string Major { get; set; }
            public DateTime? Created { get; set; }
        }

        private sealed class PersonOption
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Note { get; set; }
            public int? CurrentClassId { get; set; }
            public string CurrentClassName { get; set; }
        }

        private readonly DispatcherTimer _searchDelay = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(250) };
        private ClassOption CurrentClass => comboBoxClass.SelectedItem as ClassOption;

        public ShowClasses()
        {
            InitializeComponent();
            _searchDelay.Tick += async (s, e) => { _searchDelay.Stop(); await LoadStudentsAsync(); };
            Loaded += async (s, e) => await LoadClassesAsync();
        }

        // ---------- classes ----------
        private async Task LoadClassesAsync(int? keepClassId = null)
        {
            var classes = new List<ClassOption>();
            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand("SELECT ClassID, ClassName, major, CreatedDate FROM classes ORDER BY ClassName", connection))
                    using (var reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            classes.Add(new ClassOption
                            {
                                ClassID = reader.GetInt32(0),
                                ClassName = reader.Str("ClassName"),
                                Major = reader.Str("major"),
                                Created = reader.Date("CreatedDate")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The classes could not be loaded. Check that the database is running.\n" + ex.Message, "Can't load classes", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            comboBoxClass.ItemsSource = classes;
            if (classes.Count == 0)
            {
                ShowIdle("No classes yet", "Create a class first, then come back here to put students and teachers in it.", true);
                return;
            }
            var keep = classes.FirstOrDefault(c => c.ClassID == keepClassId);
            comboBoxClass.SelectedItem = keep ?? classes[0];
        }

        private void ShowIdle(string title, string hint, bool offerCreate)
        {
            RosterGrid.Visibility = Visibility.Collapsed;
            IdleState.Visibility = Visibility.Visible;
            IdleTitle.Text = title;
            IdleHint.Text = hint;
            IdleAction.Visibility = offerCreate ? Visibility.Visible : Visibility.Collapsed;
            AddPanel.IsEnabled = false;
            deleteButton.IsEnabled = false;
            ClassMeta.Text = string.Empty;
        }

        private async void comboBoxClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var current = CurrentClass;
            if (current == null) return;

            ClassMeta.Text = string.Join(", ", new[]
            {
                string.IsNullOrWhiteSpace(current.Major) ? null : "Major: " + current.Major,
                current.Created.HasValue ? "created " + current.Created.Value.ToString("d MMM yyyy") : null
            }.Where(x => x != null));
            IdleState.Visibility = Visibility.Collapsed;
            RosterGrid.Visibility = Visibility.Visible;
            AddPanel.IsEnabled = true;
            deleteButton.IsEnabled = true;
            SearchBox.Clear();

            await LoadTeachersAsync();
            await LoadStudentsAsync();
            await LoadCandidatesAsync();
        }

        private void IdleAction_Click(object sender, RoutedEventArgs e) => HubHelper.Find(this)?.SelectTab(0);

        // ---------- rosters ----------
        private async Task LoadTeachersAsync()
        {
            var current = CurrentClass;
            if (current == null) return;
            var teachers = new List<Teacher>();
            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    const string sql = "SELECT t.TeacherID, t.p_pic, t.f_name, t.m_name, t.l_name, t.email, t.phone FROM teacher_registry t " +
                                       "JOIN teacherclasses tc ON t.TeacherID = tc.TeacherID WHERE tc.ClassID = @c ORDER BY t.l_name, t.f_name";
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@c", current.ClassID);
                        using (var reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                teachers.Add(new Teacher
                                {
                                    Id = reader.GetInt32(0),
                                    P_Pic = reader.Str("p_pic"),
                                    FirstName = reader.Str("f_name"),
                                    MiddleName = reader.Str("m_name"),
                                    LastName = reader.Str("l_name"),
                                    Email = reader.Str("email"),
                                    Phone = reader.Str("phone")
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The teachers of this class could not be loaded.\n" + ex.Message, "Can't load teachers", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            TeachersList.ItemsSource = teachers;
            TeachersEmpty.Visibility = teachers.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            TeachersHeader.Text = "Teachers  (" + teachers.Count + ")";
        }

        private async Task LoadStudentsAsync()
        {
            var current = CurrentClass;
            if (current == null) return;
            string search = SearchBox.Text.Trim();
            var students = new List<Student>();
            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    string sql = "SELECT s.StudentID, s.p_pic, s.f_name, s.m_name, s.l_name, s.email, s.phone FROM student_registry s " +
                                 "JOIN studentclasses sc ON s.StudentID = sc.StudentID WHERE sc.ClassID = @c";
                    if (search.Length > 0) sql += " AND (s.f_name LIKE @q OR s.m_name LIKE @q OR s.l_name LIKE @q OR CONCAT(s.f_name, ' ', s.l_name) LIKE @q)";
                    sql += " ORDER BY s.l_name, s.f_name";
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@c", current.ClassID);
                        if (search.Length > 0) command.Parameters.AddWithValue("@q", "%" + search + "%");
                        using (var reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                students.Add(new Student
                                {
                                    Id = reader.GetInt32(0),
                                    P_Pic = reader.Str("p_pic"),
                                    F_Name = reader.Str("f_name"),
                                    M_Name = reader.Str("m_name"),
                                    L_Name = reader.Str("l_name"),
                                    Email = reader.Str("email"),
                                    Phone = reader.Str("phone")
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The students of this class could not be loaded.\n" + ex.Message, "Can't load students", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            StudentsList.ItemsSource = students;
            StudentsEmpty.Text = search.Length > 0 ? "No student in this class matches “" + search + "”."
                                                   : "No student is in this class yet. Use the Add to class bar above.";
            StudentsEmpty.Visibility = students.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            StudentsHeader.Text = "Students  (" + (search.Length > 0 ? students.Count + " found" : students.Count.ToString()) + ")";
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _searchDelay.Stop();
            _searchDelay.Start();
        }

        // ---------- adding people ----------
        private async void AddKind_Changed(object sender, RoutedEventArgs e)
        {
            if (comboBoxTeacherStudent == null) return;
            await LoadCandidatesAsync();
        }

        private async Task LoadCandidatesAsync()
        {
            var current = CurrentClass;
            if (current == null) return;
            bool teacher = radioTeacher.IsChecked == true;
            var people = new List<PersonOption>();
            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    string sql;
                    if (teacher)
                    {
                        sql = "SELECT t.TeacherID AS Id, t.f_name, t.m_name, t.l_name, NULL AS ClassID, NULL AS ClassName FROM teacher_registry t " +
                              "WHERE t.TeacherID NOT IN (SELECT TeacherID FROM teacherclasses WHERE ClassID = @c) ORDER BY t.l_name, t.f_name";
                    }
                    else
                    {
                        sql = "SELECT s.StudentID AS Id, s.f_name, s.m_name, s.l_name, c.ClassID, c.ClassName FROM student_registry s " +
                              "LEFT JOIN studentclasses sc ON s.StudentID = sc.StudentID LEFT JOIN classes c ON c.ClassID = sc.ClassID " +
                              "WHERE sc.ClassID IS NULL OR sc.ClassID <> @c ORDER BY s.l_name, s.f_name";
                    }
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@c", current.ClassID);
                        using (var reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string className = reader.Str("ClassName");
                                people.Add(new PersonOption
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = NameHelper.Join(reader.Str("f_name"), reader.Str("m_name"), reader.Str("l_name")),
                                    Note = className == null ? "" : "currently in " + className,
                                    CurrentClassId = reader.Int("ClassID"),
                                    CurrentClassName = className
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The list of people could not be loaded.\n" + ex.Message, "Can't load people", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            comboBoxTeacherStudent.ItemsSource = people;
            comboBoxTeacherStudent.Tag = people.Count == 0 ? (teacher ? "Every teacher is already in this class" : "Every student is already in this class") : "Choose who to add";
        }

        private async void AddTeacherOrStudent_Click(object sender, RoutedEventArgs e)
        {
            var current = CurrentClass;
            var person = comboBoxTeacherStudent.SelectedItem as PersonOption;
            if (current == null || person == null)
            {
                MessageBox.Show("Choose who to add first.", "Nothing chosen", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            bool teacher = radioTeacher.IsChecked == true;

            // A student belongs to one class at a time, so adding them here moves them.
            if (!teacher && person.CurrentClassId.HasValue)
            {
                var answer = MessageBox.Show(person.Name + " is in " + person.CurrentClassName + ". Move them to " + current.ClassName + "?",
                    "Move student", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (answer != MessageBoxResult.Yes) return;
            }

            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    string sql = teacher
                        ? "INSERT INTO teacherclasses (TeacherID, ClassID) VALUES (@p, @c)"
                        : "INSERT INTO studentclasses (StudentID, ClassID) VALUES (@p, @c) ON DUPLICATE KEY UPDATE ClassID = VALUES(ClassID)";
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@p", person.Id);
                        command.Parameters.AddWithValue("@c", current.ClassID);
                        await command.ExecuteNonQueryAsync();
                    }
                }
                MessageBox.Show(person.Name + " was added to " + current.ClassName + ".");
                comboBoxTeacherStudent.SelectedItem = null;
                await LoadTeachersAsync();
                await LoadStudentsAsync();
                await LoadCandidatesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("They could not be added to the class.\n" + ex.Message, "Couldn't add", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ---------- removing ----------
        private async void RemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            var student = (sender as FrameworkElement)?.DataContext as Student;
            if (student == null || CurrentClass == null) return;
            if (MessageBox.Show("Remove " + student.FullName + " from " + CurrentClass.ClassName + "? Their student record is kept.",
                "Remove from class", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            await ExecuteAsync("DELETE FROM studentclasses WHERE StudentID = @p AND ClassID = @c", student.Id);
            await LoadStudentsAsync();
            await LoadCandidatesAsync();
        }

        private async void RemoveTeacher_Click(object sender, RoutedEventArgs e)
        {
            var teacher = (sender as FrameworkElement)?.DataContext as Teacher;
            if (teacher == null || CurrentClass == null) return;
            if (MessageBox.Show("Remove " + teacher.FullName + " from " + CurrentClass.ClassName + "? Their teacher record is kept.",
                "Remove from class", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            await ExecuteAsync("DELETE FROM teacherclasses WHERE TeacherID = @p AND ClassID = @c", teacher.Id);
            await LoadTeachersAsync();
            await LoadCandidatesAsync();
        }

        private async Task ExecuteAsync(string sql, int personId)
        {
            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@p", personId);
                        command.Parameters.AddWithValue("@c", CurrentClass.ClassID);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("That change could not be saved.\n" + ex.Message, "Couldn't save", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ---------- deleting the class ----------
        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var current = CurrentClass;
            if (current == null) return;

            var answer = MessageBox.Show(
                "Delete the class “" + current.ClassName + "”? Its schedule, modules and grades are deleted too. " +
                "Students and teachers stay in the system, they just won't belong to a class.",
                "Delete class", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (answer != MessageBoxResult.Yes) return;

            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        foreach (var sql in new[]
                        {
                            "DELETE FROM student_module WHERE ModuleID IN (SELECT ModuleID FROM modules WHERE ClassID = @c)",
                            "DELETE FROM modules WHERE ClassID = @c",
                            "DELETE FROM classschedules WHERE ClassID = @c",
                            "DELETE FROM studentclasses WHERE ClassID = @c",
                            "DELETE FROM teacherclasses WHERE ClassID = @c",
                            "DELETE FROM classes WHERE ClassID = @c"
                        })
                        {
                            using (var command = new MySqlCommand(sql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@c", current.ClassID);
                                await command.ExecuteNonQueryAsync();
                            }
                        }
                        transaction.Commit();
                    }
                }
                MessageBox.Show("The class was deleted.");
                await LoadClassesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The class could not be deleted.\n" + ex.Message, "Couldn't delete", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
