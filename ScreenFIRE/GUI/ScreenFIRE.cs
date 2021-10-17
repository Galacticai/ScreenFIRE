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

            //EventBox_SSOverlayImage.Drawn += Draw_DragBegin;
            EventBox_SSOverlayImage.Drawn += Draw_DragMotion;
            EventBox_SSOverlayImage.DragEnd += Draw_DragEnd;
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


        private gdk.Point startPoint;
        private gdk.Point endPoint;
        private sysd.Graphics g;
        private sysd.Bitmap Overlay_Image;
        private readonly Random random = new();

        private void Draw_DragBegin(object sender, EventArgs ev) {
            startPoint = Monitors.Pointer_Point();
            g = sysd.Graphics.FromImage(Overlay_Image ??= new(Allocation.Width, Allocation.Height));
        }
        private void Draw_DragMotion(object sender, EventArgs args) {
            startPoint = Monitors.Pointer_Point();
            g = sysd.Graphics.FromImage(Overlay_Image ??= new(Allocation.Width, Allocation.Height));
            ////! Clean up
            //if (Overlay_Image == null) Overlay_Image = new(Allocation.Width, Allocation.Height);

            endPoint = Monitors.Pointer_Point();
            gdk.Rectangle gdk_rect = Vision.Geometry.PointsToRectangle(startPoint, endPoint);
            //sysd.Rectangle rect = new(gdk_rect.X, gdk_rect.Y, gdk_rect.Width, gdk_rect.Height);

            Gdk.Display.Default.GetPointer(out int x, out int y);
            int w = random.Next(100, Allocation.Width),
                h = random.Next(100, Allocation.Height);
            //    x = random.Next(Allocation.Width - w),
            //    y = random.Next(Allocation.Height - h);
            g.DrawRectangle(new sysd.Pen(new sysd.SolidBrush(sysd.Color.DeepPink), 2), new(x, y, w, h));


            SSOverlayImage.Pixbuf = new((byte[])new sysd.ImageConverter().ConvertTo(Overlay_Image, typeof(byte[])));
            g.Dispose();
            System.Threading.Thread.Sleep(100);
        }

        private void Draw_DragEnd(object sender, EventArgs args) {

        }
    }
}
