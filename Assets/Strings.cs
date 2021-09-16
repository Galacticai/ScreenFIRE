using ScreenFIRE.Modules.Companion;

namespace ScreenFIRE.Assets {

    public enum IStrings {

        ScreenFIRE

    }

    public record Strings {
        /// <summary> Fetch a specific string </summary>
        /// <param name="name"></param>
        /// <returns>Localized string according to requested language / or system language if not specified</returns>
        public static string Fetch(IStrings name)//, ILanguage language = ILanguage.System)
             => Language.GetSystemLanguage() switch {
                 ILanguage.Arabic => Ar(name),
                 ILanguage.Chinese => Zh(name),

                 //! System / English / Other
                 _ => En(name),
             };


        #region Localized strings

        private static string En(IStrings Name)
          => Name switch {
              //? name => "value"

              IStrings.ScreenFIRE => "ScreenFIRE",

              _ => string.Empty, //! PLACEHOLDER
          };

        private static string Ar(IStrings Name)
          => Name switch {

              IStrings.ScreenFIRE => "حريق الشاشة ScreenFIRE",

              _ => string.Empty, //! PLACEHOLDER
          };

        private static string Zh(IStrings Name)
          => Name switch {

              IStrings.ScreenFIRE => "屏幕火 ScreenFIRE",

              _ => string.Empty, //! PLACEHOLDER
          };

        #endregion


    }
}
