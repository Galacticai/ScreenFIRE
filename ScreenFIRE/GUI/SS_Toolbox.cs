using Gtk;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.math;
using ScreenFIRE.Modules.Companion.math.Vision.Geometry;
using System;
using c = Cairo;
using g = Gdk;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.GUI {

    class SS_Toolbox : Window {

        [UI] private readonly Overlay Toolbox_Overlay = null;
        [UI] private readonly DrawingArea SS_Toolbox_BG_DrawingArea = null;

        #region Main buttons
        [UI] private readonly Button Apply_Button = null;
        [UI] private readonly Button Close_Button = null;
        [UI] private readonly ToggleButton SelectArea_ToggleButton = null;
        [UI] private readonly ToggleButton Text_ToggleButton = null;
        [UI] private readonly ToggleButton Shape_ToggleButton = null;
        [UI] private readonly Button Color_Button = null;
        #endregion

        [UI] private readonly ButtonBox SelectArea_ButtonBox = null;
        [UI] private readonly ButtonBox Shape_ButtonBox = null;
        [UI] private readonly ButtonBox Text_ButtonBox = null;

        private void AssignEvents() {
            DeleteEvent += delegate { Application.Quit(); };

            SS_Toolbox_BG_DrawingArea.Drawn += SS_Toolbox_BG_DrawingArea_Drawn;
            SS_Toolbox_BG_DrawingArea.AddEvents((int)(
                    g.EventMask.ButtonPressMask |
                    g.EventMask.PointerMotionMask |
                    g.EventMask.ButtonReleaseMask));
            SS_Toolbox_BG_DrawingArea.ButtonPressEvent += SS_Toolbox_BG_DrawingArea_ButtonPressEvent;
            SS_Toolbox_BG_DrawingArea.MotionNotifyEvent += SS_Toolbox_BG_DrawingArea_MotionNotifyEvent;
            SS_Toolbox_BG_DrawingArea.ButtonReleaseEvent += SS_Toolbox_BG_DrawingArea_ButtonReleaseEvent;

            Close_Button.Clicked += delegate { Close(); };
            Apply_Button.Clicked += Apply_Button_Clicked;

            SelectArea_ToggleButton.Toggled += Section_ToggleButton_Toggled;
            Shape_ToggleButton.Toggled += Section_ToggleButton_Toggled;
            Text_ToggleButton.Toggled += Section_ToggleButton_Toggled;
        }
        private void AssignEtc() {
            //!? TEMP
            Decorated = true;
            SS_Toolbox_BG_DrawingArea.Visible = false;
            //SelectArea_ButtonBox.ShowAll();
            //SelectArea_ButtonBox.Visible = false;
            //Shape_ButtonBox.ShowAll();
            //Shape_ButtonBox.Visible = false;
            //Text_ButtonBox.ShowAll();
            //Text_ButtonBox.Visible = false;
        }

        public SS_Toolbox() : this(new Builder("SS_Toolbox.glade")) { }

        private SS_Toolbox(Builder builder) : base(builder.GetRawOwnedObject("SS_Toolbox")) {
            builder.Autoconnect(this);
            AssignEvents();
            AssignEtc();
        }

        private bool Moving = false;
        private g.Size MoveDiff;
        private double bgAlpha = .1;
        private void SS_Toolbox_BG_DrawingArea_Drawn(object sender, DrawnArgs ev) {
            using c.Context context = ev.Cr;

            if (bgAlpha != 0 | bgAlpha != .36) {
                double factor = .06;
                if (Moving) bgAlpha += factor;
                else bgAlpha -= factor;
                mathCommon.ForceInRange(ref bgAlpha, 0, .28);
            }

            context.SetSourceRGBA(224 / 255, 202 / 255, 142 / 255, bgAlpha);
            context.RoundedRectangle(new(Allocation.X, Allocation.Y, Allocation.Width, Allocation.Height),
                                     Math.Min(Allocation.Width, Allocation.Height));
            context.Fill();
            SS_Toolbox_BG_DrawingArea.QueueDraw();
        }
        private void SS_Toolbox_BG_DrawingArea_ButtonPressEvent(object sender, ButtonPressEventArgs ev) {
            //? Only accept Button 1 (Left click)
            if (ev.Event.Button != 1) return;
            Moving = true;
            GetPosition(out int x, out int y);
            g.Display.Default.GetPointer(out int px, out int py);
            MoveDiff = new(px - x, py - y);
        }
        private void SS_Toolbox_BG_DrawingArea_MotionNotifyEvent(object sender, MotionNotifyEventArgs ev) {
            if (!Moving) return;
            g.Point pointer = Monitors.Pointer_Point();
            Move(pointer.X - MoveDiff.Width, pointer.Y - MoveDiff.Height);
        }
        private void SS_Toolbox_BG_DrawingArea_ButtonReleaseEvent(object sender, ButtonReleaseEventArgs ev) {

            //? Only accept Button 1 (Left click)
            if (ev.Event.Button != 1) return;
            Moving = false;
        }

        private void Apply_Button_Clicked(object sender, EventArgs ev) {

        }

        private void Section_ToggleButton_Toggled(object sender, EventArgs ev) {
            if (sender == SelectArea_ToggleButton) {
                SelectArea_ButtonBox.Visible = SelectArea_ToggleButton.Active;
            } else if (sender == Shape_ToggleButton) {
                Shape_ButtonBox.Visible = Shape_ToggleButton.Active;
            } else if (sender == Text_ToggleButton) {
                Text_ButtonBox.Visible = Text_ToggleButton.Active;
            }
            Resize(0, 0);
        }
    }
}
