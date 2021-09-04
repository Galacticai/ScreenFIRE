using MetroFramework;
using System.Drawing;

namespace GeekAssistant.Modules.General.Companion.Style {
    /// <summary> Dynamically selected colors according to global Light/Dark c.S.DarkTheme & !instance.inverseTheme </summary>  
    internal static class colors {

        public struct UI {
            /// <returns> #ffffff &gt; #111111</returns>
            public static Color bg(bool anti = false) => c.theme(anti) ? constColors.UI.bg_Dark : constColors.UI.bg;
            /// <returns> #000000 &gt; #ffffff</returns>
            public static Color fg(bool anti = false) => c.theme(anti) ? constColors.UI.fg_Dark : constColors.UI.fg;
            /// <returns> <see cref="MetroThemeStyle.Light"/> &gt; <see cref="MetroThemeStyle.Dark"/></returns>
            public static MetroThemeStyle Metro(bool anti = false) => c.theme(anti) ? MetroThemeStyle.Dark : MetroThemeStyle.Light;

            public struct Buttons {
                /// <returns> #f5f5f5 &gt; #202020</returns>
                public static Color BarBG(bool anti = false) => c.theme(anti) ? constColors.UI.Buttons.BarBG_Dark : constColors.UI.Buttons.BarBG;
                public struct FlatAppearance {
                    /// <returns> #c0c0c0 &gt; #404040</returns>
                    public static Color MouseOverBackColor(bool anti = false) => c.theme(anti) ? constColors.UI.Buttons.FlatAppearance.MouseOverBackColor_Dark : constColors.UI.Buttons.FlatAppearance.MouseOverBackColor;
                    /// <returns> #404040 &gt; #646464</returns>
                    public static Color MouseDownBackColor(bool anti = false) => c.theme(anti) ? constColors.UI.Buttons.FlatAppearance.MouseDownBackColor_Dark : constColors.UI.Buttons.FlatAppearance.MouseDownBackColor;
                }
            }

            public struct SwitchButton {
                /// <returns>(<see cref="Buttons.BarBG"/>) : #c0c0c0 &gt; #404040</returns>
                public static Color bg(bool anti = false) => Buttons.BarBG(anti);
                /// <returns>(<see cref="Misc.Green"/>) : A=128 :  #008020 &gt; #5FBF77</returns>
                public static Color bg_Hover(bool anti = false) => Color.FromArgb(128, Misc.Green(anti));
                /// <returns>(<see cref="Misc.Green"/>) : #008020 &gt; #5FBF77</returns>
                public static Color bg_Active(bool anti = false) => Misc.Green(anti);
            }
        }

        public struct Misc {
            /// <returns>#008020 &gt; #5FBF77</returns>
            public static Color Green(bool anti = false) => c.theme(anti) ? constColors.Misc.Green_Dark : constColors.Misc.Green;
            /// <returns>#800080 &gt; #FF7AFF</returns>
            public static Color Purple(bool anti = false) => c.theme(anti) ? constColors.Misc.Purple_Dark : constColors.Misc.Purple;
        }

        public struct inf {
            /// <summary> Derive color automatically using <see cref="global::inf.lvls"/></summary>
            /// <returns><list type="bullet">
            /// <item>-1 = <see cref="global::inf.lvls.Information"/> = <see cref="infBlue"/> = #005073 &gt; #bfecff</item>
            /// <item>0 = <see cref="global::inf.lvls.Warn"/> = <see cref="warnYellow"/> = #735400 &gt; #ffeebf</item>
            /// <item>1 = <see cref="global::inf.lvls.Error"/> = <see cref="errRed"/> = #9a0000 &gt; #ffbfbf</item>
            /// <item>10 = <see cref="global::inf.lvls.FatalError"/> = <see cref="errRed"/> = #9a0000 &gt; #ffbfbf</item>
            /// <item>2 = <see cref="global::inf.lvls.Question"/> = <see cref="questBlue"/> = #406d80 &gt; #bfdfff</item>
            /// </list></returns>
            public static Color From_inflvls(General.inf.lvls lvls)
                => lvls switch {
                    General.inf.lvls.Warn => warnYellow(), // 0
                    General.inf.lvls.Error => errRed(), // 1
                    General.inf.lvls.FatalError => errRed(), // 10
                    General.inf.lvls.Question => questBlue(), // 2
                    _ => infBlue()  // -1
                };
            /// <returns>#005073 &gt; #bfecff</returns>
            public static Color infBlue(bool anti = false) => c.theme(anti) ? constColors.infColorRes.infBlue_Dark : constColors.infColorRes.infBlue;
            /// <returns>#735400 &gt; #ffeebf</returns>
            public static Color warnYellow(bool anti = false) => c.theme(anti) ? constColors.infColorRes.warnYellow_Dark : constColors.infColorRes.warnYellow;
            /// <returns>#9a0000 &gt; #ffbfbf</returns>
            public static Color errRed(bool anti = false) => c.theme(anti) ? constColors.infColorRes.errRed_Dark : constColors.infColorRes.errRed;
            /// <returns>#406d80 &gt; #bfdfff</returns>
            public static Color questBlue(bool anti = false) => c.theme(anti) ? constColors.infColorRes.questBlue_Dark : constColors.infColorRes.questBlue;
        }

        public struct Iconcolors {
            /// <returns>#665200 &gt; #bfcdff</returns>
            public static Color SwitchTheme(bool anti = false) => c.theme(anti) ? constColors.Iconcolors.SwitchTheme_Dark : constColors.Iconcolors.SwitchTheme;
            /// <returns>#FFF4BF &gt; #293666</returns>
            public static Color SwitchTheme_bg(bool anti = false) => c.theme(anti) ? constColors.Iconcolors.SwitchTheme_Dark_bg : constColors.Iconcolors.SwitchTheme_bg;
            /// <returns>#006647 &gt; #BFFFBF</returns>
            public static Color Smile(bool anti = false) => c.theme(anti) ? constColors.Iconcolors.Smile_Dark : constColors.Iconcolors.Smile;
            /// <returns>#006A80 &gt; #BFF4FF</returns>
            public static Color Settings(bool anti = false) => c.theme(anti) ? constColors.Iconcolors.Settings_Dark : constColors.Iconcolors.Settings;
            /// <returns>#006647 &gt; #B2FFDC</returns>
            public static Color ToU(bool anti = false) => c.theme(anti) ? constColors.Iconcolors.ToU_Dark : constColors.Iconcolors.ToU;
            /// <returns>#800057 &gt; #ffbfd9</returns>
            public static Color Donate(bool anti = false) => c.theme(anti) ? constColors.Iconcolors.Donate_Dark : constColors.Iconcolors.Donate;
            /// <returns>#1F4099 &gt; #C0D0FF</returns>
            public static Color Commands(bool anti = false) => c.theme(anti) ? constColors.Iconcolors.Commands_Dark : constColors.Iconcolors.Commands;
        }


        /// <summary> Constant colors not affected by themes or anything </summary>
        public struct constColors {
            public struct UI {
                /// <summary> #fdfffd </summary>
                public static Color bg { get => ColorTranslator.FromHtml("#fdfffd"); }
                /// <summary> #0f110f </summary>
                public static Color bg_Dark { get => ColorTranslator.FromHtml("#0f110f"); }

                /// <summary> #051005 </summary>
                public static Color fg { get => ColorTranslator.FromHtml("#051005"); }
                /// <summary> #ffffff </summary>
                public static Color fg_Dark { get => ColorTranslator.FromHtml("#ffffff"); }

                public struct Buttons {
                    /// <summary> #f0F5f0 </summary>
                    public static Color BarBG { get => ColorTranslator.FromHtml("#f0F5f0"); }
                    /// <summary> #1f221f </summary>
                    public static Color BarBG_Dark { get => ColorTranslator.FromHtml("#1f221f"); }

                    public struct FlatAppearance {
                        /// <summary> #C0C0C0 </summary>
                        public static Color MouseOverBackColor { get => ColorTranslator.FromHtml("#C0C0C0"); }
                        /// <summary> #2f332f </summary>
                        public static Color MouseOverBackColor_Dark { get => ColorTranslator.FromHtml("#2f332f"); }

                        /// <summary> #1f221f </summary>
                        public static Color MouseDownBackColor { get => ColorTranslator.FromHtml("#1f221f"); }
                        /// <summary> #596459 </summary>
                        public static Color MouseDownBackColor_Dark { get => ColorTranslator.FromHtml("#596459"); }
                    }
                }
            }

            public struct infColorRes {
                /// <summary> #bfecff </summary>
                public static Color infBlue_Dark { get => ColorTranslator.FromHtml("#bfecff"); }
                /// <summary> #005073 </summary>
                public static Color infBlue { get => ColorTranslator.FromHtml("#005073"); }

                /// <summary> #ffeebf </summary>
                public static Color warnYellow_Dark { get => ColorTranslator.FromHtml("#ffeebf"); }
                /// <summary> #735400 </summary>
                public static Color warnYellow { get => ColorTranslator.FromHtml("#735400"); }

                /// <summary> #ffbfbf </summary>
                public static Color errRed_Dark { get => ColorTranslator.FromHtml("#ffbfbf"); }
                /// <summary> #9a0000 </summary>
                public static Color errRed { get => ColorTranslator.FromHtml("#9a0000"); }

                /// <summary> #bfdfff </summary>
                public static Color questBlue_Dark { get => ColorTranslator.FromHtml("#bfdfff"); }
                /// <summary> #406d80 </summary>
                public static Color questBlue { get => ColorTranslator.FromHtml("#406d80"); }
            }

            public struct Iconcolors {
                /// <summary> #bfcdff </summary>
                public static Color SwitchTheme_Dark { get => ColorTranslator.FromHtml("#bfcdff"); }
                /// <summary> #293666 </summary>
                public static Color SwitchTheme_Dark_bg { get => ColorTranslator.FromHtml("#293666"); }
                /// <summary> #665200 </summary>
                public static Color SwitchTheme { get => ColorTranslator.FromHtml("#665200"); }
                /// <summary> #FFF4BF </summary>
                public static Color SwitchTheme_bg { get => ColorTranslator.FromHtml("#FFF4BF"); }

                /// <summary> #BFFFBF </summary>
                public static Color Smile_Dark { get => ColorTranslator.FromHtml("#BFFFBF"); }
                /// <summary> #006647 </summary>
                public static Color Smile { get => ColorTranslator.FromHtml("#006647"); }

                /// <summary> #BFF4FF </summary>
                public static Color Settings_Dark { get => ColorTranslator.FromHtml("#BFF4FF"); }
                /// <summary> #006A80 </summary>
                public static Color Settings { get => ColorTranslator.FromHtml("#006A80"); }

                /// <summary> #B2FFDC </summary>
                public static Color ToU_Dark { get => ColorTranslator.FromHtml("#B2FFDC"); }
                /// <summary> #006647 </summary>
                public static Color ToU { get => ColorTranslator.FromHtml("#006647"); }

                /// <summary> #ffbfd9 </summary>
                public static Color Donate_Dark { get => ColorTranslator.FromHtml("#ffbfd9"); }
                /// <summary> #800057 </summary>
                public static Color Donate { get => ColorTranslator.FromHtml("#800057"); }

                /// <summary> #C0D0FF </summary>
                public static Color Commands_Dark { get => ColorTranslator.FromHtml("#C0D0FF"); }
                /// <summary> #1F4099 </summary>
                public static Color Commands { get => ColorTranslator.FromHtml("#1F4099"); }
            }

            public struct Misc {
                /// <summary> #5FBF77 </summary>
                public static Color Green_Dark { get => ColorTranslator.FromHtml("#5FBF77"); }
                /// <summary> #008020 </summary>
                public static Color Green { get => ColorTranslator.FromHtml("#008020"); }

                /// <summary> #FF7AFF </summary>
                public static Color Purple_Dark { get => ColorTranslator.FromHtml("#FF7AFF"); }
                /// <summary> #800080 </summary>
                public static Color Purple { get => ColorTranslator.FromHtml("#800080"); }
            }
        }
    }
}
