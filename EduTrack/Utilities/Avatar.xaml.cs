using System.Windows;
using System.Windows.Controls;

namespace EduTrack.Utilities
{
    /// <summary>Round profile picture that falls back to the person's initials when there is no photo.</summary>
    public partial class Avatar : UserControl
    {
        private static readonly PhotoBrushConverter Converter = new PhotoBrushConverter();

        public static readonly DependencyProperty PhotoProperty = DependencyProperty.Register(
            nameof(Photo), typeof(string), typeof(Avatar), new PropertyMetadata(null, (d, e) => ((Avatar)d).Refresh()));

        public static readonly DependencyProperty InitialsProperty = DependencyProperty.Register(
            nameof(Initials), typeof(string), typeof(Avatar), new PropertyMetadata(null, (d, e) => ((Avatar)d).Refresh()));

        public string Photo { get => (string)GetValue(PhotoProperty); set => SetValue(PhotoProperty, value); }
        public string Initials { get => (string)GetValue(InitialsProperty); set => SetValue(InitialsProperty, value); }

        public Avatar()
        {
            InitializeComponent();
            SizeChanged += (s, e) => InitialsText.FontSize = System.Math.Max(10, ActualHeight * 0.38);
        }

        private void Refresh()
        {
            InitialsText.Text = Initials;
            PhotoEllipse.Fill = Converter.Convert(Photo, typeof(System.Windows.Media.Brush), null, null) as System.Windows.Media.Brush;
        }
    }
}
