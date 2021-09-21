using Gtk;
using ScreenFIRE.GUI;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.OS;
using System;

namespace ScreenFIRE {

	class Program {

		public const string packageName = "com.nhk.ScreenFIRE";

		public static Application app = new(packageName, GLib.ApplicationFlags.None);

		public static Config Config = new();
		public static GUI.ScreenFIRE ScreenFIRE = new();

		[STAThread]
		public static void Main(string[] args) {
			if (!Platform.IsSupported)
				throw new PlatformNotSupportedException(
								$"Sorry ScreenFIRE does not support Platform ID \"{Environment.OSVersion.Platform}\""
								+ $"{Common.n}Please run ScreenFIRE on Windows or Linux.");

			PrepareEnvironment.Run();

			Application.Init();

			app.Register(GLib.Cancellable.Current);

			app.AddWindow(Config);
			app.AddWindow(ScreenFIRE);

			Config.Show();
			//ScreenFIRE.Show();
			Application.Run();
		}
	}
}
