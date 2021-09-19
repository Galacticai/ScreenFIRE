using Gtk;
using ScreenFIRE.Assets;
using ScreenFIRE.Assets.Embedded;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.math;
using ScreenFIRE.Modules.Save;
using System;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.GUI {

    class Config : Window {
        [UI] private readonly Label _label1 = null;
        [UI] private readonly Image LogoImage = null;
        [UI] private readonly Button SF_Button_AllMonitors = null;
        [UI] private readonly Button SF_Button_MonitorAtPointer = null;
        [UI] private readonly Button SF_Button_WindowAtPointer = null;
        [UI] private readonly Button SF_Button_ActiveWindow = null;

        private static readonly string[] txt
            = Strings.Fetch(IStrings.FiredAScreenshot_,
                            IStrings.ThisButtonHasBeenClicked,
                            IStrings.times_1,
                            IStrings.times_2,
                            IStrings.SomethingWentWrong___,
                            IStrings.ChooseHowYouWouldLikeToFireYourScreenshot_,
                            IStrings.AllMonitors,
                            IStrings.MonitorAtPointer,
                            IStrings.WindowAtPointer,
                            IStrings.ActiveWindow);
        private void AssignEvents() {
            DeleteEvent += Window_DeleteEvent;
            SF_Button_AllMonitors.Clicked += SF_Button_AllMonitors_Clicked;
            SF_Button_MonitorAtPointer.Clicked += SF_Button_MonitorAtPointer_Clicked;
            SF_Button_WindowAtPointer.Clicked += SF_Button_WindowAtPointer_Clicked;
            SF_Button_ActiveWindow.Clicked += SF_Button_ActiveWindow_Clicked;
        }

        public Config() : this(new Builder("Config.glade")) { }

        private Config(Builder builder) : base(builder.GetRawOwnedObject("Config")) {
            builder.Autoconnect(this);

            AssignEvents();
            LogoImage.Pixbuf
                = new Gdk.Pixbuf(Vision.BitmapToByteArray(SF.Logo))
                            .ScaleSimple(360, 360, Gdk.InterpType.Bilinear);
            LogoImage.SetSizeRequest(360, 360);
            _label1.Text = txt[5];
            SF_Button_AllMonitors.Label = txt[6];
            SF_Button_MonitorAtPointer.Label = txt[7];
            SF_Button_WindowAtPointer.Label = txt[8];
            SF_Button_ActiveWindow.Label = txt[9];
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs ev)
                => Application.Quit();


        private int _counter;
        private void UpdateLabel() {
            _label1.Text = txt[0] + Common.nn
                         + txt[1] + (1 + (_counter++)) + (_counter > 1 ? txt[3] : txt[2]);
        }
        private void SF_Button_AllMonitors_Clicked(object sender, EventArgs ev) {
            Capture(IScreenshotType.AllMonitors);
        }
        private void SF_Button_MonitorAtPointer_Clicked(object sender, EventArgs ev) {
            Capture(IScreenshotType.MonitorAtPointer);
        }
        private void SF_Button_WindowAtPointer_Clicked(object sender, EventArgs ev) {
            Capture(IScreenshotType.WindowAtPointer);
        }
        private void SF_Button_ActiveWindow_Clicked(object sender, EventArgs ev) {
            Capture(IScreenshotType.ActiveWindow);
        }
        private void Capture(IScreenshotType screenshotType) {
            //Timer timer = new(timerCallback);
            //timer.Change(2000, Timeout.Infinite);
            //void timerCallback(object state) {
            Screenshot ss = new(screenshotType);
            if (!Save.Local(ss, this))
                new MessageDialog(this,
                                  DialogFlags.Modal,
                                  MessageType.Warning,
                                  ButtonsType.Ok,
                                  txt[9])
                    .Run();
            UpdateLabel();
            //}

        }
    }
}
