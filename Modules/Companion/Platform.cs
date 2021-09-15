using System;
using System.Globalization;


namespace ScreenFIRE.Modules.Companion {

    /// <summary> Common class for general ScreenFIRE stuff </summary>
    static class Platform {


        private static string CurrentLang => CultureInfo.InstalledUICulture.TwoLetterISOLanguageName;


        public static bool CurrentLangIsEnglish => CurrentLang == "en";
        public static bool CurrentLangIsArabic => CurrentLang == "ar";
        public static bool CurrentLangIsChinese => CurrentLang == "zh";


        /// <summary> Indicate if the current platform is Unix based </summary>
        public static bool RunningLinux
            => Environment.OSVersion.Platform == PlatformID.Unix;

        /// <summary> Indicate if the current platform is Win32NT based (Windows NT and above) </summary>
        public static bool RunningWindows
            => Environment.OSVersion.Platform == PlatformID.Win32NT;

    }
}
