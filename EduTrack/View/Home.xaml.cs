using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows;

namespace EduTrack.View
{
    public partial class Home : UserControl, INotifyPropertyChanged
    {
        private int _currentSlideIndex = 1;
        private const int TotalSlides = 4;

        public int CurrentSlideIndex
        {
            get { return _currentSlideIndex; }
            set
            {
                if (_currentSlideIndex != value)
                {
                    _currentSlideIndex = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CurrentSlideIndicator));
                }
            }
        }

        public string CurrentSlideIndicator => $"{CurrentSlideIndex}/{TotalSlides}";

        public Home()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentSlideIndex == TotalSlides)
                CurrentSlideIndex = 1;
            else
                CurrentSlideIndex++;

            UpdateSlideVisibility();
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
          if (CurrentSlideIndex == 1)
                CurrentSlideIndex = TotalSlides;
            else
                CurrentSlideIndex--;

            UpdateSlideVisibility();
        }

        private void UpdateSlideVisibility()
        {
            SlideImage1.Visibility = CurrentSlideIndex == 1 ? Visibility.Visible : Visibility.Collapsed;
            SlideImage2.Visibility = CurrentSlideIndex == 2 ? Visibility.Visible : Visibility.Collapsed;
            SlideImage3.Visibility = CurrentSlideIndex == 3 ? Visibility.Visible : Visibility.Collapsed;
            SlideImage4.Visibility = CurrentSlideIndex == 4 ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
