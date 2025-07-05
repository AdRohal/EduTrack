using EduTrack.ViewModel;
using MySql.Data.MySqlClient;
using System;
using System.Windows;
using EduTrack.Data;

namespace EduTrack
{
    public partial class NextWindow : Window
    {
        public NextWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click_1(object sender, RoutedEventArgs e)
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

            bool loginSuccess = AuthenticateUser(username, password);

            if (loginSuccess)
            {
                MainWindow mainWindow = new MainWindow();

                (mainWindow.DataContext as NavigationVM)?.SetConnectedUsername(username);

                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Login failed (check your credentials)");
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            try
            {
                using (var connection = DatabaseConnectionManager.CreateConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM Login WHERE UserName = @UserName AND Password = @Password", connection))
                    {
                        command.Parameters.AddWithValue("@UserName", username);
                        command.Parameters.AddWithValue("@Password", password);

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
        }

        private void SignUp_Click_1(object sender, RoutedEventArgs e)
        {
            SignUpPage signUpPage = new SignUpPage();
            signUpPage.Show();
            Close();
        }

        private void CloseBtn_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
