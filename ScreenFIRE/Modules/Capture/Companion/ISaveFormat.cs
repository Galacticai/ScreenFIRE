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
        jpg,
        bmp

        //? Maybe later => gif, mp4, ... mp3 ??
    }

    public class SaveFormat {
        public static ISaveFormat From_SaveOptionsFormat()
            => Common.SaveOptions.Format switch {
                1 => ISaveFormat.jpg,
                2 => ISaveFormat.bmp,

                _ => ISaveFormat.png //0 & etc
            };
    }
}
