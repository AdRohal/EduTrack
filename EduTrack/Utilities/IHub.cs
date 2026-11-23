using System.Windows;
using System.Windows.Media;

namespace EduTrack.Utilities
{
    /// <summary>A section page with tabs (for example Students: All students / New student).</summary>
    public interface IHub
    {
        void SelectTab(int index);
    }

    /// <summary>A section page that can show one record's details in place of its list.</summary>
    public interface IDetailHost
    {
        void ShowDetail(FrameworkElement view);
        void CloseDetail();
    }

    public static class HubHelper
    {
        public static IDetailHost FindDetailHost(DependencyObject start)
        {
            while (start != null)
            {
                if (start is IDetailHost host) return host;
                start = VisualTreeHelper.GetParent(start) ?? LogicalTreeHelper.GetParent(start);
            }
            return null;
        }

        /// <summary>Walks up from any element to the section page that contains it.</summary>
        public static IHub Find(DependencyObject start)
        {
            while (start != null)
            {
                if (start is IHub hub) return hub;
                start = VisualTreeHelper.GetParent(start) ?? LogicalTreeHelper.GetParent(start);
            }
            return null;
        }
    }
}
