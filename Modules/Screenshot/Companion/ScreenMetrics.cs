using Gdk;

namespace ScreenFire.Modules.Screenshot.Companion {
    class ScreenMetrics {
        public float LeftMost { get; private set; }
        public Size Size { get; private set; }

        // find the furthest left as an offset float value
        /// <returns>The left-most offset from x=0 of screen(s) wanted as <seealso cref="float"/></returns>
        private float find_LeftMost(IScreenshotType screenshotType) {
            //? use the screenshot type to determine wether to use all screens or one,
            return 0; //! PLACEHOLDER
        }

        /// <returns><seealso cref="Gdk.Size"/> of screen(s) wanted</returns>
        private Size find_Size(IScreenshotType screenshotType) {
            //? use the screenshot type to determine wether to use all screens or one,
            return new(0, 0); //! PLACEHOLDER

        }

        public ScreenMetrics instance(IScreenshotType screenshotType) {
            return new() {
                LeftMost = find_LeftMost(screenshotType),
                Size = find_Size(screenshotType)
            };
        }
        public ScreenMetrics instance(float leftMost, Size size) {
            return new() {
                LeftMost = leftMost,
                Size = size
            };
        }

    }
}
