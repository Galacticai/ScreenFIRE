using Gdk;
using ScreenFIRE.Modules.Companion.math.Vision.Geometry;
using ScreenFIRE.Modules.Companion.OS;
namespace ScreenFIRE.Modules.Companion {

    internal static class Monitors {

        /// <summary> Number of monitors </summary>
        public static int Count => Display.Default.NMonitors;


        /// <summary> <see cref="Rectangle"/> array of each screen </summary>
        public static Rectangle[] Rectangles() {
            Rectangle[] rectangles = new Rectangle[Count]; //? Reset
            for (int i = 0; i < Count; i++)
                rectangles[i] = Display.Default.GetMonitor(i).Geometry;
            return rectangles;
        }


        /// <summary><see cref="Rectangle"/> spanning over all screens</summary>
        public static Rectangle BoundingRectangle()
            => Rectangles().BoundingRectangle();


        /// <param name="index"> <see cref="Monitor"/> index </param>
        /// <returns> <see cref="Monitor"/> according to <paramref name="index"/> </returns>
        public static Monitor Monitor(int index) {
            return Display.Default.GetMonitor(index);
        }

        /// <returns> <see cref="Monitor"/>[] containing all available <see cref="Monitor"/>s </returns>
        public static Monitor[] AllMonitors() {
            Monitor[] monitors = new Monitor[Count];
            for (int i = 0; i < Count; i++)
                monitors[i] = Monitor(i);
            return monitors;
        }

        /// <returns> Primary <see cref="Monitor"/> </returns>
        public static Monitor PrimaryMonitor() {
            return Display.Default.PrimaryMonitor;
        }

        /// <returns> <see cref="Point"/> Coordinates of the mouse pointer </returns>
        public static Point Pointer_Point() {
            Display.Default.GetPointer(out int x, out int y);
            return new Point(x, y);
        }
        /// <returns> (<paramref name="x"/>, <paramref name="y"/>) Coordinates of the mouse pointer </returns>
        public static void Pointer_Point(out int x, out int y) {
            Display.Default.GetPointer(out x, out y);
        }

        /// <param name="rectangle"> Target <see cref="Rectangle"/> to be fixed </param>
        /// <returns> Window <see cref="Rectangle"/> without the extra empty space dedicated to the invisible window borders in Windows 10 </returns>
        private static Rectangle FrameExtents_Fixed(Rectangle rectangle) {
            if (!Platform.RunningWindows10) return rectangle; //? failsafe
            rectangle.X += 7;
            rectangle.Height -= 7;
            rectangle.Width -= 7 + 7; // right offset - additional X (left) offset
            return rectangle;
        }

        /// <returns> Window <see cref="Rectangle"/> at the mouse pointer </returns>
        public static Rectangle ActiveWindow_Rectangle() {
            Rectangle rectangle = LastActiveWindow().FrameExtents;
            return FrameExtents_Fixed(rectangle);
        }
        /// <param name="point"> Focus <see cref="Point"/> </param>
        /// <returns> Last active <see cref="Window"/> </returns>
        public static Window LastActiveWindow() {
            //!? Unknown if can throw an exception
            return Display.Default.DefaultSeat.Pointer.LastEventWindow
                ?? Display.Default.DefaultSeat.Keyboard.LastEventWindow
                ?? Global.DefaultRootWindow; //! Fallback
        }

        /// <returns> Window <see cref="Rectangle"/> at the mouse pointer </returns>
        public static Rectangle WindowAtPointer_Rectangle() {
            return FrameExtents_Fixed(WindowAtPoint(Pointer_Point()).FrameExtents);
        }
        /// <param name="point"> Focus <see cref="Point"/> </param>
        /// <returns> <see cref="Window"/> at the <paramref name="point"/> </returns>
        public static Window WindowAtPoint(Point point) {
            Device pointer = Display.Default.DefaultSeat.Pointer;

            //!? This returns null!
            return pointer.GetWindowAtPosition(out point.X, out point.Y);
        }
        /// <returns> Monitor <see cref="Rectangle"/> at the mouse pointer </returns>
        public static Rectangle MonitorAtPointer_Rectangle() {
            return Monitor_Rectangle(MonitorAtPoint(Pointer_Point()));
        }
        /// <param name="point"> Focus <see cref="Point"/> </param>
        /// <returns> <see cref="Monitor"/> at the <paramref name="point"/> </returns>
        public static Monitor MonitorAtPoint(Point point) {
            return Display.Default.GetMonitorAtPoint(point.X, point.Y);
        }
        /// <param name="point"> Focus <see cref="Point"/> </param>
        /// <returns> <see cref="Monitor"/> at the <paramref name="point"/> </returns>
        public static Monitor MonitorAtPoint((int X, int Y) point) {
            return Display.Default.GetMonitorAtPoint(point.X, point.Y);
        }

        /// <param name="monitor"> Target <see cref="Monitor"/> </param>
        /// <returns> <see cref="Rectangle"/> of the monitor (The bigger one out of Geometry or Workarea) </returns>
        public static Rectangle Monitor_Rectangle(Monitor monitor) {
            Rectangle workA = monitor.Workarea;
            Rectangle geo = monitor.Geometry;
            if ((geo.Width / workA.Width) > 1
              | (geo.Height / workA.Height) > 1)
                geo = new(geo.X, geo.Y,
                          workA.Width + workA.X,
                          workA.Height + workA.Y);
            return geo;
        }
    }
}
