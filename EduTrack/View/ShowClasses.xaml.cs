using EduTrack.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EduTrack.View
{
    /// <summary>
    /// Interaction logic for ShowClasses.xaml
    /// </summary>
    public partial class ShowClasses : UserControl
    {
        private const string DatabaseServer = "127.0.0.1";
        private const string DatabaseName = "datebase";
        private const string DatabaseUser = "root";
        private const string DatabasePassword = "";
        public ShowClasses()
        {
            InitializeComponent();
            LoadClasses();
            this.DataContext = this;
        }

        private async Task LoadStudentsAsync(string className = null, string searchText = null)
        {
            string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";
            string query = "SELECT s.StudentID, s.p_pic, s.f_name, s.m_name, s.l_name, s.gender, s.email, s.phone, s.b_date, s.nationality, s.cin, s.j_date, s.Major " +
                           "FROM student_registry s " +
                           "JOIN studentclasses sc ON s.StudentID = sc.StudentID " +
                           "JOIN classes c ON sc.ClassID = c.ClassID";
            if (!string.IsNullOrEmpty(className))
            {
                query += $" WHERE c.ClassName = '{className}'";
            }
            if (!string.IsNullOrEmpty(searchText))
            {
                query += (string.IsNullOrEmpty(className) ? " WHERE" : " AND") + $" (s.f_name LIKE '%{searchText}%' OR s.m_name LIKE '%{searchText}%' OR s.l_name LIKE '%{searchText}%')";
            }

            ObservableCollection<Student> studentList = new ObservableCollection<Student>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Student student = new Student
                            {
                                Id = reader.GetInt32("StudentID"),
                                F_Name = reader.IsDBNull(reader.GetOrdinal("f_name")) ? null : reader.GetString("f_name"),
                                M_Name = reader.IsDBNull(reader.GetOrdinal("m_name")) ? null : reader.GetString("m_name"),
                                L_Name = reader.IsDBNull(reader.GetOrdinal("l_name")) ? null : reader.GetString("l_name"),
                                Gender = reader.IsDBNull(reader.GetOrdinal("gender")) ? null : reader.GetString("gender"),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                                Phone = reader.IsDBNull(reader.GetOrdinal("phone")) ? null : reader.GetString("phone"),
                                B_Date = reader.IsDBNull(reader.GetOrdinal("b_date")) ? (DateTime?)null : reader.GetDateTime("b_date"),
                                Nationality = reader.IsDBNull(reader.GetOrdinal("nationality")) ? null : reader.GetString("nationality"),
                                CIN = reader.IsDBNull(reader.GetOrdinal("cin")) ? null : reader.GetString("cin"),
                                Major = reader.IsDBNull(reader.GetOrdinal("major")) ? null : reader.GetString("major"),
                                J_Date = reader.IsDBNull(reader.GetOrdinal("j_date")) ? (DateTime?)null : reader.GetDateTime("j_date"),
                                P_Pic = reader.IsDBNull(reader.GetOrdinal("p_pic")) ? null : reader.GetString("p_pic"),
                            };
                            studentList.Add(student);
                        }
                    }
                }

                Dispatcher.Invoke(() =>
                {
                    StudentsList.ItemsSource = studentList;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void LoadClasses()
        {
            string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";
            string query = "SELECT ClassName FROM classes";

            List<string> classNames = new List<string>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string className = reader.GetString("ClassName");
                            classNames.Add(className);
                        }
                    }
                }

                Dispatcher.Invoke(() =>
                {
                    comboBoxClass.ItemsSource = classNames;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void LoadTeachersOrStudents(string type)
        {
            string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";
            string query = type == "Teacher" ? "SELECT f_name, m_name, l_name FROM teacher_registry" : "SELECT f_name, m_name, l_name FROM student_registry";

            List<string> names = new List<string>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string name = $"{reader.GetString("f_name")} {reader.GetString("m_name")} {reader.GetString("l_name")}";
                            names.Add(name);
                        }
                    }
                }

                Dispatcher.Invoke(() =>
                {
                    comboBoxTeacherStudent.ItemsSource = names;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void checkBoxTeacher_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxStudent.IsChecked = false;
            LoadTeachersOrStudents("Teacher");
        }

        private void checkBoxStudent_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxTeacher.IsChecked = false;
            LoadTeachersOrStudents("Student");
        }

        private void checkBoxTeacher_Unchecked(object sender, RoutedEventArgs e)
        {
            comboBoxTeacherStudent.ItemsSource = null;
        }

        private void checkBoxStudent_Unchecked(object sender, RoutedEventArgs e)
        {
            comboBoxTeacherStudent.ItemsSource = null;
        }

        private async void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text;
            await LoadStudentsAsync(searchText);
        }

        private void ScrollViewer_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as ScrollViewer)?.Focus();
        }

        private void ListBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            await LoadStudentsAsync();
        }

        private async void AddTeacherOrStudent_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";
            string selectedClass = comboBoxClass.SelectedItem as string;
            string selectedTeacherOrStudent = comboBoxTeacherStudent.SelectedItem as string;

            if (string.IsNullOrEmpty(selectedClass) || string.IsNullOrEmpty(selectedTeacherOrStudent))
            {
                MessageBox.Show("Please select a class and a teacher or student.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string[] nameParts = selectedTeacherOrStudent.Split(' ');
            string firstName = nameParts[0];
            string middleName = nameParts[1];
            string lastName = nameParts[2];

            string query = checkBoxTeacher.IsChecked == true
                ? $"SELECT TeacherID FROM teacher_registry WHERE f_name = '{firstName}' AND m_name = '{middleName}' AND l_name = '{lastName}'"
                : $"SELECT StudentID FROM student_registry WHERE f_name = '{firstName}' AND m_name = '{middleName}' AND l_name = '{lastName}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        object result = await command.ExecuteScalarAsync();
                        if (result != null)
                        {
                            int id = Convert.ToInt32(result);
                            string checkQuery = checkBoxTeacher.IsChecked == true
                                ? $"SELECT ClassName FROM classes c JOIN teacherclasses tc ON c.ClassID = tc.ClassID WHERE tc.TeacherID = {id} AND c.ClassName = '{selectedClass}'"
                                : $"SELECT ClassName FROM classes c JOIN studentclasses sc ON c.ClassID = sc.ClassID WHERE sc.StudentID = {id} AND c.ClassName = '{selectedClass}'";

                            using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                            {
                                object checkResult = await checkCommand.ExecuteScalarAsync();
                                if (checkResult != null)
                                {
                                    MessageBox.Show($"{(checkBoxTeacher.IsChecked == true ? "Teacher" : "Student")} is already in the class {selectedClass}.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                                    return;
                                }
                            }

                            string insertQuery = checkBoxTeacher.IsChecked == true
                                ? $"INSERT INTO teacherclasses (TeacherID, ClassID) VALUES ({id}, (SELECT ClassID FROM classes WHERE ClassName = '{selectedClass}'))"
                                : $"INSERT INTO studentclasses (StudentID, ClassID) VALUES ({id}, (SELECT ClassID FROM classes WHERE ClassName = '{selectedClass}'))";

                            using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                            {
                                int rowsAffected = await insertCommand.ExecuteNonQueryAsync();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show($"{(checkBoxTeacher.IsChecked == true ? "Teacher" : "Student")} has been added to the class.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                {
                                    MessageBox.Show("An error occurred while adding the teacher or student to the class.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show($"{(checkBoxTeacher.IsChecked == true ? "Teacher" : "Student")} not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void comboBoxClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";
            string selectedClass = comboBoxClass.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedClass))
            {
                await LoadStudentsAsync(selectedClass);
            }

            string queryTeachers = $"SELECT t.p_pic, t.f_name, t.m_name, t.l_name, t.email, t.phone FROM teacher_registry t JOIN teacherclasses tc ON t.TeacherID = tc.TeacherID JOIN classes c ON tc.ClassID = c.ClassID WHERE c.ClassName = '{selectedClass}'";
            string queryStudents = $"SELECT s.p_pic, s.f_name, s.m_name, s.l_name, s.email, s.phone FROM student_registry s JOIN studentclasses sc ON s.StudentID = sc.StudentID JOIN classes c ON sc.ClassID = c.ClassID WHERE c.ClassName = '{selectedClass}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (MySqlCommand commandTeachers = new MySqlCommand(queryTeachers, connection))
                    using (MySqlDataReader readerTeachers = (MySqlDataReader)await commandTeachers.ExecuteReaderAsync())
                    {
                        List<Teacher> teachers = new List<Teacher>();
                        while (await readerTeachers.ReadAsync())
                        {
                            Teacher teacher = new Teacher
                            {
                                P_Pic = readerTeachers.GetString("p_pic"),
                                FirstName = readerTeachers.GetString("f_name"),
                                MiddleName = readerTeachers.GetString("m_name"),
                                LastName = readerTeachers.GetString("l_name"),
                                Email = readerTeachers.GetString("email"),
                                Phone = readerTeachers.GetString("phone")
                            };
                            teachers.Add(teacher);
                        }

                        Dispatcher.Invoke(() =>
                        {
                            TeachersList.ItemsSource = teachers;
                        });
                    }

                    using (MySqlCommand commandStudents = new MySqlCommand(queryStudents, connection))
                    using (MySqlDataReader readerStudents = (MySqlDataReader)await commandStudents.ExecuteReaderAsync())
                    {
                        List<Student> students = new List<Student>();
                        while (await readerStudents.ReadAsync())
                        {
                            Student student = new Student
                            {
                                P_Pic = readerStudents.GetString("p_pic"),
                                F_Name = readerStudents.GetString("f_name"),
                                M_Name = readerStudents.GetString("m_name"),
                                L_Name = readerStudents.GetString("l_name"),
                                Email = readerStudents.GetString("email"),
                                Phone = readerStudents.GetString("phone")
                            };
                            students.Add(student);
                        }

                        Dispatcher.Invoke(() =>
                        {
                            StudentsList.ItemsSource = students;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            await LoadStudentsAsync(selectedClass);
        }

        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";
            string selectedClass = comboBoxClass.SelectedItem as string;

            if (string.IsNullOrEmpty(selectedClass))
            {
                MessageBox.Show("Please select a class to delete.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string queryDeleteStudentClasses = $"DELETE FROM studentclasses WHERE ClassID = (SELECT ClassID FROM classes WHERE ClassName = '{selectedClass}')";
            string queryDeleteTeacherClasses = $"DELETE FROM teacherclasses WHERE ClassID = (SELECT ClassID FROM classes WHERE ClassName = '{selectedClass}')";
            string queryDeleteClass = $"DELETE FROM classes WHERE ClassName = '{selectedClass}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (MySqlCommand commandDeleteStudentClasses = new MySqlCommand(queryDeleteStudentClasses, connection))
                    {
                        await commandDeleteStudentClasses.ExecuteNonQueryAsync();
                    }
                    using (MySqlCommand commandDeleteTeacherClasses = new MySqlCommand(queryDeleteTeacherClasses, connection))
                    {
                        await commandDeleteTeacherClasses.ExecuteNonQueryAsync();
                    }
                    using (MySqlCommand commandDeleteClass = new MySqlCommand(queryDeleteClass, connection))
                    {
                        int rowsAffected = await commandDeleteClass.ExecuteNonQueryAsync();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Class has been deleted.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            // Refresh the classes list
                            LoadClasses();
                        }
                        else
                        {
                            MessageBox.Show("An error occurred while deleting the class.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}