using EduTrack.Models;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EduTrack.View
{
    /// <summary>
    /// Interaction logic for DetailedEmployeeInfoView.xaml
    /// </summary>
    public partial class DetailedEmployeeInfoView : Window
    {
        private const string DatabaseServer = "127.0.0.1";
        private const string DatabaseName = "datebase";
        private const string DatabaseUser = "root";
        private const string DatabasePassword = "";

        private readonly MySqlConnection connection;

        private string uploadedFileName;
        private string uploadedFileName1;
        private string uploadedFileName2;
        private string uploadedFileName3;
        private string uploadedFileName4;
        private string uploadedFileName5;
        private readonly int  employeeId;

        public DetailedEmployeeInfoView(Employee selectedEmployee)
        {
            InitializeComponent();

            this.Width = 900;
            this.Height = 679;

            this.MaxWidth = 1174;
            this.MaxHeight = 679;

            this.SizeToContent = SizeToContent.Manual;

            string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";
            connection = new MySqlConnection(connectionString);

            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1)
            };
            ReminderMessage.BeginAnimation(TextBlock.OpacityProperty, fadeInAnimation);

            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();

            contract_start.SelectedDateChanged += contract_start_SelectedDateChanged;
            contract_end.SelectedDateChanged += contract_end_SelectedDateChanged;

            Date_appointment.SelectedDateChanged += Date_appointment_SelectedDateChanged;

            employeeId = selectedEmployee.Id;

            LoadEmployeeData(employeeId);
        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(1)
            };
            ReminderMessage.BeginAnimation(TextBlock.OpacityProperty, fadeOutAnimation);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateEmployee();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this employee?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand($"DELETE FROM employee_registry WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("@Id", employeeId);

                command.ExecuteNonQuery();

                connection.Close();

                MessageBox.Show("Employee has been deleted successfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }



        private void UpdateEmployee()
        {
            try
            {
                string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"UPDATE employee_registry SET 
                                  p_pic = @p_pic, f_name = @f_name, m_name = @m_name, l_name = @l_name, gender = @gender, 
                                  email = @email, phone = @phone, b_date = @b_date, nationality = @nationality,
                                  cin = @cin, address = @address, cin_front_copy = @cin_front_copy, 
                                  cin_back_copy = @cin_back_copy, organization = @organization, role = @role, 
                                  j_date = @j_date, contract_start = @contract_start, contract_end = @contract_end, 
                                  contract_copy = @contract_copy, resume_copy = @resume_copy, high_degree = @high_degree, 
                                  year_graduation = @year_graduation, university = @university, specialization = @specialization,
                                  diploma_copy = @diploma_copy, salary = @salary
                                  WHERE Id = @Id";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@p_pic", uploadedFileName);
                        command.Parameters.AddWithValue("@f_name", txtFirstName.Text);
                        command.Parameters.AddWithValue("@m_name", txtMiddleName.Text);
                        command.Parameters.AddWithValue("@l_name", txtLastName.Text);
                        command.Parameters.AddWithValue("@gender", comboBoxGender.SelectedItem == null ? "" : comboBoxGender.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@email", txtEmail.Text);
                        command.Parameters.AddWithValue("@phone", txtPhone.Text);
                        command.Parameters.AddWithValue("@b_date", datePickerBirthDate.SelectedDate.GetValueOrDefault());
                        command.Parameters.AddWithValue("@nationality", comboBoxNationality.SelectedItem == null ? "" : comboBoxNationality.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@cin", txtCin.Text);
                        command.Parameters.AddWithValue("@address", txtAddress.Text);
                        command.Parameters.AddWithValue("@cin_front_copy", uploadedFileName1);
                        command.Parameters.AddWithValue("@cin_back_copy", uploadedFileName2);
                        command.Parameters.AddWithValue("@organization", txtOrganization.Text);
                        command.Parameters.AddWithValue("@role", txtRole.Text);
                        command.Parameters.AddWithValue("@j_date", Date_appointment.SelectedDate.GetValueOrDefault());
                        command.Parameters.AddWithValue("@contract_start", contract_start.SelectedDate.GetValueOrDefault());
                        command.Parameters.AddWithValue("@contract_end", contract_end.SelectedDate.GetValueOrDefault());
                        command.Parameters.AddWithValue("@contract_copy", uploadedFileName3);
                        command.Parameters.AddWithValue("@resume_copy", uploadedFileName4);
                        command.Parameters.AddWithValue("@high_degree", txtHighestDegree.Text);
                        command.Parameters.AddWithValue("@year_graduation", string.IsNullOrEmpty(txtYearGraduation.Text) ? 0 : int.Parse(txtYearGraduation.Text));
                        command.Parameters.AddWithValue("@university", txtUniversity.Text);
                        command.Parameters.AddWithValue("@specialization", txtSpecialization.Text);
                        command.Parameters.AddWithValue("@diploma_copy", uploadedFileName5);
                        command.Parameters.AddWithValue("@salary", string.IsNullOrEmpty(txtSalary.Text) ? 0 : decimal.Parse(txtSalary.Text));
                        command.Parameters.AddWithValue("@Id", employeeId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Employee information updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to update employee information!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating employee information: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == true)
            {
                uploadedFileName = openFileDialog.FileName;

                uploadedImage.Source = new BitmapImage(new Uri(uploadedFileName));
            }
        }

        private void UploadDocument_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                uploadedFileName1 = openFileDialog.FileName;

                FileNameLabel.Content = System.IO.Path.GetFileName(uploadedFileName1);

                ViewButton.Visibility = Visibility.Visible;
            }
        }

        private void UploadDocument_Click2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                uploadedFileName2 = openFileDialog.FileName;

                FileNameLabel2.Content = System.IO.Path.GetFileName(uploadedFileName2);

                ViewButton2.Visibility = Visibility.Visible;
            }
        }

        private void UploadDocument_Click3(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                uploadedFileName3 = openFileDialog.FileName;

                FileNameLabel3.Content = System.IO.Path.GetFileName(uploadedFileName3);

                ViewButton3.Visibility = Visibility.Visible;
            }
        }

        private void UploadDocument_Click4(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                uploadedFileName4 = openFileDialog.FileName;

                FileNameLabel4.Content = System.IO.Path.GetFileName(uploadedFileName4);

                ViewButton4.Visibility = Visibility.Visible;
            }
        }

        private void UploadDocument_Click5(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                uploadedFileName5 = openFileDialog.FileName;

                FileNameLabel5.Content = System.IO.Path.GetFileName(uploadedFileName5);

                ViewButton5.Visibility = Visibility.Visible;
            }
        }

        private void ViewDocument_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(uploadedFileName))
            {
                DocumentViewerWindow documentViewer = new DocumentViewerWindow(uploadedFileName1);
                documentViewer.ShowDialog();
            }
        }

        private void ViewDocument_Click2(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(uploadedFileName2))
            {
                DocumentViewerWindow documentViewer = new DocumentViewerWindow(uploadedFileName2);
                documentViewer.ShowDialog();
            }
        }

        private void ViewDocument_Click3(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(uploadedFileName3))
            {
                DocumentViewerWindow documentViewer = new DocumentViewerWindow(uploadedFileName3);
                documentViewer.ShowDialog();
            }
        }

        private void ViewDocument_Click4(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(uploadedFileName4))
            {
                DocumentViewerWindow documentViewer = new DocumentViewerWindow(uploadedFileName4);
                documentViewer.ShowDialog();
            }
        }

        private void ViewDocument_Click5(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(uploadedFileName5))
            {
                DocumentViewerWindow documentViewer = new DocumentViewerWindow(uploadedFileName5);
                documentViewer.ShowDialog();
            }
        }



        private void contract_start_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculateContractYear();
        }

        private void contract_end_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculateContractYear();
        }

        private void CalculateContractYear()
        {
            if (contract_start.SelectedDate != null && contract_end.SelectedDate != null)
            {
                TimeSpan timeSpan = contract_end.SelectedDate.Value - contract_start.SelectedDate.Value;
                int totalYears = (int)(timeSpan.TotalDays / 365.25);
                int totalMonths = (int)((timeSpan.TotalDays % 365.25) / 30.4375);

                string yearsString = totalYears == 1 ? "year" : "years";
                string monthsString = totalMonths == 1 ? "month" : "months";

                if (totalYears == 0)
                {
                    contract_years.Text = $"{totalMonths} {monthsString}";
                }
                else if (totalMonths == 0)
                {
                    contract_years.Text = $"{totalYears} {yearsString}";
                }
                else
                {
                    contract_years.Text = $"{totalYears} {yearsString} {totalMonths} {monthsString}";
                }
            }
            else
            {
                contract_years.Text = "";
            }
        }

        private void CalculateExperience()
        {
            if (Date_appointment.SelectedDate != null)
            {
                DateTime appointmentDate = Date_appointment.SelectedDate.Value;
                DateTime currentDate = DateTime.Today;

                int totalYears = currentDate.Year - appointmentDate.Year;
                int totalMonths = currentDate.Month - appointmentDate.Month;

                if (totalMonths < 0)
                {
                    totalYears--;
                    totalMonths += 12;
                }

                if (currentDate.Day < appointmentDate.Day)
                {
                    totalMonths--;
                }

                if (totalMonths < 0)
                {
                    totalMonths += 12;
                }

                if (totalYears == 0)
                {
                    if (totalMonths == 1)
                        Exp_years.Text = $"{totalMonths} month";
                    else
                        Exp_years.Text = $"{totalMonths} months";
                }
                else
                {
                    if (totalMonths == 0)
                    {
                        if (totalYears == 1)
                            Exp_years.Text = $"{totalYears} year";
                        else
                            Exp_years.Text = $"{totalYears} years";
                    }
                    else
                    {
                        if (totalYears == 1)
                            Exp_years.Text = $"{totalYears} year {totalMonths} months";
                        else
                            Exp_years.Text = $"{totalYears} years {totalMonths} months";
                    }
                }
            }
            else
            {
                Exp_years.Text = "";
            }
        }

        private void Date_appointment_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculateExperience();
        }

        private void LoadEmployeeData(int employeeId)
        {
            try
            {
                string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"SELECT p_pic, f_name, m_name, l_name, gender, email, phone, b_date, nationality, cin, address, 
           cin_front_copy, cin_back_copy, organization, role, j_date, contract_start, contract_end, 
           contract_copy, resume_copy, high_degree, year_graduation, university, specialization, diploma_copy, salary
           FROM employee_registry WHERE Id = @Id";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", employeeId);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                uploadedFileName = reader["p_pic"].ToString();
                                uploadedImage.Source = new BitmapImage(new Uri(uploadedFileName));


                                txtFirstName.Text = reader["f_name"].ToString();
                                txtMiddleName.Text = reader["m_name"].ToString();
                                txtLastName.Text = reader["l_name"].ToString();
                                comboBoxGender.SelectedItem = reader["gender"].ToString();
                                txtEmail.Text = reader["email"].ToString();
                                txtPhone.Text = reader["phone"].ToString();
                                datePickerBirthDate.SelectedDate = Convert.ToDateTime(reader["b_date"]);
                                comboBoxNationality.SelectedItem = reader["nationality"].ToString();
                                txtCin.Text = reader["cin"].ToString();
                                txtAddress.Text = reader["address"].ToString();
                                uploadedFileName1 = reader["cin_front_copy"].ToString();
                                uploadedFileName2 = reader["cin_back_copy"].ToString();
                                txtOrganization.Text = reader["organization"].ToString();
                                txtRole.Text = reader["role"].ToString();
                                Date_appointment.SelectedDate = Convert.ToDateTime(reader["j_date"]);
                                contract_start.SelectedDate = Convert.ToDateTime(reader["contract_start"]);
                                contract_end.SelectedDate = Convert.ToDateTime(reader["contract_end"]);
                                uploadedFileName3 = reader["contract_copy"].ToString();
                                uploadedFileName4 = reader["resume_copy"].ToString();
                                txtHighestDegree.Text = reader["high_degree"].ToString();
                                txtYearGraduation.Text = reader["year_graduation"].ToString();
                                txtUniversity.Text = reader["university"].ToString();
                                txtSpecialization.Text = reader["specialization"].ToString();
                                uploadedFileName5 = reader["diploma_copy"].ToString();
                                txtSalary.Text = reader["salary"].ToString();
                                FileNameLabel.Content = System.IO.Path.GetFileName(uploadedFileName1);
                                FileNameLabel2.Content = System.IO.Path.GetFileName(uploadedFileName2);
                                FileNameLabel3.Content = System.IO.Path.GetFileName(uploadedFileName3);
                                FileNameLabel4.Content = System.IO.Path.GetFileName(uploadedFileName4);
                                FileNameLabel5.Content = System.IO.Path.GetFileName(uploadedFileName5);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employee data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
