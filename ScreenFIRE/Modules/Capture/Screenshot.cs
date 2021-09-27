using Gdk;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.math;

namespace ScreenFIRE.Modules.Capture {

    class Screenshot : IDisposable {
        #region IDisposable
        bool disposed;
        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing) {
                    Image = null;
                }
            }
            disposed = true;
        }
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        //public string UIDAndTime_SHA512 { get; }

        /// <summary>
        /// Unique ID specific for this screenshot <br/><br/>
        /// >> Example: 0f8fad5b-d9cb-469f-a165-70867728950e
        /// </summary>
        public Guid UID { get; }

        /// <summary> Date and time of screen firing </summary>
        public DateTime Time { get; }

        public IScreenshotType? ScreenshotType { get; private set; }
        public Rectangle ImageRectangle { get; private set; }
        public Pixbuf Image { get; private set; }

        private static Rectangle GetRectangle(IScreenshotType screenshotType)
            => screenshotType switch {
                IScreenshotType.MonitorAtPointer => Monitors.MonitorAtPointer_Rectangle(),
                IScreenshotType.WindowAtPointer => Monitors.WindowAtPointer_Rectangle(),
                IScreenshotType.ActiveWindow => Monitors.ActiveWindow_Rectangle(),

                //! IScreenshotType.All
                _ => new Monitors().AllRectangle
            };

        /// <summary> Auto (using <see cref="IScreenshotType"/>) </summary>
        /// <param name="imageRectangle"> Rectangle to be captured</param>
        public Screenshot(IScreenshotType screenshotType) {
            //UIDAndTime_SHA512 = txt.ToSHA512($"{UID}_{Time.ToLongTimeString()}");
            UID = Guid.NewGuid();
            Time = DateTime.Now;
            ScreenshotType = screenshotType;
            ImageRectangle = GetRectangle(screenshotType);
            Image = Vision.Screenshot(ImageRectangle);
        }

        /// <summary> Custom </summary>
        /// <param name="imageRectangle"> Rectangle to be captured</param>
        public Screenshot(Rectangle imageRectangle) {
            //UIDAndTime_SHA512 = txt.ToSHA512($"{UID}_{Time.ToLongTimeString()}");
            UID = Guid.NewGuid();
            Time = DateTime.Now;
            ScreenshotType = null;
            ImageRectangle = imageRectangle;
            Image = Vision.Screenshot(imageRectangle);
        }
    }
}
