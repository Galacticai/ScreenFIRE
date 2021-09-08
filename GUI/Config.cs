using Gdk;
using Gtk;
using System;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFire.GUI;

class Config : Gtk.Window {
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
        Pixbuf ss;
        Gdk.Rectangle monitorGeo = Display.GetMonitor(0).Geometry;
        if (monitorGeo.Width > 0 & monitorGeo.Height > 0)
            ss = Screenshot(new(0, 0, monitorGeo.Width, monitorGeo.Height));
        else throw new ArgumentOutOfRangeException("Fatal Error: Monitor geometry is out of range.");

        FileChooserDialog fcd = new("Save the fire", null, FileChooserAction.Save);
        fcd.AddButton(Stock.Cancel, ResponseType.Cancel);
        fcd.AddButton(Stock.Save, ResponseType.Ok);
        fcd.DefaultResponse = ResponseType.Ok;
        fcd.SelectMultiple = false;

        ResponseType response = (ResponseType)fcd.Run();

        if (response == ResponseType.Ok
           & System.IO.Directory.Exists(fcd.CurrentFolder)
           & ss != null)
            ss.Save(
                    (fcd.Filename.ToLower().Contains(".png") ? fcd.Filename : $"{fcd.Filename}.png"),
                    "png");

        fcd.Destroy();

        _label1.Text = $"Fired a Screenshot!\n\nThis button has been clicked {(1 + (_counter++))} time{(_counter > 1 ? "s" : "")}.";
    }
    public static Pixbuf Screenshot(Gdk.Rectangle rect) {

        Gdk.Pixbuf pixBuf = new Gdk.Pixbuf(Gdk.Colorspace.Rgb, false, 8,
                                       rect.Width, rect.Height);
        pixBuf.GetFromDrawable(rect, Gdk.Colormap.System, 0, 0, 0, 0,
                               rect.Width, rect.Height);
        pixBuf.ScaleSimple(400, 300, Gdk.InterpType.Bilinear);

        return pixBuf;
    }
    public static byte[] GetScreenshot(int compressionLevel) {
        var root = Gdk.Global.DefaultRootWindow;

        int width, height;
        root.GetSize(out width, out height);

        var tmp = new Gdk.Pixbuf(Gdk.Colorspace.Rgb, false, 8, width, height);
        var screenshot = tmp.GetFromDrawable(root, root.Colormap, 0, 0, 0, 0, width, height);

        if (compressionLevel == 0) {
            // return uncompressed
        }
        screenshot.Save("screen.jpg", "jpeg");
        screenshot.Save("screen.bmp", "bmp");
        return screenshot.SaveToBuffer("jpeg");
    }
}
