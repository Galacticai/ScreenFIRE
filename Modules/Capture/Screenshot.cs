using ScreenFIRE.Modules.Capture.Companion;
using System;
using System.Drawing;

namespace ScreenFIRE.Modules.Capture {

    class Screenshot {

        public Guid UID { get; private set; } // Example: 0f8fad5b-d9cb-469f-a165-70867728950e 

        public IScreenshotType ScreenshotType { get; private set; }
        public Rectangle ImageRectangle { get; private set; }
        //public Screens Screens { get; private set; }

        public static Screenshot New(IScreenshotType screenshotType,
                                     Rectangle imageRectangle)
            => new() {
                UID = Guid.NewGuid(),
                ScreenshotType = screenshotType,
                ImageRectangle = imageRectangle
            };

    }
}
