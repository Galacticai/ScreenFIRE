using Gtk;
using ScreenFIRE.Assets;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Save;
using System;
using System.Threading;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.GUI {

    class Config : Window {
        [UI] private readonly Label _label1 = null;
        [UI] private readonly Button ss_Button_AllMonitors = null;
        [UI] private readonly Button ss_Button_MonitorAtPointer = null;
        [UI] private readonly Button ss_Button_WindowAtPointer = null;
        [UI] private readonly Button ss_Button_ActiveWindow = null;

        private static readonly string[] labelText = Strings.Fetch(IStrings.FiredAScreenshot_,
                                                                   IStrings.ThisButtonHasBeenClicked,
                                                                   IStrings.times_1,
                                                                   IStrings.times_2);
        private void AssignEvents() {
            DeleteEvent += Window_DeleteEvent;
            ss_Button_AllMonitors.Clicked += ss_Button_AllMonitors_Clicked;
            ss_Button_MonitorAtPointer.Clicked += ss_Button_MonitorAtPointer_Clicked;
            ss_Button_WindowAtPointer.Clicked += ss_Button_WindowAtPointer_Clicked;
            ss_Button_ActiveWindow.Clicked += ss_Button_ActiveWindow_Clicked;
        }

        public Config() : this(new Builder("Config.glade")) { }

        private Config(Builder builder) : base(builder.GetRawOwnedObject("Config")) {
            builder.Autoconnect(this);

            AssignEvents();

            _label1.Text = Strings.Fetch(IStrings.ChooseHowYouWouldLikeToFireYourScreen_);
            ss_Button_AllMonitors.Label = Strings.Fetch(IStrings.AllMonitors);
            ss_Button_MonitorAtPointer.Label = Strings.Fetch(IStrings.MonitorAtPointer);
            ss_Button_WindowAtPointer.Label = Strings.Fetch(IStrings.WindowAtPointer);
            ss_Button_ActiveWindow.Label = Strings.Fetch(IStrings.ActiveWindow);
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs ev)
                => Application.Quit();


        private int _counter;
        private void UpdateLabel() {
            _label1.Text = labelText[0] + Common.nn
                         + labelText[1] + (1 + (_counter++)) + (_counter > 1 ? labelText[3] : labelText[2]);
        }
        private void ss_Button_AllMonitors_Clicked(object sender, EventArgs ev) {
            Timer timer = new(timerCallback);
            timer.Change(2000, Timeout.Infinite);
            void timerCallback(object state) {
                Screenshot ss = new(IScreenshotType.AllMonitors);
                Save.Local(ss, this);
                UpdateLabel();
            }
        }
        private void ss_Button_MonitorAtPointer_Clicked(object sender, EventArgs ev) {
            Timer timer = new(timerCallback);
            timer.Change(2000, Timeout.Infinite);
            void timerCallback(object state) {
                Screenshot ss = new(IScreenshotType.MonitorAtPointer);
                Save.Local(ss, this);
                UpdateLabel();
            }
        }
        private void ss_Button_WindowAtPointer_Clicked(object sender, EventArgs ev) {
            Timer timer = new(timerCallback);
            timer.Change(2000, Timeout.Infinite);
            void timerCallback(object state) {
                Screenshot ss = new(IScreenshotType.WindowAtPointer);
                Save.Local(ss, this);
                UpdateLabel();
            }
        }
        private void ss_Button_ActiveWindow_Clicked(object sender, EventArgs ev) {
            Timer timer = new(timerCallback);
            timer.Change(2000, Timeout.Infinite);
            void timerCallback(object state) {
                Screenshot ss = new(IScreenshotType.ActiveWindow);
                Save.Local(ss, this);
                UpdateLabel();
            }
        }
    }
}
