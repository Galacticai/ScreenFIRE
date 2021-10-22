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
                    GUID_NoDashes = null;
                    GUID_Short = null;
                    ScreenshotType = null;
                    Image.Dispose();
                    if (Image_SystemDrawing != null)
                        Image_SystemDrawing.Dispose();
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

        /// <summary> <see cref="GUID"/> but without the dashes <br/><br/>
        /// >> Example: 0f8fad5bd9cb469fa16570867728950e </summary>
        internal string GUID_NoDashes { get; private set; }

        /// <summary> Shorter form of <see cref="GUID"/> <br/><br/>
        /// >> Example: 0f8fad5b </summary>
        internal string GUID_Short { get; private set; }

        /// <summary> Date and time of screen firing </summary>
        internal DateTime Time { get; private set; }

        /// <summary> Type of this <see cref="Screenshot"/> (<see cref="null"/> = Custom rectangle) </summary>
        internal IScreenshotType? ScreenshotType { get; private set; }


        /// <summary> Target <see cref="g.Rectangle"/> </summary>
        internal g.Rectangle ImageRectangle { get; private set; }

        /// <summary> Image as <see cref="g.Pixbuf"/> (Cross-Platform) <br/><br/>
        /// Compatibility: <br/>
        /// ✔️ Linux >> Recommended <br/>
        /// ✔️ Windows 10 >> Works <br/>
        /// ⚠️ Windows 11 >> <seealso cref="NullReferenceException"/> (at <see cref="g.Pixbuf.Save(string, string)"/>) </summary>
        internal g.Pixbuf Image { get; private set; }

        /// <summary> Image as <see cref="sysd.Image"/> <br/><br/>
        /// Compatibility: <br/>
        /// ✔️ Windows (All) >> Recommended <br/>
        /// ✔️ Linux - ZorinOS 16 >> Works <br/>
        /// ⚠️ Other Linux >> <see cref="TypeInitializationException"/> &amp; <see cref="DllNotFoundException"/> <br/>
        /// (Unable to load shared library 'libgdiplus' or one of its dependencies) </summary>
        internal sysd.Image Image_SystemDrawing { get; private set; }

        private void Init(g.Rectangle rectangle, IScreenshotType? screenshotType = null) {
            ScreenshotType = screenshotType; //? null = Custom rectangle

            ImageRectangle = rectangle;

            DateTime TimeBeforeSS = DateTime.Now;
            //!? ^^DEBUG^^ Execution time (ms): 12:00:00:001

            Image = VisionCommon.Screenshot(ImageRectangle);
            //!? ^^DEBUG^^ Execution time (ms): 12:00:00:057 ~ 100

            TimeSpan averageSSTime = new((DateTime.Now - TimeBeforeSS).Ticks / 2);
            //!? ^^DEBUG^^ Execution time (ms): 12:00:00:059 ~ 102
            Time = TimeBeforeSS + averageSSTime;
            //!? ^^DEBUG^^ (i7-3770 Windows 11) averageSSTime = 30 ~ 51 ms

            if (Platform.RunningWindows)
                Image_SystemDrawing = Image.ToBitmap(Common.LocalSave_Settings.Format);

            //? 0f8fad5b-d9cb-469f-a165-70867728950e
            GUID = Guid.NewGuid();
            //? 0f8fad5bd9cb469fa16570867728950e
            GUID_NoDashes = GUID.ToString().NoDashes();
            //? 0f8fad5b
            GUID_Short = GUID_NoDashes[..8];
        }

        /// <summary> ••• Auto ••• Take a screenshot using <see cref="IScreenshotType"/> </summary>
        /// <param name="screenshotType"> Type of the <see cref="Screenshot"/> which determines the rectangle automatically </param>
        internal Screenshot(IScreenshotType screenshotType = IScreenshotType.AllMonitors)
            => Init(screenshotType.GetRectangle(), screenshotType);

        /// <summary> ••• Specific ••• Take a screenshot of <paramref name="rectangle"/> </summary>
        /// <param name="rectangle"> <see cref="g.Rectangle"/> to be captured </param>
        internal Screenshot(g.Rectangle rectangle)
            => Init(rectangle);

    }
}
