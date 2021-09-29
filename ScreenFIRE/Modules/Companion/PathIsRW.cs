namespace ScreenFIRE.Modules.Companion {
    class PathIsRW {
        public static bool Run(string path) {
            try {
                string lockfile = Path.Combine(path, "lock.file");
                using var fs = File.Create(lockfile, bufferSize: 1, FileOptions.DeleteOnClose);
                fs.Close();
                File.Delete(lockfile); //idk just doubling down i guess
                return true;
            } catch { return false; }
        }
    }
}
