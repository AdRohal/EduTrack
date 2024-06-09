using System;
using System.Threading;
using System.Windows;

namespace EduTrack
{
    public partial class App : Application
    {
        private static Mutex mutex;

        [STAThread]
        public static void AppMain()
        {
            mutex = new Mutex(true, "{Your Unique Mutex ID}");

            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                App app = new App();
                app.InitializeComponent();
                app.Run(new ScreenLoading());
            }
            else
            {
                MessageBox.Show("Application is already running.");
                Current.Shutdown();
            }
        }
    }
}
