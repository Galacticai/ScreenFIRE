using System.IO;

namespace GeekAssistant.Modules.General {
    internal static class PrepareAppdata {
        public static void Run() {
            if (!Directory.Exists(c.GA))
                Directory.CreateDirectory(c.GA);
            if (!Directory.Exists(c.GA_tools))
                Directory.CreateDirectory(c.GA_tools);
            if (!File.Exists($@"{c.GA_tools}\adb.exe"))
                File.WriteAllBytes($@"{c.GA_tools}\adb.exe", prop.tools.adb);
            if (!File.Exists($@"{c.GA_tools}\AdbWinApi.dll"))
                File.WriteAllBytes($@"{c.GA_tools}\AdbWinApi.dll", prop.tools.AdbWinApi);
            if (!File.Exists($@"{c.GA_tools}\AdbWinUsbApi.dll"))
                File.WriteAllBytes($@"{c.GA_tools}\AdbWinUsbApi.dll", prop.tools.AdbWinUsbApi);
            if (!File.Exists($@"{c.GA_tools}\fastboot.exe"))
                File.WriteAllBytes($@"{c.GA_tools}\fastboot.exe", prop.tools.fastboot);
            if (!File.Exists($@"{c.GA_tools}\busybox"))
                File.WriteAllBytes($@"{c.GA_tools}\busybox", prop.tools.busybox);
        }
    }
}
