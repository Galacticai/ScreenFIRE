using System;

namespace ScreenFIRE.Modules.Companion {
    /// <summary>
    ///     <list type="bullet"> 
    /// <item> ❌ 0 Win32S           // The operating system is Win32s.This value is no longer in use. </item>
    /// <item> ❌ 1 Win32Windows     // The operating system is Windows 95 or Windows 98. This value is no longer in use. </item>
    /// <item> 2 Win32NT          // The operating system is Windows NT or later. </item>
    /// <item> ❌ 3 WinCE            // The operating system is Windows CE. This value is no longer in use. </item>
    /// <item> 4 Unix             // The operating system is Unix. </item>
    /// <item> 5 Xbox             // The development platform is Xbox 360. This value is no longer in use. </item>
    /// <item> 6 MacOSX 	         // The operating system is Macintosh.This value was returned by Silverlight.On.NET Core, its replacement is Unix. </item>
    /// <item> 7 Other            // Any other operating system. This includes Browser (WASM). </item>
    ///     </list>
    /// </summary>
    class CurrentPlatform {
        public bool Linux => Environment.OSVersion.Platform == PlatformID.Unix;

    }
}
