namespace ScreenFIRE.Modules.Companion.math {

    /// <summary> Manipulate numbers and misc stuff </summary>
    internal class mathMisc {

        /// <summary> Random manipulation </summary>
        public struct Random {
            /// <summary> Generate a random integer between <c>min</c> and <c>max</c> paremeters </summary>
            /// <param name="min">Minimum range for output</param>
            /// <param name="max">Maximum range for output</param>
            /// <returns><paramref name="max"/> &lt; (Integer) &lt; <paramref name="max"/></returns>
            public static int Int(int min, int max)
                    => new System.Random().Next(min, max);

            /// <summary> Chooses a random object from input array </summary>
            /// <param name="arr">Input Object array</param>
            /// <returns>Random object from input array</returns>
            public static T FromArray<T>(T[] arr)
                    => arr[Int(0, arr.Length - 1)]; // -1 because it starts from 0
        }

        /// <summary> Force input variable into the range of min and max </summary>
        /// <param name="input">Value to process</param>
        /// <param name="min">Minimum floor</param>
        /// <param name="max">Maximum ceiling</param>
        /// <returns><list type="bullet">
        /// <item>Returns <paramref name="max"/> — if <paramref name="target"/> &gt; <paramref name="max"/></item>
        /// <item>Returns <paramref name="min"/> — if <paramref name="target"/> &lt; <paramref name="min"/></item>
        /// <item>Returns <paramref name="target"/> — if already in range</item>
        /// </list></returns>
        public static double ForcedInRange(ref double target, double min, double max) {
            if (target > max) return target = max;
            if (target < min) return target = min;
            return target;
        }

        /// <summary> Check if <paramref name="input"/> is in range </summary>
        /// <param name="input">Value to process</param>
        /// <param name="min">Minimum floor</param>
        /// <param name="max">Maximum ceiling</param>
        /// <returns>true if input is in [<paramref name="min"/>, <paramref name="max"/>] range. Else false</returns>
        public static bool IsInRange(double input, double min, double max)
            => input >= min & input <= max;

        /// <summary> Scale while respecting the proportions </summary>
        public struct Scale {
            /// <summary> Scale <paramref name="input"/> to fit <paramref name="bound"/> while respecting the proportions </summary>
            /// <param name="input"> Target to be scaled </param>
            /// <param name="bound"> Destination boundary </param>
            /// <returns> Scaled Width &amp; Height (Fit mode) </returns>
            public static (double Width, double Height) Fit((double Width, double Height) input,
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

        public struct Arrays {

            /// <summary> Add <paramref name="elements"/> array to the end of <paramref name="array"/></summary>
            /// <typeparam name="T">Type of the array to be used</typeparam>
            /// <param name="array">Array to manipulate</param>
            /// <param name="expansion">Element to add to <paramref name="array"/></param>
            /// <returns>Array {<paramref name="array"/>, <paramref name="expansion"/>} as <typeparamref name="T"/>[]</returns>
            public static T[] AddArrays<T>(T[] array, params T[] elements)
                => array.Concat(elements).ToArray();
            //{
            //type[] expandedArray = new type[array.Length + 1]; // set expandedArray size to "length" (bigger by 1)
            //for (int i = 0; i < array.Length; i++)
            //    expandedArray[i] = array[i]; // copy all elements from array to expandedArray
            //expandedArray[array.Length] = element; // add the final element
            //return expandedArray;
            //}
        }
    }
}
