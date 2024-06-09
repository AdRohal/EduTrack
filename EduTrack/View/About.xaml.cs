using EduTrack.Models;
using MySql.Data.MySqlClient;
using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EduTrack.View
{
    /// <summary>
    /// Interaction logic for Transactions.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
        }

        private void OpenLink1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/AdRohal");
        }

        private void OpenLink2(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.linkedin.com/in/adrohal/");
        }

        private void OpenLink3(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://paypal.me/rhladam?country.x=MA&locale.x=en_US");
        }



    }
}
