using System.Linq;

namespace ScreenFIRE.Modules.Companion.math {

    internal static class Arrays {

        /// <summary> Add <paramref name="elements"/> array to the end of <paramref name="array"/></summary>
        /// <typeparam name="T">Type of the array to be used</typeparam>
        /// <param name="array">Array to manipulate</param>
        /// <param name="expansion">Element to add to <paramref name="array"/></param>
        /// <returns>Array {<paramref name="array"/>, <paramref name="expansion"/>} as <typeparamref name="T"/>[]</returns>
        public static T[] AddArrays<T>(this T[] array, params T[] elements)
            => array.Concat(elements).ToArray();
    }
}
