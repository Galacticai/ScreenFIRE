using Material.Icons;
using System.Collections.Generic;
using System.IO;
using env = System.Environment;

namespace ScreenFIRE.Modules.Companion {

    /// <summary> Prepare the environment for ScreenFIRE </summary>
    internal static class PrepareEnvironment {
        internal static bool Run() {

            //! User profile is missing
            if (!Directory.Exists(Common.UserProfile))
                return false;

            //? (User) >> Pictures
            string MyPicturesPath = string.IsNullOrEmpty(env.GetFolderPath(env.SpecialFolder.MyPictures))
                                   ? Path.Combine(Common.UserProfile, "Pictures")
                                   : env.GetFolderPath(env.SpecialFolder.MyPictures);
            if (!Directory.Exists(MyPicturesPath))
                Directory.CreateDirectory(MyPicturesPath);

            //? (User) >> Pictures >> ScreenFIRE
            if (!Directory.Exists(Common.SF))
                Directory.CreateDirectory(Common.SF);

            //? (System App Data) >> ScreenFIRE
            if (!Directory.Exists(Common.SF_Data))
                Directory.CreateDirectory(Common.SF_Data);

            //? SaveOptions
            if (string.IsNullOrEmpty(Common.LocalSave_Settings.Location))
                Common.LocalSave_Settings.Location = Common.SF;

            ////? Rebuild previously fetched strings according to current language
            //Strings.RebuildStorage(Languages.SystemLanguage());

            //? Build Material Icons cache
            if (Common.Cache.MaterialIcons == null) {
                Common.Cache.MaterialIcons = (Dictionary<MaterialIconKind, string>)MaterialIconDataFactory.DataSetCreate();
                Common.Cache.Save();
            }

            //? Delete 1mo old screenshots
            Delete1MonthOldScreenshots.Run();

            return true;
        }
    }
}
