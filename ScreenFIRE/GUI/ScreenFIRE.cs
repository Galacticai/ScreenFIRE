using Gtk;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion.math;
using ScreenFIRE.Modules.Companion.math.Vision;
using ScreenFIRE.Modules.Companion.math.Vision.Geometry;
using ScreenFIRE.Modules.Components.DrawObject;
using System;
using System.Threading;
using c = Cairo;
using g = Gdk;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.GUI {

    class ScreenFIRE : Window {

        private Screenshot Screenshot { get; set; }

        private DrawType _drawType = DrawType.None;
        internal DrawType drawType { get => _drawType; set => _drawType = value; }

        [UI] private readonly Image ScreenshotImage = null;
        [UI] private readonly DrawingArea SSDrawingArea = null;

        private void AssignEvents() {
            DeleteEvent += delegate { Application.Quit(); };
            Hidden += OnHidden;
            Shown += OnShown;

            AddEvents((int)(g.EventMask.ButtonPressMask
                          | g.EventMask.PointerMotionMask
                          | g.EventMask.ButtonReleaseMask));
            ButtonPressEvent += Draw_ButtonPressEvent;
            MotionNotifyEvent += Draw_MotionNotifyEvent;
            ButtonReleaseEvent += Draw_ButtonReleaseEvent;
            SSDrawingArea.Drawn += new DrawnHandler(SSDrawingArea_Drawn);
        }
        private void AssignEtc() {
            KeepAbove = true;
            Decorated = false;
        }

        public ScreenFIRE() : this(new Builder("ScreenFIRE.glade")) { }

        private ScreenFIRE(Builder builder) : base(builder.GetRawOwnedObject("ScreenFIRE")) {
            builder.Autoconnect(this);
            AssignEvents();
            AssignEtc();
        }

        private void OnHidden(object sender, EventArgs ev) {
            Screenshot.Dispose();
            Program.Config.ShowAll();
        }
        private void OnShown(object sender, EventArgs ev) {
            //var displaySize = Monitors.BoundingRectangle();
            //Move(displaySize.Width, displaySize.Height);
            //Boundary = new(0, 0, 0, 0);

            Program.Config.Hide();

            Thread.Sleep(300); //!? TEMP

            Screenshot = new Screenshot(IScreenshotType.AllMonitors);
            ScreenshotImage.Pixbuf = Screenshot.Image;

            Move(0, 0);

            SSDrawingArea.SetAllocation(Screenshot.ImageRectangle);
            SSDrawingArea.SetSizeRequest(Screenshot.ImageRectangle.Width,
                                         Screenshot.ImageRectangle.Height);
        }
        private c.PointD startPoint;// = new(0, 0);
        private c.PointD endPoint;
        internal c.Rectangle Boundary;
        private double BoundaryAlpha = 1;
        private bool BoundaryAlphaIncreasing = true;
        private double dimAlpha = 0;
        private double dashesOffset = 5;
        private void SSDrawingArea_Drawn(object sender, DrawnArgs ev) {
            using c.Context context = ev.Cr;

            context.Antialias = c.Antialias.Subpixel;

            c.Rectangle ssRect = Screenshot.ImageRectangle.ToCairoRectangle();

            if (dimAlpha < .5) dimAlpha += .05;
            context.SetSourceRGBA(.11, .12, .13, dimAlpha);
            context.Save();
            context.Rectangle(Boundary);
            context.Rectangle(ssRect);
            context.FillRule = c.FillRule.EvenOdd;
            context.Fill();
            context.Restore();

            if (drawType == DrawType.None) {
                BoundaryAlpha += BoundaryAlphaIncreasing ? .024 : -.024;
                if (BoundaryAlpha > 1) BoundaryAlphaIncreasing = false;
                else if (BoundaryAlpha < .3) BoundaryAlphaIncreasing = true;
            }
            mathCommon.ForceInRange(ref BoundaryAlpha, (.3, 1));
            dashesOffset += 1;
            if (dashesOffset >= 24) dashesOffset = 0;
            context.SetDash(new double[] { 1.5, .5, 8, .5, 1.5 }, dashesOffset);

            context.LineWidth = 3;
            c.Rectangle guideRect = new(Boundary.X - context.LineWidth, Boundary.Y - context.LineWidth, Boundary.Width + context.LineWidth * 2, Boundary.Height + context.LineWidth * 2);
            context.SetSourceRGBA(.1, .1, .1, BoundaryAlpha - .3);
            context.Rectangle(guideRect);
            context.Stroke();

            context.LineWidth = 1.8;
            context.SetSourceRGBA(1, 1, 1, BoundaryAlpha);
            context.Rectangle(guideRect);
            context.Stroke();

            SSDrawingArea.QueueDraw();

            context.GetTarget().Dispose();
        }
        private bool newDrag = false;
        private void Draw_ButtonPressEvent(object sender, ButtonPressEventArgs ev) {
            //? Only accept Button 1 (Left click)
            if (ev.Event.Button != 1) return;
            newDrag = true;
            BoundaryAlpha = 1;
        }

        private void Draw_MotionNotifyEvent(object sender, MotionNotifyEventArgs ev) {
            if (newDrag) {
                startPoint = new(ev.Event.X, ev.Event.Y);

                drawType = DrawType.Select;
                Program.ssToolbox.Hide();
                newDrag = false;
            }

            //? Ignore if not drawing
            if (drawType == DrawType.None) return;

            //? Update endPoint & Generate bounding rectangle
            GetSize(out int w, out int h);
            double endX = ev.Event.X, endY = ev.Event.Y;
            mathCommon.ForceInRange(ref endX, (0, w));
            mathCommon.ForceInRange(ref endY, (0, h));
            endPoint = new(endX, endY);

            Boundary = PointsToRectangle.Accurate(startPoint, endPoint);

        }

        private void Draw_ButtonReleaseEvent(object sender, ButtonReleaseEventArgs ev) {
            //? Only accept Button 1 (Left click)
            if (ev.Event.Button != 1) return;
            drawType = DrawType.None;
            UpdateToolbox();
        }

        private void UpdateToolbox() {
            Program.ssToolbox.Show();
            Program.ssToolbox.KeepAbove = true;
            Program.ssToolbox.Decorated = false;

            // No need for the toolbox width currently (discarded ( _ ))
            Program.ssToolbox.GetSize(out _, out int toolboxH);

            //? Move to the top-left side of the selection (Boundary)
            Program.ssToolbox.Move((int)Boundary.X, (int)(Boundary.Y - toolboxH));

            // Get and recalculate the old values (toolboxH)
            Program.ssToolbox.GetPosition(out int toolboxX, out int toolboxY);
            Program.ssToolbox.GetSize(out int toolboxW, out toolboxH);

            (int x, int y) offset = (0, -10);
            //? Avoid overlapping the selection (Boundary)
            if (toolboxY < 0) {
                offset.y = 10;
                toolboxY = (int)(Boundary.Y + Boundary.Height);
            }
            if (toolboxX < 0) {
                offset.x = 10;
                toolboxX = (int)(Boundary.X + Boundary.Width);
            }
            //? Apply the offset
            toolboxX += offset.x; toolboxY += offset.y;

            GetSize(out int w, out int h);

            //? Avoid clipping out of the screen
            mathCommon.ForceInRange(ref toolboxX, (0, w - toolboxW));
            mathCommon.ForceInRange(ref toolboxY, (0, h - toolboxH));

            Program.ssToolbox.Move(toolboxX, toolboxY);
        }
    }
}
