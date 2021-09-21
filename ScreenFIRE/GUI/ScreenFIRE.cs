using Gtk;
using ScreenFIRE.Assets;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using System;
using System.Threading.Tasks;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.GUI {


	class ScreenFIRE : Window {

		private Screenshot Screenshot { get; set; }

		[UI] private readonly Image ScreenshotImage = null;
		[UI] private readonly Button Close_Button = null;

		private static string[] txt_privatenameusedonlybythisfunction_238157203985ty9486t4 = null;
		private static async Task<string> txt(int index) {
			return (txt_privatenameusedonlybythisfunction_238157203985ty9486t4
				   ??= (await Strings.Fetch(IStrings.FiredAScreenshot_,//0
											IStrings.ThisButtonHasBeenClicked //1
											))
						)[index];
		}
		private void AssignEvents() {
			DeleteEvent += Window_DeleteEvent;
			Close_Button.Clicked += Close_Button_Clicked;
		}

		public ScreenFIRE() : this(new Builder("ScreenFIRE.glade")) {
		}

		private ScreenFIRE(Builder builder) : base(builder.GetRawOwnedObject("ScreenFIRE")) {
			builder.Autoconnect(this);
			AssignEvents();

			Screenshot = new Screenshot(IScreenshotType.AllMonitors);
			ScreenshotImage.Pixbuf = Screenshot.Image;
			Move(0, 0);
		}

		private void Window_DeleteEvent(object sender, DeleteEventArgs ev)
				=> Application.Quit();

		private void Close_Button_Clicked(object sender, EventArgs ev)
				=> Hide();

	}
}
