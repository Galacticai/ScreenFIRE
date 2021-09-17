using ScreenFIRE.Modules.Companion;

namespace ScreenFIRE.Assets {

    public enum IStrings {
        ScreenFIRE,

        SaveAs,


    }

    public record Strings {
        /// <summary> Fetch a specific string </summary>
        /// <param name="name"></param>
        /// <returns>Localized string according to requested language / or system language if not specified</returns>
        public static string Fetch(IStrings name)
             => Language.GetSystemLanguage() switch { //! System language
                 ILanguage.Arabic => Ar(name),
                 ILanguage.Chinese => Zh(name),

                 //! English / Other
                 _ => En(name),
             };


        #region Localized strings

        private static string En(IStrings Name)
          => Name switch {
              //? name => "value"

              IStrings.ScreenFIRE => "ScreenFIRE",

              IStrings.SaveAs => "Save As",


              //!? Last resort
              _ => $"⚠ STRING MISSING: \"{Name}\" ⚠",
          };

        private static string Ar(IStrings Name)
          => Name switch {

              IStrings.ScreenFIRE => "حريق الشاشة ScreenFIRE",

              IStrings.SaveAs => "حفظ باسم",


              //? Fallback to English.
              _ => En(Name)
          };

        private static string Zh(IStrings Name)
          => Name switch {

              IStrings.ScreenFIRE => "屏幕火 ScreenFIRE",

              IStrings.SaveAs => "另存为",


              //? Fallback to English.
              _ => En(Name)
          };

        #endregion


    }
}
