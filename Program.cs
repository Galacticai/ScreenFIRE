using Gtk;
using ScreenFire.GUI;
using System;

namespace ScreenFire
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();

            Application app = new("com.nhk.ScreenFire", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            Config config = new();
            app.AddWindow(config);

            config.Show();
            Application.Run();
        }
    }
}