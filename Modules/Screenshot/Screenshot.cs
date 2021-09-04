using ScreenFire.Modules.Screenshot.Companion;


namespace ScreenFire.Modules.Screenshot;
class Screenshot {

    public System.Guid UniqueID { get; private set; }

    public ScreenMetrics ScreenMetrics { get; private set; }
    public ImageMetrics ImageMetrics { get; private set; }

    public static Screenshot instance(ScreenMetrics screenMetrics, ImageMetrics imageMetrics)
        => new() {
            UniqueID = new(),
            ScreenMetrics = screenMetrics,
            ImageMetrics = imageMetrics
        };
}
