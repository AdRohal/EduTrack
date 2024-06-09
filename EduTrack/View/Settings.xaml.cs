using EduTrack.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace EduTrack.View
{
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Btn_Checked_1(object sender, RoutedEventArgs e)
        {
            Pages.Content = new BackupVM();
        }
    }
}
