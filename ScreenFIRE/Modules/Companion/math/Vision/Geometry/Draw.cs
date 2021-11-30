using System;
using c = Cairo;

namespace ScreenFIRE.Modules.Companion.math.Vision.Geometry {
    /// <summary> Easy-to-use <c>Cairo</c> drawing functions </summary>
    public static class Draw {
        private const double Pi = Math.PI;

        /// <summary> ••• Rectangle ••• Rounded mode <br/>
        /// Create a rounded rectangle which corners are controlled by <paramref name="cornerRadius"/> </summary>
        /// <param name="context"> Target <see cref="c.Context"/> </param>
        /// <param name="boundary"> <see cref="c.Rectangle"/> bounding the output rounded rectangle </param>
        /// <param name="cornerRadius"> Corner radius of the output rounded rectangle </param>
        public static void RoundedRectangle(this c.Context context, c.Rectangle boundary, double cornerRadius = -1) {
            if (cornerRadius == 0) {
                context.Rectangle(boundary);
                return;
            }
            if (cornerRadius == -1) cornerRadius = Math.Min(boundary.Width, boundary.Height) / 2;
            mathCommon.ForceInRange(ref cornerRadius, 0, boundary.Height / 2);
            mathCommon.ForceInRange(ref cornerRadius, 0, boundary.Width / 2);

            context.NewSubPath();
            context.Arc(boundary.X + boundary.Width - cornerRadius, boundary.Y + cornerRadius, cornerRadius, -Pi / 2, 0);
            context.Arc(boundary.X + boundary.Width - cornerRadius, boundary.Y + boundary.Height - cornerRadius, cornerRadius, 0, Pi / 2);
            context.Arc(boundary.X + cornerRadius, boundary.Y + boundary.Height - cornerRadius, cornerRadius, Pi / 2, Pi);
            context.Arc(boundary.X + cornerRadius, boundary.Y + cornerRadius, cornerRadius, Pi, Pi * 3 / 2);
            context.ClosePath();
        }

        /// <summary> ••• Cicle ••• Radius mode <br/>
        /// Create a circle in <paramref name="context"/> which is defined by the <paramref name="center"/> and <paramref name="point"/> which belongs to its parameter </summary>
        /// <param name="context"> Target <see cref="c.Context"/> </param>
        /// <param name="center"> Center <see cref="c.PointD"/> of the output circle </param>
        /// <param name="point"> A <see cref="c.PointD"/> that belongs to the parameter of the circle </param>
        public static void Circle(this c.Context context, c.PointD center, c.PointD point) {
            double radius = GeometryCommon.Distance(center, point);
            context.NewSubPath();
            context.Arc(center.X, center.Y, radius, 0, 2 * Pi);
            context.ClosePath();
        }
        /// <summary> ••• Cicle ••• Boundary mode <br/>
        /// Create a circle in <paramref name="context"/> which is bounded inside of <paramref name="boundary"/> (as <see cref="c.Rectangle"/>) </summary>
        /// <param name="context"> Target <see cref="c.Context"/> </param>
        /// <param name="boundary"> <see cref="c.Rectangle"/> bounding the output circle </param>
        public static void Circle(this c.Context context, c.Rectangle boundary) {
            c.PointD center = new(boundary.X + (boundary.Width / 2),
                                  boundary.Y + (boundary.Height / 2));
            double radius = Math.Min(boundary.Width / 2, boundary.Height / 2);
            context.NewSubPath();
            context.Arc(center.X, center.Y, radius, 0, 2 * Pi);
            context.ClosePath();
        }

        /// <summary> ••• Ellipse ••• Boundary mode <br/>
        /// Create an ellipse which is defined by the <paramref name="boundary"/> </summary>
        /// <param name="context"> Target <see cref="c.Context"/> </param>
        /// <param name="boundary"> <see cref="c.Rectangle"/> bounding the output ellipse </param>
        public static void Ellipse(this c.Context context, c.Rectangle boundary) {
            c.PointD center = new(boundary.X + boundary.Width / 2,
                                  boundary.Y + boundary.Height / 2);
            context.Save();
            context.Translate(center.X, center.Y);
            context.Scale(boundary.Width / 2, boundary.Height / 2);
            context.Arc(0, 0, 1, 0, 2 * Pi);
            context.Restore();
        }

        /// <summary> </summary>
        /// <param name="context"></param>
        /// <param name="boundary"></param>
        public static void X_Sign(this c.Context context, c.Rectangle boundary) {
            context.NewSubPath();

            //? \
            context.MoveTo(boundary.X, boundary.Y);
            context.LineTo(boundary.X + boundary.Width, boundary.Y + boundary.Height);
            //? /
            context.MoveTo(boundary.X + boundary.Width, boundary.Y);
            context.LineTo(boundary.X, boundary.Y + boundary.Height);

            context.ClosePath();
        }

        public static void Arrow(this c.Context context, c.PointD origin, c.PointD head) {
            double length = GeometryCommon.Distance(origin, head),
                   angle = Pi / 4,
                   head_angle = Pi / 6,
                   head_length = length / 10;

            context.MoveTo(origin);

            context.NewSubPath();
            context.RelLineTo(length * Math.Cos(angle), length * Math.Sin(angle));
            context.RelLineTo(-head_length * Math.Cos(angle - head_angle), -head_angle * Math.Sin(angle - head_angle));
            context.RelLineTo(head_length * Math.Cos(angle - head_angle), head_angle * Math.Sin(angle - head_angle));
            context.RelLineTo(-head_length * Math.Cos(angle + head_angle), -head_angle * Math.Sin(angle + head_angle));
            context.ClosePath();
        }

        public static void Text(this c.Context context,
                                c.PointD anchor_BottomLeft,
                                string text,
                                double scale,
                                c.FontSlant fontSlant = c.FontSlant.Normal,
                                c.FontWeight fontWeight = c.FontWeight.Normal) {

            context.SelectFontFace("Sans", fontSlant, fontWeight);
            context.SetFontSize(scale);

            context.MoveTo(anchor_BottomLeft);
            context.ShowText(text);

            context.FillPreserve();
        }

    }
}
