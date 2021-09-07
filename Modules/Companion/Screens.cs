using Gdk;
using ScreenFire.Modules.Companion.math;
using System.Collections.Generic;

namespace ScreenFire.Modules.Companion;

/// <summary> Screens rectangles </summary>
class Screens {

    /// <summary> Number of displays </summary>
    public int Count => Rectangles == null ? Rectangles.Count : throw new System.NullReferenceException();

    private bool _RectanglesAssigned = false;
    /// <summary><see cref="Rectangle"/> array of each screen</summary> 
    public List<Rectangle> Rectangles { get; private set; }

    /// <summary><see cref="Rectangle"/> spanning over all screens</summary>
    public Rectangle AllRectangle { get; private set; }



    /// <returns> <see cref="Rectangle"/> array of each screen </returns>
    private List<Rectangle> find_Rectangles() {
        Rectangles.Clear(); //? Clear before adding
        for (int i = 0; i < Count; i++)
            Rectangles.Add(new Display(new()).GetMonitor(i).Geometry);
        return Rectangles;
    }


    /// <summary> AUTO </summary>
    /// <returns> <see cref="Rectangle"/> spanning over all screens </returns>
    private Rectangle find_AllRectangle()
            => AllRectangle = Vision.Geometry.BoundingRectangle(find_Rectangles().ToArray());

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
    public Screens Instance(List<Rectangle> rectangles)
            => new() {
                AllRectangle = find_AllRectangle(rectangles),
                Rectangles = rectangles
            };
}
