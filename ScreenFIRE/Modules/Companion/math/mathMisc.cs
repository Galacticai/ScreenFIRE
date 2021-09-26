using System.Drawing;
using System.Linq;

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
            public static type FromArray<type>(type[] arr)
                    => arr[Int(0, arr.Length - 1)]; // -1 because it starts from 0
        }

        /// <summary> Force input variable into the range of min and max </summary>
        /// <param name="input">Value to process</param>
        /// <param name="min">Minimum floor</param>
        /// <param name="max">Maximum ceiling</param>
        /// <returns><list type="bullet">
        /// <item>Returns <paramref name="max"/> — if <paramref name="input"/> &gt; <paramref name="max"/></item>
        /// <item>Returns <paramref name="min"/> — if <paramref name="input"/> &lt; <paramref name="min"/></item>
        /// <item>Returns <paramref name="input"/> — if already in range</item>
        /// </list></returns>
        public static double ForcedInRange(double input, double min, double max) {
            if (input > max) return max;
            if (input < min) return min;
            return input;
        }

        /// <summary> Check if <paramref name="input"/> is in range </summary>
        /// <param name="input">Value to process</param>
        /// <param name="min">Minimum floor</param>
        /// <param name="max">Maximum ceiling</param>
        /// <returns>true if input is in [<paramref name="min"/>, <paramref name="max"/>] range. Else false</returns>
        public static bool IsInRange(double input, double min, double max)
            => input >= min & input <= max;

        /// <summary>
        /// Scale a <see cref="Size"/> height and keep the width proportionally correct
        /// </summary>
        /// <param name="input"> Initial input <see cref="Size"/> </param>
        /// <param name="destHeight"> Desired height (Width will follow proportionally) </param>
        /// <returns> Scaled <see cref="Size"/> </returns>
        public static Size ScaleToHeight(Size input, int destHeight)
            => new(input.Width / (input.Height / destHeight), destHeight);


        public struct Arrays {

            /// <summary> Add <paramref name="elements"/> array to the end of <paramref name="array"/></summary>
            /// <typeparam name="type">Type of the array to be used</typeparam>
            /// <param name="array">Array to manipulate</param>
            /// <param name="expansion">Element to add to <paramref name="array"/></param>
            /// <returns>Array {<paramref name="array"/>, <paramref name="expansion"/>} as <typeparamref name="type"/>[]</returns>
            public static type[] AddArrays<type>(type[] array, params type[] elements)
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
