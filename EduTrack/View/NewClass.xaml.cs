using System;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using EduTrack.Data;

namespace EduTrack.View
{
    /// <summary>
    /// Interaction logic for NewClass.xaml
    /// </summary>
    public partial class NewClass : UserControl
    {
        public NewClass()
        {
            InitializeComponent();
            UpdateLastClass();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
                {
                    connection.Open();

                    string sql = @"INSERT INTO classes
                    (ClassName, major, Description, CreatedDate)
                    VALUES 
                    (@ClassName, @major, @Description, @CreatedDate)";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ClassName", txtClassName.Text);
                        command.Parameters.AddWithValue("@major", txtMajor.Text);
                        command.Parameters.AddWithValue("@Description", txtDescription.Text);
                        command.Parameters.AddWithValue("@CreatedDate", datePickerCreateDate.SelectedDate.GetValueOrDefault());

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Class information saved successfully!");
                            UpdateLastClass();
                        }
                        else
                        {
                            MessageBox.Show("Failed to save class information!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        public void UpdateLastClass()
        {
            string sql = "SELECT * FROM classes ORDER BY ClassID DESC LIMIT 1";

            try
            {
                using (MySqlConnection connection = DatabaseConnectionManager.CreateConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string className = reader["ClassName"].ToString();
                                string major = reader["major"].ToString();
                                string description = reader["Description"].ToString();
                                string createdDate = ((DateTime)reader["CreatedDate"]).ToString("g");

                                txtLastClass.Text = $"Name: {className}\nMajor: {major}\nDescription: {description}\nCreated Date: {createdDate}";
                            }
                            else
                            {
                                txtLastClass.Text = "No Class Found";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                txtLastClass.Text = "Error loading last class";
            }
        }
    }
}