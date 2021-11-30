using Gtk;
using ScreenFIRE.GUI;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.OS;
using System;

namespace ScreenFIRE {

    class Program {

        public const string PackageName = "com.xeroling.ScreenFIRE";

        public static Application app = new(PackageName, GLib.ApplicationFlags.None);

        public static Config Config = new();
        public static GUI.ScreenFIRE ScreenFIRE = new();
        public static GUI.SS_Toolbox SS_Toolbox = new();


        [STAThread]
        public static void Main(string[] args) {
            if (!Platform.IsSupported)
                throw new PlatformNotSupportedException
                                ($"Sorry ScreenFIRE does not support Platform ID \"{Environment.OSVersion.Platform}\""
                                + $"{Common.n}Please run ScreenFIRE on Windows or Linux.");

            if (!PrepareEnvironment.Run()) throw new NullReferenceException
                    ("ScreenFIRE cannot be used with your current system state!");

            Application.Init();

            app.Register(GLib.Cancellable.Current);

            app.AddWindow(Config);
            app.AddWindow(ScreenFIRE);
            app.AddWindow(SS_Toolbox);

            foreach (string arg in args)
                if (arg.ToLower() == "screenshot")
                    ScreenFIRE.Show();

            //!? TEMPORARY
            //Config.Show();
            //ScreenFIRE.Show();
            SS_Toolbox.Show();

            Application.Run();
        }
    }
}
