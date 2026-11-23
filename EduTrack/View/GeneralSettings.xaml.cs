using System.Windows;
using System.Windows.Controls;
using EduTrack.Utilities;

namespace EduTrack.View
{
    public partial class GeneralSettings : UserControl
    {
        private bool _ready;

        public GeneralSettings()
        {
            InitializeComponent();
            switch (ThemeManager.Current)
            {
                case ThemeManager.Paper: PaperTile.IsChecked = true; break;
                case ThemeManager.Slate: SlateTile.IsChecked = true; break;
                default: ClassicTile.IsChecked = true; break;
            }
            _ready = true;
        }

        private void Classic_Checked(object sender, RoutedEventArgs e) { if (_ready) ThemeManager.Apply(ThemeManager.Classic); }

        private void Paper_Checked(object sender, RoutedEventArgs e) { if (_ready) ThemeManager.Apply(ThemeManager.Paper); }

        private void Slate_Checked(object sender, RoutedEventArgs e) { if (_ready) ThemeManager.Apply(ThemeManager.Slate); }
    }
}
