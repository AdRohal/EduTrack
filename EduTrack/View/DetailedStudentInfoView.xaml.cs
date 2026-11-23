using EduTrack.Models;
using Microsoft.Win32;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EduTrack.Data;
using EduTrack.Utilities;

namespace EduTrack.View
{
    /// <summary>
    /// Interaction logic for DetailedStudentInfoView.xaml
    /// </summary>
    public partial class DetailedStudentInfoView : UserControl
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
        private readonly Student selectedStudent;
        private readonly int studentId;

        private ObservableCollection<string> classes;
        public DetailedStudentInfoView(Student selectedStudent)
        {
            InitializeComponent();



            studentId = selectedStudent.Id;
            LoadStudentData(studentId);
            LoadClasses();
            LoadStudentClass();
        }


        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this student?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("DELETE FROM studentclasses WHERE StudentID = @StudentID; DELETE FROM student_registry WHERE StudentID = @StudentID", connection);

                    command.Parameters.AddWithValue("@StudentID", studentId);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Student has been deleted successfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    OnClosed();
                }
            }
        }
        private void LoadClasses()
        {
            classes = new ObservableCollection<string>();

            using (var connection = DatabaseConnectionManager.CreateConnection())
            {
                connection.Open();

                string query = "SELECT ClassName FROM classes";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    classes.Add(reader.GetString(0));
                }
                reader.Close();
            }
            comboBoxClass.ItemsSource = classes;
        }
        private void LoadStudentClass()
        {
            using (var connection = DatabaseConnectionManager.CreateConnection())
            {
                connection.Open();

                string query = "SELECT ClassID FROM studentclasses WHERE StudentID = @StudentID";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentID", studentId);
                object result = command.ExecuteScalar();
                if (result == null)
                {
                    comboBoxClass.SelectedItem = null;
                }
                else
                {
                    int classId = Convert.ToInt32(result);

                    query = "SELECT ClassName FROM classes WHERE ClassID = @ClassID";

                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ClassID", classId);
                    string className = command.ExecuteScalar()?.ToString();

                    comboBoxClass.SelectedItem = className;
                }
            }
        }

        private void UpdateStudent()
        {
            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    connection.Open();

                    string sql = @"UPDATE student_registry SET 
                          p_pic = @p_pic, f_name = @f_name, m_name = @m_name, l_name = @l_name, gender = @gender, 
                          email = @email, phone = @phone, b_date = @b_date, nationality = @nationality,
                          cin = @cin, address = @address, cin_front_copy = @cin_front_copy, 
                          cin_back_copy = @cin_back_copy, school_name = @school_name, major = @major, 
                          j_date = @j_date, high_degree = @high_degree, 
                          year_graduation = @year_graduation, university = @university, specialization = @specialization,
                          diploma_copy = @diploma_copy, emerg_contact_f_name = @emerg_contact_f_name, 
                          emerg_contact_m_name = @emerg_contact_m_name, emerg_contact_l_name = @emerg_contact_l_name, 
                          emerg_contact_relation = @emerg_contact_relation, emerg_contact_phone = @emerg_contact_phone, 
                          emerg_contact_cin = @emerg_contact_cin, emerg_contact_cin_f_copy = @emerg_contact_cin_f_copy, 
                          emerg_contact_cin_b_copy = @emerg_contact_cin_b_copy
                          WHERE StudentID = @StudentID";

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
                        command.Parameters.AddWithValue("@school_name", txtOrganization.Text);
                        command.Parameters.AddWithValue("@major", txtModule.Text);
                        command.Parameters.AddWithValue("@j_date", (object)Date_appointment.SelectedDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@high_degree", txtHighestDegree.Text);
                        command.Parameters.AddWithValue("@year_graduation", string.IsNullOrEmpty(txtYearGraduation.Text) ? 0 : int.Parse(txtYearGraduation.Text));
                        command.Parameters.AddWithValue("@university", txtUniversity.Text);
                        command.Parameters.AddWithValue("@specialization", txtSpecialization.Text);
                        command.Parameters.AddWithValue("@diploma_copy", uploadedFileName5);
                        command.Parameters.AddWithValue("@emerg_contact_f_name", txtEmergContactFname.Text);
                        command.Parameters.AddWithValue("@emerg_contact_m_name", txtEmergContactMname.Text);
                        command.Parameters.AddWithValue("@emerg_contact_l_name", txtEmergContactLname.Text);
                        command.Parameters.AddWithValue("@emerg_contact_relation", txtEmergContactRelation.Text);
                        command.Parameters.AddWithValue("@emerg_contact_phone", txtEmergContactPhone.Text);
                        command.Parameters.AddWithValue("@emerg_contact_cin", txtEmergContactCIN.Text);
                        command.Parameters.AddWithValue("@emerg_contact_cin_f_copy", uploadedFileName3);
                        command.Parameters.AddWithValue("@emerg_contact_cin_b_copy", uploadedFileName4);
                        command.Parameters.AddWithValue("@StudentID", studentId);


                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Student information updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to update student information!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating student information: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!FormValidation.Validate(this, txtFirstName, txtLastName, txtEmail)) return;

            UpdateStudent();
            string className = comboBoxClass.SelectedItem?.ToString();
            if (className == null)
            {
                MessageBox.Show("Please select a class.");
                return;
            }
            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    connection.Open();

                    string query = "SELECT ClassID FROM classes WHERE ClassName = @ClassName";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ClassName", className);
                    int classId = Convert.ToInt32(command.ExecuteScalar());

                    query = "SELECT COUNT(*) FROM studentclasses WHERE StudentID = @StudentID";
                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@StudentID", studentId);
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        query = "UPDATE studentclasses SET ClassID = @ClassID WHERE StudentID = @StudentID";
                    }
                    else
                    {
                        query = "INSERT INTO studentclasses (StudentID, ClassID) VALUES (@StudentID, @ClassID)";
                    }

                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@StudentID", studentId);
                    command.Parameters.AddWithValue("@ClassID", classId);
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
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

        private void LoadStudentData(int studentId)
        {
            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    connection.Open();

                    string sql = @"SELECT p_pic, f_name, m_name, l_name, gender, email, phone, b_date, nationality, cin, address, 
       cin_front_copy, cin_back_copy, school_name, major, j_date, high_degree, year_graduation, university, specialization, diploma_copy, 
       emerg_contact_f_name, emerg_contact_m_name, emerg_contact_l_name, emerg_contact_relation, emerg_contact_phone, 
       emerg_contact_cin, emerg_contact_cin_f_copy, emerg_contact_cin_b_copy
       FROM student_registry WHERE StudentID = @StudentID";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", studentId);
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
                                txtOrganization.Text = reader["school_name"].ToString();
                                txtModule.Text = reader["major"].ToString();
                                Date_appointment.SelectedDate = (reader["j_date"] as DateTime?);
                                txtHighestDegree.Text = reader["high_degree"].ToString();
                                txtYearGraduation.Text = reader["year_graduation"].ToString();
                                txtUniversity.Text = reader["university"].ToString();
                                txtSpecialization.Text = reader["specialization"].ToString();
                                uploadedFileName5 = reader["diploma_copy"].ToString();
                                txtEmergContactFname.Text = reader["emerg_contact_f_name"].ToString();
                                txtEmergContactMname.Text = reader["emerg_contact_m_name"].ToString();
                                txtEmergContactLname.Text = reader["emerg_contact_l_name"].ToString();
                                txtEmergContactRelation.Text = reader["emerg_contact_relation"].ToString();
                                txtEmergContactPhone.Text = reader["emerg_contact_phone"].ToString();
                                txtEmergContactCIN.Text = reader["emerg_contact_cin"].ToString();
                                uploadedFileName3 = reader["emerg_contact_cin_f_copy"].ToString();
                                uploadedFileName4 = reader["emerg_contact_cin_b_copy"].ToString();
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