using System;
using System.Linq;
using c = Cairo;
using g = Gdk;

namespace ScreenFIRE.Modules.Companion.math.Vision.Geometry {
    /// <summary> Shapes and stuff math </summary>
    static class GeometryCommon {
        /// <summary> Find the bounding rectangle of several rectangles </summary>
        /// <param name="rectangles">Rectangles to process</param>
        /// <returns><see cref="g.Rectangle"/> which contains all <paramref name="rectangles"/>[]</returns>
        public static g.Rectangle BoundingRectangle(this g.Rectangle[] rectangles) {
            int xMin = rectangles.Min(rect => rect.X),
                yMin = rectangles.Min(rect => rect.Y),
                xMax = rectangles.Max(rect => rect.X + rect.Width),
                yMax = rectangles.Max(rect => rect.Y + rect.Height);
            return new g.Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);
        }
        /// <summary> Find the bounding rectangle of several rectangles </summary>
        /// <param name="rectangles">Rectangles to process</param>
        /// <returns><see cref="c.Rectangle"/> which contains all <paramref name="rectangles"/>[]</returns>
        public static c.Rectangle BoundingRectangle(this c.Rectangle[] rectangles) {
            var s = BoundingRectangle(rectangles);
            double xMin = rectangles.Min(rect => rect.X),
                   yMin = rectangles.Min(rect => rect.Y),
                   xMax = rectangles.Max(rect => rect.X + rect.Width),
                   yMax = rectangles.Max(rect => rect.Y + rect.Height);
            return new c.Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);
        }

        /// <summary> Calculates the distance between 2 points </summary>
        /// <returns> Distance = √[ (x₂ - x₁)² + (y₂ - y₁)² ] </returns>
        public static double Distance(c.PointD point1, c.PointD point2) {
            return Math.Sqrt(Math.Pow((point2.X - point1.X), 2)
                           + Math.Pow((point2.Y - point1.Y), 2));
        }
    }
}