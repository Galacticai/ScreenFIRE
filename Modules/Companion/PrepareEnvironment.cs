using System.IO;

namespace ScreenFIRE.Modules.Companion {

    /// <summary> Prepare the environment for ScreenFIRE </summary>
    internal static class PrepareEnvironment {
        public static void Run() {
            if (!Directory.Exists(c.SF)) Directory.CreateDirectory(c.SF);
            if (!Directory.Exists(c.SF_Data)) Directory.CreateDirectory(c.SF_Data);
        }
    }
}
