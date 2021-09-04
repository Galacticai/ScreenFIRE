using ScreenFire.Modules.Screenshot.Companion;


namespace ScreenFire.Modules.Screenshot;
class Screens {

    public System.Guid UniqueID { get; private set; }

    public ScreenMetrics ScreenMetrics { get; private set; }
    public ImageMetrics ImageMetrics { get; private set; }

    public static Screens instance(ScreenMetrics screenMetrics, ImageMetrics imageMetrics)
        => new() {
            UniqueID = System.Guid.NewGuid(),
            ScreenMetrics = screenMetrics,
            ImageMetrics = imageMetrics
        };
}
