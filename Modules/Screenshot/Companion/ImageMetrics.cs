using Gdk;

namespace ScreenFire.Modules.Screenshot.Companion;

class ImageMetrics {

    public Rectangle Rectangle { get; private set; }


    /// <returns><see cref="Rectangle"/> </returns> 
    Rectangle find_Rectangle(IScreenshotType screenshotType) {
        //
        //? do
        //
        return Rectangle = new(0, 0, 0, 0); //# PLACEHOLDER
    }

    public static ImageMetrics instance(Rectangle rectangle)
        => new() { Rectangle = rectangle };

}
