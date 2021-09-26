using ScreenFIRE.Assets;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;


namespace ScreenFIRE.Modules.Companion {

    /// <summary> Common class for general ScreenFIRE stuff </summary>
    static class Common {

        #region Abbreviations for ScreenFIRE
        /// <summary> (<see cref="Environment.SpecialFolder.UserProfile"/>) </summary>
        public static string UserProfile
            => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        /// <summary> (<see cref="Environment.SpecialFolder.MyPictures"/>) </summary>
        public static string PicturesDir
            => Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        /// <summary> (<see cref="Environment.SpecialFolder.ApplicationData"/>) </summary>
        public static string AppData
            => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        /// <summary> (<see cref="Environment.SpecialFolder.MyPictures"/>) >> ScreenFIRE </summary>
        public static string SF => Path.Combine(PicturesDir, "ScreenFIRE");

        /// <summary> (<see cref="Environment.SpecialFolder.ApplicationData"/>) >> ScreenFIRE </summary>
        public static string SF_Data => Path.Combine(AppData, "ScreenFIRE");

        /// <returns> GNU GPL v3.0 license link <c>(https://www.gnu.org/licenses/gpl-3.0.html)</c> </returns>
        public const string SF_License = "https://www.gnu.org/licenses/gpl-3.0.html";

        /// <returns> ScreenFIRE repository link <c>(https://github.com/NHKomaiha/ScreenFIRE)</c> </returns>
        public const string SF_GitRepo = "https://github.com/NHKomaiha/ScreenFIRE";
        /// <returns> ScreenFIRE repository >> issues link <c>(https://github.com/NHKomaiha/ScreenFIRE/issues)</c> </returns>
        public const string SF_GitRepo_issues = $"{SF_GitRepo}/issues";

        #endregion


        #region General Abbreviations

        /// <returns> Get current version field </summary>
        public static Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public static string VersionString(bool MajMin_only = false, bool includePhase = true) {
            Version version = Version; //! Get once
            return $"{(MajMin_only ? string.Empty : "v")}"
                 + $"{version.Major}.{version.Minor}"
                 + (includePhase & !MajMin_only ? $" #{PhaseString()}" : string.Empty);
        }
        public static string PhaseString()
            => Version.Build switch {
                2 => Strings.Fetch(IStrings.Development).Result,
                1 => Strings.Fetch(IStrings.Beta).Result,
                _ => Strings.Fetch(IStrings.Public).Result,
            };

        /// <returns> Current copyright string </returns>
        public static string Copyright => FileVersionInfo.GetVersionInfo
                                                (Assembly.GetExecutingAssembly().Location)
                                            .LegalCopyright;

        /// <returns> Simple .NET new line (<see cref="Environment.NewLine"/>) <br/>
        /// \r\n for non-Unix platforms, or \n for Unix platforms. </returns>
        public static string n = Environment.NewLine;
        /// <summary> Double! .NET new line (<see cref="Environment.NewLine"/>) <br/>
        /// \r\n\r\n for non-Unix platforms, or \n\n for Unix platforms. </returns>
        public static string nn = n + n;

        /// <summary> Ideographic Space (Tab)</summary>
        /// <returns> ( <c>\u3000</c> )</returns>
        public const string tab = "\u3000";

        ///<summary> Narrow no-break space </summary>
        /// <returns> (ex: 32GB -> 32 GB ) ( <c>\u202F</c> )</returns>
        public const string NarrowSpace = "\u202F";

        ///<summary> Horizontal Ellipses </summary>
        /// <returns> (3 dots) ( … ) ( <c>\u2026</c> )</returns>
        public const string Ellipses = @"…"; //"\u2026";
        ///<summary> Vertical Ellipses </summary>
        /// <returns> (Vertical 3 dots) ( ︙ ) ( <c>\uFE19</c> )</returns>
        public const string Ellipses_Vertical = @"︙"; //"\uFE19";

        ///<summary> Right single quotation mark </summary>
        /// <returns> ( ’ ) ( <c>\u2019</c> )</returns>
        public const string Apostrophe = @"’"; //"\u2019";

        ///<summary> Curved left double quotation </summary>
        /// <returns> ( “ ) ( <c>\u201C</c> )</returns>
        public const string DoubleQuotation_Left = @"“"; //"\u201C";
        ///<summary> Curved right double quotation </summary>
        /// <returns> ( ” ) ( <c>\u201D</c> )</returns>
        public const string DoubleQuotation_Right = @"”"; //"\u201D";

        ///<summary> Ratio symbol </summary>
        /// <returns> ( ∶ ) ( <c>\u2236</c> )</returns>
        public const string Ratio = @"∶"; //"\u2236";

        ///<summary> Bullet symbol </summary>
        /// <returns> ( • ) ( <c>\u2022</c> )</returns>
        public const string Bullet = @"•"; //"\u2022";

        ///<summary> En dash </summary>
        /// <returns> ( – ) ( <c>\u2013</c> ) </returns>
        public const string RangeDash = @"–"; //"\u2013";

        #endregion

    }
}
