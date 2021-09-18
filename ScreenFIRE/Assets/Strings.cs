using ScreenFIRE.Modules.Companion;
using System.Threading.Tasks;

namespace ScreenFIRE.Assets {

    public enum IStrings {
        ScreenFIRE,

        dot,

        SaveAs,
        FileAlreadyExists,
        alreadyExists,
        DoYouWantToReplaceTheExistingFile,

    }

    public record Strings {



        /// <summary> Fetch a specific string </summary>
        /// <param name="Name"> String name provided by <see cref="IStrings"/> </param>
        /// <returns> Localized string according to requested language / or system language if not specified </returns>
        public static async Task<string> Fetch(IStrings Name) {
            ILanguages language = Languages.DotNetToILanguages();
            return language switch { //! System language
                //ILanguages.English => En(Name),
                ILanguages.Arabic => Ar(Name),
                //ILanguages.Chinese => Zh(name),

                //! English / Other
                _ => await Languages.TranslateText(En(Name), ILanguages.Arabic)// language)
            };
        }


        #region Localized strings

        private static string En(IStrings Name)
          => Name switch {
              IStrings.ScreenFIRE => "ScreenFIRE",

              IStrings.dot => ".",

              IStrings.SaveAs => "Save As",
              IStrings.FileAlreadyExists => "File already exists",
              IStrings.alreadyExists => "already exists.",
              IStrings.DoYouWantToReplaceTheExistingFile => "Do you want to replace the existing file?",


              //!? Last resort
              _ => $"⚠ STRING MISSING: \"{Name}\" ⚠"
          };

        private static string Ar(IStrings Name)
          => Name switch {
              IStrings.ScreenFIRE => "حريق الشاشة ScreenFIRE",

              IStrings.dot => ".",

              IStrings.SaveAs => "حفظ باسم",
              IStrings.FileAlreadyExists => "الملف موجود مسبقاً.",
              IStrings.alreadyExists => "موجود مسبقاً.",
              IStrings.DoYouWantToReplaceTheExistingFile => "هل تودّ استبدال الملف السابق؟",


              //? Fallback to English.
              _ => En(Name)
          };

        //private static string Zh(IStrings Name)
        //  => Name switch {

        //      IStrings.ScreenFIRE => "屏幕火 ScreenFIRE",

        //      IStrings.dot => "。",

        //      IStrings.SaveAs => "另存为",
        //      IStrings.FileAlreadyExists => "文件已存在。",
        //      IStrings.alreadyExists => "已经存在。",
        //      IStrings.DoYouWantToReplaceTheExistingFile => "是否要替换现有文件？",


        //      //? Fallback to English.
        //      _ => En(Name)
        //  };

        #endregion


    }
}
