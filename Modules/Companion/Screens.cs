using Gdk;
using System.Linq;


namespace ScreenFire.Modules.Companion;

class Screens {

    //!  PLACEHOLDER
    private static Rectangle BoundingRectangle(Rectangle[] rectangles) {
        int xMin = rectangles.Min(s => s.X),
            yMin = rectangles.Min(s => s.Y),
            xMax = rectangles.Max(s => s.X + s.Width),
            yMax = rectangles.Max(s => s.Y + s.Height);
        return new Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);
    }
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
    private Rectangle[] find_AllRectangle() {

        return Rectangles = new Rectangle[] { new(0, 0, 0, 0) }; //! PLACEHOLDER
    }


    public static Screens Instance()
        => new() {

        };
}
