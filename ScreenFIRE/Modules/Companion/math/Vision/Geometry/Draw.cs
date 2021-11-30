using System;
using c = Cairo;

namespace ScreenFIRE.Modules.Companion.math.Vision.Geometry {

    public static class Draw {
        private const double Pi = Math.PI;

        public static void RoundedRectangle(this c.Context context, c.Rectangle boundary, double cornerRadius) {
            if (cornerRadius == 0) {
                context.Rectangle(boundary);
                return;
            }

            mathCommon.ForceInRange(ref cornerRadius, 0, boundary.Height / 2);
            mathCommon.ForceInRange(ref cornerRadius, 0, boundary.Width / 2);

            //double degrees = Pi / 180;

            context.NewSubPath();
            context.Arc(boundary.X + boundary.Width - cornerRadius, boundary.Y + cornerRadius, cornerRadius, -Pi / 2, 0);
            context.Arc(boundary.X + boundary.Width - cornerRadius, boundary.Y + boundary.Height - cornerRadius, cornerRadius, 0, Pi / 2);
            context.Arc(boundary.X + cornerRadius, boundary.Y + boundary.Height - cornerRadius, cornerRadius, Pi / 2, Pi);
            context.Arc(boundary.X + cornerRadius, boundary.Y + cornerRadius, cornerRadius, Pi, Pi * 3 / 2);
            context.ClosePath();
        }

        public static void Circle(this c.Context context, c.PointD point1, c.PointD point2) {
            context.NewSubPath();
            context.Arc(point1.X, point1.Y, GeometryCommon.Distance(point1, point2), 0, 2 * Math.PI);
            context.ClosePath();
        }

        public static void Circle(this c.Context context, c.Rectangle boundary) {
            double xCenter = boundary.X + (boundary.Width / 2),
                   yCenter = boundary.Y + (boundary.Height / 2);
            context.NewSubPath();
            context.Arc(xCenter, yCenter, Math.Min(boundary.Width / 2, boundary.Height / 2), 0, 2 * Math.PI);
            context.ClosePath();
        }

        public static void Ellipse(this c.Context context, c.Rectangle boundary) {
            throw new NotImplementedException();
            //context.RoundedRectangle(boundary, Math.Min(boundary.Width / 2, boundary.Height / 2));
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
