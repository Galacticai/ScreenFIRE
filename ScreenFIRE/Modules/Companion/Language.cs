using cult = System.Globalization.CultureInfo;

namespace ScreenFIRE.Modules.Companion {

    /// <summary>
    /// <list type="bullet">
    /// <item> 0 • System • Current system language (<see cref="English"/> if system language not supported) </item>
    /// <item> 1 • English </item>
    /// <item> 2 • Arabic </item>
    /// <item> 3 • Chinese </item>
    /// <item> 4 • Other • Any language not listed </item>
    /// </list>
    /// </summary>
    public enum ILanguage {
        System,

        English,
        Arabic,
        Chinese,

        Other,
    }

    public class Language {

        public static string TwoLetterISOLanguageName
            => cult.InstalledUICulture.TwoLetterISOLanguageName;

        #region Shortcuts
        public static bool CurrentLangIsEnglish => TwoLetterISOLanguageName == "en";
        public static bool CurrentLangIsArabic => TwoLetterISOLanguageName == "ar";
        public static bool CurrentLangIsChinese => TwoLetterISOLanguageName == "zh";
        #endregion

        public static ILanguage GetSystemLanguage()
            => TwoLetterISOLanguageName switch {
                "en" => ILanguage.English,
                "ar" => ILanguage.Arabic,
                "zh" => ILanguage.Chinese,

                _ => ILanguage.Other
            };
    }

}
