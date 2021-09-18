﻿using Gdk;
using ScreenFIRE.Modules.Companion.math;
using System;

namespace ScreenFIRE.Modules.Companion {

    class Monitors {

        #region Static methods

        /// <param name="index"> <see cref="Monitor"/> index </param>
        /// <returns> <see cref=" Monitor"/> according to <paramref name="index"/> </returns>
        public static Monitor GetMonitor(int index) {
            return Display.Default.GetMonitor(index);
        }

        /// <returns> <see cref="Monitor"/>[] containing all available <see cref="Monitor"/>s </returns>
        public static Monitor[] GetAllMonitors() {
            Monitor[] monitors = new Monitor[Count];
            for (int i = 0; i < Count; i++)
                monitors[i] = GetMonitor(i);
            return monitors;
        }

        /// <returns> Primary <see cref="Monitor"/> </returns>
        public static Monitor PrimaryMonitor() {
            return Display.Default.PrimaryMonitor;
        }

        /// <returns> <see cref="Point"/> coordinates of the mouse pointer </returns>
        public static Point Pointer_Point() {
            Display.Default.GetPointer(out int x, out int y);
            return new Point(x, y);
        }

        /// <returns> Window <see cref="Rectangle"/> at the mouse pointer </returns>
        public static Rectangle ActiveWindow_Rectangle() {
            LastActiveWindow().GetGeometry(out int x, out int y, out int w, out int h);
            return new Rectangle(x, y, w, h);
        }
        /// <param name="point"> Focus <see cref="Point"/> </param>
        /// <returns> Last active <see cref="Window"/> </returns>
        public static Window LastActiveWindow() {
            //!? Unknown if can throw an exception
            return Display.Default.DefaultSeat.Pointer.LastEventWindow
                ?? Display.Default.DefaultSeat.Keyboard.LastEventWindow
                ?? Global.DefaultRootWindow; //!? NOT TESTED
            // TODO: Test this
        }

        /// <returns> Window <see cref="Rectangle"/> at the mouse pointer </returns>
        public static Rectangle WindowAtPointer_Rectangle() {
            Point pointLocation = Pointer_Point();
            Window windowAtPosition = WindowAtPoint(pointLocation);
            windowAtPosition.GetGeometry(out int x, out int y, out int w, out int h);
            return new Rectangle(x, y, w, h);
        }
        /// <param name="point"> Focus <see cref="Point"/> </param>
        /// <returns> <see cref="Window"/> at the <paramref name="point"/> </returns>
        public static Window WindowAtPoint(Point point) {
            Device pointer = Display.Default.DefaultSeat.Pointer;
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

        /// <param name="index"> <see cref="Monitor"/> index </param>
        /// <returns> <see cref="Rectangle"/> of the monitor (The bigger one out of Geometry or Workarea) </returns>
        public static Rectangle Monitor_Rectangle(Monitor monitor) {
            Rectangle work = monitor.Workarea;
            Rectangle geo = monitor.Geometry;
            if ((geo.Width / work.Width) > 1
              | (geo.Height / work.Height) > 1)
                geo = new(geo.X, geo.Y,
                          work.Width + work.X,
                          work.Height + work.Y);
            return geo;
        }


        #endregion


        /// <summary> Number of monitors </summary>
        public static int Count => Display.Default.NMonitors;

        public Monitor[] AllMonitors { get; }


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
        private Rectangle AllRectangle_Auto() {
            return AllRectangle
                        = Vision.Geometry.BoundingRectangle(Rectangles_Auto());
        }

        /// <summary> MANUAL </summary>
        /// <returns> <see cref="Rectangle"/> spanning over all screens </returns>
        private Rectangle AllRectangle_Auto(Rectangle[] rectangles) {
            return AllRectangle
                        = Vision.Geometry.BoundingRectangle(rectangles);
        }



        /// <summary> AUTO </summary>
        public Monitors() {
            AllMonitors = GetAllMonitors();

            AllRectangle = AllRectangle_Auto(); //! It updates both props
            /*Rectangles = Rectangles;*/ //? No need to find, already done above
        }

        /// <summary> MANUAL </summary> 
        public Monitors(Rectangle[] rectangles) {
            AllMonitors = GetAllMonitors();

            AllRectangle = AllRectangle_Auto(rectangles);
            Rectangles = rectangles;

        }
    }
}