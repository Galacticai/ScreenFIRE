using System.Drawing.Imaging;

namespace ScreenFIRE.Modules.Capture.Companion
{

    /// <summary> Saving format of the <see cref="ScreenshotInfo"/> 
    /// <list type="bullet">
    /// <item> 0 • bmp </item>
    /// <item> 1 • png </item>
    /// <item> 2 • jpg </item>
    /// <item> ( Maybe later => gif, mp4, ... mp3 ?? ) </item>
    /// </list>
    /// </summary>
    enum ISaveFormat
    {
        bmp,
        png,
        jpg
        //? Maybe later => gif, mp4, ... mp3 ??
    }

    static class SaveFormat
    {
        public static ImageFormat ToImageFormat(ISaveFormat saveFormat)
            => saveFormat switch
            {
                ISaveFormat.bmp => ImageFormat.Bmp,
                ISaveFormat.png => ImageFormat.Png,
                ISaveFormat.jpg => ImageFormat.Jpeg,

                _ => ImageFormat.Png,
            };
    }
}
