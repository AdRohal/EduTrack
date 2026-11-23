using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using EduTrack.Data;

namespace EduTrack
{
    /// <summary>Start-up screen. It checks the database connection for real instead of showing a fake progress bar.</summary>
    public partial class ScreenLoading : Window
    {
        public ScreenLoading()
        {
            InitializeComponent();
            Loaded += async (s, e) => await ConnectAsync();
        }

        private async Task ConnectAsync()
        {
            FailurePanel.Visibility = Visibility.Collapsed;
            StatusText.Text = "Connecting to the database";
            progressBar.BeginAnimation(System.Windows.Controls.Primitives.RangeBase.ValueProperty,
                new DoubleAnimation(0, 85, TimeSpan.FromMilliseconds(1400)) { EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut } });

            var minimumShown = Task.Delay(1100);
            string error = null;
            bool ok = true;
            if (EmbeddedDatabase.IsBundled)
            {
                // The database that ships with the app only runs while the app does, so start it now (the first time it also sets itself up).
                StatusText.Text = "Starting the database";
                ok = await Task.Run(() => EmbeddedDatabase.EnsureStarted(DatabaseConnectionManager.DatabaseName, out error));
            }
            if (ok) ok = await Task.Run(() => DatabaseConnectionManager.TestConnection(out error));
            await minimumShown;

            if (ok)
            {
                StatusText.Text = "Getting things ready";
                await Task.Run(() => SchemaUpgrades.Apply());
                await DatabaseConnectionManager.WarmUpAsync();
                StatusText.Text = "Ready";
                progressBar.BeginAnimation(System.Windows.Controls.Primitives.RangeBase.ValueProperty, new DoubleAnimation(100, TimeSpan.FromMilliseconds(180)));
                await Task.Delay(220);
                OpenSignIn();
            }
            else
            {
                progressBar.BeginAnimation(System.Windows.Controls.Primitives.RangeBase.ValueProperty, null);
                progressBar.Value = 0;
                StatusText.Text = "Can't reach the database";
                FailureText.Text = EmbeddedDatabase.IsBundled
                    ? "EduTrack's built-in database could not start. Close EduTrack from the tray icon and open it again; if it keeps failing, reinstall EduTrack (your data is kept).\n" + error
                    : "EduTrack couldn't connect to the database “" + DatabaseConnectionManager.DatabaseName + "” on " + DatabaseConnectionManager.Server +
                      ". Start MySQL (for example from the XAMPP control panel), make sure the database exists, then try again.\n" + error;
                FailurePanel.Visibility = Visibility.Visible;
            }
        }

        private async void Retry_Click(object sender, RoutedEventArgs e) => await ConnectAsync();

        private void OpenAnyway_Click(object sender, RoutedEventArgs e) => OpenSignIn();

        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void OpenSignIn()
        {
            new NextWindow().Show();
            Close();
        }
    }
}
