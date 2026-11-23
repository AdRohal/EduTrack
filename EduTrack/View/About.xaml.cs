using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EduTrack.Utilities;
using QRCoder;

namespace EduTrack.View
{
    public partial class About : UserControl
    {
        private const string Website = "https://adamrohal.tech";

        public About()
        {
            InitializeComponent();
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            VersionText.Text = Loc.T("Version " + version.Major + "." + version.Minor + "." + version.Build);
            Loaded += (s, e) => { ThemeManager.ThemeChanged += DrawQr; DrawQr(); };
            Unloaded += (s, e) => ThemeManager.ThemeChanged -= DrawQr;
        }

        /// <summary>One QR code per theme: dark modules on a light tile, tinted to match the theme.</summary>
        private void DrawQr()
        {
            byte[] dark, light;
            switch (ThemeManager.Current)
            {
                case ThemeManager.Paper: dark = new byte[] { 0x13, 0x30, 0x2A, 255 }; light = new byte[] { 255, 255, 255, 255 }; break;
                case ThemeManager.Slate: dark = new byte[] { 0x0E, 0x15, 0x13, 255 }; light = new byte[] { 0xE8, 0xEF, 0xEB, 255 }; break;
                default: dark = new byte[] { 0x21, 0x25, 0x29, 255 }; light = new byte[] { 255, 255, 255, 255 }; break;
            }
            using (var generator = new QRCodeGenerator())
            using (var data = generator.CreateQrCode(Website, QRCodeGenerator.ECCLevel.Q))
            {
                byte[] png = new PngByteQRCode(data).GetGraphic(10, dark, light, true);
                var image = new BitmapImage();
                using (var stream = new MemoryStream(png))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                }
                image.Freeze();
                QrImage.Source = image;
                QrTile.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(light[0], light[1], light[2]));
            }
        }

        private void OpenLink1(object sender, RoutedEventArgs e) => Open("https://github.com/AdRohal");

        private void OpenLink2(object sender, RoutedEventArgs e) => Open("https://www.linkedin.com/in/adrohal/");

        private void OpenLink3(object sender, RoutedEventArgs e) => Open("https://ko-fi.com/adrohal");

        private static void Open(string url) => Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
    }
}