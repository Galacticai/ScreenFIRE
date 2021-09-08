using ScreenFIRE.Modules.Companion;
using System;

namespace ScreenFIRE.Modules.Screenshot.Companion {

    class ScreenshotInfo {

        public Guid UID { get; private set; } // Example: 0f8fad5b-d9cb-469f-a165-70867728950e 

        public IScreenshotType ScreenshotType { get; private set; }
        public Screens Screens { get; private set; }
        public ImageMetrics ImageMetrics { get; private set; }

        public static ScreenshotInfo Instance(IScreenshotType screenshotType,
                                              ImageMetrics imageMetrics) {
            return new() {
                UID = Guid.NewGuid(),
                ScreenshotType = screenshotType,
                ImageMetrics = imageMetrics
            };
        }
    }
}