using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using EduTrack.Data;

namespace EduTrack.View
{
    /// <summary>
    /// Interaction logic for NewEmployee.xaml
    /// </summary>
    public partial class NewEmployee : UserControl
    {
        private string uploadedFileName;
        private string uploadedFileName1;
        private string uploadedFileName2;
        private string uploadedFileName3;
        private string uploadedFileName4;
        private string uploadedFileName5;

        public NewEmployee()
        {
            InitializeComponent();

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

            CreateDirectoryStructure();

            contract_start.SelectedDateChanged += contract_start_SelectedDateChanged;
            contract_end.SelectedDateChanged += contract_end_SelectedDateChanged;

            Date_appointment.SelectedDateChanged += Date_appointment_SelectedDateChanged;
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
            try
            {
                using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
                {
                    connection.Open();

                    string sql = @"INSERT INTO employee_registry 
           (p_pic, f_name, m_name, l_name, gender, email, phone, b_date, nationality, 
            cin, address, cin_front_copy, cin_back_copy,organization, role, j_date, contract_start, 
            contract_end, contract_copy, resume_copy, high_degree, year_graduation, 
            university, specialization, diploma_copy, salary)
           VALUES 
           (@p_pic, @f_name, @m_name , @l_name, @gender, @email, @phone, @b_date, @nationality, 
            @cin, @address, @cin_front_copy, @cin_back_copy, @organization, @role, @j_date, @contract_start, 
            @contract_end, @contract_copy, @resume_copy, @high_degree, @year_graduation, 
            @university, @specialization, @diploma_copy, @salary)";

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
                        command.Parameters.AddWithValue("@salary", string.IsNullOrEmpty(txtSalary.Text) ? 0 : int.Parse(txtSalary.Text));

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Employee information saved successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to save employee information!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving employee information: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (!string.IsNullOrEmpty(uploadedFileName1))
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

        private void CreateDirectoryStructure()
        {
            try
            {
                string baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                string[] directories = { "EduTrack", "EduTrack\\Employees", "EduTrack\\Students", "EduTrack\\Classes", "EduTrack\\Teachers" };

                foreach (string directory in directories)
                {
                    string directoryPath = Path.Combine(baseDirectory, directory);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating directory structure: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}