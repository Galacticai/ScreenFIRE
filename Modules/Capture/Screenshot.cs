using ScreenFIRE.Modules.Capture.Companion;
using System;

namespace ScreenFIRE.Modules.Capture {

    class Screenshot {

        /// <summary> 
        /// Unique ID specific for this screenshot <br/><br/>
        /// >> Example: 0f8fad5b-d9cb-469f-a165-70867728950e 
        /// </summary>
        public Guid UID { get; private set; }

        /// <summary> Date and time of screen firing </summary>
        public DateTime Time { get; private set; }

        public IScreenshotType ScreenshotType { get; private set; }
        public Gdk.Rectangle ImageRectangle { get; private set; }
        //public Screens Screens { get; private set; }

        public static Screenshot New(IScreenshotType screenshotType,
                                     Gdk.Rectangle imageRectangle)
            => new() {
                UID = Guid.NewGuid(),
                Time = DateTime.Now,
                ScreenshotType = screenshotType,
                ImageRectangle = imageRectangle
            };

    }
}
