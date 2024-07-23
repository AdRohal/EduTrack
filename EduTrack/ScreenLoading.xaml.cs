using System.Threading.Tasks;
using System.Windows;

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
