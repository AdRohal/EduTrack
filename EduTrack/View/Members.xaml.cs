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
    /// <summary>
    /// Interaction logic for Members.xaml
    /// </summary>
    public partial class Members : UserControl
    {
        public Members()
        {
            InitializeComponent();
        }

        private void Btn_Checked(object sender, RoutedEventArgs e)
        {
            Pages.Content = new NewEmployeeVM();
        }

        private void Btn_Checked_1(object sender, RoutedEventArgs e)
        {
            Pages.Content = new ShowEmployeesVM();
        }
    }
}
