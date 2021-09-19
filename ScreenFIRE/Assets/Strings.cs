using ScreenFIRE.Modules.Companion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScreenFIRE.Assets {

    public enum IStrings {
        ScreenFIRE,

        OK, Yes, No, Cancel,

        ChooseHowYouWouldLikeToFireYourScreenshot_,
        FiredAScreenshot_,
        ThisButtonHasBeenClicked,
        times_1,
        times_2,

        SomethingWentWrong___,

        AllMonitors,
        MonitorAtPointer,
        WindowAtPointer,
        ActiveWindow,

        SaveAs___,
        FileAlreadyExists_,
        alreadyExists,
        WouldYouLikeToReplaceTheExistingFile_,

    }

    public record Strings {

        public static string FetchAndJoin(params IStrings[] Names) {
            return string.Join(" ", Fetch(Names));
        }

        /// <summary> Fetch a set of strings </summary>
        /// <param name="Name"> String names provided by <see cref="IStrings"/> </param>
        /// <returns> Localized <see cref="string"/>[] array according to system language </returns>
        public static async Task<string[]> Fetch(params IStrings[] Names) {
            List<string> result = new();
            foreach (var name in Names)
                result.Add(Fetch(name,
                                  false)); //! skip translation when fetching multiple strings 


            //! >> Translation is currently disabled || `Languages.TranslateText` is broken 


            //? Remove this if enabling translation:
            return result.ToArray();

            //? Do not remove the following:

            //!? string joint = "#!#";//! Where strings will be joined/split
            //!? 
            //!? string joined = string.Join(joint, result);
            //!? 
            //!? string joined_Translated
            //!?     = await Languages.TranslateText(joined,
            //!?                                     Languages.DotNetToILanguages(
            //!?                                         Languages.TwoLetterISOLanguageName)); //! translate all at once
            //!? 
            //!? string[] result_Translated = joined_Translated.Split(joint);
            //!? 
            //!? return result_Translated;
        }

        /// <summary> Fetch a specific string </summary>
        /// <param name="Name"> String name provided by <see cref="IStrings"/> </param>
        /// <returns> Localized <see cref="string"/> according to system language </returns>
        public static string Fetch(IStrings Name, bool translate = true) {
            ILanguages language = Languages.DotNetToILanguages();
            return language switch { //! System language
                //ILanguages.English => En(Name),
                ILanguages.Arabic => Ar(Name),
                //ILanguages.Chinese => Zh(name),

                //! English / Other
                _ => translate ? En(Name)/*await Languages.TranslateText(En(Name), language)*/ : En(Name),
            };
        }


        #region Localized strings

        private static string En(IStrings Name)
          => Name switch {
              IStrings.ScreenFIRE => "ScreenFIRE",

              IStrings.OK => "OK",
              IStrings.Yes => "Yes",
              IStrings.No => "No",
              IStrings.Cancel => "Cancel",

              IStrings.ChooseHowYouWouldLikeToFireYourScreenshot_ => "Choose how you would like to to fire your screenshot!",
              IStrings.FiredAScreenshot_ => "Fired a screenshot!",
              IStrings.ThisButtonHasBeenClicked => "This button has been clicked",
              IStrings.times_1 => "time",
              IStrings.times_2 => "times",

              IStrings.SomethingWentWrong___ => "Something went wrong...",

              IStrings.AllMonitors => "All monitors",
              IStrings.MonitorAtPointer => "Monitor at pointer",
              IStrings.WindowAtPointer => "Window at pointer",
              IStrings.ActiveWindow => "Active window",

              IStrings.SaveAs___ => "Save as...",
              IStrings.FileAlreadyExists_ => "File already exists.",
              IStrings.alreadyExists => "already exists",
              IStrings.WouldYouLikeToReplaceTheExistingFile_ => "Would you like to replace the existing file?",


              //!? Last resort
              _ => $"⚠ STRING MISSING: \"{Name}\" ⚠"
          };

        private static string Ar(IStrings Name)
          => Name switch {
              IStrings.ScreenFIRE => "حريق الشاشة ScreenFIRE",

              IStrings.OK => "حسناً",
              IStrings.Yes => "نعم",
              IStrings.No => "لا",
              IStrings.Cancel => "إلغاء",

              IStrings.ChooseHowYouWouldLikeToFireYourScreenshot_ => "اختر كيف تود طلق صورة شاشتك!",
              IStrings.FiredAScreenshot_ => "تم اطلاق صورة الشاشة!",
              IStrings.ThisButtonHasBeenClicked => "هذا الزر قد ضغط",
              IStrings.times_1 => "مرة",
              IStrings.times_2 => "مرات",

              IStrings.SomethingWentWrong___ => "حدث خطأ ما...",

              IStrings.AllMonitors => "جميع الشاشات",
              IStrings.MonitorAtPointer => "الشاشة عند المؤشر",
              IStrings.WindowAtPointer => "النافذة عند المؤشر",
              IStrings.ActiveWindow => "النافذة الفعالة",

              IStrings.SaveAs___ => "حفظ باسم...",
              IStrings.FileAlreadyExists_ => "الملف موجود مسبقاً.",
              IStrings.alreadyExists => "موجود مسبقاً",
              IStrings.WouldYouLikeToReplaceTheExistingFile_ => "هل تودّ استبدال الملف السابق؟",


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
