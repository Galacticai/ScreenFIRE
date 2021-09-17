using Gtk;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Save;
using System;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.GUI {

    class Config : Window {
        [UI] private Label _label1 = null;
        [UI] private Button ss_Button = null;

        private void AssignEvents() {
            DeleteEvent += Window_DeleteEvent;
            ss_Button.Clicked += ss_Button_Clicked;
        }

        public Config() : this(new Builder("Config.glade")) { }

        private Config(Builder builder) : base(builder.GetRawOwnedObject("Config")) {
            builder.Autoconnect(this);

            AssignEvents();
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs ev)
                => Application.Quit();

        private int _counter;
        private void ss_Button_Clicked(object sender, EventArgs ev) {
            Screenshot ss = new(IScreenshotType.All, new Monitors().AllRectangle);

            Save.Local(ss);

            _label1.Text = $"Fired a Screenshot!\n\nThis button has been clicked {(1 + (_counter++))} time{(_counter > 1 ? "s" : "")}.";
        }
    }
}
