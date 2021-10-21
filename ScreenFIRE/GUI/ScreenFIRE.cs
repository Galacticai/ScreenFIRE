using Gtk;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion.math.Vision;
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
            ScreenshotImage.Pixbuf = Screenshot.GdkImage;
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
        private c.ImageSurface dSurface;
        private c.Context dContext;

        private void Draw_ButtonPressEvent(object sender, ButtonPressEventArgs ev) {
            //! ### Prepare

            //! Only accept Button 1 (Left click)
            if (ev.Event.Button != 1) return;

            dSurface = new c.ImageSurface(c.Format.Argb32, SSDrawingArea.Allocation.Width, SSDrawingArea.Allocation.Height);
            dContext = new c.Context(dSurface);

            startPoint = new(ev.Event.X, ev.Event.Y);

            //! ### Then allow drawing
            DRAWING = true;
        }

        private void Draw_MotionNotifyEvent(object sender, MotionNotifyEventArgs ev) {
            //! Ignore if not drawing
            if (!DRAWING) return;
            //? Failesafe
            if (dSurface == null | dContext == null) return;

            //! Update endPoint & Generate bounding rectangle
            endPoint = new(ev.Event.X, ev.Event.Y);
            c.Rectangle rect = PointsToRectangle.Accurate(startPoint, endPoint);

            dContext.SetSourceRGB(0.1, 0.5, 0.6);
            dContext.LineWidth = 5;

            dContext.Rectangle(rect);

            dContext.Paint();


            //SSOverlayImage.Pixbuf = new((byte[])new sysd.ImageConverter().ConvertTo(Overlay_Image, typeof(byte[])));
        }

        private void Draw_ButtonReleaseEvent(object sender, ButtonReleaseEventArgs ev) {
            //! Only accept Button 1 (Left click)
            if (ev.Event.Button != 1) return;

            //! ### Block drawing
            DRAWING = false;
            //! ### Then finish up
            dContext.GetTarget().Dispose();
            dContext.Dispose();
        }
    }
}
