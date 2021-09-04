
using System.Drawing;

namespace GeekAssistant.Modules.General {

    /// <summary>
    /// Inform the user using Info() form by pulling info from inf + implicitly or explicity determining the icon/color
    /// </summary>
    internal static class inf { //inform

        #region Info

        public static bool infoAnswer = false;

        /// <summary>
        /// <list>
        /// <item>Current Error lvl and msg </item>
        /// <item>Set while a process is running;</item>
        /// <item>When an Exception is thrown the (info) form will use this ErrorInfo</item>
        /// </list>
        /// </summary>
        public static (string code, lvls lvl, string title, string msg, string fullFatalError) detail;
        /// <summary> YesButton can be null or empty </summary>
        public static (string YesButton, string NoButton) YesNoButtons;
        /// <summary> Use index [0] as light color and [1] as dark color </summary>
        public static (Image[] icon, Color[] color) theme;
        public enum lvls { Information = -1, Warn = 0, Error = 1, FatalError = 10, Question = 2 }

        #endregion

        private static string detailcode => string.IsNullOrEmpty(detail.code) ? detail.title : $"❰{detail.code}❱";
        public static string WindowTitle
            => detail.lvl switch {
                lvls.Warn => $" ⚠  Warn: {detailcode} — Geek Assistant", // 0
                lvls.Error => $" ❌  Error: {detailcode} — Geek Assistant", // 1 // 10
                lvls.FatalError => $" ❌  Fatal Error: {detailcode} — Geek Assistant",
                _ => $"{detail.title} — Geek Assistant"  // -1 and 2
            };

        /// <summary>
        /// Inform the user using Info() form by pulling info from inf + implicitly or explicity determining the icon/color
        /// </summary>
        /// <param name="infDetail">info details provided by inf.detail</param> 
        /// <param name="YesNoButtons">Text of the Left(true) and right(false) buttons(YesButton, NoButton)</param>
        /// <returns>True if YesButton was clicked or False if NoButton was clicked</returns>
        public static bool Run((string code, lvls lvl, string title, string msg, string fullFatalError) infDetail,
                               (string YesButton, string NoButton) YesNoButtons = default) {
            if (!string.IsNullOrEmpty(infDetail.code)) detail.code = infDetail.code;
            return Run(infDetail.lvl, infDetail.title, infDetail.msg, YesNoButtons, default, infDetail.fullFatalError);
        }

        /// <summary>
        /// Inform the user using Info() form by pulling info from inf + implicitly or explicity determining the icon/color
        /// </summary> 
        /// <param name="YesNoButtons">Text of the Left(true) and right(false) buttons(YesButton, NoButton)</param>
        /// <returns>True if YesButton was clicked or False if NoButton was clicked</returns>
        public static bool Run((string YesButton, string NoButton) YesNoButtons = default)
                => Run(detail.lvl, detail.title, detail.msg, YesNoButtons);
        /// <summary>
        /// Inform the user using Info() form by pulling info from inf + implicitly or explicity determining the icon/color
        /// </summary>
        /// <param name="_lvl">info level as System.Windows.Forms.DialogResult lvls</param>
        /// <param name="_title">Title of the question</param>
        /// <param name="_msg">More text to view below the title</param>
        /// <param name="_fullFatalError">Fatal error text (full error)</param>  
        /// <returns>True if YesButton was clicked or False if NoButton was clicked</returns>
        public static bool Run(lvls _lvl, string _title, string _msg, string _fullFatalError)
                => Run(_lvl, _title, _msg, default, default, _fullFatalError);

        public static bool Run(lvls _lvl,
                               string _title, string _msg,
                               (string YesButton, string NoButton) _YesNoButtons = default,
                               (Image[] icon, Color[] color) _theme = default,
                               string _fullFatalError = null) {
            //reset 
            infoAnswer = false;
            //set 
            detail = (detail.code, _lvl, _title, _msg, _fullFatalError);
            YesNoButtons = _YesNoButtons;
            theme = _theme;

            new Info().ShowDialog();
            return infoAnswer;
        }
    }
}