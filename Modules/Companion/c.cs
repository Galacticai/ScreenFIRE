using System;
using System.IO;

namespace ScreenFIRE.Modules.Companion {

    /// <summary> Common class for general ScreenFIRE stuff </summary>
    class c {

        public static string SF
            => Path.Combine(
                    new[] {
                        Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                        "Pictures", "ScreenFIRE"
                    }
                );

        public static string SF_Data
            => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            "ScreenFIRE");


        /// <summary> Indicate if the current platform is Unix based </summary>
        public static bool RunningLinux
            => Environment.OSVersion.Platform == PlatformID.Unix;

        ///// <summary> Indicate if the current platform is Win32NT based (Windows NT and above) </summary>
        //public static bool RunningWindows
        //    => Environment.OSVersion.Platform == PlatformID.Win32NT;





        #region Public Abbreviations 

        /// <summary> Get current copyright string </summary>
        public static readonly string copyright = System.Diagnostics.FileVersionInfo.GetVersionInfo(
                                                        System.Reflection.Assembly.GetExecutingAssembly().Location
                                                    ).LegalCopyright;
        /// <summary> Get current version field </summary>
        public static readonly Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        /// <summary> Simple .NET new line (Environment.NewLine) </summary>
        public static readonly string n = Environment.NewLine;
        /// <summary> Simple html new line (&lt;br/&gt;) </summary>
        public static readonly string br = "<br/>";
        /// <summary> Tab: (Unicode) U+3000 | (HTML) And#12288; | (Description) Ideographic Space </summary>
        public static readonly string tab = @"　";
        /// <summary> Double Tab: (Unicode) U+3000 | (HTML) And#12288; | (Description) Ideographic Space </summary>
        public static readonly string tab_double = @"　　";

        #endregion
    }
}
