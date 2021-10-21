using ScreenFIRE.Modules.Companion.OS.Companion;
using System;
using System.Runtime.InteropServices;

namespace ScreenFIRE.Modules.Companion.OS {

    /// <summary> Common class for general ScreenFIRE stuff </summary>
    public static class Platform {

        /// <summary> Indicates whether the current platform is supported by ScreenFIRE (Linux or Windows) </summary>
        internal static bool IsSupported => (RunningLinux | RunningWindows);

        /// <summary> Indicate if the current platform is Linux based </summary>
        public static bool RunningLinux
            => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);


        /// <summary> <see cref="true"/> if running Win32NT base (Windows NT and above) </summary>
        public static bool RunningWindows
            => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        /// <returns> <see cref="true"/> if running Windows 10 (22000 > Build >= 10240) </returns>
        public static bool RunningWindows10() {
            if (!RunningWindows) return false;
            return (Environment.OSVersion.Version.Build >= IWindowsVersion.Windows10.Build)
                 & (Environment.OSVersion.Version.Build < IWindowsVersion.Windows11.Build);
        }
        /// <returns> <see cref="true"/> if running Windows 11 (Build >= 22000) </returns>
        public static bool RunningWindows11_orAbove() {
            if (!RunningWindows) return false;
            return Environment.OSVersion.Version.Build >= IWindowsVersion.Windows11.Build;
        }

    }
}
