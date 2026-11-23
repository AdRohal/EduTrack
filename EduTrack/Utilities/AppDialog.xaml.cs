using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EduTrack.Utilities
{
    /// <summary>Themed replacement for the system message box.</summary>
    public partial class AppDialog : Window
    {
        private MessageBoxResult _result = MessageBoxResult.None;
        private MessageBoxResult _escapeResult = MessageBoxResult.Cancel;

        private AppDialog() { InitializeComponent(); }

        public static MessageBoxResult Ask(string message, string caption, MessageBoxButton buttons, MessageBoxImage image)
        {
            var dialog = new AppDialog();
            var owner = Application.Current?.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive && w.IsVisible && !(w is AppDialog))
                        ?? Application.Current?.MainWindow;
            if (owner != null && owner.IsVisible && owner != dialog) dialog.Owner = owner;
            else dialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dialog.Build(message, caption, buttons, image);
            dialog.ShowDialog();
            return dialog._result == MessageBoxResult.None ? dialog._escapeResult : dialog._result;
        }

        private void Build(string message, string caption, MessageBoxButton buttons, MessageBoxImage image)
        {
            bool destructive = message != null && message.IndexOf("delete", StringComparison.OrdinalIgnoreCase) >= 0
                               && buttons != MessageBoxButton.OK;

            string glyph = "i"; string fg = "PrimaryBrush"; string bg = "PrimarySoftBrush";
            if (image == MessageBoxImage.Error) { glyph = "!"; fg = "DangerBrush"; bg = "DangerSoftBrush"; }
            else if (image == MessageBoxImage.Warning || destructive)
            {
                glyph = "!";
                fg = destructive ? "DangerBrush" : "AccentBrush";
                bg = destructive ? "DangerSoftBrush" : "AccentSoftBrush";
            }
            else if (image == MessageBoxImage.Question) { glyph = "?"; }

            IconGlyph.Text = glyph;
            IconGlyph.SetResourceReference(TextBlock.ForegroundProperty, fg);
            IconBadge.SetResourceReference(Border.BackgroundProperty, bg);

            TitleText.Text = string.IsNullOrWhiteSpace(caption) ? DefaultTitle(image) : caption;
            MessageText.Text = message ?? string.Empty;

            switch (buttons)
            {
                case MessageBoxButton.OK:
                    AddButton("OK", MessageBoxResult.OK, true, true);
                    _escapeResult = MessageBoxResult.OK;
                    break;
                case MessageBoxButton.OKCancel:
                    AddButton("Cancel", MessageBoxResult.Cancel, false, false);
                    AddButton("OK", MessageBoxResult.OK, true, true);
                    break;
                case MessageBoxButton.YesNo:
                    AddButton(destructive ? "Keep it" : "No", MessageBoxResult.No, false, false);
                    AddButton(destructive ? "Delete" : "Yes", MessageBoxResult.Yes, true, true, destructive);
                    _escapeResult = MessageBoxResult.No;
                    break;
                case MessageBoxButton.YesNoCancel:
                    AddButton("Cancel", MessageBoxResult.Cancel, false, false);
                    AddButton("No", MessageBoxResult.No, false, false);
                    AddButton("Yes", MessageBoxResult.Yes, true, true);
                    break;
            }
        }

        private static string DefaultTitle(MessageBoxImage image)
        {
            switch (image)
            {
                case MessageBoxImage.Error: return "Something went wrong";
                case MessageBoxImage.Warning: return "Check this first";
                case MessageBoxImage.Question: return "Please confirm";
                default: return "EduTrack";
            }
        }

        private void AddButton(string text, MessageBoxResult result, bool primary, bool isDefault, bool danger = false)
        {
            var button = new Button
            {
                Content = text,
                MinWidth = 96,
                Margin = new Thickness(ButtonRow.Children.Count == 0 ? 0 : 10, 0, 0, 0),
                IsDefault = isDefault
            };
            if (primary) button.SetResourceReference(StyleProperty, danger ? "DangerButton" : "PrimaryButton");
            button.Click += (s, e) => { _result = result; Close(); };
            ButtonRow.Children.Add(button);
            if (isDefault) Loaded += (s, e) => button.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) { _result = _escapeResult; Close(); }
        }
    }
}
