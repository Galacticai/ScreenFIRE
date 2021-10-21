using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.math.Vision;
using ScreenFIRE.Modules.Companion.OS;
using System;
using g = Gdk;
using sysd = System.Drawing;

namespace ScreenFIRE.Modules.Capture {
    internal class Screenshot : IDisposable {
        #region IDisposable
        bool disposed;
        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing) {
                    ShortGUID = null;
                    ScreenshotType = null;
                    Image.Dispose();
                    WindowsImage.Dispose();
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

        /// <summary> Unique ID specific for this <see cref="Screenshot"/> <br/><br/>
        /// >> Example: 0f8fad5b-d9cb-469f-a165-70867728950e </summary>
        internal Guid GUID { get; private set; }

        /// <summary> Shorter form of <see cref="GUID"/> <br/><br/>
        /// >> Example: 0f8fad5b </summary>
        internal string ShortGUID { get; private set; }

        /// <summary> Date and time of screen firing </summary>
        internal DateTime Time { get; private set; }

        /// <summary> Type of this <see cref="Screenshot"/> (<see cref="null"/> = Custom rectangle) </summary>
        internal IScreenshotType? ScreenshotType { get; private set; }


        /// <summary> Target <see cref="g.Rectangle"/> </summary>
        internal g.Rectangle ImageRectangle { get; private set; }

        /// <summary> Image as <see cref="g.Pixbuf"/> (Cross-Platform) <br/><br/>
        /// Compatibility: <br/>
        /// ✔️ Linux: Recommended <br/>
        /// ✔️ Windows 10: Works <br/>
        /// ⚠️ Windows 11: Can throw <seealso cref="NullReferenceException"/> </summary>
        internal g.Pixbuf Image { get; private set; }

        /// <summary> Image as <see cref="sysd.Image"/> <br/><br/>
        /// Compatibility: <br/>
        /// ✔️ Windows (All): Recommended <br/>
        /// ⚠️ Linux: Various exceptions </summary>
        internal sysd.Image WindowsImage { get; private set; }

        private void Init(g.Rectangle rectangle, IScreenshotType? screenshotType = null) {
            GUID = Guid.NewGuid(); //? 0f8fad5b-d9cb-469f-a165-70867728950e
            ShortGUID = GUID.ToString()[..'-']; //? 0f8fad5b
            Time = DateTime.Now;
            ScreenshotType = screenshotType; //? null = Custom rectangle
            ImageRectangle = rectangle;
            Image = VisionCommon.Screenshot(ImageRectangle);
            if (Platform.RunningWindows)
                WindowsImage = Image.ToBitmap(Common.LocalSave_Settings.Format);
        }

        /// <summary> ••• Auto ••• Take a screenshot using <see cref="IScreenshotType"/> </summary>
        /// <param name="imageRectangle"> Rectangle to be captured</param>
        internal Screenshot(IScreenshotType screenshotType = IScreenshotType.AllMonitors)
            => Init(screenshotType.GetRectangle(), screenshotType);

        /// <summary> ••• Specific ••• Take a screenshot of <paramref name="rectangle"/> </summary>
        /// <param name="rectangle"> <see cref="g.Rectangle"/> to be captured </param>
        internal Screenshot(g.Rectangle rectangle)
            => Init(rectangle);

    }
}
