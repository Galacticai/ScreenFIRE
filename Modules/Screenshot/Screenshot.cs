
using ScreenFire.Modules.Screenshot.Companion;
using System;

namespace ScreenFire.Modules.Screenshot;

class Screens {
    //? Example: 0f8fad5b-d9cb-469f-a165-70867728950e 
    public Guid UniqueID { get; private set; }

    public ScreenMetrics ScreenMetrics { get; private set; }
    public ImageMetrics ImageMetrics { get; private set; }

    public static Screens Instance(ScreenMetrics screenMetrics, ImageMetrics imageMetrics)
        => new() {
            UniqueID = Guid.NewGuid(),
            ScreenMetrics = screenMetrics,
            ImageMetrics = imageMetrics
        };
}
