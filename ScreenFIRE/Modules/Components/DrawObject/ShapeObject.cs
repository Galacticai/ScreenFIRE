using Gtk;
using ScreenFIRE.Modules.Companion.math.Vision.Geometry;
using System;
using c = Cairo;
using g = Gdk;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.Modules.Components.DrawObject {

    internal partial class ShapeObject : Window {
        [UI] private readonly DrawingArea d = null;
        private void AssignEvents() {
            DeleteEvent += delegate { Application.Quit(); };

            d.Drawn += d_Drawn;
            d.AddEvents((int)(
                    g.EventMask.ButtonPressMask |
                    g.EventMask.PointerMotionMask |
                    g.EventMask.ButtonReleaseMask));
            d.ButtonPressEvent += d_ButtonPressEvent;
            d.MotionNotifyEvent += d_MotionNotifyEvent;
            d.ButtonReleaseEvent += d_ButtonReleaseEvent;

        }

        private ShapeObject() : this(new Builder("Shape_Object.glade")) {
            //DrawingType = drawing;
            //Boundary = boundary;
            //Color = color;
            //BorderSize = borderSize;
            //BorderColor = borderColor;
            Move((int)Boundary.X, (int)Boundary.Y);
        }

        private ShapeObject(Builder builder) : base(builder.GetRawOwnedObject("Shape_Object")) {
            builder.Autoconnect(this);
            AssignEvents();
        }


        private void d_Drawn(object sender, DrawnArgs ev) {
            //? Cancel if not initialized
            if (DrawingType == null) return;

            using c.Context context = ev.Cr;
            context.Antialias = c.Antialias.Gray;

            switch (DrawingType) {
            case DrawType.Rectangle:
                context.Rectangle(Boundary);
                break;
            case DrawType.RoundedRectangle:
                context.RoundedRectangle(Boundary);
                break;
            case DrawType.Circle:
                context.Circle(Boundary);
                break;
            case DrawType.Ellipse:
                context.Ellipse(Boundary);
                break;
            case DrawType.X_Sign:
                context.X_Sign(Boundary);
                break;
            case DrawType.Arrow:
                context.Arrow(Origin, Point1);
                break;
            }

            //? Shape fill color
            if (Color.A > 0) {
                context.SetSourceRGBA(Color.R, Color.G, Color.B, Color.A);
                context.Fill();
            }
            //? Shape border stroke
            if (BorderColor.A > 0) {
                context.LineWidth = BorderSize;
                context.SetSourceRGBA(BorderColor.R, BorderColor.G, BorderColor.B, BorderColor.A);
                context.Stroke();
            }
        }

        private void d_ButtonPressEvent(object sender, ButtonPressEventArgs ev) {

        }
        private void d_MotionNotifyEvent(object sender, MotionNotifyEventArgs ev) {

        }
        private void d_ButtonReleaseEvent(object sender, ButtonReleaseEventArgs ev) {

        }
    }

    internal enum DrawType {
        None,
        Select,

        Rectangle,
        RoundedRectangle,
        Circle,
        Ellipse,
        X_Sign,
        Arrow
    }
    internal partial class ShapeObject : Window {
        internal DrawType? DrawingType { get; private set; }
        internal c.Rectangle Boundary { get; set; }
        internal (double R, double G, double B, double A) Color { get; set; }
        internal double BorderSize { get; set; }
        internal (double R, double G, double B, double A) BorderColor { get; set; }
        internal double CornerRadius { get; set; }
        internal c.PointD Origin { get; set; }
        internal c.PointD Point1 { get; set; }

        public static ShapeObject Rectangle(
                    c.Rectangle boundary,
                    (double R, double G, double B, double A) color,
                    double borderSize,
                    (double R, double G, double B, double A) borderColor) {
            ShapeObject Rectangle_ShapeObject
                = new() {
                    DrawingType = DrawType.Rectangle,
                    Boundary = boundary,
                    Color = color,
                    BorderSize = borderSize,
                    BorderColor = borderColor
                };
            Program.app.AddWindow(Rectangle_ShapeObject);
            return Rectangle_ShapeObject;
        }
        public static ShapeObject RoundedRectangle(
                    c.Rectangle boundary,
                    double cornerRadius,
                    (double R, double G, double B, double A) color,
                    double borderSize,
                    (double R, double G, double B, double A) borderColor) {
            ShapeObject RoundedRectangle_ShapeObject
                = new() {
                    DrawingType = DrawType.RoundedRectangle,
                    Boundary = boundary,
                    CornerRadius = cornerRadius,
                    Color = color,
                    BorderSize = borderSize,
                    BorderColor = borderColor
                };
            Program.app.AddWindow(RoundedRectangle_ShapeObject);
            return RoundedRectangle_ShapeObject;
        }

        //public ShapeObject Circle(
        //            c.PointD center,
        //            c.PointD point,
        //            g.RGBA color,
        //            double borderSize,
        //            g.RGBA borderColor)
        //    => new() {
        //        DrawingType = DrawType.RoundedRectangle,
        //        Center = center,
        //        Point = point,
        //        Color = color,
        //        BorderSize = borderSize,
        //        BorderColor = borderColor
        //    };

        public static ShapeObject Circle(
                    c.Rectangle boundary,
                    (double R, double G, double B, double A) color,
                    double borderSize,
                    (double R, double G, double B, double A) borderColor) {
            //? Force the boundary to be a square
            double minWH = Math.Min(boundary.Width, boundary.Height);
            boundary = new(boundary.X, boundary.Y, minWH, minWH);
            ShapeObject Circle_ShapeObject
                = new() {
                    DrawingType = DrawType.Circle,
                    Boundary = boundary,
                    Color = color,
                    BorderSize = borderSize,
                    BorderColor = borderColor
                };
            Program.app.AddWindow(Circle_ShapeObject);
            return Circle_ShapeObject;
        }

        public static ShapeObject Ellipse(
                    c.Rectangle boundary,
                    (double R, double G, double B, double A) color,
                    double borderSize,
                    (double R, double G, double B, double A) borderColor) {
            ShapeObject Ellipse_ShapeObject
                = new() {
                    DrawingType = DrawType.Ellipse,
                    Boundary = boundary,
                    Color = color,
                    BorderSize = borderSize,
                    BorderColor = borderColor
                };
            Program.app.AddWindow(Ellipse_ShapeObject);
            return Ellipse_ShapeObject;
        }

        public static ShapeObject X_Sign(
                    c.Rectangle boundary,
                    (double R, double G, double B, double A) color,
                    double borderSize,
                    (double R, double G, double B, double A) borderColor) {
            ShapeObject Xsign_hapeObject
                = new() {
                    DrawingType = DrawType.X_Sign,
                    Boundary = boundary,
                    Color = color,
                    BorderSize = borderSize,
                    BorderColor = borderColor
                };
            Program.app.AddWindow(Xsign_hapeObject);
            return Xsign_hapeObject;
        }

        public static ShapeObject Arrow(
                    c.PointD origin,
                    c.PointD point1,
                    (double R, double G, double B, double A) color,
                    double borderSize,
                    (double R, double G, double B, double A) borderColor) {
            ShapeObject Arrow_ShapeObject
                = new() {
                    DrawingType = DrawType.Arrow,
                    Origin = origin,
                    Point1 = point1,
                    Color = color,
                    BorderSize = borderSize,
                    BorderColor = borderColor
                };
            Program.app.AddWindow(Arrow_ShapeObject);
            return Arrow_ShapeObject;
        }
    }
}
