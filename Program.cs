using Gtk;
using ScreenFIRE.GUI;
using ScreenFIRE.Modules.Companion;
using System;

namespace ScreenFIRE {

    class Program {

        public const string packageName = "com.nhk.ScreenFIRE";

        private static bool PlatformIsSupported
            => Environment.OSVersion.Platform == PlatformID.Unix
             | Environment.OSVersion.Platform == PlatformID.Win32NT;

        [STAThread]
        public static void Main(string[] args) {
            if (!PlatformIsSupported)
                throw new PlatformNotSupportedException(
                                $"Sorry ScreenFIRE does not support Platform ID \"{Environment.OSVersion.Platform}\""
                                + $"{c.n}Please run ScreenFIRE on Windows or Linux.");

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
