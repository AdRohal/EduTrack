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
using EduTrack.Data;
using EduTrack.Utilities;

namespace EduTrack.View
{
    /// <summary>
    /// Interaction logic for DetailedTeacherInfoView.xaml
    /// </summary>
    public partial class DetailedTeacherInfoView : UserControl
    {
        /// <summary>Raised when the user goes back to the list, or after the record is deleted.</summary>
        public event EventHandler Closed;

        private void OnClosed() => Closed?.Invoke(this, EventArgs.Empty);

        private void Back_Click(object sender, RoutedEventArgs e) => OnClosed();

        private string uploadedFileName;
        private string uploadedFileName1;
        private string uploadedFileName2;
        private string uploadedFileName3;
        private string uploadedFileName4;
        private string uploadedFileName5;
        private readonly Teacher selectedTeacher;
        private readonly int teacherId;

        public DetailedTeacherInfoView(Teacher selectedTeacher)
        {
            InitializeComponent();





            contract_start.SelectedDateChanged += contract_start_SelectedDateChanged;
            contract_end.SelectedDateChanged += contract_end_SelectedDateChanged;

            Date_appointment.SelectedDateChanged += Date_appointment_SelectedDateChanged;

            teacherId = selectedTeacher.Id;

            LoadTeacherData(teacherId);
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!FormValidation.Validate(this, txtFirstName, txtLastName, txtEmail)) return;

            UpdateTeacher();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this Teacher?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("DELETE FROM teacherclasses WHERE TeacherID = @TeacherID; DELETE FROM teacher_registry WHERE TeacherID = @TeacherID", connection);

                    command.Parameters.AddWithValue("@TeacherID", teacherId);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Teacher has been deleted successfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    OnClosed();
                }
            }
        }



        private void UpdateTeacher()
        {
            try
            {
                using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
                {
                    connection.Open();

                    string sql = @"UPDATE teacher_registry SET 
                                  p_pic = @p_pic, f_name = @f_name, m_name = @m_name, l_name = @l_name, gender = @gender, 
                                  email = @email, phone = @phone, b_date = @b_date, nationality = @nationality,
                                  cin = @cin, address = @address, cin_front_copy = @cin_front_copy, 
                                  cin_back_copy = @cin_back_copy, organization = @organization, role = @role, 
                                  j_date = @j_date, contract_start = @contract_start, contract_end = @contract_end, 
                                  contract_copy = @contract_copy, resume_copy = @resume_copy, high_degree = @high_degree, 
                                  year_graduation = @year_graduation, university = @university, specialization = @specialization,
                                  diploma_copy = @diploma_copy, salary = @salary, module = @module
                                  WHERE TeacherID = @TeacherID";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@p_pic", uploadedFileName);
                        command.Parameters.AddWithValue("@f_name", txtFirstName.Text);
                        command.Parameters.AddWithValue("@m_name", txtMiddleName.Text);
                        command.Parameters.AddWithValue("@l_name", txtLastName.Text);
                        command.Parameters.AddWithValue("@gender", comboBoxGender.SelectedItem == null ? "" : comboBoxGender.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@email", txtEmail.Text);
                        command.Parameters.AddWithValue("@phone", txtPhone.Text);
                        command.Parameters.AddWithValue("@b_date", (object)datePickerBirthDate.SelectedDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@nationality", comboBoxNationality.SelectedItem == null ? "" : comboBoxNationality.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@cin", txtCin.Text);
                        command.Parameters.AddWithValue("@address", txtAddress.Text);
                        command.Parameters.AddWithValue("@cin_front_copy", uploadedFileName1);
                        command.Parameters.AddWithValue("@cin_back_copy", uploadedFileName2);
                        command.Parameters.AddWithValue("@organization", txtOrganization.Text);
                        command.Parameters.AddWithValue("@role", txtRole.Text);
                        command.Parameters.AddWithValue("@j_date", (object)Date_appointment.SelectedDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@contract_start", (object)contract_start.SelectedDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@contract_end", (object)contract_end.SelectedDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@contract_copy", uploadedFileName3);
                        command.Parameters.AddWithValue("@resume_copy", uploadedFileName4);
                        command.Parameters.AddWithValue("@high_degree", txtHighestDegree.Text);
                        command.Parameters.AddWithValue("@year_graduation", string.IsNullOrEmpty(txtYearGraduation.Text) ? 0 : int.Parse(txtYearGraduation.Text));
                        command.Parameters.AddWithValue("@university", txtUniversity.Text);
                        command.Parameters.AddWithValue("@specialization", txtSpecialization.Text);
                        command.Parameters.AddWithValue("@diploma_copy", uploadedFileName5);
                        command.Parameters.AddWithValue("@salary", string.IsNullOrEmpty(txtSalary.Text) ? 0 : decimal.Parse(txtSalary.Text));
                        command.Parameters.AddWithValue("@module", txtModule.Text);
                        command.Parameters.AddWithValue("@TeacherID", teacherId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Teacher information updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to update teacher information!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating teacher information: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == true)
            {
                uploadedFileName = openFileDialog.FileName;

                FormHelper.ShowPhoto(uploadedImage, uploadedFileName);
            }
        }

        private void UploadDocument_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                uploadedFileName1 = openFileDialog.FileName;

                FormHelper.ShowDoc(FileNameLabel, ViewButton, uploadedFileName1);

                ViewButton.Visibility = Visibility.Visible;
            }
        }

        private void UploadDocument_Click2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                uploadedFileName2 = openFileDialog.FileName;

                FormHelper.ShowDoc(FileNameLabel2, ViewButton2, uploadedFileName2);

                ViewButton2.Visibility = Visibility.Visible;
            }
        }

        private void UploadDocument_Click3(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                // Get the selected file name
                uploadedFileName3 = openFileDialog.FileName;

                FormHelper.ShowDoc(FileNameLabel3, ViewButton3, uploadedFileName3);

                ViewButton3.Visibility = Visibility.Visible;
            }
        }

        private void UploadDocument_Click4(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                uploadedFileName4 = openFileDialog.FileName;

                FormHelper.ShowDoc(FileNameLabel4, ViewButton4, uploadedFileName4);

                ViewButton4.Visibility = Visibility.Visible;
            }
        }

        private void UploadDocument_Click5(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                uploadedFileName5 = openFileDialog.FileName;

                FormHelper.ShowDoc(FileNameLabel5, ViewButton5, uploadedFileName5);

                ViewButton5.Visibility = Visibility.Visible;
            }
        }

        private void ViewDocument_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(uploadedFileName1))
            {
                DocumentViewerOverlay.Show(uploadedFileName1);
            }
        }

        private void ViewDocument_Click2(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(uploadedFileName2))
            {
                DocumentViewerOverlay.Show(uploadedFileName2);
            }
        }

        private void ViewDocument_Click3(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(uploadedFileName3))
            {
                DocumentViewerOverlay.Show(uploadedFileName3);
            }
        }

        private void ViewDocument_Click4(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(uploadedFileName4))
            {
                DocumentViewerOverlay.Show(uploadedFileName4);
            }
        }

        private void ViewDocument_Click5(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(uploadedFileName5))
            {
                DocumentViewerOverlay.Show(uploadedFileName5);
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

        public async Task LoadTeacherData(int teacherId)
        {
            try
            {
                using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
                {
                    connection.Open();

                    string sql = @"SELECT p_pic, f_name, m_name, l_name, gender, email, phone, b_date, nationality, cin, address, 
           cin_front_copy, cin_back_copy, organization, role, j_date, contract_start, contract_end, 
           contract_copy, resume_copy, high_degree, year_graduation, university, specialization, diploma_copy, salary, module
           FROM teacher_registry WHERE TeacherID = @TeacherID";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TeacherID", teacherId);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                uploadedFileName = reader["p_pic"].ToString();
                                FormHelper.ShowPhoto(uploadedImage, uploadedFileName);
                                txtFirstName.Text = reader["f_name"].ToString();
                                txtMiddleName.Text = reader["m_name"].ToString();
                                txtLastName.Text = reader["l_name"].ToString();
                                comboBoxGender.SelectedItem = reader["gender"].ToString();
                                txtEmail.Text = reader["email"].ToString();
                                txtPhone.Text = reader["phone"].ToString();
                                datePickerBirthDate.SelectedDate = (reader["b_date"] as DateTime?);
                                comboBoxNationality.SelectedItem = reader["nationality"].ToString();
                                txtCin.Text = reader["cin"].ToString();
                                txtAddress.Text = reader["address"].ToString();
                                uploadedFileName1 = reader["cin_front_copy"].ToString();
                                uploadedFileName2 = reader["cin_back_copy"].ToString();
                                txtOrganization.Text = reader["organization"].ToString();
                                txtRole.Text = reader["role"].ToString();
                                Date_appointment.SelectedDate = (reader["j_date"] as DateTime?);
                                contract_start.SelectedDate = (reader["contract_start"] as DateTime?);
                                contract_end.SelectedDate = (reader["contract_end"] as DateTime?);
                                uploadedFileName3 = reader["contract_copy"].ToString();
                                uploadedFileName4 = reader["resume_copy"].ToString();
                                txtHighestDegree.Text = reader["high_degree"].ToString();
                                txtYearGraduation.Text = reader["year_graduation"].ToString();
                                txtUniversity.Text = reader["university"].ToString();
                                txtSpecialization.Text = reader["specialization"].ToString();
                                uploadedFileName5 = reader["diploma_copy"].ToString();
                                txtSalary.Text = reader["salary"].ToString();
                                txtModule.Text = reader["module"].ToString();
                                FormHelper.ShowDoc(FileNameLabel, ViewButton, uploadedFileName1);
                                FormHelper.ShowDoc(FileNameLabel2, ViewButton2, uploadedFileName2);
                                FormHelper.ShowDoc(FileNameLabel3, ViewButton3, uploadedFileName3);
                                FormHelper.ShowDoc(FileNameLabel4, ViewButton4, uploadedFileName4);
                                FormHelper.ShowDoc(FileNameLabel5, ViewButton5, uploadedFileName5);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading teacher data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}