using ScreenFIRE.Modules.Companion;
using g = Gdk;

namespace ScreenFIRE.Modules.Capture.Companion {
    internal static class ScreenshotExtensions {
        internal static g.Rectangle GetRectangle(this IScreenshotType screenshotType)
            => screenshotType switch {
                IScreenshotType.MonitorAtPointer => Monitors.MonitorAtPointer_Rectangle(),
                IScreenshotType.WindowAtPointer => Monitors.WindowAtPointer_Rectangle(),
                IScreenshotType.ActiveWindow => Monitors.ActiveWindow_Rectangle(),

                //? IScreenshotType.All
                _ => Monitors.BoundingRectangle()
            };
    }
}
