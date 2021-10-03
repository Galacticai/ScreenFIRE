using ScreenFIRE.Modules.Capture;

namespace ScreenFIRE.Modules.Save {

    partial class Save {
        public static bool Clipboard(Screenshot screenshot) {
            try {
                Gtk.Clipboard.GetDefault(Gdk.Display.Default).Image = screenshot.Image;
                return true;
            } catch { return false; }
        }
    }
}