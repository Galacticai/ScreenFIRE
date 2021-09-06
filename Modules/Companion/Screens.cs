using Gdk;

namespace ScreenFire.Modules.Companion;


class Screens {

    //!  PLACEHOLDER
    //! /PLACEHOLDER

    /// <summary> Number of displays </summary>
    public int Count => Displays.Length;

    /// <summary> <see cref="Display"/>[] of all displays</summary>
    Display[] Displays = new DisplayManager(new System.IntPtr()).ListDisplays();


    /// <summary><see cref="Rectangle"/> array of each screen</summary> 
    public Rectangle[] Rectangles { get; private set; }

    /// <summary><see cref="Rectangle"/> spanning over all screens</summary>
    public Rectangle AllRectangle { get; private set; }


    /// <returns> <see cref="Rectangle"/> array of each screen </returns>
    private Rectangle[] find_Rectangles() {

        return Rectangles = new Rectangle[] { new(0, 0, 0, 0) }; //! PLACEHOLDER
    }

    /// <returns> <see cref="Rectangle"/> spanning over all screens </returns>
    private Rectangle find_AllRectangle()
            => AllRectangle = math.Vision.Geometry.BoundingRectangle(Rectangles); //! PLACEHOLDER



    public static Screens New()
        => new() {

        };
}
