using System.Windows;
using System.Windows.Controls;
using EduTrack.Utilities;
using EduTrack.ViewModel;

namespace EduTrack.View
{
    public partial class Settings : UserControl, IHub
    {
        public Settings()
        {
            InitializeComponent();
            Loaded += (s, e) => { if (Pages.Content == null) TabAppearance.IsChecked = true; };
        }

        public void SelectTab(int index)
        {
            if (index == 1) TabBackup.IsChecked = true; else TabAppearance.IsChecked = true;
        }

        private void Btn_Checked(object sender, RoutedEventArgs e) => Pages.Content = new GeneralSettings();

        private void Btn_Checked_1(object sender, RoutedEventArgs e) => Pages.Content = new BackupVM();
    }
}
