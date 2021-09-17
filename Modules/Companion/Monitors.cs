using Gdk;
using GLib;
using ScreenFIRE.Modules.Companion.math;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ScreenFIRE.Modules.Companion {

    /// <summary> Screens rectangles </summary>
    class Monitors {


        #region Static methods
        public static Monitor Monitor(int index)
            => Display.Default.GetMonitor(index);
        public static Monitor PrimaryMonitor()
            => Display.Default.PrimaryMonitor;
        public static Point Pointer_Coords() {
            Display.Default.GetPointer(out int x, out int y);
            return new Point(x, y);
        }

        /// <returns>Window <see cref="Rectangle"/> at the mouse pointer</returns>
        public static Rectangle WindowAtPointer_Coords() {
            Device pointer = Display.Default.DefaultSeat.Pointer;

            Point pointLocation = Pointer_Coords();

            Window windowAtPosition
                = pointer.GetWindowAtPosition(out pointLocation.X, out pointLocation.Y);

            windowAtPosition.GetGeometry(out int x, out int y, out int w, out int h);
            return new Rectangle(x, y, w, h);
        }

        public static Rectangle MonitorRectangle(int index) {
            Rectangle work = Monitor(index).Workarea;
            Rectangle geo = Monitor(index).Geometry;
            if ((geo.Width / work.Width) > 1
              | (geo.Height / work.Height) > 1)
                geo = new(geo.X, geo.Y,
                          work.Width + work.X,
                          work.Height + work.Y);
            return geo;
        }

        public static Monitor[] GetMonitors() {
            Monitor[] monitors = new Monitor[Count];
            for (int i = 0; i < Count; i++)
                monitors[i] = Monitor(i);
            return monitors;
        }

        #endregion


        /// <summary> Number of monitors </summary>
        public static int Count => Display.Default.NMonitors;

        /// <summary> <see cref="Rectangle"/> array of each screen</summary> 
        public Rectangle[] Rectangles { get; private set; }

        /// <summary><see cref="Rectangle"/> spanning over all screens</summary>
        public Rectangle AllRectangle { get; private set; }


        /// <returns> <see cref=" Rectangle"/> array of each screen </returns>
        private Rectangle[] Rectangles_Auto() {
            Rectangles = new Rectangle[Count]; //? Reset
            Rectangle gRect;  // outside || Prevent redeclaring every loop  
            for (int i = 0; i < Count; i++) {
                gRect = Display.Default.GetMonitor(i).Geometry;
                Rectangles[i] = new(gRect.X, gRect.Y, gRect.Width, gRect.Height); // convert from Gdk to System.Drawing
            }
            return Rectangles;
        }


        /// <summary> AUTO </summary>
        /// <returns> <see cref="Rectangle"/> spanning over all screens </returns>
        private Rectangle AllRectangle_Auto()
                => AllRectangle = Vision.Geometry.BoundingRectangle(Rectangles_Auto());

        /// <summary> MANUAL </summary>
        /// <returns> <see cref="Rectangle"/> spanning over all screens </returns>
        private Rectangle AllRectangle_Auto(Rectangle[] rectangles)
                => AllRectangle = Vision.Geometry.BoundingRectangle(rectangles);



        /// <summary> AUTO </summary>
        public Monitors() {
            AllRectangle = AllRectangle_Auto(); //! It updates both props
            /*Rectangles = Rectangles;*/ //? No need to find, already done above
        }

        /// <summary> MANUAL </summary> 
        public Monitors(Rectangle[] rectangles) {
            AllRectangle = AllRectangle_Auto(rectangles);
            Rectangles = rectangles;
        }
    }
}
