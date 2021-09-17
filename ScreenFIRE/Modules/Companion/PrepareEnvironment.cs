using System.IO;

namespace ScreenFIRE.Modules.Companion {

    /// <summary> Prepare the environment for ScreenFIRE </summary>
    internal static class PrepareEnvironment {
        public static void Run() {
            if (!Directory.Exists(Common.SF)) Directory.CreateDirectory(Common.SF);
            if (!Directory.Exists(Common.SF_Data)) Directory.CreateDirectory(Common.SF_Data);
        }
    }
}
