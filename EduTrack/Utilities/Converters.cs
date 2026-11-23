using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace EduTrack.Utilities
{
    /// <summary>Shows the element when the bound number is zero (used for "nothing here yet" states).</summary>
    public class ZeroToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int n = value is int i ? i : 0;
            return n == 0 ? Visibility.Visible : Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }

    /// <summary>Loads a photo from a file path without locking the file; returns null when the file is missing.</summary>
    public class PhotoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value as string;
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) return null;
            try
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.DecodePixelWidth = 160;
                image.UriSource = new Uri(path, UriKind.Absolute);
                image.EndInit();
                image.Freeze();
                return image;
            }
            catch { return null; }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }

    /// <summary>Formats a nullable date as "12 Mar 2024", or an en dash when empty.</summary>
    public class ShortDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is DateTime d ? d.ToString("d MMM yyyy", CultureInfo.InvariantCulture) : "–";
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }

    /// <summary>Empty text shows an en dash so tables never have blank-looking cells.</summary>
    public class DashIfEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => string.IsNullOrWhiteSpace(value as string) ? "–" : value;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}

namespace EduTrack.Utilities
{
    /// <summary>Same as PhotoConverter but returns an ImageBrush, ready to fill a circle.</summary>
    public class PhotoBrushConverter : System.Windows.Data.IValueConverter
    {
        private static readonly PhotoConverter Photo = new PhotoConverter();

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var source = Photo.Convert(value, typeof(System.Windows.Media.ImageSource), null, culture) as System.Windows.Media.ImageSource;
            if (source == null) return null;
            var brush = new System.Windows.Media.ImageBrush(source) { Stretch = System.Windows.Media.Stretch.UniformToFill };
            brush.Freeze();
            return brush;
        }
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
            => throw new System.NotSupportedException();
    }
}

namespace EduTrack.Utilities
{
    /// <summary>True when the bound value is not null. Used to enable "Open" only when a row is selected.</summary>
    public class NotNullConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) => value != null;
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
            => throw new System.NotSupportedException();
    }
}
