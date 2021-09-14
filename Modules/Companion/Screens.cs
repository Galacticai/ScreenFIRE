using ScreenFIRE.Modules.Companion.math;
using System.Drawing;
using g = Gdk;

namespace ScreenFIRE.Modules.Companion {

    /// <summary> Screens rectangles </summary>
    class Screens {

        /// <summary> Number of monitors </summary>
        public static int Count => g.Display.Default.NMonitors;

        /// <summary> <see cref="Rectangle"/> array of each screen</summary> 
        public Rectangle[] Rectangles { get; private set; }

        /// <summary><see cref="Rectangle"/> spanning over all screens</summary>
        public Rectangle AllRectangle { get; private set; }


        public static g.Monitor[] GetMonitors() {
            g.Monitor[] monitors = new g.Monitor[Count];
            for (int i = 0; i < Count; i++)
                monitors[i] = g.Display.Default.GetMonitor(i);
            return monitors;
        }

        /// <returns> <see cref=" Rectangle"/> array of each screen </returns>
        private Rectangle[] Rectangles_Auto() {
            Rectangles = new Rectangle[Count]; //? Reset
            g.Rectangle gRect;  // outside || Prevent redeclaring every loop  
            for (int i = 0; i < Count; i++) {
                gRect = g.Display.Default.GetMonitor(i).Geometry;
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
        public Screens New()
                => new() {
                    AllRectangle = AllRectangle_Auto(), //! It updates both props
                    Rectangles = Rectangles //? No need to find, already done above
                };

        /// <summary> MANUAL </summary>
        public Screens New(Rectangle[] rectangles)
                => new() {
                    AllRectangle = AllRectangle_Auto(rectangles),
                    Rectangles = rectangles
                };
    }
}
