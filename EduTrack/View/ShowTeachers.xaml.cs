using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using EduTrack.Models;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EduTrack.View
{
    /// <summary>
    /// Interaction logic for ShowTeachers.xaml
    /// </summary>
    public partial class ShowTeachers : UserControl
    {
        private const string DatabaseServer = "127.0.0.1";
        private const string DatabaseName = "datebase";
        private const string DatabaseUser = "root";
        private const string DatabasePassword = "";

        public ShowTeachers()
        {
            InitializeComponent();
            LoadTeachers();
            this.DataContext = this;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Teacher selectedTeacher = TeachersList.SelectedItem as Teacher;

            if (selectedTeacher == null)
            {
                MessageBox.Show("Please select an teacher first.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            DetailedTeacherInfoView detailedView = new DetailedTeacherInfoView(selectedTeacher)
            {
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Title = "Teacher Details"
            };
            detailedView.ShowDialog();
        }

        private async Task LoadTeachersAsync(string searchText = null)
        {
            string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";
            string query = "SELECT TeacherID, p_pic,f_name, m_name, l_name, gender, email, phone, b_date, nationality, cin, address, role, j_date, contract_start, contract_end, salary, Module FROM teacher_registry";
            if (!string.IsNullOrEmpty(searchText))
            {
                query += $" WHERE f_name LIKE '%{searchText}%' OR m_name LIKE '%{searchText}%' OR l_name LIKE '%{searchText}%'";
            }

            ObservableCollection<Teacher> empList = new ObservableCollection<Teacher>();

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
                            Teacher teacher = new Teacher
                            {
                                Id = reader.GetInt32("TeacherID"),
                                P_Pic = reader.IsDBNull(reader.GetOrdinal("p_pic")) ? null : reader.GetString("p_pic"),
                                FirstName = reader.IsDBNull(reader.GetOrdinal("f_name")) ? null : reader.GetString("f_name"),
                                MiddleName = reader.IsDBNull(reader.GetOrdinal("m_name")) ? null : reader.GetString("m_name"),
                                LastName = reader.IsDBNull(reader.GetOrdinal("l_name")) ? null : reader.GetString("l_name"),
                                Gender = reader.IsDBNull(reader.GetOrdinal("gender")) ? null : reader.GetString("gender"),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                                Phone = reader.IsDBNull(reader.GetOrdinal("phone")) ? null : reader.GetString("phone"),
                                BirthDate = reader.IsDBNull(reader.GetOrdinal("b_date")) ? (DateTime?)null : reader.GetDateTime("b_date"),
                                Nationality = reader.IsDBNull(reader.GetOrdinal("nationality")) ? null : reader.GetString("nationality"),
                                Cin = reader.IsDBNull(reader.GetOrdinal("cin")) ? null : reader.GetString("cin"),
                                Address = reader.IsDBNull(reader.GetOrdinal("address")) ? null : reader.GetString("address"),
                                Role = reader.IsDBNull(reader.GetOrdinal("role")) ? null : reader.GetString("role"),
                                JoinDate = reader.IsDBNull(reader.GetOrdinal("j_date")) ? (DateTime?)null : reader.GetDateTime("j_date"),
                                ContractStartDate = reader.IsDBNull(reader.GetOrdinal("contract_start")) ? (DateTime?)null : reader.GetDateTime("contract_start"),
                                ContractEndDate = reader.IsDBNull(reader.GetOrdinal("contract_end")) ? (DateTime?)null : reader.GetDateTime("contract_end"),
                                Salary = reader.IsDBNull(reader.GetOrdinal("salary")) ? 0 : reader.GetInt32("salary"),
                                Module = reader.IsDBNull(reader.GetOrdinal("Module")) ? null : reader.GetString("Module")
                            };
                            empList.Add(teacher);
                        }
                    }
                }
                Dispatcher.Invoke(() =>
                {
                    TeachersList.ItemsSource = empList;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadTeachers()
        {
            await LoadTeachersAsync();
        }
        private async void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text;
            await LoadTeachersAsync(searchText);
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
            await LoadTeachersAsync();
        }
    }
}
