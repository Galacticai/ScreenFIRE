using Gtk;
using ScreenFIRE.Assets;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using System;
using System.Threading.Tasks;
using gui = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.GUI {


	class ScreenFIRE : Window {

		private Screenshot Screenshot { get; set; }

		[gui]
		private readonly Image ScreenshotImage = null;
		[gui]
		private readonly Button Close_Button = null;
		[gui]
		private readonly DrawingArea drawingArea = null;

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
			Close_Button.Clicked += Close_Button_Clicked;
			//drawingArea.ond += OnExpose;
		}

		public ScreenFIRE() : this(new Builder("ScreenFIRE.glade")) { }

		private ScreenFIRE(Builder builder) : base(builder.GetRawOwnedObject("ScreenFIRE")) {
			builder.Autoconnect(this);
			AssignEvents();

			Screenshot = new Screenshot(IScreenshotType.AllMonitors);
			ScreenshotImage.Pixbuf = Screenshot.Image;
			Move(0, 0);
		}
		private void Close_Button_Clicked(object sender, EventArgs ev)
				=> Hide();

		//protected override bool OnDrawn(Context g) {
		//
		//}
		//void OnExpose(object sender, ExposeEventArgs args) {
		//	DrawingArea area = (DrawingArea)sender;
		//	Cairo.Context cr = Gdk.CairoHelper.Create(area.GdkWindow);
		//
		//	cr.LineWidth = 9;
		//	cr.SetSourceRGB(0.7, 0.2, 0.0);
		//
		//	int width, height;
		//	width = Allocation.Width;
		//	height = Allocation.Height;
		//
		//	cr.Translate(width / 2, height / 2);
		//	cr.Arc(0, 0, (width < height ? width : height) / 2 - 10, 0, 2 * Math.PI);
		//	cr.StrokePreserve();
		//
		//	cr.SetSourceRGB(0.3, 0.4, 0.6);
		//	cr.Fill();
		//
		//	((IDisposable)cr.Target).Dispose();
		//	((IDisposable)cr).Dispose();
		//}
	}
}
