
using ScreenFire.Modules.Screenshot.Companion;
using System;

namespace ScreenFire.Modules.Screenshot;

class Screenshot {

    public Guid UniqueID { get; private set; } // Example: 0f8fad5b-d9cb-469f-a165-70867728950e 

    public ScreenMetrics ScreenMetrics { get; private set; }
    public ImageMetrics ImageMetrics { get; private set; }

    public static Screenshot Instance(ScreenMetrics screenMetrics, ImageMetrics imageMetrics)
        => new() {
            UniqueID = Guid.NewGuid(),
            ScreenMetrics = screenMetrics,
            ImageMetrics = imageMetrics
        };
}
