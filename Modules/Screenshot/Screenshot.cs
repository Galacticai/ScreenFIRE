
using Gdk;
using ScreenFire.Modules.Companion;
using ScreenFire.Modules.Screenshot.Companion;
using System;
using System.Drawing.Imaging;

namespace ScreenFire.Modules.Screenshot;

class Screenshot {

    public Guid UID { get; private set; } // Example: 0f8fad5b-d9cb-469f-a165-70867728950e 

    public IScreenshotType ScreenshotType { get; private set; }
    public ImageFormat ImageFormat { get; private set; }
    public Screens Screens { get; private set; }
    public ImageMetrics ImageMetrics { get; private set; }

    public static Screenshot New(IScreenshotType screenshotType,
                                 ImageMetrics imageMetrics,
                                 ImageFormat imageFormat = null) {

        if (imageFormat == null) imageFormat = ImageFormat.Png;

        Rectangle finalRectangle = screenshotType switch {
            IScreenshotType.Custom => imageMetrics.Rectangle,
            //IScreenshotType.WindowUnderMouse => ,
            //IScreenshotType.ScreenUnderMouse => ,
            //IScreenshotType.ActiveWindow => ,
            _ => new Screens().AllRectangle
        };

        return new() {
            UID = Guid.NewGuid(),
            ScreenshotType = screenshotType,
            ImageMetrics = imageMetrics,
            ImageFormat = imageFormat
        };
    }
}
