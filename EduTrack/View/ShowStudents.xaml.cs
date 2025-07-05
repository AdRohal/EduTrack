using Bogus;
using EduTrack.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EduTrack.Data;

namespace EduTrack.View
{
    public partial class ShowStudents : UserControl
    {
        public ShowStudents()
        {
            InitializeComponent();
            LoadStudents();
            this.DataContext = this;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Student selectedStudent = StudentsList.SelectedItem as Student;

            if (selectedStudent == null)
            {
                MessageBox.Show("Please select a student first.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            DetailedStudentInfoView detailedView = new DetailedStudentInfoView(selectedStudent)
            {
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Title = "Student Details"
            };
            detailedView.ShowDialog();
        }

        private async Task LoadStudentsAsync(string searchText = null)
        {
            string query = "SELECT StudentID, p_pic ,f_name, m_name, l_name, gender, email, phone, b_date, nationality, cin, j_date, Major FROM student_registry";
            if (!string.IsNullOrEmpty(searchText))
            {
                query += $" WHERE f_name LIKE '%{searchText}%' OR m_name LIKE '%{searchText}%' OR l_name LIKE '%{searchText}%'";
            }

            ObservableCollection<Student> studentList = new ObservableCollection<Student>();

            try
            {
                using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
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

        private async void LoadStudents()
        {

            await LoadStudentsAsync();
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
    }
}