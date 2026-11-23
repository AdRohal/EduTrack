using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using EduTrack.Data;
using EduTrack.Models;
using EduTrack.Utilities;
using MySql.Data.MySqlClient;

namespace EduTrack.View
{
    public partial class ShowEmployees : UserControl
    {
        private readonly DispatcherTimer _searchDelay = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(250) };

        public ShowEmployees()
        {
            InitializeComponent();
            _searchDelay.Tick += async (s, e) => { _searchDelay.Stop(); await LoadEmployeesAsync(SearchBox.Text.Trim()); };
            Loaded += async (s, e) => { await LoadEmployeesAsync(); SearchBox.Focus(); };
        }

        private void OpenSelected()
        {
            if (!(EmployeesList.SelectedItem is Employee selected)) return;

            var host = HubHelper.FindDetailHost(this);
            if (host == null) return;
            var detailedView = new DetailedEmployeeInfoView(selected);
            detailedView.Closed += (s, e) => host.CloseDetail();
            host.ShowDetail(detailedView);
        }

        private void Button_Click1(object sender, RoutedEventArgs e) => OpenSelected();

        private void EmployeesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is DependencyObject source && ItemsControl.ContainerFromElement(EmployeesList, source) is DataGridRow)
                OpenSelected();
        }

        private void EmployeesList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { OpenSelected(); e.Handled = true; }
        }

        private async Task LoadEmployeesAsync(string searchText = null)
        {
            string query = "SELECT id, p_pic, f_name, m_name, l_name, gender, email, phone, b_date, nationality, cin, address, organization, role, j_date, contract_start, contract_end, high_degree, year_graduation, university, specialization, salary FROM employee_registry";
            bool searching = !string.IsNullOrEmpty(searchText);
            if (searching)
                query += " WHERE f_name LIKE @q OR m_name LIKE @q OR l_name LIKE @q OR CONCAT(f_name, ' ', l_name) LIKE @q";
            query += " ORDER BY l_name, f_name";

            var employees = new ObservableCollection<Employee>();
            try
            {
                using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        if (searching) command.Parameters.AddWithValue("@q", "%" + searchText + "%");
                        using (var reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                employees.Add(new Employee
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    P_Pic = reader.Str("p_pic"),
                                    FirstName = reader.Str("f_name"),
                                    MiddleName = reader.Str("m_name"),
                                    LastName = reader.Str("l_name"),
                                    Gender = reader.Str("gender"),
                                    Email = reader.Str("email"),
                                    Phone = reader.Str("phone"),
                                    BirthDate = reader.Date("b_date"),
                                    Nationality = reader.Str("nationality"),
                                    Cin = reader.Str("cin"),
                                    Address = reader.Str("address"),
                                    Organization = reader.Str("organization"),
                                    Role = reader.Str("role"),
                                    HighestDegree = reader.Str("high_degree"),
                                    University = reader.Str("university"),
                                    JoinDate = reader.Date("j_date"),
                                    ContractStartDate = reader.Date("contract_start"),
                                    ContractEndDate = reader.Date("contract_end"),
                                    YearGraduation = reader.Int("year_graduation"),
                                    Salary = reader.Int("salary") ?? 0
                                });
                            }
                        }
                    }
                }
                EmployeesList.ItemsSource = employees;
                ShowState(employees.Count, searching ? searchText : null);
            }
            catch (Exception ex)
            {
                ShowState(0, null);
                MessageBox.Show("The employee list could not be loaded. Check that the database is running.\n" + ex.Message,
                    "Can't load employees", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowState(int count, string search)
        {
            CountText.Text = search == null ? (count == 1 ? "1 employee" : count + " employees")
                                            : (count == 1 ? "1 match" : count + " matches");
            bool empty = count == 0;
            EmptyState.Visibility = empty ? Visibility.Visible : Visibility.Collapsed;
            EmployeesList.Visibility = empty ? Visibility.Collapsed : Visibility.Visible;
            if (!empty) return;

            if (search != null)
            {
                EmptyTitle.Text = "No employee matches “" + search + "”";
                EmptyHint.Text = "Check the spelling, or try just a first or last name.";
                EmptyAction.Visibility = Visibility.Collapsed;
            }
            else
            {
                EmptyTitle.Text = "No employees yet";
                EmptyHint.Text = "Employees you add will show up here, ready to search and open.";
                EmptyAction.Visibility = Visibility.Visible;
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _searchDelay.Stop();
            _searchDelay.Start();
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e) => await LoadEmployeesAsync(SearchBox.Text.Trim());

        private void EmptyAction_Click(object sender, RoutedEventArgs e) => HubHelper.Find(this)?.SelectTab(0);
    }
}
