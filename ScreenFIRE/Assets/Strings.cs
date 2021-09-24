using ScreenFIRE.Modules.Companion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScreenFIRE.Assets {

    public enum IStrings {
        ScreenFIRE,
        ScreenFIREConfig,

        Screenshot,
        SavingOptions,
        About,

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
        FreeAreaSelection,

        SaveAs___,
        FileAlreadyExists_,
        alreadyExists,
        WouldYouLikeToReplaceTheExistingFile_,

    }

    public record Strings {

        public static async Task<string> FetchAndJoin(params IStrings[] Names) {
            return string.Join(" ", await Fetch(Names));
        }

        /// <summary> Fetch a set of strings </summary>
        /// <param name="Name"> String names provided by <see cref="IStrings"/> </param>
        /// <returns> Localized <see cref="string"/>[] array according to system language </returns>
        public static async Task<string[]> Fetch(params IStrings[] Names) {
            List<string> result = new();
            foreach (var name in Names)
                result.Add(await Fetch(name,
                                       false)); //! skip translation when fetching multiple strings


            //! >> Translation is currently disabled || `Languages.TranslateText` is broken


            //? Remove this if enabling translation:
            return result.ToArray();

            //? Do not remove the following:

            //!? string joint = "\u21da\u21db"; //! ⇚⇛ Where strings will be joined/split
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

#pragma warning disable CS1998 //>> "Async method lacks 'await' operators and will run synchronously"
        /// <summary> Fetch a specific string </summary>
        /// <param name="Name"> String name provided by <see cref="IStrings"/> </param>
        /// <returns> Localized <see cref="string"/> according to system language </returns>
        public static async Task<string> Fetch(IStrings Name, bool translate = true, ILanguages? language = null) {

            return (language ?? Languages.DotNetToILanguages()) switch { //! System language
                //ILanguages.English => En(Name),
                ILanguages.Arabic => Ar(Name),
                ILanguages.ChineseSimplified => Zh(Name),

                //! English / Other
                _ => Zh(Name) //translate ? await Languages.TranslateText(En(Name), language) : En(Name),
            };
        }
#pragma warning restore CS1998 //<< "Async method lacks 'await' operators and will run synchronously"


        #region Localized strings

        private static string En(IStrings Name)
          => Name switch {
              IStrings.ScreenFIRE => "ScreenFIRE",
              IStrings.ScreenFIREConfig => "ScreenFIRE Configuration",

              IStrings.Screenshot => "Screenshot",
              IStrings.SavingOptions => "Saving options",
              IStrings.About => "About",

              IStrings.OK => "OK",
              IStrings.Yes => "Yes",
              IStrings.No => "No",
              IStrings.Cancel => "Cancel",

              IStrings.ChooseHowYouWouldLikeToFireYourScreenshot_ => "Choose how you would like to to fire your screenshot!",
              IStrings.FiredAScreenshot_ => "Fired a screenshot!",
              IStrings.ThisButtonHasBeenClicked => "This button has been clicked",
              IStrings.times_1 => "time",
              IStrings.times_2 => "times",

              IStrings.SomethingWentWrong___ => $"Something went wrong{Common.Ellipses }",

              IStrings.AllMonitors => "All monitors",
              IStrings.MonitorAtPointer => "Monitor at pointer",
              IStrings.WindowAtPointer => "Window at pointer",
              IStrings.ActiveWindow => "Active window",
              IStrings.FreeAreaSelection => "Free area selection",

              IStrings.SaveAs___ => $"Save as{Common.Ellipses }",
              IStrings.FileAlreadyExists_ => "File already exists.",
              IStrings.alreadyExists => "already exists",
              IStrings.WouldYouLikeToReplaceTheExistingFile_ => "Would you like to replace the existing file?",


              //!? Last resort
              _ => $"⚠ STRING MISSING: \"{Name}\" ⚠"
          };

        private static string Ar(IStrings Name)
          => Name switch {
              IStrings.ScreenFIRE => "حريق الشاشة ScreenFIRE",
              IStrings.ScreenFIREConfig => "إعدادات ScreenFIRE",

              IStrings.Screenshot => "لقطة شاشة",
              IStrings.SavingOptions => "خيارات الحفظ",
              IStrings.About => "حول هذا",

              IStrings.OK => "حسناً",
              IStrings.Yes => "نعم",
              IStrings.No => "لا",
              IStrings.Cancel => "إلغاء",

              IStrings.ChooseHowYouWouldLikeToFireYourScreenshot_ => "اختر كيف تود طلق صورة شاشتك!",
              IStrings.FiredAScreenshot_ => "تم اطلاق صورة الشاشة!",
              IStrings.ThisButtonHasBeenClicked => "هذا الزر قد ضغط",
              IStrings.times_1 => "مرة",
              IStrings.times_2 => "مرات",

              IStrings.SomethingWentWrong___ => $"حدث خطأ ما{Common.Ellipses }",

              IStrings.AllMonitors => "جميع الشاشات",
              IStrings.MonitorAtPointer => "الشاشة عند المؤشر",
              IStrings.WindowAtPointer => "النافذة عند المؤشر",
              IStrings.ActiveWindow => "النافذة الفعالة",
              IStrings.FreeAreaSelection => "تحديد مساحة حرة",

              IStrings.SaveAs___ => $"حفظ باسم{Common.Ellipses }",
              IStrings.FileAlreadyExists_ => "الملف موجود مسبقاً.",
              IStrings.alreadyExists => "موجود مسبقاً",
              IStrings.WouldYouLikeToReplaceTheExistingFile_ => "هل تودّ استبدال الملف السابق؟",


              //? Fallback to English.
              _ => En(Name)
          };

        private static string Zh(IStrings Name)
          => Name switch {
              IStrings.ScreenFIRE => "屏幕火 ScreenFIRE",
              IStrings.ScreenFIREConfig => "ScreenFIRE 配置",

              IStrings.Screenshot => "截屏",
              IStrings.SavingOptions => "保存选项",
              IStrings.About => "对这个",

              IStrings.OK => "好的",
              IStrings.Yes => "是的",
              IStrings.No => "不",
              IStrings.Cancel => "取消",

              IStrings.ChooseHowYouWouldLikeToFireYourScreenshot_ => "选择您希望如何触发屏幕截图！",
              IStrings.FiredAScreenshot_ => "发了截图！",
              IStrings.ThisButtonHasBeenClicked => "此按钮已被点击",
              IStrings.times_1 => "次",
              IStrings.times_2 => "次",

              IStrings.SomethingWentWrong___ => $"出了些问题{Common.Ellipses }",

              IStrings.AllMonitors => "所有显示器",
              IStrings.MonitorAtPointer => "在指针处监控",
              IStrings.WindowAtPointer => "指针处的窗口",
              IStrings.ActiveWindow => "活动窗口",
              IStrings.FreeAreaSelection => "区域选择",

              IStrings.SaveAs___ => $"另存为{Common.Ellipses }",
              IStrings.FileAlreadyExists_ => "文件已存在。",
              IStrings.alreadyExists => "已经存在",
              IStrings.WouldYouLikeToReplaceTheExistingFile_ => "您想替换现有文件吗？",


              //? Fallback to English.
              _ => En(Name)
          };

        #endregion


    }
}
