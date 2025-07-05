using MySql.Data.MySqlClient;
using System;
using System.Windows;
using EduTrack.Data;

namespace EduTrack
{
    public partial class SignUpPage : Window
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void SignUpBtn_Click_1(object sender, RoutedEventArgs e)
        {
            // Test database connection first
            if (!DatabaseConnectionManager.TestConnection(out string errorMessage))
            {
                MessageBox.Show($"Database connection failed: {errorMessage}", "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string username = UserName.Text;
            string password = Password.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Input Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (CreateUser(username, password))
            {
                MessageBox.Show("Signup successful");
                NavigateToNextWindow();
            }
            else
            {
                MessageBox.Show("Signup failed (username may already exist or other issues)");
            }
        }

        private bool CreateUser(string username, string password)
        {
            try
            {
                string query = "INSERT INTO Login (UserName, Password) VALUES (@UserName, @Password)";

                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserName", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error creating user: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating user: {ex.Message}");
                return false;
            }
        }

        private void NavigateToNextWindow()
        {
            NextWindow nextWindow = new NextWindow();
            nextWindow.Show();
            Close();
        }

        private void Login_Click_1(object sender, RoutedEventArgs e)
        {
            NavigateToNextWindow();
        }

        private void CloseBtn_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
