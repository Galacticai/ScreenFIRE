using ScreenFire.Modules.Companion;
using ScreenFire.Modules.Screenshot.Companion;
using System;

namespace ScreenFire.Modules.Screenshot;

class Save_Local {

    public Guid UID { get; private set; } // Example: 0f8fad5b-d9cb-469f-a165-70867728950e 

    public IScreenshotType ScreenshotType { get; private set; }
    public ISaveFormat SaveFormat { get; private set; }
    public Screens Screens { get; private set; }
    public ImageMetrics ImageMetrics { get; private set; }

    //! Save location
    //! -- File Name
    //! -- Folder
    //! 
    //! ?? etc

    public static Save_Local Instance(ScreenshotInfo screenshot,
                                      ISaveFormat saveFormat = ISaveFormat.PNG)
        => new() {
            UID = screenshot.UID,
            SaveFormat = saveFormat
        };
}
