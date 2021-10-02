using ScreenFIRE.Modules.Companion.math;
using ScreenFIRE.Modules.Companion.OS;

namespace ScreenFIRE.Modules.Companion {

    public static class Monitors {

        /// <summary> Number of monitors </summary>
        public static int Count => Gdk.Display.Default.NMonitors;


        /// <summary> <see cref="Gdk.Rectangle"/> array of each screen </summary>
        public static Gdk.Rectangle[] Rectangles() {
            Gdk.Rectangle[] rectangles = new Gdk.Rectangle[Count]; //? Reset
            for (int i = 0; i < Count; i++)
                rectangles[i] = Gdk.Display.Default.GetMonitor(i).Geometry;
            return rectangles;
        }


        /// <summary><see cref="Gdk.Rectangle"/> spanning over all screens</summary>
        public static Gdk.Rectangle BoundingRectangle()
            => Vision.Geometry.BoundingRectangle(Rectangles());


        /// <param name="index"> <see cref="Gdk.Monitor"/> index </param>
        /// <returns> <see cref="Gdk.Monitor"/> according to <paramref name="index"/> </returns>
        public static Gdk.Monitor Monitor(int index) {
            return Gdk.Display.Default.GetMonitor(index);
        }

        /// <returns> <see cref="Gdk.Monitor"/>[] containing all available <see cref="Gdk.Monitor"/>s </returns>
        public static Gdk.Monitor[] AllMonitors() {
            Gdk.Monitor[] monitors = new Gdk.Monitor[Count];
            for (int i = 0; i < Count; i++)
                monitors[i] = Monitor(i);
            return monitors;
        }

        /// <returns> Primary <see cref="Gdk.Monitor"/> </returns>
        public static Gdk.Monitor PrimaryMonitor() {
            return Gdk.Display.Default.PrimaryMonitor;
        }

        /// <returns> <see cref="Gdk.Point"/> coordinates of the mouse pointer </returns>
        public static Gdk.Point Pointer_Point() {
            Gdk.Display.Default.GetPointer(out int x, out int y);
            return new Gdk.Point(x, y);
        }

        /// <param name="rectangle"> Target <see cref="Gdk.Rectangle"/> to be fixed </param>
        /// <returns> Window <see cref="Gdk.Rectangle"/> without the extra empty space dedicated to the invisible window borders in Windows 10 </returns>
        private static Gdk.Rectangle FixWindowRectangle_ForWindows10(Gdk.Rectangle rectangle) {
            if (!Platform.RunningWindows10) return rectangle; //? failsafe
            rectangle.X += 7;
            rectangle.Height -= 7;
            rectangle.Width -= 7 + 7; // right offset - additional X (left) offset
            return rectangle;
        }

        /// <returns> Window <see cref="Gdk.Rectangle"/> at the mouse pointer </returns>
        public static Gdk.Rectangle ActiveWindow_Rectangle() {
            Gdk.Rectangle rectangle = LastActiveWindow().FrameExtents;
            return Platform.RunningWindows10
                   ? FixWindowRectangle_ForWindows10(rectangle)
                   : rectangle;
        }
        /// <param name="point"> Focus <see cref="Point"/> </param>
        /// <returns> Last active <see cref="Window"/> </returns>
        public static Gdk.Window LastActiveWindow() {
            //!? Unknown if can throw an exception
            return Gdk.Display.Default.DefaultSeat.Pointer.LastEventWindow
                ?? Gdk.Display.Default.DefaultSeat.Keyboard.LastEventWindow
                ?? Gdk.Global.DefaultRootWindow; //! Fallback
        }

        /// <returns> Window <see cref="Gdk.Rectangle"/> at the mouse pointer </returns>
        public static Gdk.Rectangle WindowAtPointer_Rectangle() {
            Gdk.Point pointLocation = Pointer_Point();
            Gdk.Window windowAtPosition = WindowAtPoint(pointLocation);
            return Platform.RunningWindows10
                   ? FixWindowRectangle_ForWindows10(windowAtPosition.FrameExtents)
                   : windowAtPosition.FrameExtents;
        }
        /// <param name="point"> Focus <see cref="Gdk.Point"/> </param>
        /// <returns> <see cref="Gdk.Window"/> at the <paramref name="point"/> </returns>
        public static Gdk.Window WindowAtPoint(Gdk.Point point) {
            Gdk.Device pointer = Gdk.Display.Default.DefaultSeat.Pointer;

            //!? This returns null!
            return pointer.GetWindowAtPosition(out point.X, out point.Y);
        }

        /// <returns> Monitor <see cref="Gdk.Rectangle"/> at the mouse pointer </returns>
        public static Gdk.Rectangle MonitorAtPointer_Rectangle() {
            return Monitor_Rectangle(MonitorAtPoint(Pointer_Point()));
        }
        /// <param name="point"> Focus <see cref="Gdk.Point"/> </param>
        /// <returns> <see cref="Gdk.Monitor"/> at the <paramref name="point"/> </returns>
        public static Gdk.Monitor MonitorAtPoint(Gdk.Point point) {
            return Gdk.Display.Default.GetMonitorAtPoint(point.X, point.Y);
        }

        /// <param name="monitor"> Target <see cref="Gdk.Monitor"/> </param>
        /// <returns> <see cref="Gdk.Rectangle"/> of the monitor (The bigger one out of Geometry or Workarea) </returns>
        public static Gdk.Rectangle Monitor_Rectangle(Gdk.Monitor monitor) {
            Gdk.Rectangle work = monitor.Workarea;
            Gdk.Rectangle geo = monitor.Geometry;
            if ((geo.Width / work.Width) > 1
              | (geo.Height / work.Height) > 1)
                geo = new(geo.X, geo.Y,
                          work.Width + work.X,
                          work.Height + work.Y);
            return geo;
        }
    }
}
