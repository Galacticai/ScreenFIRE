using Gtk;
using ScreenFIRE.GUI;
using System;

namespace ScreenFIRE
{

    class Program
    {

        public const string packageName = "com.nhk.ScreenFIRE";

        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();

            Application ScreenFIRE = new(packageName, GLib.ApplicationFlags.None);
            ScreenFIRE.Register(GLib.Cancellable.Current);

            Config ScreenFIRE_config = new();
            ScreenFIRE.AddWindow(ScreenFIRE_config);

            ScreenFIRE_config.Show();
            Application.Run();
        }
    }
}
