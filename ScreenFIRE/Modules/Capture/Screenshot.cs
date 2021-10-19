using Gdk;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.math.Vision;
using System;
using sysd = System.Drawing;

namespace ScreenFIRE.Modules.Capture {

    class Screenshot : IDisposable {
        #region IDisposable
        bool disposed;
        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing) {
                    ScreenshotType = null;
                    GdkImage.Dispose();
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
        public Guid UID { get; private set; }

        /// <summary> Date and time of screen firing </summary>
        public DateTime Time { get; private set; }

        public IScreenshotType? ScreenshotType { get; private set; }

        public Rectangle ImageRectangle { get; private set; }
        /// <summary> Image as <see cref="Pixbuf"/> </summary>
        public Pixbuf GdkImage { get; private set; }
        /// <summary> Image as <see cref="sysd.Image"/> </summary>
        public sysd.Image SysImage { get; private set; }

        private static Rectangle GetRectangle(IScreenshotType screenshotType)
            => screenshotType switch {
                IScreenshotType.MonitorAtPointer => Monitors.MonitorAtPointer_Rectangle(),
                IScreenshotType.WindowAtPointer => Monitors.WindowAtPointer_Rectangle(),
                IScreenshotType.ActiveWindow => Monitors.ActiveWindow_Rectangle(),

                //! IScreenshotType.All
                _ => Monitors.BoundingRectangle()
            };

        private void CommonSetting_Pre() {
            UID = Guid.NewGuid();
            Time = DateTime.Now;
        }
        private void CommonSetting_Post() {
            GdkImage = VisionCommon.Screenshot(ImageRectangle);
            //SysImage = GdkImage.PixbufToBitmap(Common.LocalSave_Settings.Format);
        }


        /// <summary> Auto (using <see cref="IScreenshotType"/>) </summary>
        /// <param name="imageRectangle"> Rectangle to be captured</param>
        public Screenshot(IScreenshotType screenshotType) {
            CommonSetting_Pre();
            ScreenshotType = screenshotType;
            ImageRectangle = GetRectangle(screenshotType);
            CommonSetting_Post();
        }

        /// <summary> Custom </summary>
        /// <param name="imageRectangle"> Rectangle to be captured</param>
        public Screenshot(Rectangle imageRectangle) {
            CommonSetting_Pre();
            ScreenshotType = null;
            ImageRectangle = imageRectangle;
            CommonSetting_Post();
        }
    }
}
