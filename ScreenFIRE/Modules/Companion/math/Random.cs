namespace ScreenFIRE.Modules.Companion.math {

    /// <summary> Random manipulation </summary>
    internal static class Random {

        /// <summary> Pick a random object from input array </summary>
        /// <param name="arr">Input <typeparamref name="type"/>[] array</param>
        /// <returns> Random element from <paramref name="arr"/> (as <typeparamref name="type"/>) </returns>
        public static type PickRandom<type>(this type[] arr) {
            return FromArray(arr);
        }
        /// <summary> Pick a random object from input array </summary>
        /// <param name="arr"> Input <typeparamref name="type"/>[] array </param>
        /// <returns> Random element from <paramref name="arr"/> (as <typeparamref name="type"/>) </returns>
        public static type FromArray<type>(type[] arr)
                => arr[new System.Random().Next(0, arr.Length - 1)]; // -1 because it starts from 0


        //? Bad
        ///// <summary> Generate a random integer between <c>min</c> and <c>max</c> paremeters </summary>
        ///// <param name="min">Minimum range for output</param>
        ///// <param name="max">Maximum range for output</param>
        ///// <returns><paramref name="max"/> &lt; (Integer) &lt; <paramref name="max"/></returns>
        //public static int Int(int min, int max)
        //        => new System.Random().Next(min, max);
    }
}
