using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using EduTrack.ViewModel;
using EduTrack.Properties;

namespace EduTrack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public MainWindow()
        {
            InitializeComponent();
            Pages.Content = new HomeVM();
            Loaded += (sender, e) => StartClock();
            NavigationVM viewModel = new NavigationVM();
            DataContext = viewModel;
            
            CreateDirectoryStructure();
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            Pages.Content = new HomeVM();
        }

        private void BtnCustomer_Click(object sender, RoutedEventArgs e)
        {
            Pages.Content = new StudentsVM();
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            NextWindow nextWindow = new NextWindow();
            nextWindow.Show();
            Close();
        }

        private void StartClock()
        {
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Tick += (sender, e) =>
            {
                ClockTextBlock.Text = DateTime.Now.ToString("dddd dd/MM/yyyy hh:mm tt");
            };

            timer.Start();
        }


        private void ProfileIcon_Click(object sender, RoutedEventArgs e)
        {
            if (ProfilePopup.IsOpen)
            {
                ProfilePopup.IsOpen = false;
            }
            else
            {
                ProfilePopup.PlacementTarget = (UIElement)sender;
                ProfilePopup.IsOpen = true;
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            string url = e.Uri.AbsoluteUri;
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url));
            e.Handled = true;
        }

        private void BtnMember_Checked(object sender, RoutedEventArgs e)
        {
            Pages.Content = new MembersVM();
        }

        private void BtnSettings_Checked(object sender, RoutedEventArgs e)
        {
            Pages.Content = new SettingsVM();
        }

        private void BtnTeachers_Checked(object sender, RoutedEventArgs e)
        {
            Pages.Content = new TeachersVM();
        }

        private void BtnClass_Checked(object sender, RoutedEventArgs e)
        {
            Pages.Content = new ClassVM();
        }

        private void Btn_Checked(object sender, RoutedEventArgs e)
        {
            Pages.Content = new AboutVM();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CreateDirectoryStructure()
        {
            try
            {
                string baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                string[] directories = { "EduTrack", "EduTrack\\Employees", "EduTrack\\Students", "EduTrack\\Classes", "EduTrack\\Teachers" };

                foreach (string directory in directories)
                {
                    string directoryPath = Path.Combine(baseDirectory, directory);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating directory structure: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
