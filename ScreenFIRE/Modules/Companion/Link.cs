using ScreenFIRE.Modules.Companion.OS;
using System.Diagnostics;

namespace ScreenFIRE.Modules.Companion {
    internal static class Link {
        public static bool Open(string url) {
            try {
                Process.Start(url);
            } catch {
                if (Platform.RunningWindows)
                    Process.Start(
                        new ProcessStartInfo("cmd", $"/c start {url.Replace("&", "^&")}") {
                            CreateNoWindow = true
                        });

                else if (Platform.RunningLinux)
                    Process.Start("xdg-open", url);

                else return false;
            }
            return true;
        }
    }
}
