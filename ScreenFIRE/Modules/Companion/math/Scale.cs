using System;

namespace ScreenFIRE.Modules.Companion.math {

    /// <summary> Scale while respecting the proportions </summary>
    internal static class Scale {
        /// <summary> Scale <paramref name="input"/> to fit <paramref name="bound"/> while respecting the proportions </summary>
        /// <param name="input"> Target to be scaled </param>
        /// <param name="bound"> Destination boundary </param>
        /// <returns> Scaled Width &amp; Height (Fit mode) </returns>
        public static (double Width, double Height) Fit(this (double Width, double Height) input,
                                                        (double Width, double Height) bound) {
            double scale = Math.Min(bound.Width / input.Width,
                                    bound.Height / input.Height);
            return (input.Width * scale, input.Height * scale);
        }
        /// <summary> Scale <paramref name="input"/> to fill <paramref name="bound"/> while respecting the proportions </summary>
        /// <param name="input"> Target to be scaled </param>
        /// <param name="bound"> Destination boundary </param>
        /// <returns> Scaled Width &amp; Height (Fill mode) </returns>
        public static (double Width, double h) Fill((double Width, double Height) input,
                                                    (double Width, double Height) bound) {
            double scale = Math.Max(bound.Width / input.Width,
                                    bound.Height / input.Height);
            return (input.Width * scale, input.Height * scale);
        }
    }
}
