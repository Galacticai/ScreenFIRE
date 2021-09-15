using cult = System.Globalization.CultureInfo;

namespace ScreenFIRE.Modules.Companion {

    /// <summary>
    /// <list type="bullet">
    /// <item> 0 • Other • Any language not listed </item>
    /// <item> 1 • English </item>
    /// <item> 2 • Arabic </item>
    /// <item> 3 • Chinese </item>
    /// </list>
    /// </summary>
    public enum ILanguage {
        Other,

        English,
        //! Later
        Arabic,
        Chinese
    }

    public class Language {
        public static ILanguage GetSystemLanguage()
            => cult.InstalledUICulture.TwoLetterISOLanguageName switch {
                "en" => ILanguage.English,
                "ar" => ILanguage.Arabic,
                "zh" => ILanguage.Chinese,

                _ => ILanguage.Other
            };
    }

}
