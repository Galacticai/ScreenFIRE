using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;


namespace ScreenFIRE.Modules.Companion {

    /// <summary> Common class for general ScreenFIRE stuff </summary>
    static class Common {

        #region ScreenFIRE environment
        /// <summary> (<see cref="Environment.SpecialFolder.UserProfile"/>) </summary>
        public static string UserProfile => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        /// <summary> (<see cref="Environment.SpecialFolder.MyPictures"/>) > ScreenFIRE </summary>
        public static string SF
            => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder
                                .MyPictures),
                            "ScreenFIRE");

        /// <summary> (<see cref="env.SpecialFolder.ApplicationData"/>) > ScreenFIRE </summary>
        public static string SF_Data
            => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder
                                .ApplicationData),
                            "ScreenFIRE");

        #endregion


        #region Public Abbreviations

        /// <returns> Current copyright string </returns>
        public static string Copyright => FileVersionInfo.GetVersionInfo(
                                                Assembly.GetExecutingAssembly().Location
                                          ).LegalCopyright;

        /// <returns> Get current version field </summary>
        public static Version Version => Assembly.GetExecutingAssembly().GetName().Version;

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
