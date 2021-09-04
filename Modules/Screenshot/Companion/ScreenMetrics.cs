using Gdk;

namespace ScreenFire.Modules.Screenshot.Companion;

class ScreenMetrics {
    public float LeftMost { get; private set; }

    public Size Size { get; private set; }


    /// <returns>The left-most offset from x=0 of screen(s) wanted as <see cref="float"/></returns>
    float find_LeftMost(IScreenshotType screenshotType) {
        if (screenshotType == IScreenshotType.All) {
            return 0; //! PLACEHOLDER 
        } else if (screenshotType == IScreenshotType.ScreenUnderMouse) {
            return 0; //! PLACEHOLDER 
        }

        return LeftMost = 0; //! PLACEHOLDER 

    }

    /// <returns><see cref="Gdk.Size"/> of screen(s) wanted</returns>
    Size find_Size(IScreenshotType screenshotType) {
        //
        //? use the screenshot type to determine wether to use all screens or one
        //
        return Size = new(0, 0); //! PLACEHOLDER
    }

    /// <summary> AUTO </summary>
    /// <param name="screenshotType"></param>
    /// <returns></returns>
    public ScreenMetrics Instance(IScreenshotType screenshotType) {
        return new() {
            LeftMost = find_LeftMost(screenshotType),
            Size = find_Size(screenshotType)
        };
    }
    /// <summary> MANUAL </summary>
    /// <param name="leftMost">offset of the furthest left position of screen(s)</param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static ScreenMetrics Instance(float leftMost, Size size) {
        return new() {
            LeftMost = leftMost,
            Size = size
        };
    }

}
