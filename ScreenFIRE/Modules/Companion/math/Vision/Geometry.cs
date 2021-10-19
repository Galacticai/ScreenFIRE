using System;
using System.Linq;
using c = Cairo;
using g = Gdk;

namespace ScreenFIRE.Modules.Companion.math.Vision {
    /// <summary> Shapes and stuff math </summary>
    internal static class Geometry {
        /// <summary> Find the bounding rectangle of several rectangles </summary>
        /// <param name="rectangles">Rectangles to process</param>
        /// <returns><see cref="g.Rectangle"/> which contains all <paramref name="rectangles"/>[]</returns>
        public static g.Rectangle BoundingRectangle(this g.Rectangle[] rectangles)
            => BoundingRectangle(rectangles);
        /// <summary> Find the bounding rectangle of several rectangles </summary>
        /// <param name="rectangles">Rectangles to process</param>
        /// <returns><see cref="c.Rectangle"/> which contains all <paramref name="rectangles"/>[]</returns>
        public static c.Rectangle BoundingRectangle(this c.Rectangle[] rectangles) {
            double xMin = rectangles.Min(s => s.X),
                   yMin = rectangles.Min(s => s.Y),
                   xMax = rectangles.Max(s => s.X + s.Width),
                   yMax = rectangles.Max(s => s.Y + s.Height);
            return new c.Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);
        }

        public struct PointsToRectangle {
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
}


// System.Drawing
///// <param name="boundsRect">Original rectangle to convert</param>
///// <param name="radius">Radius of rounded corners</param>
///// <returns>Rounded rectangle as <see cref="GraphicsPath"/></returns>
//public static GraphicsPath RoundedRect(Rectangle boundsRect, double radius) {
//    // Bound radius by half the height and width of boundsRect
//    mathMisc.ForcedInRange(ref radius, 0, boundsRect.Height / 2);
//    mathMisc.ForcedInRange(ref radius, 0, boundsRect.Width / 2);
//
//    int diameter = (int)radius * 2;
//    Rectangle arc = new(boundsRect.Location, new Size(diameter, diameter));
//    GraphicsPath path = new();
//    path.StartFigure();
//    if (radius == 0) {
//        path.AddRectangle(boundsRect);
//        return path;
//    }
//    // top left arc
//    path.AddArc(arc, 180, 90);
//    // top right arc
//    arc.X = boundsRect.Right - diameter;
//    path.AddArc(arc, 270, 90);
//    // bottom right arc
//    arc.Y = boundsRect.Bottom - diameter;
//    path.AddArc(arc, 0, 90);
//    // bottom left arc
//    arc.X = boundsRect.Left;
//    path.AddArc(arc, 90, 90);
//
//    path.CloseFigure();
//    return path;
//}
//
///// <summary>
///// Draws a circle (outline) using <see cref="Graphics"/> <paramref name="g"/> and <see cref="Brush"/> <paramref name="brush"/>
///// </summary>
///// <param name="g"> Target <see cref="Graphics"/> </param>
///// <param name="pen"> Target <see cref="Pen"/> </param>
///// <param name="centerX">X of center point</param>
///// <param name="centerY">Y of center point</param>
///// <param name="radius">radius of the circle</param>
//public static void DrawCircle(Graphics g, Pen pen, float centerX, float centerY, float radius) {
//    g.DrawEllipse(pen, centerX - radius, centerY - radius,
//                  radius + radius, radius + radius);
//}
///// <summary>
///// Draws a filled circle using <see cref="Graphics"/> <paramref name="g"/> and <see cref="Brush"/> <paramref name="brush"/>
///// </summary>
///// <param name="g"> Target <see cref="Graphics"/> </param>
///// <param name="brush"></param>
///// <param name="centerX">X of center point</param>
///// <param name="centerY">Y of center point</param>
///// <param name="radius">radius of the circle</param>
//public static void FillCircle(Graphics g, Brush brush, float centerX, float centerY, float radius) {
//    g.FillEllipse(brush, centerX - radius, centerY - radius,
//                  radius + radius, radius + radius);
//}
///// <param name="centerX">X of center point</param>
///// <param name="centerY">Y of center point</param>
///// <param name="radius">radius of the circle</param>
///// <returns>Circle path as <see cref="GraphicsPath"/></returns>
//public static GraphicsPath Circle(float centerX, float centerY, float radius) {
//    GraphicsPath result = new();
//    result.AddEllipse(centerX - radius,
//                      centerY - radius,
//                      radius + radius,
//                      radius + radius);
//    return result;
//}
//
///// <param name="boundsRect">Parent rectangle</param>
///// <returns>Circle path as <see cref="GraphicsPath"/> bounded by <paramref name="boundsRect"/></returns>
//public static GraphicsPath EllipseInRect(Rectangle boundsRect) {
//    GraphicsPath result = new();
//    result.AddEllipse(boundsRect);
//    return result;
//}