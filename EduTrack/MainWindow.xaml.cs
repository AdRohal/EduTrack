using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using EduTrack.Utilities;
using EduTrack.ViewModel;

namespace EduTrack
{
    public partial class MainWindow : Window
    {
        private readonly NavigationVM _viewModel = new NavigationVM();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
            _viewModel.PropertyChanged += ViewModel_PropertyChanged;

            ThemeManager.ThemeChanged += UpdateThemeIcon;
            Closed += (s, e) => ThemeManager.ThemeChanged -= UpdateThemeIcon;
            UpdateThemeIcon();

            Loaded += (s, e) =>
            {
                StartClock();
                NavHome.IsChecked = true;
            };

            CreateDirectoryStructure();
        }

        /// <summary>Opens a section by name, optionally on a given tab (0 = the "new" form). Used by the dashboard shortcuts.</summary>
        public void GoTo(string section, int tab = -1)
        {
            switch (section)
            {
                case "Students": NavStudents.IsChecked = true; break;
                case "Teachers": NavTeachers.IsChecked = true; break;
                case "Employees": NavEmployees.IsChecked = true; break;
                case "Classes": NavClasses.IsChecked = true; break;
                case "Settings": NavSettings.IsChecked = true; break;
                case "About": NavAbout.IsChecked = true; break;
                default: NavHome.IsChecked = true; break;
            }
            if (tab < 0) return;

            // The section page is created by its data template, so wait until it is loaded before picking the tab.
            Dispatcher.BeginInvoke(new Action(() => FindHub(Pages)?.SelectTab(tab)), DispatcherPriority.Loaded);
        }

        private static IHub FindHub(DependencyObject parent)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is IHub hub) return hub;
                var nested = FindHub(child);
                if (nested != null) return nested;
            }
            return null;
        }

        private void Nav_Checked(object sender, RoutedEventArgs e)
        {
            if (Pages == null) return;
            object page;
            switch (((RadioButton)sender).Name)
            {
                case "NavStudents": page = new StudentsVM(); break;
                case "NavTeachers": page = new TeachersVM(); break;
                case "NavEmployees": page = new MembersVM(); break;
                case "NavClasses": page = new ClassVM(); break;
                case "NavSettings": page = new SettingsVM(); break;
                case "NavAbout": page = new AboutVM(); break;
                default: page = new HomeVM(); break;
            }
            Pages.Content = page;
            // A short fade so the change of page is noticed, not jarring.
            Pages.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(180)));
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control) return;
            RadioButton target = null;
            switch (e.Key)
            {
                case Key.D1: target = NavHome; break;
                case Key.D2: target = NavStudents; break;
                case Key.D3: target = NavTeachers; break;
                case Key.D4: target = NavEmployees; break;
                case Key.D5: target = NavClasses; break;
                case Key.D6: target = NavSettings; break;
                case Key.D7: target = NavAbout; break;
            }
            if (target != null) { target.IsChecked = true; e.Handled = true; }
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(NavigationVM.ConnectedUsername)) return;
            string name = _viewModel.ConnectedUsername;
            if (string.IsNullOrWhiteSpace(name)) return;
            UserNameText.Text = name;
            AvatarInitial.Text = name.Substring(0, 1).ToUpperInvariant();
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e) => Close();

        private void MinimizeButton_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
            => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

        private void Window_StateChanged(object sender, EventArgs e)
        {
            bool maximized = WindowState == WindowState.Maximized;
            // A chromeless maximised window spills over the screen edge by the resize border; pull it back in.
            // (The size and position when maximised are set to the monitor's work area in WndProc, so no margin is needed.)
            Root.Margin = new Thickness(0);
            MaxIcon.SetResourceReference(System.Windows.Shapes.Path.DataProperty, maximized ? "IconRestore" : "IconMaximize");
            MaxButton.ToolTip = maximized ? "Restore" : "Maximize";
        }

        public void ShowDocument(string path) => Viewer.Open(path);

        // ---- Keep a maximised window inside the work area (taskbar on any edge, any monitor) ----
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            ((System.Windows.Interop.HwndSource)PresentationSource.FromVisual(this)).AddHook(WndProc);
        }

        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_GETMINMAXINFO = 0x0024;
            if (msg != WM_GETMINMAXINFO) return IntPtr.Zero;

            IntPtr monitor = MonitorFromWindow(hwnd, 2 /* MONITOR_DEFAULTTONEAREST */);
            if (monitor == IntPtr.Zero) return IntPtr.Zero;
            var info = new MONITORINFO { cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(MONITORINFO)) };
            if (!GetMonitorInfo(monitor, ref info)) return IntPtr.Zero;

            var mmi = (MINMAXINFO)System.Runtime.InteropServices.Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
            mmi.ptMaxPosition.x = info.rcWork.left - info.rcMonitor.left;
            mmi.ptMaxPosition.y = info.rcWork.top - info.rcMonitor.top;
            mmi.ptMaxSize.x = info.rcWork.right - info.rcWork.left;
            mmi.ptMaxSize.y = info.rcWork.bottom - info.rcWork.top;
            System.Runtime.InteropServices.Marshal.StructureToPtr(mmi, lParam, true);
            handled = true;
            return IntPtr.Zero;
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct POINT { public int x, y; }
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct RECT { public int left, top, right, bottom; }
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct MINMAXINFO { public POINT ptReserved, ptMaxSize, ptMaxPosition, ptMinTrackSize, ptMaxTrackSize; }
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct MONITORINFO { public int cbSize; public RECT rcMonitor, rcWork; public int dwFlags; }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr MonitorFromWindow(IntPtr hwnd, int flags);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO info);

        private void Theme_Click(object sender, RoutedEventArgs e) => ThemeManager.Toggle();

        private void UpdateThemeIcon()
        {
            ThemeIcon.SetResourceReference(System.Windows.Shapes.Path.DataProperty, ThemeManager.IsDark ? "IconSun" : "IconMoon");
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            new NextWindow().Show();
            Close();
        }

        private void StartClock()
        {
            void Tick() => ClockTextBlock.Text = DateTime.Now.ToString("dddd d MMMM, HH:mm");
            Tick();
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(20) };
            timer.Tick += (s, e) => Tick();
            timer.Start();
            Closed += (s, e) => timer.Stop();
        }

        private void CreateDirectoryStructure()
        {
            try
            {
                string baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string[] directories = { "EduTrack", "EduTrack\\Employees", "EduTrack\\Students", "EduTrack\\Classes", "EduTrack\\Teachers" };
                foreach (string directory in directories)
                {
                    string directoryPath = Path.Combine(baseDirectory, directory);
                    if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating directory structure: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
