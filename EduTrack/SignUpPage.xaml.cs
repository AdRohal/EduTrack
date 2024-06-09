using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace EduTrack
{
    public partial class SignUpPage : Window
    {
        private const string DatabaseServer = "127.0.0.1";
        private const string DatabaseName = "datebase";
        private const string DatabaseUser = "root";
        private const string DatabasePassword = "";

        private readonly MySqlConnection connection;

        public SignUpPage()
        {
            InitializeComponent();
            string connectionString = $"Server={DatabaseServer};Database={DatabaseName};User ID={DatabaseUser};Password={DatabasePassword};";
            connection = new MySqlConnection(connectionString);
        }

        private void SignUpBtn_Click_1(object sender, RoutedEventArgs e)
        {
            string username = UserName.Text;
            string password = Password.Password;

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

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    cmd.ExecuteNonQuery();
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
            finally
            {
                connection.Close();
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
