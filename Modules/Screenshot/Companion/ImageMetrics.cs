using Gdk;
using ScreenFire.Modules.Companion;

namespace ScreenFire.Modules.Screenshot.Companion;

/// <summary> Image measurements and properties </summary>
class ImageMetrics {

    public Rectangle Rectangle { get; private set; }

    /// <returns><see cref="Rectangle"/> </returns> 
    Rectangle find_Rectangle(IScreenshotType screenshotType) {

        Rectangle = screenshotType switch {
            IScreenshotType.Custom => Rectangle,
            //IScreenshotType.WindowUnderMouse => ,
            //IScreenshotType.ScreenUnderMouse => ,
            //IScreenshotType.ActiveWindow => ,
            /* vv IScreenshotType.All vv */
            _ => new Screens().AllRectangle
        };

        return Rectangle = new Screens().AllRectangle; //! PLACEHOLDER
    }

    /// <summary> MANUAL </summary>
    /// <param name="rectangle"></param>
    /// <returns></returns>
    public static ImageMetrics Instance(Rectangle rectangle)
        => new() { Rectangle = rectangle };

}
