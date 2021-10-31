using System;
using c = Cairo;
using g = Gdk;

namespace ScreenFIRE.Modules.Companion.math.Vision.Geometry {
    /// <summary> Shapes and stuff math </summary>
    static class PointsToRectangle {
        /// <summary> ••• <see cref="int"/> ••• Convert 2 points to a rectangle </summary>
        /// <returns> <see cref="g.Rectangle"/> built from <paramref name="point1"/> and <paramref name="point2"/> </returns>
        public static g.Rectangle Integer(g.Point point1, g.Point point2) {
            var (X, Y, Width, Height) = Accurate_Raw((point1.X, point1.Y), (point2.X, point2.Y));
            return new g.Rectangle((int)X, (int)Y, (int)Width, (int)Height);
        }
        /// <summary> ••• Accurate (<see cref="double"/>) ••• Convert 2 points to a rectangle </summary>
        /// <returns> <see cref="c.Rectangle"/> built from <paramref name="point1"/> and <paramref name="point2"/> </returns>
        public static c.Rectangle Accurate(c.PointD point1, c.PointD point2) {
            var (X, Y, Width, Height) = Accurate_Raw((point1.X, point1.Y), (point2.X, point2.Y));
            return new c.Rectangle(X, Y, Width, Height);
        }
        /// <summary> ••• Raw • Accurate (<see cref="double"/>) ••• Convert 2 points to a rectangle </summary>
        /// <returns> Raw rectangle built from <paramref name="point1"/> and <paramref name="point2"/> </returns>
        public static (double X, double Y, double Width, double Height) Accurate_Raw(
                            (double X, double Y) point1, (double X, double Y) point2) {
            double left = Math.Min(point1.X, point2.X),
                   right = Math.Max(point1.X, point2.X),
                   top = Math.Min(point1.Y, point2.Y),
                   bottom = Math.Max(point1.Y, point2.Y),

                   width = right - left,
                   height = bottom - top;

            return (left, top, width, height);
        }
    }
}
