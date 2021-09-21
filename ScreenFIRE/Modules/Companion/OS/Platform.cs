using ScreenFIRE.Modules.Companion.OS.Companion;
using System;
using System.Runtime.InteropServices;

namespace ScreenFIRE.Modules.Companion.OS {

    /// <summary> Common class for general ScreenFIRE stuff </summary>
    public static class Platform {



        /// <summary> Indicate if the current platform is Unix based </summary>
        public static bool RunningLinux
                => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);



        /// <summary> Indicate if the current platform is Win32NT based (Windows NT and above) </summary>
        public static bool RunningWindows
                => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        /// <summary> Indicates if Windows 10 (10.0...) is the current OS</summary>
        public static bool RunningWindows10
            => RunningWindows
                && //! Step into only if running Windows
                    (Environment.OSVersion.Version.ToString(2)
                        == IWindowsVersion.Windows10.ToString(2));


        /// <summary> Indicates whether the current platform is supported by ScreenFIRE (Linux or Windows) </summary>
        public static bool IsSupported => (RunningLinux | RunningWindows);
    }
}
