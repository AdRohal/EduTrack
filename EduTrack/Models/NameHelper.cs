using System;

namespace EduTrack.Models
{
    public static class NameHelper
    {
        /// <summary>"Sara" + "El Amrani" becomes "SE". Used for avatars when a person has no photo.</summary>
        public static string Initials(string first, string last)
        {
            string a = string.IsNullOrWhiteSpace(first) ? "" : first.Trim().Substring(0, 1);
            string b = string.IsNullOrWhiteSpace(last) ? "" : last.Trim().Substring(0, 1);
            string result = (a + b).ToUpperInvariant();
            return result.Length == 0 ? "?" : result;
        }

        public static string Join(params string[] parts)
        {
            return string.Join(" ", Array.FindAll(parts, p => !string.IsNullOrWhiteSpace(p)));
        }
    }
}
