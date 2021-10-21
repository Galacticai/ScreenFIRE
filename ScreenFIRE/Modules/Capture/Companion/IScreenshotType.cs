namespace ScreenFIRE.Modules.Capture.Companion {

    /// <summary> Types of screenshots
    /// <list type="bullet">
    /// <item> All screens </item>
    /// <item> Monitor at the mouse pointer </item>
    /// <item> Window at the mouse pointer </item>
    /// <item> Active window </item>
    /// </list>
    /// </summary>
    enum IScreenshotType {
        AllMonitors,
        MonitorAtPointer,
        WindowAtPointer,
        ActiveWindow,
        /*Custom*///! Already being treated in Screenshot.cs
    }
}
