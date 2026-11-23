using System;
using System.Threading;
using System.Windows;
using EduTrack.Utilities;

namespace EduTrack
{
    public partial class App : Application
    {
        private static Mutex mutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            ThemeManager.Initialize();
            Loc.Initialize();
            DispatcherUnhandledException += (s, args) =>
            {
                MessageBox.Show("EduTrack hit a problem it could not recover from:\n" + args.Exception.Message,
                    "Unexpected error", MessageBoxButton.OK, MessageBoxImage.Error);
                args.Handled = true;
            };
            base.OnStartup(e);
            CreateTrayIcon();
        }

        // ---- Tray icon: lets the user bring EduTrack back or close it (and with it, the database) ----
        private System.Windows.Forms.NotifyIcon _tray;

        private void CreateTrayIcon()
        {
            try
            {
                var menu = new System.Windows.Forms.ContextMenuStrip { RightToLeft = Loc.IsRtl ? System.Windows.Forms.RightToLeft.Yes : System.Windows.Forms.RightToLeft.No };
                menu.Items.Add(Loc.T("Open EduTrack"), null, (s, a) => BringToFront());
                menu.Items.Add(new System.Windows.Forms.ToolStripSeparator());
                menu.Items.Add(Loc.T("Exit EduTrack"), null, (s, a) => Dispatcher.Invoke(Shutdown));

                _tray = new System.Windows.Forms.NotifyIcon
                {
                    Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    Text = "EduTrack",
                    ContextMenuStrip = menu,
                    Visible = true
                };
                _tray.DoubleClick += (s, a) => BringToFront();
            }
            catch { /* the app works without a tray icon */ }
        }

        private void BringToFront()
        {
            Dispatcher.Invoke(() =>
            {
                foreach (Window w in Windows)
                {
                    if (!w.ShowInTaskbar || !w.IsVisible) continue;
                    if (w.WindowState == WindowState.Minimized) w.WindowState = WindowState.Normal;
                    w.Activate();
                    return;
                }
            });
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (_tray != null) { _tray.Visible = false; _tray.Dispose(); }
            // Close the built-in database with the app so nothing keeps running in the background.
            EduTrack.Data.EmbeddedDatabase.Stop();
            base.OnExit(e);
        }

        [STAThread]
        public static void AppMain()
        {
            mutex = new Mutex(true, "EduTrack.SingleInstance");

            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                App app = new App();
                app.InitializeComponent();
                app.Run(new ScreenLoading());
            }
            else
            {
                System.Windows.MessageBox.Show("EduTrack is already running.");
                Current.Shutdown();
            }
        }
    }
}
