using System.Collections.Generic;
using System.Linq;

namespace ScreenFIRE.Modules.Companion.math {

    static class mathCommon {
        public static type Max<type>(params type[] target) {
            if (target.Length == 1) return target[0];
            List<type> targetList = new();
            foreach (var item in target)
                targetList.Add(item);
            return targetList.Max();
        }
        public static type Min<type>(params type[] target) {
            if (target.Length == 1) return target[0];
            List<type> targetList = new();
            foreach (var item in target)
                targetList.Add(item);
            return targetList.Min();
        }

        /// <summary> Force &amp; set <paramref name="target"/> to be in the <paramref name="boundaries"/> </summary>
        public static double ForceInRange(ref double target, params (double min, double max)[] boundaries) {
            foreach (var (min, max) in boundaries) {
                if (target > max) target = max;
                if (target < min) target = min;
            }
            return target;
        }
        /// <summary> Force &amp; set <paramref name="target"/> to be in the <paramref name="boundaries"/> </summary>
        public static int ForceInRange(ref int target, params (int min, int max)[] boundaries) {
            foreach (var (min, max) in boundaries) {
                if (target > max) target = max;
                if (target < min) target = min;
            }
            return target;
        }

        /// <summary> Check if <paramref name="input"/> is in the <paramref name="boundaries"/> </summary>
        public static bool IsInRange(this double input, params (double min, double max)[] boundaries) {
            foreach (var (min, max) in boundaries)
                if (input < min | input > max)
                    return false;
            return true;
        }
        /// <summary> Check if <paramref name="input"/> is in the <paramref name="boundaries"/> </summary>
        public static bool IsInRange(this int input, params (int min, int max)[] boundaries) {
            foreach (var (min, max) in boundaries)
                if (input < min | input > max)
                    return false;
            return true;
        }
    }
}
