using Gtk;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.math;
using System;
using System.Threading;
using gdk = Gdk;
using sysd = System.Drawing;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.GUI {


    class ScreenFIRE : Window {

        private Screenshot Screenshot { get; set; }

        [UI] private readonly Image ScreenshotImage = null;
        [UI] private readonly Button Close_Button = null;
        [UI] private readonly Image SSOverlayImage = null;
        [UI] private readonly EventBox EventBox_SSOverlayImage = null;


        private void AssignEvents() {
            DeleteEvent += delegate { Application.Quit(); };
            Hidden += OnHidden;
            Shown += OnShown;
            Close_Button.Clicked += Close_Button_Clicked;

            EventBox_SSOverlayImage.AddEvents((int)
                      (gdk.EventMask.ButtonPressMask
                     | gdk.EventMask.ButtonReleaseMask
                     | gdk.EventMask.PointerMotionMask));
            EventBox_SSOverlayImage.ButtonPressEvent += Draw_ButtonPressEvent;
            EventBox_SSOverlayImage.MotionNotifyEvent += Draw_MotionNotifyEvent;
            EventBox_SSOverlayImage.ButtonReleaseEvent += Draw_ButtonReleaseEvent;
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

            EventBox_SSOverlayImage.SetAllocation(Screenshot.ImageRectangle);
        }

        private void Close_Button_Clicked(object sender, EventArgs ev) {
            Screenshot.Dispose();
            Overlay_Image.Dispose();
            Hide();
        }

        private bool DRAWING = false;
        private gdk.Point startPoint;
        private gdk.Point endPoint;
        private sysd.Graphics g;
        private sysd.Bitmap Overlay_Image;
        private readonly Random random = new();

        private void Draw_ButtonPressEvent(object sender, EventArgs ev) {
            startPoint = Monitors.Pointer_Point();
            Overlay_Image = new(Allocation.Width, Allocation.Height);
            g = sysd.Graphics.FromImage(Overlay_Image);

            DRAWING = true;
        }

        private void Draw_MotionNotifyEvent(object sender, EventArgs args) {
            if (!DRAWING) return;

            //Overlay_Image = new(Allocation.Width, Allocation.Height);

            endPoint = Monitors.Pointer_Point();
            gdk.Rectangle gdk_rect = Vision.Geometry.PointsToRectangle(startPoint, endPoint);
            sysd.Rectangle rect = new(gdk_rect.X, gdk_rect.Y, gdk_rect.Width, gdk_rect.Height);

            using var brush = new sysd.SolidBrush(sysd.Color.DeepPink);
            using var pen = new sysd.Pen(brush, 2);
            g.DrawRectangle(pen, rect);

            SSOverlayImage.Pixbuf = new((byte[])new sysd.ImageConverter().ConvertTo(Overlay_Image, typeof(byte[])));
        }

        private void Draw_ButtonReleaseEvent(object sender, EventArgs args) {
            DRAWING = false;

            g.Dispose();
        }
    }
}
