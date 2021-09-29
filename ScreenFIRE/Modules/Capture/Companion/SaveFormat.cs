using ScreenFIRE.Modules.Companion;

namespace ScreenFIRE.Modules.Capture.Companion {

    /// <summary> Saving format of the <see cref="Screenshot"/>
    /// <list type="bullet">
    /// <item> 0 • png </item>
    /// <item> 1 • jpg </item>
    /// <item> 2 • bmp </item>
    /// <item> ( Maybe later => gif, mp4, ... mp3 ?? ) </item>
    /// </list>
    /// </summary>
    public enum ISaveFormat {
        png,
        jpeg,
        bmp

        //? Maybe later => gif, mp4, ... mp3 ??
    }


    public class SaveFormat {
        public static string StringWithDesctiption_From_SaveOptionsFormat(string[] txt_strings) {
            return Common.LocalSave_Settings.Format switch {
                ISaveFormat.bmp => $"bmp ({txt_strings[2]})",
                ISaveFormat.jpeg => $"jpg ({txt_strings[1]})",
                _ => $"png ({txt_strings[0]})" //0 & etc
            };
        }
    }
}
