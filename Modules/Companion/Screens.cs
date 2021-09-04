using Gdk;


namespace ScreenFire.Modules.Companion;
class Screens {

    public int Count { get; private set; }

    /// <summary><see cref="Rectangle"/> spanning over all screens</summary>
    public Rectangle AllRectangle { get; private set; }

    /// <summary><see cref="Rectangle"/> array of each screen</summary> 
    public Rectangle[] Rectangles { get; private set; }


    /// <returns></returns>
    private Rectangle[] find_Rectangles() {

        return Rectangles = new Rectangle[] { new(0, 0, 0, 0) }; //# PLACEHOLDER
    }


    public static Screens instance()
        => new() {

        };
}
