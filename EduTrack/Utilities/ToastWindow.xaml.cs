using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace EduTrack.Utilities
{
    /// <summary>Small non-blocking confirmation shown at the bottom of the active window.</summary>
    public partial class ToastWindow : Window
    {
        private static ToastWindow _current;

        private ToastWindow(string message)
        {
            InitializeComponent();
            Message.Text = message;
        }

        public static void Show(string message)
        {
            var app = Application.Current;
            if (app == null) return;
            app.Dispatcher.Invoke(() =>
            {
                _current?.Close();
                var toast = new ToastWindow(message);
                var owner = app.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive && w.IsVisible && !(w is ToastWindow)) ?? app.MainWindow;
                toast.Opacity = 0;
                toast.Loaded += (s, e) =>
                {
                    if (owner != null && owner.WindowState != WindowState.Minimized)
                    {
                        toast.Left = owner.Left + (owner.ActualWidth - toast.ActualWidth) / 2;
                        toast.Top = owner.Top + owner.ActualHeight - toast.ActualHeight - 24;
                    }
                    toast.BeginAnimation(OpacityProperty, new DoubleAnimation(1, TimeSpan.FromMilliseconds(160)));
                    var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(2800) };
                    timer.Tick += (a, b) =>
                    {
                        timer.Stop();
                        var fade = new DoubleAnimation(0, TimeSpan.FromMilliseconds(220));
                        fade.Completed += (c, d) => toast.Close();
                        toast.BeginAnimation(OpacityProperty, fade);
                    };
                    timer.Start();
                };
                _current = toast;
                toast.Closed += (s, e) => { if (_current == toast) _current = null; };
                toast.Show();
            });
        }
    }
}
