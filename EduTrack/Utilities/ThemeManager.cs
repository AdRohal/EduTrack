using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace EduTrack.Utilities
{
    /// <summary>
    /// Switches between the colour themes and remembers the choice.
    /// Classic = the original dark blue look (default), Paper = light green, Slate = dark green.
    /// </summary>
    public static class ThemeManager
    {
        public const string Classic = "classic";
        public const string Paper = "paper";
        public const string Slate = "slate";

        private static readonly string[] Order = { Classic, Paper, Slate };

        private static readonly string SettingsFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EduTrack", "theme.txt");

        public static string Current { get; private set; } = Classic;

        /// <summary>True for the themes with a dark page background (everything except Paper).</summary>
        public static bool IsDark => Current != Paper;

        public static event Action ThemeChanged;

        /// <summary>Applies the saved theme (Classic when nothing was saved). Call once at startup.</summary>
        public static void Initialize()
        {
            string theme = Classic;
            try
            {
                if (File.Exists(SettingsFile))
                {
                    string saved = File.ReadAllText(SettingsFile).Trim().ToLowerInvariant();
                    if (saved == "light") saved = Paper;      // values written by the first redesign
                    else if (saved == "dark") saved = Slate;
                    if (Order.Contains(saved)) theme = saved;
                }
            }
            catch { /* fall back to Classic */ }
            Apply(theme, save: false);
        }

        /// <summary>Moves to the next theme in the list; used by the quick button in the sidebar.</summary>
        public static void Toggle() => Apply(Order[(Array.IndexOf(Order, Current) + 1) % Order.Length], save: true);

        public static void Apply(string theme, bool save = true)
        {
            if (!Order.Contains(theme)) theme = Classic;
            string file = theme == Paper ? "Theme.Light.xaml" : theme == Slate ? "Theme.Dark.xaml" : "Theme.Classic.xaml";
            var next = new ResourceDictionary { Source = new Uri("Styles/" + file, UriKind.Relative) };

            var dictionaries = Application.Current.Resources.MergedDictionaries;
            var current = dictionaries.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Styles/Theme."));
            if (current != null) dictionaries[dictionaries.IndexOf(current)] = next;
            else dictionaries.Insert(0, next);

            Current = theme;
            if (save)
            {
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(SettingsFile));
                    File.WriteAllText(SettingsFile, theme);
                }
                catch { /* the preference is optional */ }
            }
            ThemeChanged?.Invoke();
        }
    }
}
