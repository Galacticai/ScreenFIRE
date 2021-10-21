using ScreenFIRE.Assets;
using ScreenFIRE.Modules.Companion;
using System.Drawing.Imaging;

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

        //! Maybe later => gif, mp4, ... mp3 ??
    }


    public static class SaveFormat {
        public static string StringWithDesctiption(ISaveFormat? Specific_ISaveFormat = null) {
            return (Specific_ISaveFormat ?? Common.LocalSave_Settings.Format) switch {
                ISaveFormat.bmp => $"{Strings.Fetch(IStrings.Original).Result} {Common.RangeDash} bmp",
                ISaveFormat.jpeg => $"{Strings.Fetch(IStrings.Efficiency).Result} {Common.RangeDash} jpg",
                _ => $"{Strings.Fetch(IStrings.Quality).Result} {Common.RangeDash} png" //0 & etc
            };
        }

        public static ImageFormat ToSystemDrawing(this ISaveFormat saveFormat) {
            return saveFormat switch {
                ISaveFormat.bmp => ImageFormat.Bmp,
                ISaveFormat.jpeg => ImageFormat.Jpeg,
                //ISaveFormat.png
                _ => ImageFormat.Png
            };
        }
    }
}
