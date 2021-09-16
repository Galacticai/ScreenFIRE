using System;


namespace ScreenFIRE.Modules.Companion {

    /// <summary> Common class for general ScreenFIRE stuff </summary>
    static class Platform {

        /// <summary> Indicate if the current platform is Unix based </summary>
        public static bool RunningLinux
            => Environment.OSVersion.Platform == PlatformID.Unix;

        /// <summary> Indicate if the current platform is Win32NT based (Windows NT and above) </summary>
        public static bool RunningWindows
            => Environment.OSVersion.Platform == PlatformID.Win32NT;

    }
}
