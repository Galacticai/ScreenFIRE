using Gtk;
using ScreenFIRE.Assets;
using ScreenFIRE.Assets.Embedded;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.math;
using ScreenFIRE.Modules.Save;
using System.Threading;
using System.Threading.Tasks;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.GUI {

    class Config : ApplicationWindow {
        [UI] private readonly Label _label1 = null;
        [UI] private readonly Image LogoImage = null;
        [UI] private readonly Label Screenshot_TabButton = null;
        [UI] private readonly Label SaveOptions_TabButton = null;
        [UI] private readonly Label About_TabButton = null;
        [UI] private readonly Button SF_Button_AllMonitors = null;
        [UI] private readonly Button SF_Button_MonitorAtPointer = null;
        [UI] private readonly Button SF_Button_WindowAtPointer = null;
        [UI] private readonly Button SF_Button_ActiveWindow = null;
        [UI] private readonly Button SF_Button_Custom = null;

        private static string[] txt_privatenameusedonlybythisfunction_238157203985ty9486t4 = null;
        private static async Task<string> txt(int index) {
            return (txt_privatenameusedonlybythisfunction_238157203985ty9486t4
                   ??= (await Strings.Fetch(IStrings.ScreenFIREConfig,//0
                                            IStrings.FiredAScreenshot_,//1
                                            IStrings.ThisButtonHasBeenClicked,//2
                                            IStrings.times_1,//3
                                            IStrings.times_2,//4
                                            IStrings.SomethingWentWrong___,//5
                                            IStrings.ChooseHowYouWouldLikeToFireYourScreenshot_,//6
                                            IStrings.AllMonitors,//7
                                            IStrings.MonitorAtPointer,//8
                                            IStrings.WindowAtPointer,//9
                                            IStrings.ActiveWindow,//10
                                            IStrings.FreeAreaSelection,//11
                                            IStrings.Screenshot,//12
                                            IStrings.SavingOptions,//13
                                            IStrings.About//14
                                            ))
                        )[index];
        }
        private void AssignEvents() {
            DeleteEvent += delegate { Application.Quit(); };
            this.
            SF_Button_AllMonitors.Clicked
                += async delegate { await Capture(IScreenshotType.AllMonitors); };
            SF_Button_MonitorAtPointer.Clicked
                += async delegate { await Capture(IScreenshotType.MonitorAtPointer); };
            SF_Button_WindowAtPointer.Clicked
                += async delegate { await Capture(IScreenshotType.WindowAtPointer); };
            SF_Button_ActiveWindow.Clicked
                += async delegate { await Capture(IScreenshotType.ActiveWindow); };

            SF_Button_Custom.Clicked += delegate { Program.ScreenFIRE.ShowAll(); };
        }

        public Config() : this(new Builder("Config.glade")) { }

        private Config(Builder builder) : base(builder.GetRawOwnedObject("Config")) {
            builder.Autoconnect(this);

            AssignEvents();

            Title = txt(0).Result;
            Screenshot_TabButton.Text = txt(12).Result;
            SaveOptions_TabButton.Text = txt(13).Result;
            About_TabButton.Text = txt(14).Result;

            LogoImage.Pixbuf
                = new Gdk.Pixbuf(Vision.BitmapToByteArray(SF.Logo))
                            .ScaleSimple(128, 128, Gdk.InterpType.Bilinear);
            _label1.Text = txt(6).Result;
            SF_Button_AllMonitors.Label = txt(7).Result;
            SF_Button_MonitorAtPointer.Label = txt(8).Result;
            SF_Button_WindowAtPointer.Label = txt(9).Result;
            SF_Button_ActiveWindow.Label = txt(10).Result;
            SF_Button_Custom.Label = txt(11).Result;


        }



        private int _counter;

        private async Task Capture(IScreenshotType screenshotType) {
            Visible = false;
            AcceptFocus = false;
            Thread.Sleep(1000); //! temprary to make sure the window is fully hidden

            using var ss = new Screenshot(screenshotType);
            if (!await Save.Local(ss, this)) {
                MessageDialog failDialog = new(this,
                                               DialogFlags.Modal,
                                               MessageType.Warning,
                                               ButtonsType.Ok,
                                               await txt(5));
                failDialog.Run();
                failDialog.Destroy();
            } else {
                _label1.Text = await txt(1) + Common.nn
                             + await txt(2) + " " + (1 + _counter++) + " " + (_counter > 1 ? await txt(4) : await txt(3));
            }

            AcceptFocus = true;
            ShowAll();

        }
    }
}
