using ScreenFIRE.Modules.Companion.math;
using System.Drawing;
using g = Gdk;

namespace ScreenFIRE.Modules.Companion {

    /// <summary> Screens rectangles </summary>
    class Screens {

        /// <summary> Number of monitors </summary>
        public int Count => new g.Display(new()).NMonitors;

        /// <summary> <see cref="Rectangle"/> array of each screen</summary> 
        public Rectangle[] Rectangles { get; private set; }

        /// <summary><see cref="Rectangle"/> spanning over all screens</summary>
        public Rectangle AllRectangle { get; private set; }



        /// <returns> <see cref=" Rectangle"/> array of each screen </returns>
        private Rectangle[] find_Rectangles() {
            var display = new g.Display(new());

            Rectangles = new Rectangle[Count]; //? Reset  

            g.Rectangle gRect;  // outside. to prevent redeclaring every loop  
            for (int i = 0; i < Count; i++) {
                gRect = display.GetMonitor(i).Geometry;
                Rectangles[i] = new(gRect.X, gRect.Y, gRect.Width, gRect.Height);// convert from Gdk to System.Drawing
            }
            return Rectangles;
        }


        /// <summary> AUTO </summary>
        /// <returns> <see cref="Rectangle"/> spanning over all screens </returns>
        private Rectangle find_AllRectangle()
                => AllRectangle = Vision.Geometry.BoundingRectangle(find_Rectangles());

        /// <summary> MANUAL </summary>
        /// <returns> <see cref="Rectangle"/> spanning over all screens </returns>
        private Rectangle find_AllRectangle(Rectangle[] rectangles)
                => AllRectangle = Vision.Geometry.BoundingRectangle(rectangles);



        /// <summary> AUTO </summary>
        public Screens Instance()
                => new() {
                    AllRectangle = find_AllRectangle(), //! It updaes both props
                    Rectangles = Rectangles //? No need to find, already done above
                };

        /// <summary> MANUAL </summary>
        public Screens Instance(Rectangle[] rectangles)
                => new() {
                    AllRectangle = find_AllRectangle(rectangles),
                    Rectangles = rectangles
                };
    }
}
