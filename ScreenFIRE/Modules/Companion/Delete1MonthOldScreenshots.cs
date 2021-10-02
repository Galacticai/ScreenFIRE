namespace ScreenFIRE.Modules.Companion {
    class Delete1MonthOldScreenshots {
        public static bool Run() {
            try {
                DateTime oneMonthAgo = DateTime.Now.AddMonths(-1);
                foreach (var dir in Directory.EnumerateDirectories(Common.LocalSave_Settings.Location)) {
                    foreach (var file in Directory.EnumerateFiles(dir))
                        //! Delete file if 1 mo old
                        if (new FileInfo(file).CreationTime < oneMonthAgo)
                            File.Delete(file);
                    //! Delete dir after deleting files if 1mo old
                    if (new DirectoryInfo(dir).CreationTime < oneMonthAgo)
                        Directory.Delete(dir);
                }
                return true;
            } catch {
                return false;
            }
        }
    }
}
