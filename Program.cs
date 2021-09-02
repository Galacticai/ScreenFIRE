using Gtk;
using ScreenFire.GUI;
using System;

namespace ScreenFire;

class Program {

    public const string packageName = "com.nhk.ScreenFire";

    [STAThread]
    public static void Main(string[] args) {
        Application.Init();

        Application ScreenFire = new(packageName, GLib.ApplicationFlags.None);
        ScreenFire.Register(GLib.Cancellable.Current);

        Config ScreenFire_config = new();
        ScreenFire.AddWindow(ScreenFire_config);

        ScreenFire_config.Show();
        Application.Run();
    }
}
