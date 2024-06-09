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

namespace EduTrack
{
    public partial class ScreenLoading : Window
    {
        public ScreenLoading()
        {
            InitializeComponent();
            StartLoading();
        }

        private async void StartLoading()
        {
            progressBar.Maximum = 100;

            for (int i = 0; i <= 100; i++)
            {
                await Task.Delay(30);

                progressBar.Value = i;
            }
            ShowNextWindow();
        }

        private void ShowNextWindow()
        {
            NextWindow nextWindow = new NextWindow();
            nextWindow.Show();
            this.Close();
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            progressLabel.Content = $"Loading... {e.NewValue}%";
        }
    }
}
