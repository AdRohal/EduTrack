using System;
using System.Windows;
using System.Windows.Controls;
using EduTrack.Utilities;
using EduTrack.ViewModel;

namespace EduTrack.View
{
    public partial class Students : UserControl, IHub, IDetailHost
    {
        public Students()
        {
            InitializeComponent();
            Loaded += (s, e) => { if (Pages.Content == null) TabList.IsChecked = true; };
        }

        /// <summary>0 opens the "new" form, anything else opens the list.</summary>
        public void SelectTab(int index)
        {
            if (index == 0) TabNew.IsChecked = true; else TabList.IsChecked = true;
        }

        private void Btn_Checked(object sender, RoutedEventArgs e) => Pages.Content = new NewStudentVM();

        private void Btn_Checked_1(object sender, RoutedEventArgs e) => Pages.Content = new ShowStudentsVM();
        public void ShowDetail(FrameworkElement view)
        {
            Pages.Content = view;
            Pages.BeginAnimation(OpacityProperty, new System.Windows.Media.Animation.DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(160)));
        }

        public void CloseDetail()
        {
            Pages.Content = new ShowStudentsVM();
            TabList.IsChecked = true;
        }

        // Clicking a tab while a record is open brings back that tab's page.
        private void TabList_Click(object sender, RoutedEventArgs e)
        {
            if (!(Pages.Content is ShowStudentsVM)) Pages.Content = new ShowStudentsVM();
        }

        private void TabNew_Click(object sender, RoutedEventArgs e)
        {
            if (!(Pages.Content is NewStudentVM)) Pages.Content = new NewStudentVM();
        }
    }
}