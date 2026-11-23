using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace EduTrack.Utilities
{
    /// <summary>Shared behaviour of the person forms: showing a photo and document rows when a record may have neither.</summary>
    public static class FormHelper
    {
        /// <summary>Shows the photo at <paramref name="path"/>; a missing or unreadable file leaves the picture empty.</summary>
        public static void ShowPhoto(Image target, string path)
        {
            target.Source = null;
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) return;
            try
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(path, UriKind.Absolute);
                image.EndInit();
                image.Freeze();
                target.Source = image;
            }
            catch { /* leave the picture empty */ }
        }

        /// <summary>Names the stored document, or says none was chosen, and only offers "open" when there is something to open.</summary>
        public static void ShowDoc(Label label, Button viewButton, string path)
        {
            bool has = !string.IsNullOrWhiteSpace(path);
            label.Content = has ? Path.GetFileName(path) : "No file chosen";
            viewButton.Visibility = has ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
