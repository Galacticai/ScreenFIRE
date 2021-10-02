using ScreenFIRE.Assets;
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
        public static string StringWithDesctiption(ISaveFormat? Specific_ISaveFormat = null) {
            return (Specific_ISaveFormat ?? Common.LocalSave_Settings.Format) switch {
                ISaveFormat.bmp => $"bmp ({Strings.Fetch(IStrings.Original).Result})",
                ISaveFormat.jpeg => $"jpg ({Strings.Fetch(IStrings.Efficiency).Result})",
                _ => $"png ({Strings.Fetch(IStrings.Quality).Result})" //0 & etc
            };
        }
    }
}
