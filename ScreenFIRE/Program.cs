using Gtk;
using ScreenFIRE.GUI;
using ScreenFIRE.Modules.Companion;
using System;

namespace ScreenFIRE {

    class Program {

        public const string packageName = "com.nhk.ScreenFIRE";

        public static Application ScreenFIRE = new(packageName, GLib.ApplicationFlags.None);

        public static Config Config = new();
        public static ScreenshotFullScreen ScreenshotFullScreen = new();
        private static bool PlatformIsSupported
            => Environment.OSVersion.Platform == PlatformID.Unix
             | Environment.OSVersion.Platform == PlatformID.Win32NT;

        [STAThread]
        public static void Main(string[] args) {
            if (!PlatformIsSupported)
                throw new PlatformNotSupportedException(
                                $"Sorry ScreenFIRE does not support Platform ID \"{Environment.OSVersion.Platform}\""
                                + $"{Common.n}Please run ScreenFIRE on Windows or Linux.");

            PrepareEnvironment.Run();

            Application.Init();

            ScreenFIRE.Register(GLib.Cancellable.Current);

            ScreenFIRE.AddWindow(Config);
            ScreenFIRE.AddWindow(ScreenshotFullScreen);

            Config.Show();
            Application.Run();
        }
    }
}
