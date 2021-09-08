
namespace ScreenFire.Modules.Screenshot.Companion;

/// <summary> Saving format of the <see cref="ScreenshotInfo"/> 
/// <list type="bullet">
/// <item> 0 • bmp </item>
/// <item> 1 • png </item>
/// <item> 2 • jpg </item>
/// <item> ( Maybe later => gif, mp4, ... mp3 ?? ) </item>
/// </list>
/// </summary>
enum ISaveFormat {
    bmp,
    png,
    jpg
    //? Maybe later => gif, mp4, ... mp3 ??
}