using Cairo;
using Gtk;
using ScreenFIRE.Assets;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.math;
using System;
using System.Threading;
using System.Threading.Tasks;
using gdk = Gdk;

namespace ScreenFIRE.GUI {


	class ScreenFIRE : Window {

		private Screenshot Screenshot { get; set; }

		[Builder.Object]
		private readonly Image ScreenshotImage = null;
		[Builder.Object]
		private readonly Button Close_Button = null;
		[Builder.Object]
		private readonly DrawingArea SS_DrawingArea = null;

		private static string[] txt_privatenameusedonlybythisfunction_238157203985ty9486t4 = null;
		private static async Task<string> txt(int index) {
			return (txt_privatenameusedonlybythisfunction_238157203985ty9486t4
				   ??= (await Strings.Fetch(IStrings.FiredAScreenshot_,//0
											IStrings.ThisButtonHasBeenClicked //1
											))
						)[index];
		}
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
			ScreenshotImage.Pixbuf = Screenshot.Image;
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
											 Screenshot.Image.Width,
											 Screenshot.Image.Height)
											 ); //prepare the context
			SS_DrawingArea.Realize();

		}
		private void Draw_DragMotion(object sender, DragMotionArgs args) {
			endPoint = Monitors.Pointer_Point(); //set the end point on motion

			gdk.Rectangle gdkRect = Vision.Geometry.PointsToRectangle(startPoint, endPoint);
			g.MoveTo(gdkRect.X, gdkRect.Y);
			g.SetSourceRGB(0.3, 0.4, 0.6);
			Rectangle rect = new(gdkRect.X, gdkRect.Y, gdkRect.Width, gdkRect.Height);
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
