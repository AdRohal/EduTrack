using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EduTrack.Data;
using EduTrack.Models;
using EduTrack.Utilities;
using EduTrack.ViewModel;
using MySql.Data.MySqlClient;

namespace EduTrack.View
{
    /// <summary>Landing page: live counts, newest students, class sizes and shortcuts.</summary>
    public partial class Home : UserControl
    {
        private sealed class ClassSize
        {
            public string Name { get; set; }
            public int Count { get; set; }
            public int Max { get; set; }
        }

        private sealed class ContractRow
        {
            public string Name { get; set; }
            public string Detail { get; set; }
            public string When { get; set; }
        }

        private sealed class GapRow
        {
            public string Name { get; set; }
            public int Count { get; set; }
        }

        private static string Until(DateTime end)
        {
            int days = (int)(end.Date - DateTime.Today).TotalDays;
            if (days < 0) return "ended " + (-days) + (days == -1 ? " day ago" : " days ago");
            if (days == 0) return "today";
            return "in " + days + (days == 1 ? " day" : " days");
        }

        private IList<Student> _recent = new List<Student>();
        private IList<ClassSize> _sizes = new List<ClassSize>(), _majors = new List<ClassSize>();
        private IList<ContractRow> _contracts = new List<ContractRow>();
        private IList<GapRow> _gaps = new List<GapRow>();

        // Height of one row in each list (item height + margins). The lists show as many rows as the card has room for, so nothing scrolls.
        private const double RecentRow = 54, BarRow = 48, ContractRow_ = 50, GapRow_ = 36;

        private static int Fit(FrameworkElement host, double row) => Math.Max(1, (int)(host.ActualHeight / row));

        private void Host_SizeChanged(object sender, SizeChangedEventArgs e) => ApplyCapacity();

        private void ApplyCapacity()
        {
            RecentList.ItemsSource = _recent.Take(Fit(RecentHost, RecentRow)).ToList();
            ClassSizes.ItemsSource = _sizes.Take(Fit(ClassHost, BarRow)).ToList();
            MajorList.ItemsSource = _majors.Take(Fit(MajorHost, BarRow)).ToList();
            ContractList.ItemsSource = _contracts.Take(Fit(ContractHost, ContractRow_)).ToList();
            GapList.ItemsSource = _gaps.Take(Fit(GapHost, GapRow_)).ToList();
        }

        public Home()
        {
            InitializeComponent();
            Loaded += async (s, e) => { GreetingText.Text = Greeting(Window.GetWindow(this)); await LoadAsync(); };
        }

        private static string Greeting(Window host)
        {
            int hour = DateTime.Now.Hour;
            string part = hour < 12 ? "Good morning" : hour < 18 ? "Good afternoon" : "Good evening";
            var user = (host?.DataContext as NavigationVM)?.ConnectedUsername;
            return string.IsNullOrWhiteSpace(user) ? part : part + ", " + user;
        }

        private async Task LoadAsync()
        {
            try
            {
                int students, teachers, employees, classes;
                var recent = new List<Student>();
                var sizes = new List<ClassSize>();
                var majors = new List<ClassSize>();
                var contracts = new List<ContractRow>();
                var gaps = new List<GapRow>();

                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    await connection.OpenAsync();
                    students = await CountAsync(connection, "student_registry");
                    teachers = await CountAsync(connection, "teacher_registry");
                    employees = await CountAsync(connection, "employee_registry");
                    classes = await CountAsync(connection, "classes");

                    using (var command = new MySqlCommand(
                        "SELECT StudentID, p_pic, f_name, l_name, major, j_date FROM student_registry ORDER BY StudentID DESC LIMIT 14", connection))
                    using (var reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            recent.Add(new Student
                            {
                                Id = reader.GetInt32(0),
                                P_Pic = reader.Str("p_pic"),
                                F_Name = reader.Str("f_name"),
                                L_Name = reader.Str("l_name"),
                                Major = reader.Str("major"),
                                J_Date = reader.Date("j_date")
                            });
                        }
                    }

                    using (var command = new MySqlCommand(
                        "SELECT c.ClassName, COUNT(sc.StudentID) AS n FROM classes c LEFT JOIN studentclasses sc ON sc.ClassID = c.ClassID " +
                        "GROUP BY c.ClassID, c.ClassName ORDER BY n DESC, c.ClassName LIMIT 14", connection))
                    using (var reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            sizes.Add(new ClassSize { Name = reader.GetString(0), Count = Convert.ToInt32(reader.GetValue(1)) });
                    }

                    using (var command = new MySqlCommand(
                        "SELECT major, COUNT(*) AS n FROM student_registry WHERE major IS NOT NULL AND major <> '' " +
                        "GROUP BY major ORDER BY n DESC, major LIMIT 14", connection))
                    using (var reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            majors.Add(new ClassSize { Name = reader.GetString(0), Count = Convert.ToInt32(reader.GetValue(1)) });
                    }

                    using (var command = new MySqlCommand(
                        "SELECT f_name, l_name, role, contract_end, 'Teacher' FROM teacher_registry " +
                        "WHERE contract_end IS NOT NULL AND contract_end <= DATE_ADD(CURDATE(), INTERVAL 90 DAY) " +
                        "UNION ALL " +
                        "SELECT f_name, l_name, role, contract_end, 'Employee' FROM employee_registry " +
                        "WHERE contract_end IS NOT NULL AND contract_end <= DATE_ADD(CURDATE(), INTERVAL 90 DAY) " +
                        "ORDER BY contract_end LIMIT 14", connection))
                    using (var reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            DateTime end = reader.GetDateTime(3);
                            string role = reader.IsDBNull(2) ? "" : reader.GetString(2);
                            contracts.Add(new ContractRow
                            {
                                Name = (reader.Str("f_name") + " " + reader.Str("l_name")).Trim(),
                                Detail = string.IsNullOrWhiteSpace(role) ? reader.GetString(4) : reader.GetString(4) + " · " + role,
                                When = Until(end)
                            });
                        }
                    }

                    foreach (var (label, table, idCols) in new[]
                    {
                        ("Students", "student_registry", "(p_pic IS NULL OR p_pic = '' OR cin_front_copy IS NULL OR cin_front_copy = '' OR cin_back_copy IS NULL OR cin_back_copy = '')"),
                        ("Teachers", "teacher_registry", "(p_pic IS NULL OR p_pic = '' OR cin_front_copy IS NULL OR cin_front_copy = '' OR cin_back_copy IS NULL OR cin_back_copy = '')"),
                        ("Employees", "employee_registry", "(p_pic IS NULL OR p_pic = '' OR cin_front_copy IS NULL OR cin_front_copy = '' OR cin_back_copy IS NULL OR cin_back_copy = '')")
                    })
                    {
                        using (var command = new MySqlCommand("SELECT COUNT(*) FROM " + table + " WHERE " + idCols, connection))
                        {
                            int n = Convert.ToInt32(await command.ExecuteScalarAsync());
                            if (n > 0) gaps.Add(new GapRow { Name = label, Count = n });
                        }
                    }
                }

                int majorMax = Math.Max(1, majors.Count == 0 ? 1 : majors.Max(c => c.Count));
                foreach (var m in majors) m.Max = majorMax;

                int max = Math.Max(1, sizes.Count == 0 ? 1 : sizes.Max(c => c.Count));
                foreach (var size in sizes) size.Max = max;

                StudentCount.Text = students.ToString();
                TeacherCount.Text = teachers.ToString();
                EmployeeCount.Text = employees.ToString();
                ClassCount.Text = classes.ToString();

                bool empty = students + teachers + employees + classes == 0;
                WelcomeCard.Visibility = empty ? Visibility.Visible : Visibility.Collapsed;
                ActivityGrid.Visibility = empty ? Visibility.Collapsed : Visibility.Visible;
                InsightsGrid.Visibility = ActivityGrid.Visibility;
                _majors = majors;
                MajorEmpty.Visibility = majors.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
                _contracts = contracts;
                ContractsEmpty.Visibility = contracts.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
                _gaps = gaps;
                GapsEmpty.Visibility = gaps.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
                SubText.Text = empty ? "Welcome. Let's set up your school." : "Here is what the register holds right now.";

                _recent = recent;
                RecentEmpty.Visibility = recent.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
                _sizes = sizes;
                ClassSizesEmpty.Visibility = sizes.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
                ApplyCapacity();
            }
            catch (Exception ex)
            {
                SubText.Text = "The register could not be read: " + ex.Message;
            }
        }

        private static async Task<int> CountAsync(MySqlConnection connection, string table)
        {
            using (var command = new MySqlCommand("SELECT COUNT(*) FROM " + table, connection))
                return Convert.ToInt32(await command.ExecuteScalarAsync());
        }

        private MainWindow Main => Window.GetWindow(this) as MainWindow;

        private void Students_Click(object sender, RoutedEventArgs e) => Main?.GoTo("Students");

        private void Teachers_Click(object sender, RoutedEventArgs e) => Main?.GoTo("Teachers");

        private void Employees_Click(object sender, RoutedEventArgs e) => Main?.GoTo("Employees");

        private void Classes_Click(object sender, RoutedEventArgs e) => Main?.GoTo("Classes");

        private void NewStudent_Click(object sender, RoutedEventArgs e) => Main?.GoTo("Students", 0);

        private void NewTeacher_Click(object sender, RoutedEventArgs e) => Main?.GoTo("Teachers", 0);

        private void NewClass_Click(object sender, RoutedEventArgs e) => Main?.GoTo("Classes", 0);
    }
}
