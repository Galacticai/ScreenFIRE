﻿namespace ScreenFIRE.Modules.Capture.Companion {

    /// <summary> Types of screenshots 
    /// <list type="bullet">
    /// <item>All screens</item>
    /// <item>Monitor at the mouse pointer</item>
    /// <item>Window at the mouse pointer</item>
    /// <item>Active window</item> 
    /// <item>Custom rectangle defined by the metrics</item> 
    /// </list>
    /// </summary>
    enum IScreenshotType {
        AllMonitors,
        MonitorAtPointer,
        WindowAtPointer,
        ActiveWindow //,
        //Custom
    }
}