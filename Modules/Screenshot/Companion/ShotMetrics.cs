using Gdk;

namespace ScreenFire.Modules.Screenshot.Companion;

public class ShotMetrics {

    public Rectangle Area { get; private set; }
    public Point Offset { get; private set; }
    public bool DefaultSavePath { get; private set; }

    public static ShotMetrics instance(Rectangle area,
                                       Point offset,
                                       bool defaultSavePath) {
        return new() {
            Area = area,
            Offset = offset,
            DefaultSavePath = defaultSavePath
        };
    }
}
