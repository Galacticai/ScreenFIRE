namespace ScreenFIRE.Modules.Companion.math {

    internal static class mathCommon {
        /// <summary> Force input variable into the range of min and max </summary>
        /// <param name="target">Value to process</param>
        /// <param name="min">Minimum floor</param>
        /// <param name="max">Maximum ceiling</param>
        /// <returns><list type="bullet">
        /// <item>Returns <paramref name="max"/> — if <paramref name="target"/> &gt; <paramref name="max"/></item>
        /// <item>Returns <paramref name="min"/> — if <paramref name="target"/> &lt; <paramref name="min"/></item>
        /// <item>Returns <paramref name="target"/> — if already in range</item>
        /// </list></returns>
        public static double ForcedInRange(this double target, double min, double max) {
            if (target > max) return max;
            if (target < min) return min;
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
