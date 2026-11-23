using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EduTrack.Utilities;
using EduTrack.ViewModel;

namespace EduTrack
{
    /// <summary>Sign-in and first-time account creation in one window.</summary>
    public partial class NextWindow : Window
    {
        private static readonly string LastUserFile = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EduTrack", "last-user.txt");

        private bool _creating;
        private string _remembered;     // username from the last successful sign-in on this PC

        private static string LoadRemembered()
        {
            try { return System.IO.File.Exists(LastUserFile) ? System.IO.File.ReadAllText(LastUserFile).Trim() : null; }
            catch { return null; }
        }

        private static void SaveRemembered(string username)
        {
            try
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(LastUserFile));
                System.IO.File.WriteAllText(LastUserFile, username);
            }
            catch { /* remembering is optional */ }
        }

        public NextWindow()
        {
            InitializeComponent();
            UpdateThemeIcon();
            _remembered = LoadRemembered();
            if (string.IsNullOrWhiteSpace(_remembered)) _remembered = null;
            ApplyIdentityUi();
            Loaded += async (s, e) =>
            {
                bool anyUser = await Task.Run(() => AuthService.HasAnyUser());
                if (!anyUser)
                {
                    SetMode(creating: true);
                    SubText.Text = "No accounts exist yet. Create the first administrator to get started.";
                }
            };
        }

        private void SetMode(bool creating)
        {
            _creating = creating;
            SubText.Text = creating ? "This account will be able to manage students, teachers and classes."
                                    : "Use your administrator account to open the register.";
            SubmitBtn.Content = creating ? "Create account" : "Sign in";
            SwitchPrompt.Text = creating ? "Already have an account?" : "First time here?";
            SwitchBtn.Content = creating ? "Sign in" : "Create an account";
            ConfirmPanel.Visibility = creating ? Visibility.Visible : Visibility.Collapsed;
            ShowError(null);
            Password.Clear();
            ConfirmPassword.Clear();
            ApplyIdentityUi();
        }

        private bool ShowingRemembered => !_creating && _remembered != null;

        /// <summary>Welcome-back view (just the password) or the normal username field.</summary>
        private void ApplyIdentityUi()
        {
            bool remembered = ShowingRemembered;
            RememberedPanel.Visibility = remembered ? Visibility.Visible : Visibility.Collapsed;
            UsernamePanel.Visibility = remembered ? Visibility.Collapsed : Visibility.Visible;
            OtherUserBtn.Visibility = remembered ? Visibility.Visible : Visibility.Collapsed;
            if (remembered)
            {
                RememberedName.Text = _remembered;
                RememberedInitial.Text = _remembered.Substring(0, 1).ToUpperInvariant();
                HeadingText.Text = "Welcome back";
                SubText.Text = "Enter your password to continue.";
                Password.Focus();
            }
            else
            {
                HeadingText.Text = _creating ? "Create account" : "Sign in";
                UserName.Focus();
            }
        }

        private void OtherUser_Click(object sender, RoutedEventArgs e)
        {
            _remembered = null;
            Password.Clear();
            ShowError(null);
            SubText.Text = "Use your administrator account to open the register.";
            ApplyIdentityUi();
        }

        private void ShowError(string message)
        {
            ErrorBox.Visibility = string.IsNullOrEmpty(message) ? Visibility.Collapsed : Visibility.Visible;
            ErrorText.Text = message;
        }

        private void Field_Changed(object sender, RoutedEventArgs e)
        {
            if (ErrorBox != null && ErrorBox.Visibility == Visibility.Visible) ShowError(null);
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            string username = ShowingRemembered ? _remembered : UserName.Text.Trim();
            string password = Password.Password;

            if (username.Length == 0) { ShowError("Enter your username."); UserName.Focus(); return; }
            if (password.Length == 0) { ShowError("Enter your password."); Password.Focus(); return; }

            if (_creating)
            {
                if (password.Length < AuthService.MinPasswordLength) { ShowError("Use at least 6 characters for the password."); Password.Focus(); return; }
                if (password != ConfirmPassword.Password) { ShowError("The two passwords don't match."); ConfirmPassword.Focus(); return; }
            }

            SubmitBtn.IsEnabled = false;
            string original = SubmitBtn.Content as string;
            SubmitBtn.Content = _creating ? "Creating..." : "Signing in...";

            AuthResult result = AuthResult.Ok;
            string registerError = null;
            await Task.Run(() =>
            {
                if (_creating)
                {
                    registerError = AuthService.Register(username, password);
                }
                else
                {
                    result = AuthService.SignIn(username, password);
                }
            });

            SubmitBtn.IsEnabled = true;
            SubmitBtn.Content = original;

            if (_creating)
            {
                if (registerError != null) { ShowError(registerError); return; }
                OpenMain(username);
                return;
            }

            switch (result)
            {
                case AuthResult.Ok: OpenMain(username); break;
                case AuthResult.WrongCredentials:
                    ShowError("That username and password don't match. Check them and try again.");
                    Password.Clear(); Password.Focus();
                    break;
                default:
                    ShowError("Can't reach the database. Make sure MySQL is running, then try again.");
                    break;
            }
        }

        private void OpenMain(string username)
        {
            SaveRemembered(username);
            var mainWindow = new MainWindow();
            (mainWindow.DataContext as NavigationVM)?.SetConnectedUsername(username);
            mainWindow.Show();
            Close();
        }

        private void Switch_Click(object sender, RoutedEventArgs e) => SetMode(!_creating);

        private void Theme_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.Toggle();
            UpdateThemeIcon();
        }

        private void UpdateThemeIcon()
        {
            ThemeIcon.SetResourceReference(System.Windows.Shapes.Path.DataProperty, ThemeManager.IsDark ? "IconSun" : "IconMoon");
        }

        private void CloseBtn_Click_1(object sender, RoutedEventArgs e) => Close();
    }
}
