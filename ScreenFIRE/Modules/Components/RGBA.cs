using g = Gdk;

namespace ScreenFIRE.Modules.Components {

    public static class RGBA {
        public static g.RGBA New(double R, double G, double B, double A) {
            return new() { Red = R, Green = G, Blue = B, Alpha = A };
        }
    }
}
