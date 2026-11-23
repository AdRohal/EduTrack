using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;

namespace EduTrack.Utilities
{
    /// <summary>Checks the few fields every person record needs and says what to fix, next to the Save button.</summary>
    public static class FormValidation
    {
        public static bool Validate(FrameworkElement root, TextBox firstName, TextBox lastName, TextBox email)
        {
            var errorLabel = root.FindName("FormError") as TextBlock;

            bool Fail(string message, Control field)
            {
                if (errorLabel != null) errorLabel.Text = message;
                field.BringIntoView();
                field.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(firstName.Text)) return Fail("Enter the first name.", firstName);
            if (string.IsNullOrWhiteSpace(lastName.Text)) return Fail("Enter the last name.", lastName);
            if (!string.IsNullOrWhiteSpace(email.Text) && !IsEmail(email.Text.Trim()))
                return Fail("That email address doesn't look right. It should look like name@example.com.", email);

            if (errorLabel != null) errorLabel.Text = string.Empty;
            return true;
        }

        private static bool IsEmail(string value)
        {
            try { return new MailAddress(value).Address == value; }
            catch { return false; }
        }
    }
}
