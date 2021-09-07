using Gdk;
using ScreenFire.Modules.Companion;

namespace ScreenFire.Modules.Screenshot.Companion;

/// <summary> Image measurements and properties </summary>
class ImageMetrics {

    public Rectangle Rectangle { get; private set; }

    /// <returns>Focus area <see cref="Gdk.Rectangle"/> of the <see cref="Screenshot"/> instance</returns> 
    private Rectangle find_Rectangle(IScreenshotType screenshotType)
        => Rectangle = screenshotType switch {
            IScreenshotType.Custom => Rectangle,
            //IScreenshotType.WindowUnderMouse => ,
            //IScreenshotType.ScreenUnderMouse => ,
            //IScreenshotType.ActiveWindow => , 
            _ => new Screens().AllRectangle
            // ^^ IScreenshotType.All ^^ 
        };


    /// <summary> AUTO </summary>
    /// <param name="rectangle"></param>
    /// <returns></returns>
    public static ImageMetrics Instance(IScreenshotType screenshotType)
        => new ImageMetrics().AutoInstance(screenshotType);

    //! ^^ Portal above. Use `ImageMetrics.Instance(screenshotType)`  
    private ImageMetrics AutoInstance(IScreenshotType screenshotType)
        => new() {
            Rectangle = find_Rectangle(screenshotType)
        };

    /// <summary> MANUAL </summary>
    /// <param name="rectangle"></param>
    /// <returns></returns>
    public static ImageMetrics Instance(Rectangle rectangle)
        => new() { Rectangle = rectangle };

}
