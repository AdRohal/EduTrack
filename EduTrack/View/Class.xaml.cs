using System.Windows;
using System.Windows.Controls;
using EduTrack.Utilities;
using EduTrack.ViewModel;

namespace EduTrack.View
{
    public partial class Class : UserControl, IHub
    {
        public Class()
        {
            InitializeComponent();
            Loaded += (s, e) => { if (Pages.Content == null) TabList.IsChecked = true; };
        }

        /// <summary>0 = new class, 2 = schedule, 3 = grades, anything else = the list.</summary>
        public void SelectTab(int index)
        {
            switch (index)
            {
                case 0: TabNew.IsChecked = true; break;
                case 2: TabSchedule.IsChecked = true; break;
                case 3: TabGrades.IsChecked = true; break;
                default: TabList.IsChecked = true; break;
            }
        }

        private void Btn_Checked(object sender, RoutedEventArgs e) => Pages.Content = new NewClassVM();

        private void Btn_Checked_1(object sender, RoutedEventArgs e) => Pages.Content = new ShowClassesVM();

        private void Btn_Checked_2(object sender, RoutedEventArgs e) => Pages.Content = new ScheduleClassesVM();

        private void Btn_Checked_3(object sender, RoutedEventArgs e) => Pages.Content = new GradesVM();
    }
}
