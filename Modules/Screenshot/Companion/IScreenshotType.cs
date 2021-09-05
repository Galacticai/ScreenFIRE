using System.ComponentModel;

namespace ScreenFire.Modules.Screenshot.Companion;

/// <summary> Types of screenshots 
/// <list type="bullet">
/// <item>All screens</item>
/// <item>Screen under the mouse pointer</item>
/// <item>Window under the mouse pointer</item>
/// <item>Active window</item> 
/// <item>Custom rectangle defined by the metrics</item> 
/// </list>
/// </summary>
enum IScreenshotType {
    [Description("All screens")]
    All,
    [Description("Screen under the mouse pointer")]
    ScreenUnderMouse,
    [Description("Window under the mouse pointer")]
    WindowUnderMouse,
    [Description("Active window")]
    ActiveWindow,
    [Description("Custom rectangle defined by the metrics")]
    Custom
}
