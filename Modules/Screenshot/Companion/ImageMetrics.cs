using Gdk;


namespace ScreenFire.Modules.Screenshot.Companion;

class ImageMetrics {

    public Rectangle Rectangle { get; private set; }


    /// <returns><see cref="Rectangle"/> </returns> 
    Rectangle find_Rectangle(IScreenshotType screenshotType) {
        //
        //? do
        //
        return Rectangle = new(0, 0, 0, 0); //! PLACEHOLDER
    }

    /// <summary> MANUAL </summary>
    /// <param name="rectangle"></param>
    /// <returns></returns>
    public static ImageMetrics New(Rectangle rectangle)
        => new() { Rectangle = rectangle };

}
