using EduTrack.ViewModel;
using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace EduTrack
{
    public partial class NextWindow : Window
    {
        private const string DatabaseServer = "127.0.0.1";
        private const string DatabaseName = "datebase";
        private const string DatabaseUser = "root";
        private const string DatabasePassword = "";

        private readonly MySqlConnection connection;

        public NextWindow()
        {
            InitializeComponent();
            string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";
            connection = new MySqlConnection(connectionString);
        }

        private void LoginBtn_Click_1(object sender, RoutedEventArgs e)
        {
            string username = UserName.Text;
            string password = Password.Password;

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
                connection.Open();
                MySqlCommand command = new MySqlCommand($"SELECT COUNT(*) FROM Login WHERE UserName = @UserName AND Password = @Password", connection);
                command.Parameters.AddWithValue("@UserName", username);
                command.Parameters.AddWithValue("@Password", password);

                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
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
            finally
            {
                connection.Close();
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
