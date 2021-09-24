using System.IO;
using env = System.Environment;

namespace ScreenFIRE.Modules.Companion {

    /// <summary> Prepare the environment for ScreenFIRE </summary>
    internal static class PrepareEnvironment {
        public static bool Run() {

            if (!Directory.Exists(Common.UserProfile))
                return false; //! User profile is missing

            if (!Directory.Exists(env.GetFolderPath(env.SpecialFolder.MyPictures)))
                Directory.CreateDirectory(
                            env.GetFolderPath(env.SpecialFolder.MyPictures));

            if (!Directory.Exists(Common.SF))
                Directory.CreateDirectory(Common.SF);

            if (!Directory.Exists(Common.SF_Data))
                Directory.CreateDirectory(Common.SF_Data);

            return true;
        }
    }
}
