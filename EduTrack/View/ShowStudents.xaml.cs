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
    public partial class ShowStudents : UserControl
    {
        private readonly DispatcherTimer _searchDelay = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(250) };

        public ShowStudents()
        {
            InitializeComponent();
            _searchDelay.Tick += async (s, e) => { _searchDelay.Stop(); await LoadStudentsAsync(SearchBox.Text.Trim()); };
            Loaded += async (s, e) => { await LoadStudentsAsync(); SearchBox.Focus(); };
        }

        private void OpenSelected()
        {
            if (!(StudentsList.SelectedItem is Student selected)) return;

            var host = HubHelper.FindDetailHost(this);
            if (host == null) return;
            var detailedView = new DetailedStudentInfoView(selected);
            detailedView.Closed += (s, e) => host.CloseDetail();
            host.ShowDetail(detailedView);
        }

        private void Button_Click1(object sender, RoutedEventArgs e) => OpenSelected();

        private void StudentsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is DependencyObject source && ItemsControl.ContainerFromElement(StudentsList, source) is DataGridRow)
                OpenSelected();
        }

        private void StudentsList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { OpenSelected(); e.Handled = true; }
        }

        private async Task LoadStudentsAsync(string searchText = null)
        {
            string query = "SELECT StudentID, p_pic, f_name, m_name, l_name, gender, email, phone, b_date, nationality, cin, j_date, major FROM student_registry";
            bool searching = !string.IsNullOrEmpty(searchText);
            if (searching)
                query += " WHERE f_name LIKE @q OR m_name LIKE @q OR l_name LIKE @q OR CONCAT(f_name, ' ', l_name) LIKE @q";
            query += " ORDER BY l_name, f_name";

            var students = new ObservableCollection<Student>();
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
                                students.Add(new Student
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("StudentID")),
                                    P_Pic = reader.Str("p_pic"),
                                    F_Name = reader.Str("f_name"),
                                    M_Name = reader.Str("m_name"),
                                    L_Name = reader.Str("l_name"),
                                    Gender = reader.Str("gender"),
                                    Email = reader.Str("email"),
                                    Phone = reader.Str("phone"),
                                    B_Date = reader.Date("b_date"),
                                    Nationality = reader.Str("nationality"),
                                    CIN = reader.Str("cin"),
                                    Major = reader.Str("major"),
                                    J_Date = reader.Date("j_date")
                                });
                            }
                        }
                    }
                }
                StudentsList.ItemsSource = students;
                ShowState(students.Count, searching ? searchText : null);
            }
            catch (Exception ex)
            {
                ShowState(0, null);
                MessageBox.Show("The student list could not be loaded. Check that the database is running.\n" + ex.Message,
                    "Can't load students", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowState(int count, string search)
        {
            CountText.Text = Loc.T(search == null ? (count == 1 ? "1 student" : count + " students")
                                            : (count == 1 ? "1 match" : count + " matches"));
            bool empty = count == 0;
            EmptyState.Visibility = empty ? Visibility.Visible : Visibility.Collapsed;
            StudentsList.Visibility = empty ? Visibility.Collapsed : Visibility.Visible;
            if (!empty) return;

            if (search != null)
            {
                EmptyTitle.Text = Loc.T("No student matches “" + search + "”");
                EmptyHint.Text = Loc.T("Check the spelling, or try just a first or last name.");
                EmptyAction.Visibility = Visibility.Collapsed;
            }
            else
            {
                EmptyTitle.Text = Loc.T("No students yet");
                EmptyHint.Text = Loc.T("Students you add will show up here, ready to search and open.");
                EmptyAction.Visibility = Visibility.Visible;
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _searchDelay.Stop();
            _searchDelay.Start();
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e) => await LoadStudentsAsync(SearchBox.Text.Trim());

        private void EmptyAction_Click(object sender, RoutedEventArgs e) => HubHelper.Find(this)?.SelectTab(0);
    }
}
