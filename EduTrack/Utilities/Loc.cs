using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace EduTrack.Utilities
{
    /// <summary>
    /// Languages: English (the text written in the app), French, Spanish and Arabic (right to left). Screens are translated as they
    /// appear (every label, button and tooltip with fixed text), so a new screen needs no extra work, and the whole app switches at
    /// once without a restart. Text that comes from the data (names, majors...) is never touched.
    /// </summary>
    public static partial class Loc
    {
        public const string English = "en";
        public const string French = "fr";
        public const string Spanish = "es";
        public const string Arabic = "ar";
        public const string Indonesian = "id";
        public const string Chinese = "zh";
        public const string Japanese = "ja";
        public const string Korean = "ko";

        private static bool Supported(string code) => code == French || code == Spanish || code == Arabic || code == Indonesian || code == Chinese || code == Japanese || code == Korean;

        private static readonly string SettingsFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EduTrack", "language.txt");

        public static string Language { get; private set; } = English;

        /// <summary>True for any language other than the English the app is written in.</summary>
        public static bool Translating => Language != English;

        /// <summary>Arabic reads from right to left, so the whole layout is mirrored.</summary>
        public static bool IsRtl => Language == Arabic;

        public static FlowDirection Direction => IsRtl ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

        /// <summary>Culture used only for showing dates and month names; numbers and parsing elsewhere are left alone.</summary>
        public static CultureInfo DisplayCulture
        {
            get
            {
                switch (Language)
                {
                    case French: return CultureInfo.GetCultureInfo("fr-FR");
                    case Spanish: return CultureInfo.GetCultureInfo("es-ES");
                    case Arabic: return CultureInfo.GetCultureInfo("ar-MA");   // Gregorian months, like the rest of the app
                    case Indonesian: return CultureInfo.GetCultureInfo("id-ID");
                    case Chinese: return CultureInfo.GetCultureInfo("zh-CN");
                    case Japanese: return CultureInfo.GetCultureInfo("ja-JP");
                    case Korean: return CultureInfo.GetCultureInfo("ko-KR");
                    default: return CultureInfo.GetCultureInfo("en-US");
                }
            }
        }

        /// <summary>A date the way the chosen language writes it (15 Sep 2025 / 2025年9月15日 / 2025년 9월 15일).</summary>
        public static string ShortDate(DateTime date)
        {
            switch (Language)
            {
                case Chinese: case Japanese: return date.ToString("yyyy年M月d日", DisplayCulture);
                case Korean: return date.ToString("yyyy년 M월 d일", DisplayCulture);
                default: return date.ToString("d MMM yyyy", DisplayCulture);
            }
        }

        public static event Action LanguageChanged;

        // ------------------------------------------------------------------ tables

        private sealed class Pat
        {
            public Regex Rx;
            public Func<Match, string> Make;
        }

        private static readonly Dictionary<string, Dictionary<string, string>> Dictionaries = new Dictionary<string, Dictionary<string, string>>();
        private static readonly Dictionary<string, List<Pat>> Patterns = new Dictionary<string, List<Pat>>();

        private static void P(List<Pat> list, string regex, Func<Match, string> make) =>
            list.Add(new Pat { Rx = new Regex(regex, RegexOptions.Singleline), Make = make });

        static Loc()
        {
            InitFr();
            InitEs();
            InitAr();
            InitId();
            InitZh();
            InitJa();
            InitKo();
            InitClasses();
            InitClassPatterns();
        }

        // ------------------------------------------------------------------ text

        /// <summary>Translates one piece of text (or each line of a multi-line message). Unknown text is returned unchanged.</summary>
        public static string T(string english)
        {
            if (!Translating || string.IsNullOrEmpty(english)) return english;
            if (!Dictionaries.TryGetValue(Language, out var table)) return english;
            if (table.TryGetValue(english, out string exact)) return exact;

            if (Patterns.TryGetValue(Language, out var patterns))
                foreach (var pattern in patterns)
                {
                    var match = pattern.Rx.Match(english);
                    if (match.Success) return pattern.Make(match);
                }

            if (english.IndexOf('\n') >= 0)
            {
                var lines = english.Split('\n');
                for (int i = 0; i < lines.Length; i++) lines[i] = T(lines[i]);
                return string.Join("\n", lines);
            }

            // "Some message: <detail>" where only the first part is ours.
            int colon = english.IndexOf(": ", StringComparison.Ordinal);
            if (colon > 0 && table.TryGetValue(english.Substring(0, colon + 1), out string head))
                return head + " " + english.Substring(colon + 2);
            return english;
        }

        // ------------------------------------------------------------------ language choice

        public static void Initialize()
        {
            try
            {
                if (File.Exists(SettingsFile))
                {
                    string saved = File.ReadAllText(SettingsFile).Trim().ToLowerInvariant();
                    if (Supported(saved)) Language = saved;
                }
            }
            catch { /* English */ }

            // WPF does not deliver Loaded to children through a class handler, so each window is re-checked after its layout
            // changes (a new page, a new tab, a list filling up). Cheap: a text that is already translated is skipped.
            EventManager.RegisterClassHandler(typeof(Window), FrameworkElement.LoadedEvent, new RoutedEventHandler((s, e) =>
            {
                var window = (Window)s;
                Prepare(window);
                if (Translating) Retranslate(window);
                window.LayoutUpdated += (a, b) => Schedule(window);
            }));
        }

        private static void Prepare(Window window)
        {
            window.Language = XmlLanguage.GetLanguage(DisplayCulture.IetfLanguageTag);
            window.FlowDirection = Direction;
        }

        public static void SetLanguage(string code)
        {
            if (!Supported(code)) code = English;
            if (code == Language) return;
            Language = code;
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(SettingsFile));
                File.WriteAllText(SettingsFile, code);
            }
            catch { /* the preference is optional */ }

            foreach (Window window in Application.Current.Windows)
            {
                Prepare(window);
                Retranslate(window);
            }
            LanguageChanged?.Invoke();
        }

        private static readonly HashSet<Window> Pending = new HashSet<Window>();

        private static void Schedule(Window window)
        {
            if (!Translating || !Pending.Add(window)) return;
            window.Dispatcher.BeginInvoke(new Action(() =>
            {
                Pending.Remove(window);
                if (Translating) Retranslate(window);
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        // ------------------------------------------------------------------ applying to the screen

        // What an element's text was written as, and what we last put there. If the app changes the text by itself later
        // (a status line, say), the new text becomes the new original.
        private sealed class Memory { public string Source, Shown; }
        private sealed class Slots { public Memory Text = new Memory(), Content = new Memory(), Header = new Memory(), Tip = new Memory(), Title = new Memory(); }
        private static readonly ConditionalWeakTable<DependencyObject, Slots> Seen = new ConditionalWeakTable<DependencyObject, Slots>();

        private static void Retranslate(DependencyObject root)
        {
            if (root == null) return;
            if (root is FrameworkElement fe) TranslateElement(fe);

            if (root is Visual || root is System.Windows.Media.Media3D.Visual3D)
            {
                int count = VisualTreeHelper.GetChildrenCount(root);
                for (int i = 0; i < count; i++) Retranslate(VisualTreeHelper.GetChild(root, i));
            }
            // Items that are not drawn yet (closed drop-downs, tabs) live only in the logical tree.
            foreach (object child in LogicalTreeHelper.GetChildren(root))
                if (child is DependencyObject d && !(d is Visual && VisualTreeHelper.GetParent(d) == root)) Retranslate(d);
        }

        private static string Convert(Memory memory, string current)
        {
            string source = memory.Shown != null && current == memory.Shown ? memory.Source : current;
            string shown = T(source);
            memory.Source = source;
            memory.Shown = shown;
            return shown;
        }

        private static void TranslateElement(FrameworkElement element)
        {
            try
            {
                // Mirroring flips pictures too, which would turn photos, the QR code and documents into mirror images.
                if (IsRtl && (element is Image || element is Avatar || element is LogoMark) && element.FlowDirection != FlowDirection.LeftToRight)
                    element.FlowDirection = FlowDirection.LeftToRight;

                var slots = Seen.GetOrCreateValue(element);

                if (element is TextBlock block && !BindingOperations.IsDataBound(block, TextBlock.TextProperty) && block.Inlines.Count <= 1)
                {
                    string next = Convert(slots.Text, block.Text);
                    if (next != block.Text) block.Text = next;
                }
                else if (element is ContentControl content && content.Content is string text
                         && !BindingOperations.IsDataBound(content, ContentControl.ContentProperty))
                {
                    string next = Convert(slots.Content, text);
                    if (next != text) content.Content = next;
                }

                if (element is HeaderedContentControl headered && headered.Header is string header
                    && !BindingOperations.IsDataBound(headered, HeaderedContentControl.HeaderProperty))
                {
                    string next = Convert(slots.Header, header);
                    if (next != header) headered.Header = next;
                }

                if ((element is TextBox || element is ComboBox) && element.Tag is string hint)
                {
                    string next = Convert(slots.Title, hint);
                    if (next != hint) element.Tag = next;
                }

                if (element.ToolTip is string tip)
                {
                    string next = Convert(slots.Tip, tip);
                    if (next != tip) element.ToolTip = next;
                }

                if (element is Window window && !BindingOperations.IsDataBound(window, Window.TitleProperty))
                {
                    string next = Convert(slots.Title, window.Title);
                    if (next != window.Title) window.Title = next;
                }
            }
            catch { /* a label left in English is better than a crash */ }
        }
    }
}
