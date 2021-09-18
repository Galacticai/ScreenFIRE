using Gdk;
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

        public IScreenshotType? ScreenshotType { get; }// init; }
        public Rectangle ImageRectangle { get; }// init; }
        public Pixbuf Image { get; }// init; } 

        private static Rectangle GetRectangle(IScreenshotType screenshotType)
            => screenshotType switch {
                IScreenshotType.MonitorAtPointer => Monitors.MonitorAtPointer_Rectangle(),
                IScreenshotType.WindowAtPointer => Monitors.WindowAtPointer_Rectangle(),
                IScreenshotType.ActiveWindow => Monitors.ActiveWindow_Rectangle(),
                //IScreenshotType.Custom => ,

                //! IScreenshotType.All  
                _ => new Monitors().AllRectangle
            };

        /// <summary> Auto (using <see cref="IScreenshotType"/>) </summary>
        /// <param name="imageRectangle"> Rectangle to be captured</param>
        public Screenshot(IScreenshotType screenshotType) {
            UID = Guid.NewGuid();
            Time = DateTime.Now;
            ScreenshotType = screenshotType;
            ImageRectangle = GetRectangle(screenshotType);
            Image = Vision.Screenshot(ImageRectangle);
        }

        /// <summary> Custom </summary>
        /// <param name="imageRectangle"> Rectangle to be captured</param>
        public Screenshot(Rectangle imageRectangle) {
            UID = Guid.NewGuid();
            Time = DateTime.Now;
            ScreenshotType = null;
            ImageRectangle = imageRectangle;
            Image = Vision.Screenshot(imageRectangle);
        }
    }
}
