using Gtk;
using ScreenFIRE.Assets;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.math;
using ScreenFIRE.Modules.Companion.math.Vision;
using ScreenFIRE.Modules.Companion.math.Vision.Geometry;
using System;
using System.Threading.Tasks;
using c = Cairo;
using g = Gdk;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.GUI {

    class ssToolbox : Window {

        //[UI] private readonly Overlay Toolbox_Overlay = null;
        [UI] private readonly DrawingArea SS_Toolbox_BG_DrawingArea = null;
        //[UI] private readonly Box Toolbox_All_Buttons_Box = null;
        //[UI] private readonly Box Toolbox_Top_Buttons_Box = null;

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
                    g.EventMask.EnterNotifyMask |
                    g.EventMask.LeaveNotifyMask |
                    g.EventMask.ButtonReleaseMask));
            SS_Toolbox_BG_DrawingArea.ButtonPressEvent += SS_Toolbox_BG_DrawingArea_ButtonPressEvent;
            SS_Toolbox_BG_DrawingArea.MotionNotifyEvent += SS_Toolbox_BG_DrawingArea_MotionNotifyEvent;
            SS_Toolbox_BG_DrawingArea.EnterNotifyEvent += delegate { SS_Toolbox_BG_DrawingArea_Hovering = true; };
            SS_Toolbox_BG_DrawingArea.LeaveNotifyEvent += delegate { SS_Toolbox_BG_DrawingArea_Hovering = false; };
            SS_Toolbox_BG_DrawingArea.ButtonReleaseEvent += SS_Toolbox_BG_DrawingArea_ButtonReleaseEvent;

            Close_Button.Clicked += delegate { Close(); };
            Apply_Button.Clicked += async delegate { await Apply_Button_Clicked(); };

            SelectArea_ToggleButton.Toggled += SelectArea_ToggleButton_Toggled;
            Shape_ToggleButton.Toggled += Shape_ToggleButton_Toggled;
            Text_ToggleButton.Toggled += Text_ToggleButto_Toggled;
            Color_Button.Clicked += Color_Button_Clicked;
        }
        private void AssignEtc() {
            //!? Not working yet
            //CssProvider cssProvider = new(); cssProvider.LoadFromData(SF.SS_Toolbox_Styles);
            //StyleContext styleContext = Toolbox_All_Buttons_Box.StyleContext;
            //styleContext.AddProvider(cssProvider, StyleProviderPriority.User);
            //styleContext.AddClass("toolboxLists");
            //StyleContext.AddProviderForScreen(g.Screen.Default, cssProvider, StyleProviderPriority.User);
            //styleContext = Toolbox_Top_Buttons_Box.StyleContext;
            //styleContext.AddProvider(cssProvider, StyleProviderPriority.User);
            //styleContext.AddClass("toolboxLists");
            //StyleContext.AddProviderForScreen(g.Screen.Default, cssProvider, StyleProviderPriority.User);

            //!? TEMP
            //Decorated = true;
        }

        public ssToolbox() : this(new Builder("ssToolbox.glade")) { }

        private ssToolbox(Builder builder) : base(builder.GetRawOwnedObject("ssToolbox")) {
            builder.Autoconnect(this);
            AssignEvents();
            AssignEtc();
        }

        private async Task Apply_Button_Clicked() {
            Program.ssToolbox.Hide();
            await Program.Config.Capture(this, Program.ScreenFIRE.Boundary.ToGdkRectangle());
            Close();
        }


        private bool Moving = false;
        private bool SS_Toolbox_BG_DrawingArea_Hovering = false;
        private g.Size MoveDiff;
        private double bgAlpha = 0;
        private void SS_Toolbox_BG_DrawingArea_Drawn(object sender, DrawnArgs ev) {
            using c.Context context = ev.Cr;

            if (bgAlpha != 0 | bgAlpha != .36) {
                bgAlpha += Moving | SS_Toolbox_BG_DrawingArea_Hovering
                           ? .03 : -.03;
                mathCommon.ForceInRange(ref bgAlpha, (0, Moving ? .36 : .16));
            }
            context.SetSourceRGBA(.45, .2, .18, bgAlpha);
            if (bgAlpha > 0) //? Skip if invisible
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

        private void SelectArea_ToggleButton_Toggled(object sender, EventArgs ev) {
            SelectArea_ButtonBox.Visible = SelectArea_ToggleButton.Active;
            Resize(0, 0);
        }
        private void Shape_ToggleButton_Toggled(object sender, EventArgs ev) {
            Shape_ButtonBox.Visible = Shape_ToggleButton.Active;
            Resize(0, 0);
        }
        private void Text_ToggleButto_Toggled(object sender, EventArgs ev) {
            Text_ButtonBox.Visible = Text_ToggleButton.Active;
            Resize(0, 0);
        }
        private void Color_Button_Clicked(object sender, EventArgs ev) {
            ColorChooserDialog FillColor_Dialog = new(Strings.Fetch(IStrings.FillColor).Result + Common.Ellipses, this);
            FillColor_Dialog.KeepAbove = true;
            FillColor_Dialog.Modal = true;
            ResponseType chooseResponse = (ResponseType)FillColor_Dialog.Run();
            if (chooseResponse == ResponseType.Cancel)
                FillColor_Dialog.Destroy();
            else if (chooseResponse == ResponseType.Ok)
                FillColor_Dialog.Destroy();
        }
    }
}
