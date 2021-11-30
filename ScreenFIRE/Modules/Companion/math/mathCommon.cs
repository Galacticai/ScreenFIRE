namespace ScreenFIRE.Modules.Companion.math {

    static class mathCommon {
        /// <summary> Force &amp; set <paramref name="target"/> to be between <paramref name="min"/> and <paramref name="max"/> </summary>
        public static int ForceInRange(ref int target, int min, int max) {
            if (target > max) return target = max;
            if (target < min) return target = min;
            return target;
        }
        /// <summary> Force &amp; set <paramref name="target"/> to be between <paramref name="min"/> and <paramref name="max"/> </summary>
        public static double ForceInRange(ref double target, double min, double max) {
            if (target > max) return target = max;
            if (target < min) return target = min;
            return target;
        }

        /// <summary> Check if <paramref name="input"/> is in range </summary>
        /// <param name="input">Value to process</param>
        /// <param name="min">Minimum floor</param>
        /// <param name="max">Maximum ceiling</param>
        /// <returns>true if input is in [<paramref name="min"/>, <paramref name="max"/>] range. Else false</returns>
        public static bool IsInRange(this double input, double min, double max)
            => input >= min & input <= max;

    }
}
