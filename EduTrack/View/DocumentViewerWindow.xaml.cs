using System;
using System.Windows;

namespace EduTrack.View
{
    public partial class DocumentViewerWindow : Window
    {
        public DocumentViewerWindow(string imagePath)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(imagePath))
            {
                documentImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(imagePath));
            }
        }
    }
}
