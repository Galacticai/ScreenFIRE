
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
    All,
    ScreenUnderMouse,
    WindowUnderMouse,
    ActiveWindow,
    Custom
}
