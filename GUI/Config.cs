using Gtk;
using System;
using System.Drawing;
using System.Drawing.Imaging;
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
        Bitmap ss;
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
            ss.Save(fcd.Filename + ".png", ImageFormat.Png);
        fcd.Destroy();

        _label1.Text = $"Fired a Screenshot!\n\nThis button has been clicked {(1 + (_counter++))} time{(_counter > 1 ? "s" : "")}.";
    }
    public static Bitmap Screenshot(Gdk.Rectangle rect) {
        Bitmap bmp = new(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
        Graphics.FromImage(bmp).
            CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
        return bmp;
    }
}
