using ScreenFIRE.Modules.Capture;

namespace ScreenFIRE.Modules.Save {

    internal partial class Save {
        internal static bool Clipboard(Screenshot screenshot) {
            try {
                Gtk.Clipboard.GetDefault(Gdk.Display.Default).Image = screenshot.Image;
                return true;
            } catch { return false; }
        }
    }
}