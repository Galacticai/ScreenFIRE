using Cairo;
using Gtk;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.math;
using System;
using System.Threading;
using gdk = Gdk;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.GUI {


    class ScreenFIRE : Window {

        private Screenshot Screenshot { get; set; }

        [UI] private readonly Image ScreenshotImage = null;
        [UI] private readonly Button Close_Button = null;
        [UI] private readonly DrawingArea SS_DrawingArea = null;


        private void AssignEvents() {
            DeleteEvent += delegate { Application.Quit(); };
            Hidden += OnHidden;
            Shown += OnShown;
            Close_Button.Clicked += Close_Button_Clicked;

            SS_DrawingArea.DragBegin += Draw_DragBegin;
            SS_DrawingArea.DragMotion += Draw_DragMotion;
            SS_DrawingArea.DragEnd += Draw_DragEnd;
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
            Thread.Sleep(1000);

            Screenshot = new Screenshot(IScreenshotType.AllMonitors);
            ScreenshotImage.Pixbuf = Screenshot.GdkImage;
            Move(0, 0);

            SS_DrawingArea.SetAllocation(Screenshot.ImageRectangle);
        }

        private void Close_Button_Clicked(object sender, EventArgs ev) {
            Screenshot.Dispose();
            Hide();
        }


        private gdk.Point startPoint;
        private gdk.Point endPoint;
        private Context g;

        private void Draw_DragBegin(object sender, DragBeginArgs ev) {
            startPoint = Monitors.Pointer_Point(); //set the start point
            g = new Context(new ImageSurface(Format.Argb32,
                                             Screenshot.GdkImage.Width,
                                             Screenshot.GdkImage.Height)
                                             ); //prepare the context
            SS_DrawingArea.Realize();

        }
        private void Draw_DragMotion(object sender, DragMotionArgs args) {
            endPoint = Monitors.Pointer_Point(); //set the end point on motion

            gdk.Rectangle gdkRect = Vision.Geometry.PointsToRectangle(startPoint, endPoint);
            Rectangle rect = new(gdkRect.X, gdkRect.Y, gdkRect.Width, gdkRect.Height);
            g.MoveTo(rect.X, rect.Y);
            g.SetSourceRGB(0.3, 0.4, 0.6);
            g.Rectangle(rect);
            g.Stroke();
            g.Fill();

            SS_DrawingArea.QueueDraw();

        }
        private void Draw_DragEnd(object sender, DragEndArgs args) {
            g.GetTarget().Dispose();
            g.Dispose();
            Destroy();
        }
    }
}
