using ScreenFIRE.Modules.Companion;
using System;
using System.Drawing;

namespace ScreenFIRE.Modules.Capture.Companion {

    class ScreenshotInfo {

        public Guid UID { get; private set; } // Example: 0f8fad5b-d9cb-469f-a165-70867728950e 

        public IScreenshotType ScreenshotType { get; private set; }
        public Screens Screens { get; private set; }
        public Rectangle ImageRectangle { get; private set; }

        public static ScreenshotInfo Instance(IScreenshotType screenshotType,
                                              Rectangle imageRectangle) {
            return new() {
                UID = Guid.NewGuid(),
                ScreenshotType = screenshotType,
                ImageRectangle = imageRectangle
            };
        }
    }
}