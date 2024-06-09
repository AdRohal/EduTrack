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
    public partial class ShowEmployees : UserControl
    {
        private const string DatabaseServer = "127.0.0.1";
        private const string DatabaseName = "datebase";
        private const string DatabaseUser = "root";
        private const string DatabasePassword = "";

        public ShowEmployees()
        {
            InitializeComponent();
            LoadEmployees();
            this.DataContext = this;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = EmployeesList.SelectedItem as Employee;

            if (selectedEmployee == null)
            {
                MessageBox.Show("Please select an employee first.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            DetailedEmployeeInfoView detailedView = new DetailedEmployeeInfoView(selectedEmployee)
            {
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Title = "Employee Details"
            };
            detailedView.ShowDialog();
        }

        private async Task LoadEmployeesAsync(string searchText = null)
        {
            string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";
            string query = "SELECT id, p_pic, f_name, m_name, l_name, gender, email, phone, b_date, nationality, cin, address, organization, role, j_date, contract_start, contract_end, high_degree, year_graduation, university, specialization, salary FROM employee_registry";
            if (!string.IsNullOrEmpty(searchText))
            {
                query += $" WHERE f_name LIKE '%{searchText}%' OR m_name LIKE '%{searchText}%' OR l_name LIKE '%{searchText}%'";
            }

            ObservableCollection<Employee> empList = new ObservableCollection<Employee>();

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
                            Employee employee = new Employee
                            {
                                Id = reader.GetInt32("id"),
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
                                Organization = reader.IsDBNull(reader.GetOrdinal("organization")) ? null : reader.GetString("organization"),
                                Role = reader.IsDBNull(reader.GetOrdinal("role")) ? null : reader.GetString("role"),
                                HighestDegree = reader.IsDBNull(reader.GetOrdinal("high_degree")) ? null : reader.GetString("high_degree"),
                                University = reader.IsDBNull(reader.GetOrdinal("university")) ? null : reader.GetString("university"),
                                JoinDate = reader.IsDBNull(reader.GetOrdinal("j_date")) ? (DateTime?)null : reader.GetDateTime("j_date"),
                                ContractStartDate = reader.IsDBNull(reader.GetOrdinal("contract_start")) ? (DateTime?)null : reader.GetDateTime("contract_start"),
                                ContractEndDate = reader.IsDBNull(reader.GetOrdinal("contract_end")) ? (DateTime?)null : reader.GetDateTime("contract_end"),
                                YearGraduation = reader.IsDBNull(reader.GetOrdinal("year_graduation")) ? (int?)null : reader.GetInt32("year_graduation"),
                                Salary = reader.IsDBNull(reader.GetOrdinal("salary")) ? 0 : reader.GetInt32("salary")
                            };
                            empList.Add(employee);
                        }
                    }
                }

                Dispatcher.Invoke(() =>
                {
                    EmployeesList.ItemsSource = empList;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadEmployees()
        {
            await LoadEmployeesAsync();
        }
        private async void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text;
            await LoadEmployeesAsync(searchText);
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
            await LoadEmployeesAsync();
        }

    }
}
