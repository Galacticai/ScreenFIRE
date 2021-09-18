using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

using env = System.Environment;


namespace ScreenFIRE.Modules.Companion {

    /// <summary> Common class for general ScreenFIRE stuff </summary>
    static class Common {

        #region ScreenFIRE environment

        /// <summary> (<see cref="env.SpecialFolder.MyPictures"/>) > ScreenFIRE </summary>
        public static string SF
            => Path.Combine(env.GetFolderPath(env.SpecialFolder
                                .MyPictures),
                            "ScreenFIRE");

        /// <summary> (<see cref="env.SpecialFolder.ApplicationData"/>) > ScreenFIRE </summary>
        public static string SF_Data
            => Path.Combine(env.GetFolderPath(env.SpecialFolder
                                .ApplicationData),
                            "ScreenFIRE");

        #endregion


        #region Public Abbreviations 

        /// <summary> Get current copyright string </summary>
        public static string Copyright => FileVersionInfo.GetVersionInfo(
                                                Assembly.GetExecutingAssembly().Location
                                          ).LegalCopyright;

        /// <summary> Get current version field </summary>
        public static Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        /// <summary> Simple .NET new line (<see cref="env.NewLine"/>) <br/>
        /// \r\n for non-Unix platforms, or \n for Unix platforms. </summary>
        public static string n => env.NewLine;
        /// <summary> Double! .NET new line (<see cref="env.NewLine"/>) <br/>
        /// \r\n\r\n for non-Unix platforms, or \n\n for Unix platforms. </summary>
        public static string nn => n + n;
        /// <summary> Simple html new line (&lt;br/&gt;) </summary>
        public static string br => "<br/>";
        /// <summary> Double! Simple html new line (&lt;br/&gt;) </summary>
        public static string brbr => br + br;
        /// <summary> Tab: (Unicode) U+3000 | (HTML) And#12288; | (Description) Ideographic Space </summary>
        public static string tab => @"　";
        /// <summary> Double! Tab: (Unicode) U+3000 | (HTML) And#12288; | (Description) Ideographic Space </summary>
        public static string tabtab => tab + tab;

        #endregion 

    }
}
