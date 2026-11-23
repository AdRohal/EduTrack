using System.Text.RegularExpressions;
using System.Windows;
using EduTrack.Utilities;

namespace EduTrack
{
    /// <summary>
    /// Drop-in replacement for System.Windows.MessageBox. Because it lives in the EduTrack namespace, every existing
    /// MessageBox.Show call gets the themed dialog. Plain success messages become a non-blocking toast instead.
    /// </summary>
    public static class MessageBox
    {
        private static readonly Regex SuccessPattern = new Regex(
            @"\b(success(fully)?|saved|updated|deleted|imported|downloaded|added|created)\b", RegexOptions.IgnoreCase);

        private static readonly Regex FailurePattern = new Regex(
            @"\b(fail(ed|ure)?|error|cannot|unable)\b", RegexOptions.IgnoreCase);

        public static MessageBoxResult Show(string message) => Show(message, null, MessageBoxButton.OK, MessageBoxImage.None);

        public static MessageBoxResult Show(string message, string caption) => Show(message, caption, MessageBoxButton.OK, MessageBoxImage.None);

        public static MessageBoxResult Show(string message, string caption, MessageBoxButton buttons)
            => Show(message, caption, buttons, MessageBoxImage.None);

        public static MessageBoxResult Show(string message, string caption, MessageBoxButton buttons, MessageBoxImage image)
        {
            bool failure = message != null && FailurePattern.IsMatch(message);
            if (buttons == MessageBoxButton.OK && !failure && image != MessageBoxImage.Error && image != MessageBoxImage.Warning
                && message != null && SuccessPattern.IsMatch(message))
            {
                ToastWindow.Show(Loc.T(message));
                return MessageBoxResult.OK;
            }
            if (failure && image == MessageBoxImage.None) image = MessageBoxImage.Error;
            return AppDialog.Ask(message, caption, buttons, image);
        }
    }
}
