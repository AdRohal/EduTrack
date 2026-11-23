using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;

namespace EduTrack.View
{
    /// <summary>Full-size document viewer drawn inside the main window, with zoom (wheel / buttons) and drag-to-move.</summary>
    public partial class DocumentViewerOverlay : UserControl
    {
        private double _fitScale = 1, _scale = 1;
        private bool _dragging;
        private Point _dragStart;
        private double _startX, _startY;

        public DocumentViewerOverlay()
        {
            InitializeComponent();
            KeyDown += OnKeyDown;
        }

        private Windows.Data.Pdf.PdfDocument _pdf;
        private int _page;
        private int _openId;   // ignores a slow render when the viewer was closed or reopened meanwhile

        public async void Open(string path)
        {
            int id = ++_openId;
            try
            {
                if (!File.Exists(path)) throw new FileNotFoundException("The file no longer exists:\n" + path);

                _pdf = null;
                BitmapSource first;
                if (string.Equals(Path.GetExtension(path), ".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    var file = await Windows.Storage.StorageFile.GetFileFromPathAsync(path);
                    _pdf = await Windows.Data.Pdf.PdfDocument.LoadFromFileAsync(file);
                    _page = 0;
                    first = await RenderPageAsync(0);
                }
                else
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = new Uri(path);
                    image.EndInit();
                    image.Freeze();
                    first = image;
                }
                if (id != _openId) return;

                Picture.Source = first;
                TitleText.Text = Path.GetFileName(path);
                PagePanel.Visibility = _pdf != null && _pdf.PageCount > 1 ? Visibility.Visible : Visibility.Collapsed;
                UpdatePageText();
                Visibility = Visibility.Visible;
                UpdateLayout();
                FitToScreen();
                Focus();
            }
            catch (Exception ex)
            {
                _pdf = null;
                MessageBox.Show("This file could not be opened.\n" + ex.Message, "Document", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async System.Threading.Tasks.Task<BitmapSource> RenderPageAsync(int index)
        {
            using (var page = _pdf.GetPage((uint)index))
            using (var stream = new Windows.Storage.Streams.InMemoryRandomAccessStream())
            {
                // Rendered large so zooming in stays sharp.
                var options = new Windows.Data.Pdf.PdfPageRenderOptions { DestinationWidth = 2200 };
                await page.RenderToStreamAsync(stream, options);
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream.AsStreamForRead();
                image.EndInit();
                image.Freeze();
                return image;
            }
        }

        private void UpdatePageText() => PageText.Text = _pdf == null ? "" : (_page + 1) + " / " + _pdf.PageCount;

        private async void ShowPage(int index)
        {
            if (_pdf == null || index < 0 || index >= _pdf.PageCount || index == _page) return;
            int id = _openId;
            try
            {
                var image = await RenderPageAsync(index);
                if (id != _openId) return;
                _page = index;
                Picture.Source = image;
                UpdatePageText();
                FitToScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show("This page could not be shown.\n" + ex.Message, "Document", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void PrevPage_Click(object sender, RoutedEventArgs e) => ShowPage(_page - 1);
        private void NextPage_Click(object sender, RoutedEventArgs e) => ShowPage(_page + 1);

        public void CloseViewer()
        {
            _openId++;
            Visibility = Visibility.Collapsed;
            Picture.Source = null;
            _pdf = null;
            _dragging = false;
        }

        private double ImgW => Picture.Source is BitmapSource s ? s.PixelWidth * 96.0 / s.DpiX : 0;
        private double ImgH => Picture.Source is BitmapSource s ? s.PixelHeight * 96.0 / s.DpiY : 0;

        private void FitToScreen()
        {
            if (ImgW <= 0 || ImgH <= 0 || Viewport.ActualWidth <= 0) return;
            const double pad = 32;
            _fitScale = Math.Min((Viewport.ActualWidth - pad) / ImgW, (Viewport.ActualHeight - pad) / ImgH);
            _scale = _fitScale;
            Move.X = (Viewport.ActualWidth - ImgW * _scale) / 2;
            Move.Y = (Viewport.ActualHeight - ImgH * _scale) / 2;
            Apply();
        }

        private void Apply()
        {
            Scale.ScaleX = Scale.ScaleY = _scale;
            ZoomText.Text = Math.Round(_scale * 100) + "%";
        }

        // Zoom so that the point under 'anchor' (viewport coordinates) stays where it is.
        private void ZoomBy(double factor, Point anchor)
        {
            double min = Math.Min(_fitScale, 1) * 0.5, max = 16;
            double target = Math.Max(min, Math.Min(max, _scale * factor));
            double ratio = target / _scale;
            Move.X = anchor.X - (anchor.X - Move.X) * ratio;
            Move.Y = anchor.Y - (anchor.Y - Move.Y) * ratio;
            _scale = target;
            Apply();
        }

        private Point Centre => new Point(Viewport.ActualWidth / 2, Viewport.ActualHeight / 2);

        private void ZoomIn_Click(object sender, RoutedEventArgs e) => ZoomBy(1.25, Centre);
        private void ZoomOut_Click(object sender, RoutedEventArgs e) => ZoomBy(0.8, Centre);
        private void Fit_Click(object sender, RoutedEventArgs e) => FitToScreen();
        private void Close_Click(object sender, RoutedEventArgs e) => CloseViewer();

        private void Viewport_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Picture.Source != null && Math.Abs(_scale - _fitScale) < 1e-6) FitToScreen();
        }

        private void Viewport_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ZoomBy(e.Delta > 0 ? 1.15 : 1 / 1.15, e.GetPosition(Viewport));
            e.Handled = true;
        }

        private void Viewport_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                // Double-click toggles between fit and 100 %, zooming toward the cursor.
                if (Math.Abs(_scale - _fitScale) < 1e-3) ZoomBy(1 / _scale, e.GetPosition(Viewport));
                else FitToScreen();
                return;
            }
            _dragging = true;
            _dragStart = e.GetPosition(Viewport);
            _startX = Move.X; _startY = Move.Y;
            Viewport.CaptureMouse();
            Viewport.Cursor = Cursors.SizeAll;
        }

        private void Viewport_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragging) return;
            Point p = e.GetPosition(Viewport);
            Move.X = _startX + (p.X - _dragStart.X);
            Move.Y = _startY + (p.Y - _dragStart.Y);
        }

        private void Viewport_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _dragging = false;
            Viewport.ReleaseMouseCapture();
            Viewport.Cursor = Cursors.Hand;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape: CloseViewer(); break;
                case Key.Add: case Key.OemPlus: ZoomBy(1.25, Centre); break;
                case Key.Subtract: case Key.OemMinus: ZoomBy(0.8, Centre); break;
                case Key.D0: case Key.NumPad0: FitToScreen(); break;
                case Key.PageDown: case Key.Right: ShowPage(_page + 1); break;
                case Key.PageUp: case Key.Left: ShowPage(_page - 1); break;
                default: return;
            }
            e.Handled = true;
        }

        /// <summary>Opens a picture in the viewer of the main window.</summary>
        public static void Show(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            var main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (main != null) main.ShowDocument(path);
        }
    }
}
