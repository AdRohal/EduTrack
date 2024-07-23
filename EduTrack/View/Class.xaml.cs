using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EduTrack.ViewModel;

namespace EduTrack.View
{
    public partial class Class : UserControl
    {
        public Class()
        {
            InitializeComponent();
        }

        private void Btn_Checked(object sender, RoutedEventArgs e)
        {
            Pages.Content = new NewClassVM();
        }

        private void Btn_Checked_1(object sender, RoutedEventArgs e)
        {
            Pages.Content = new ShowClassesVM();
        }

        private void Btn_Checked_2(object sender, RoutedEventArgs e)
        {
            Pages.Content = new ScheduleClassesVM();
        }

        private void Btn_Checked_3(object sender, RoutedEventArgs e)
        {
            Pages.Content = new GradesVM();
        }
    }
}
