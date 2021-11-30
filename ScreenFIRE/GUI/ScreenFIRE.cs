using Gtk;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion.math.Vision.Geometry;
using System;
using c = Cairo;
using g = Gdk;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.GUI {

    class ScreenFIRE : Window {

        private Screenshot Screenshot { get; set; }

        [UI] private readonly Image ScreenshotImage = null;
        [UI] private readonly Button Close_Button = null;
        [UI] private readonly DrawingArea SSDrawingArea = null;

        private void AssignEvents() {
            DeleteEvent += delegate { Application.Quit(); };
            Hidden += OnHidden;
            Shown += OnShown;
            Close_Button.Clicked += Close_Button_Clicked;

            AddEvents((int)(g.EventMask.ButtonPressMask
                          | g.EventMask.PointerMotionMask
                          | g.EventMask.ButtonReleaseMask));
            ButtonPressEvent += Draw_ButtonPressEvent;
            MotionNotifyEvent += Draw_MotionNotifyEvent;
            ButtonReleaseEvent += Draw_ButtonReleaseEvent;
            SSDrawingArea.Drawn += new DrawnHandler(SSDrawingArea_Drawn);
        }

        public ScreenFIRE() : this(new Builder("ScreenFIRE.glade")) { }

        private ScreenFIRE(Builder builder) : base(builder.GetRawOwnedObject("ScreenFIRE")) {
            builder.Autoconnect(this);
            AssignEvents();
        }

        private void OnHidden(object sender, EventArgs ev) {
            Program.Config.ShowAll();
        }
        private void OnShown(object sender, EventArgs ev) {
            Program.Config.Hide();
            //Thread.Sleep(1000);

            Screenshot = new Screenshot(IScreenshotType.AllMonitors);
            ScreenshotImage.Pixbuf = Screenshot.Image;
            Move(0, 0);
            SSDrawingArea.SetAllocation(Screenshot.ImageRectangle);
            SSDrawingArea.SetSizeRequest(Screenshot.ImageRectangle.Width,
                                         Screenshot.ImageRectangle.Height);
        }

        private void Close_Button_Clicked(object sender, EventArgs ev) {
            Screenshot.Dispose();
            Hide();
        }

        private bool DRAWING = false;
        private c.PointD startPoint = new(0, 0);
        private c.PointD endPoint;
        private c.Rectangle Boundary;
        private g.Pixbuf lastFrame;
        private void SSDrawingArea_Drawn(object sender, DrawnArgs ev) {
            using c.Context context = ev.Cr;
            context.Antialias = c.Antialias.Subpixel;

            context.LineCap = c.LineCap.Round;
            context.LineWidth = 5;

            //context.MoveTo(startPoint);
            //context.LineTo(endPoint);
            //context.Stroke();
            //
            //context.SetSourceRGB(.6, .3, .4);
            //context.RoundedRectangle(Boundary, Boundary.Width / 2);
            //context.Stroke();
            //context.SetSourceRGBA(.6, .3, .5, .1);
            //context.Rectangle(Boundary);
            //context.Fill();
            //
            //context.LineWidth = 1;
            //context.SetSourceRGB(.3, 1, .6);
            //context.Circle(startPoint, endPoint);
            //context.Stroke();
            //
            //context.SetSourceRGB(.2, .6, .8);
            //context.Circle(Boundary);
            //
            //context.Stroke();
            //
            //context.SetSourceRGB(.2, .8, .3);
            //context.Text(endPoint, "Super duper text", 30);
            //context.Stroke();

            SSDrawingArea.QueueDraw();

            //dc.GetTarget().Dispose();
        }
        private void Draw_ButtonPressEvent(object sender, ButtonPressEventArgs ev) {
            //? Only accept Button 1 (Left click)
            if (ev.Event.Button != 1) return;

            startPoint = new(ev.Event.X, ev.Event.Y);

            DRAWING = true;
        }

        private void Draw_MotionNotifyEvent(object sender, MotionNotifyEventArgs ev) {
            //? Ignore if not drawing
            if (!DRAWING) return;

            //? Update endPoint & Generate bounding rectangle
            endPoint = new(ev.Event.X, ev.Event.Y);
            Boundary = PointsToRectangle.Accurate(startPoint, endPoint);
        }

        private void Draw_ButtonReleaseEvent(object sender, ButtonReleaseEventArgs ev) {
            //? Only accept Button 1 (Left click)
            if (ev.Event.Button != 1) return;

            //endPoint = new(ev.Event.X, ev.Event.Y);

            DRAWING = false;
        }
    }
}
