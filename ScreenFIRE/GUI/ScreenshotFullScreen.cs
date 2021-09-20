using Gtk;
using ScreenFIRE.Assets;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using System.Threading.Tasks;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.GUI {


    class ScreenshotFullScreen : Window {

        private Screenshot Screenshot { get; set; }

        [UI] private readonly Image ScreenshotImage = null;

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
        }

        public ScreenshotFullScreen() : this(new Builder("ScreenshotFullScreen.glade")) {
        }

        private ScreenshotFullScreen(Builder builder) : base(builder.GetRawOwnedObject("ScreenshotFullScreen")) {
            builder.Autoconnect(this);

            AssignEvents();
            SetPosition(WindowPosition.CenterAlways);
            Screenshot = new Screenshot(IScreenshotType.AllMonitors);
            SetDefaultSize(Screenshot.ImageRectangle.Width, Screenshot.ImageRectangle.Height);
            ScreenshotImage.Pixbuf = Screenshot.Image;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs ev)
                => Application.Quit();


    }
}
