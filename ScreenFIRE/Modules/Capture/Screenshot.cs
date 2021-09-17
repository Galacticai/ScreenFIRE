using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.math;
using System;

namespace ScreenFIRE.Modules.Capture {

    class Screenshot {

        /// <summary> 
        /// Unique ID specific for this screenshot <br/><br/>
        /// >> Example: 0f8fad5b-d9cb-469f-a165-70867728950e 
        /// </summary>
        public Guid UID { get; }

        /// <summary> Date and time of screen firing </summary>
        public DateTime Time { get; }

        public IScreenshotType ScreenshotType { get; }// init; }
        public Gdk.Rectangle ImageRectangle { get; }// init; }
        public Gdk.Pixbuf Image { get; }// init; } 

        private static Gdk.Rectangle? GetRectangle(IScreenshotType screenshotType)
            => screenshotType switch {
                IScreenshotType.All => new Monitors().AllRectangle,
                IScreenshotType.MonitorAtPointer => Monitors.MonitorAtPointer_Rectangle,
                IScreenshotType.WindowAtPointer => Monitors.WindowAtPointer_Rectangle,
                IScreenshotType.ActiveWindow => Monitors.ActiveWindow_Rectangle,
                //IScreenshotType.Custom => ,

                _ => null
            };

        public Screenshot(IScreenshotType screenshotType) {
            UID = Guid.NewGuid();
            Time = DateTime.Now;
            ScreenshotType = screenshotType;
            ImageRectangle = imageRectangle;
            Image = Vision.Screenshot(imageRectangle);
        }

        /// <summary> Custom </summary>
        /// <param name="imageRectangle"> Rectangle to be captured</param>
        public Screenshot(Gdk.Rectangle imageRectangle) {
            UID = Guid.NewGuid();
            Time = DateTime.Now;
            ScreenshotType = IScreenshotType.Custom;
            ImageRectangle = imageRectangle;
            Image = Vision.Screenshot(imageRectangle);
        }
    }
}
