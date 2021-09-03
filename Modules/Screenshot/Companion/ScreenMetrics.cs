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

        return 0; //! PLACEHOLDER 

    }

    /// <returns><see cref="Gdk.Size"/> of screen(s) wanted</returns>
    Size find_Size(IScreenshotType screenshotType) {
        //
        //? use the screenshot type to determine wether to use all screens or one
        //
        return new(0, 0); //! PLACEHOLDER
    }

    public ScreenMetrics instance(IScreenshotType screenshotType) {
        return new() {
            LeftMost = find_LeftMost(screenshotType),
            Size = find_Size(screenshotType)
        };
    }
    public ScreenMetrics instance(float leftMost, Size size) {
        return new() {
            LeftMost = leftMost,
            Size = size
        };
    }

}
