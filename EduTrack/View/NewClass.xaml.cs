using System;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace EduTrack.View
{
    /// <summary>
    /// Interaction logic for NewClass.xaml
    /// </summary>
    public partial class NewClass : UserControl
    {
        private const string DatabaseServer = "127.0.0.1";
        private const string DatabaseName = "datebase";
        private const string DatabaseUser = "root";
        private const string DatabasePassword = "";

        private readonly MySqlConnection connection;

        public NewClass()
        {
            InitializeComponent();
            string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";
            connection = new MySqlConnection(connectionString);
            UpdateLastClass();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
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

            using (connection)
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



    }
}
