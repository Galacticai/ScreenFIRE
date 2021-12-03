using Gtk;
using ScreenFIRE.Assets;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.math;
using ScreenFIRE.Modules.Companion.math.Vision;
using ScreenFIRE.Modules.Companion.math.Vision.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using c = Cairo;
using g = Gdk;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.GUI {

    class ssToolbox : Window {

        //[UI] private readonly Overlay Toolbox_Overlay = null;
        [UI] private readonly Fixed BG_Fixed = null;
        [UI] private readonly DrawingArea BG_DrawingArea = null;

        [UI] private readonly Box All_Buttons_Box = null;
        [UI] private readonly Box Top_Buttons_Box = null;
        [UI] private readonly ButtonBox Select_ButtonBox = null;
        [UI] private readonly ButtonBox Shape_ButtonBox = null;
        [UI] private readonly ButtonBox Text_ButtonBox = null;

        #region Main buttons
        [UI] private readonly Button Apply_Button = null;
        [UI] private readonly Button Close_Button = null;
        [UI] private readonly ToggleButton Select_ToggleButton = null;
        [UI] private readonly ToggleButton Text_ToggleButton = null;
        [UI] private readonly ToggleButton Shape_ToggleButton = null;
        [UI] private readonly Button Color_Button = null;
        #endregion

        private void AssignEvents() {
            DeleteEvent += delegate { Application.Quit(); };

            BG_DrawingArea.Drawn += BG_DrawingArea_Drawn;
            BG_DrawingArea.AddEvents((int)(
                    g.EventMask.ButtonPressMask |
                    g.EventMask.PointerMotionMask |
                    g.EventMask.EnterNotifyMask |
                    g.EventMask.LeaveNotifyMask |
                    g.EventMask.ButtonReleaseMask));
            BG_DrawingArea.ButtonPressEvent += BG_DrawingArea_ButtonPressEvent;
            BG_DrawingArea.MotionNotifyEvent += BG_DrawingArea_MotionNotifyEvent;
            BG_DrawingArea.EnterNotifyEvent += BG_DrawingArea_EnterNotifyEvent;
            BG_DrawingArea.LeaveNotifyEvent += BG_DrawingArea_LeaveNotifyEvent;
            BG_DrawingArea.ButtonReleaseEvent += BG_DrawingArea_ButtonReleaseEvent;

            Close_Button.Clicked += delegate { Close(); };
            Apply_Button.Clicked += async delegate { await Apply_Button_Clicked(); };

            Select_ToggleButton.Toggled += Select_ToggleButton_Toggled;
            Shape_ToggleButton.Toggled += Shape_ToggleButton_Toggled;
            Text_ToggleButton.Toggled += Text_ToggleButto_Toggled;
            Color_Button.Clicked += Color_Button_Clicked;


            //BG_Fixed.Drawn += BG_Fixed_Drawn;
        }
        private void AssignEtc() {
            //UpdateBGSize();
            //Resizable = true;
            //SetSizeRequest(Toolbox_All_Buttons_Box.WidthRequest, Toolbox_All_Buttons_Box.HeightRequest);
            //Resize(Toolbox_All_Buttons_Box.WidthRequest, Toolbox_All_Buttons_Box.HeightRequest);

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

        //private bool BGSize_Done = false;
        private void UpdateBGSize() {
            //if (BGSize_Done) return;
            List<double> widths = new();
            widths.Add(All_Buttons_Box.AllocatedWidth);
            widths.Add(Select_ButtonBox.Visible ? Select_ButtonBox.AllocatedWidth : 0);
            widths.Add(Shape_ButtonBox.Visible ? Shape_ButtonBox.AllocatedWidth : 0);
            widths.Add(Text_ButtonBox.Visible ? Text_ButtonBox.AllocatedWidth : 0);
            int bgW = (int)widths.Max()
                    + 64,
                bgH = All_Buttons_Box.AllocatedHeight
                    + (Select_ButtonBox.Visible ? Select_ButtonBox.AllocatedHeight : 0)
                    + (Shape_ButtonBox.Visible ? Shape_ButtonBox.AllocatedHeight : 0)
                    + (Text_ButtonBox.Visible ? Text_ButtonBox.AllocatedHeight : 0)
                    + 42;
            //BGSize_Done = bgW > 65;
            BG_DrawingArea.SetSizeRequest(bgW, bgH);
            BG_Fixed.SetSizeRequest(bgW, bgH);
            BG_DrawingArea.SizeAllocate(new(0, 0, bgW, bgH));
        }

        private bool Moving = false;
        private bool SS_Toolbox_BG_DrawingArea_Hovering = false;
        private g.Size MoveDiff;
        private double bgAlpha = .01;
        private void BG_DrawingArea_Drawn(object sender, DrawnArgs ev) {
            using c.Context context = ev.Cr;
            //? Clear the canvas
            context.SetSourceRGBA(0, 0, 0, 0);
            context.Operator = c.Operator.Source;
            GetSize(out int w, out int h);
            context.Rectangle(0, 0, w, h);
            context.Fill();

            //? Deal with hovering/moving colors
            double bgAlpha_min = .01, bgAlpha_hovering = .16, bgAlpha_moving = .36;
            if (bgAlpha != bgAlpha_min | bgAlpha != bgAlpha_moving) {
                bgAlpha += Moving | SS_Toolbox_BG_DrawingArea_Hovering
                           ? .03 : -.03;
                mathCommon.ForceInRange(ref bgAlpha, (bgAlpha_min, Moving ? bgAlpha_moving : bgAlpha_hovering));
            }
            //? Draw the result of the above
            context.SetSourceRGBA(1, 1, 1, bgAlpha);
            //if (bgAlpha > bgAlpha_min) //? Skip if invisible
            c.Rectangle bgRect = new(Allocation.X, Allocation.Y, Allocation.Width, Allocation.Height);
            if (Select_ButtonBox.Visible | Shape_ButtonBox.Visible | Text_ButtonBox.Visible)
                context.RoundedRectangle(bgRect, 24);
            else context.RoundedRectangle(bgRect);
            context.Fill();
            //context.LineWidth = 4;
            //context.SetSourceRGBA(0, 0, 0, .8);
            //context.Stroke();

            BG_DrawingArea.QueueDraw();

            //UpdateBGSize();
        }
        private void BG_DrawingArea_ButtonPressEvent(object sender, ButtonPressEventArgs ev) {
            //? Only accept Button 1 (Left click)
            if (ev.Event.Button != 1) return;
            Moving = true;
            GetPosition(out int x, out int y);
            g.Display.Default.GetPointer(out int px, out int py);
            MoveDiff = new(px - x, py - y);
        }
        private void BG_DrawingArea_MotionNotifyEvent(object sender, MotionNotifyEventArgs ev) {
            if (!Moving) return;
            g.Point pointer = Monitors.Pointer_Point();
            Move(pointer.X - MoveDiff.Width, pointer.Y - MoveDiff.Height);
        }
        private void BG_DrawingArea_EnterNotifyEvent(object sender, EnterNotifyEventArgs ev) {
            BG_DrawingArea.Window.Cursor = new g.Cursor(g.CursorType.Plus);
            SS_Toolbox_BG_DrawingArea_Hovering = true;
        }
        private void BG_DrawingArea_LeaveNotifyEvent(object sender, LeaveNotifyEventArgs ev) {
            //BG_DrawingArea.Window.Cursor = new g.Cursor(g.CursorType.Arrow);
            SS_Toolbox_BG_DrawingArea_Hovering = false;
        }
        private void BG_DrawingArea_ButtonReleaseEvent(object sender, ButtonReleaseEventArgs ev) {
            //? Only accept Button 1 (Left click)
            if (ev.Event.Button != 1) return;
            Moving = false;
        }

        private void Select_ToggleButton_Toggled(object sender, EventArgs ev) {
            Select_ButtonBox.Visible = Select_ToggleButton.Active;
            UpdateBGSize();
        }
        private void Shape_ToggleButton_Toggled(object sender, EventArgs ev) {
            Shape_ButtonBox.Visible = Shape_ToggleButton.Active;
            UpdateBGSize();
        }
        private void Text_ToggleButto_Toggled(object sender, EventArgs ev) {
            Text_ButtonBox.Visible = Text_ToggleButton.Active;
            UpdateBGSize();
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
