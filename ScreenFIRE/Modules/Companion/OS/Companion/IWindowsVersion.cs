using System;

namespace ScreenFIRE.Modules.Companion.OS.Companion {
    /// <summary> Windows versions </summary>
    public record IWindowsVersion {
        //!                            Windows           Version(Maj, Min)  // PLATFORM ID
        public static readonly Version Windows10         = new(10, 0);      // Win32NT
        public static readonly Version Windows81         = new(6, 3);       // Win32NT
        public static readonly Version Windows8          = new(6, 2);       // Win32NT
        public static readonly Version Windows7_2008r2   = new(6, 1);       // Win32NT
        public static readonly Version WindowsVista_2008 = new(6, 0);       // Win32NT
        public static readonly Version Windows2003       = new(5, 2);       // Win32NT
        public static readonly Version WindowsXP         = new(5, 1);       // Win32NT
        public static readonly Version Windows2000       = new(5, 0);       // Win32NT
        public static readonly Version WindowsMe         = new(4, 90);      // Win32Windows
        public static readonly Version Windows98         = new(4, 10);      // Win32Windows
        public static readonly Version Windows95_NT40    = new(4, 0);       // Win32Windows
    }
}
