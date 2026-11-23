using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using EduTrack.ViewModel;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using EduTrack.Data;
using EduTrack.Utilities;

namespace EduTrack.View
{
    /// <summary>
    /// Interaction logic for NewStudent.xaml
    /// </summary>
    public partial class NewStudent : UserControl
    {
        private string uploadedFileName;
        private string uploadedFileName1;
        private string uploadedFileName2;
        private string uploadedFileName3;
        private string uploadedFileName4;
        private string uploadedFileName5;

        private ObservableCollection<string> classes;

        public NewStudent()
        {
            InitializeComponent();

            LoadClasses();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!FormValidation.Validate(this, txtFirstName, txtLastName, txtEmail)) return;

            try
            {
                using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
                {
                    connection.Open();

                    string sql = @"INSERT INTO student_registry 
       (p_pic, f_name, m_name, l_name, gender, email, phone, b_date, nationality, 
        cin, address, cin_front_copy, cin_back_copy, school_name, major, j_date, high_degree, year_graduation, 
        university, specialization, diploma_copy, emerg_contact_f_name, emerg_contact_m_name, emerg_contact_l_name, 
        emerg_contact_relation, emerg_contact_phone, emerg_contact_cin, emerg_contact_cin_f_copy, emerg_contact_cin_b_copy)
       VALUES 
       (@p_pic, @f_name, @m_name , @l_name, @gender, @email, @phone, @b_date, @nationality, 
        @cin, @address, @cin_front_copy, @cin_back_copy, @school_name, @major, @j_date, @high_degree, @year_graduation, 
        @university, @specialization, @diploma_copy, @emerg_contact_f_name, @emerg_contact_m_name, @emerg_contact_l_name, 
        @emerg_contact_relation, @emerg_contact_phone, @emerg_contact_cin, @emerg_contact_cin_f_copy, @emerg_contact_cin_b_copy)";

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

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            string query = "SELECT LAST_INSERT_ID()";
                            MySqlCommand commandId = new MySqlCommand(query, connection);
                            int studentId = Convert.ToInt32(commandId.ExecuteScalar());

                            SaveStudent(studentId, connection);

                            MessageBox.Show("Student information saved successfully!");
                            HubHelper.Find(this)?.SelectTab(1);
                        }
                        else
                        {
                            MessageBox.Show("Failed to save student information!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void SaveStudent(int studentId, MySqlConnection connection)
        {
            if (comboBoxClass.SelectedItem != null)
            {
                string className = comboBoxClass.SelectedItem.ToString();
                string query = "SELECT ClassID FROM classes WHERE ClassName = @ClassName";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClassName", className);
                int classId = Convert.ToInt32(command.ExecuteScalar());

                query = "INSERT INTO studentclasses (StudentID, ClassID) VALUES (@StudentID, @ClassID)";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentID", studentId);
                command.Parameters.AddWithValue("@ClassID", classId);
                command.ExecuteNonQuery();
            }
        }

        private void LoadClasses()
        {
            classes = new ObservableCollection<string>();

            using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
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
    }
}