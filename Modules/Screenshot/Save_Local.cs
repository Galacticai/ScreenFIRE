using System;

namespace ScreenFire.Modules.Screenshot.Companion;

class Save_Local {

    public Guid UID { get; private set; } // Example: 0f8fad5b-d9cb-469f-a165-70867728950e 

    public IScreenshotType ScreenshotType { get; private set; }
    public ISaveFormat SaveFormat { get; private set; }

    public (string Folder, string File) Name { get; private set; }

    public static Save_Local Instance(ScreenshotInfo screenshotInfo,
                                      ISaveFormat saveFormat = ISaveFormat.png)
        => new() {
            UID = screenshotInfo.UID,
            SaveFormat = saveFormat
        };
}
