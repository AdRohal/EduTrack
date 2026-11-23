using System;
using System.Windows;
using System.Windows.Controls;
using EduTrack.Data;
using EduTrack.Utilities;
using MySql.Data.MySqlClient;

namespace EduTrack.View
{
    public partial class NewClass : UserControl
    {
        public NewClass()
        {
            InitializeComponent();
            datePickerCreateDate.SelectedDate = DateTime.Today;
            UpdateLastClass();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtClassName.Text))
            {
                FormError.Text = "Give the class a name.";
                txtClassName.Focus();
                return;
            }
            FormError.Text = string.Empty;

            try
            {
                using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
                {
                    connection.Open();

                    string sql = "INSERT INTO classes (ClassName, major, Description, CreatedDate) VALUES (@ClassName, @major, @Description, @CreatedDate)";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ClassName", txtClassName.Text.Trim());
                        command.Parameters.AddWithValue("@major", txtMajor.Text.Trim());
                        command.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                        command.Parameters.AddWithValue("@CreatedDate", (object)datePickerCreateDate.SelectedDate ?? DBNull.Value);

                        if (command.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Class saved successfully.");
                            txtClassName.Clear();
                            txtMajor.Clear();
                            txtDescription.Clear();
                            datePickerCreateDate.SelectedDate = DateTime.Today;
                            UpdateLastClass();
                            txtClassName.Focus();
                        }
                        else
                        {
                            MessageBox.Show("The class could not be saved.", "Couldn't save", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The class could not be saved.\n" + ex.Message, "Couldn't save", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void UpdateLastClass()
        {
            try
            {
                using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SELECT ClassName, major, Description, CreatedDate FROM classes ORDER BY ClassID DESC LIMIT 1", connection))
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string major = reader["major"] as string;
                            string description = reader["Description"] as string;
                            string created = reader["CreatedDate"] is DateTime d ? d.ToString("d MMM yyyy") : "no date";
                            txtLastClass.Text = reader["ClassName"] + "\n" +
                                                (string.IsNullOrWhiteSpace(major) ? "No major" : "Major: " + major) + ", started " + created +
                                                (string.IsNullOrWhiteSpace(description) ? "" : "\n" + description);
                        }
                        else
                        {
                            txtLastClass.Text = "No class has been added yet. The one you save will appear here.";
                        }
                    }
                }
            }
            catch (Exception)
            {
                txtLastClass.Text = "The last class could not be loaded.";
            }
        }
    }
}
