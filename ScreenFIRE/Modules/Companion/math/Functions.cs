using System;

namespace ScreenFIRE.Modules.Companion.math {

    /// <summary> Predefined functions </summary>
    internal static class Functions {

        // s    .-
        // |   /
        // | _-
        // 0一一一s

        /// <summary>
        /// <list>
        /// <item> Plug <paramref name="x"/> into the following function: </item>
        /// <c>
        /// <item>‏‏‎s‏‏‎ ‎‏‏‎ ‎‏‏‎‏‏‎ ‎ ‏‏‎ ‎‎‏‏‎ ‎.-   </item>
        /// <item>‏‏‎:‏‏‎ ‎‏‏‎ ‎‏‏‎‏‏‎ ‎ ‎/      </item>
        /// <item>‏‏‎: ‎_-        </item>
        /// <item>‏‏‎‎0 . . . . s </item>
        /// </c>
        /// </list>
        /// </summary>
        /// <param name="x">input</param>
        /// <param name="scale">Size of the wave (Half wave)</param>
        /// <returns> <c>f(<paramref name="x"/>)</c> </returns>
        public static double SmoothStartEnd_Increasing(this double x, double scale) {
            var X = x.ForcedInRange(0, scale); // force x between 0一一一s
            return scale * (-Math.Cos(X * Math.PI) / (2 * scale) + 1 / 2);
        }


        // s -.
        // |   \
        // |    -_
        // 0一一一s

        /// <summary>
        /// <list>
        /// <item> Plug <paramref name="x"/> into the following function: </item>
        /// <c>
        /// <item>‏‏‎‎s‏‏‎ ‎‎-.        </item>
        /// <item>‏‏‎:‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎\      </item>
        /// <item>‏‏‎:‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎-_   </item>
        /// <item>‏‏‎0 . . . . s </item>
        /// </c>
        /// </list>
        /// </summary>
        /// <param name="x">input</param>
        /// <param name="scale">Size of the wave (Half wave)</param>
        /// <returns> <c>f(<paramref name="x"/>)</c> </returns>
        public static double SmoothStartEnd_Decreasing(double x, double scale) {
            var X = x.ForcedInRange(0, scale); // force x between 0一一一s
            return scale * (Math.Sin(X * Math.PI + Math.PI / 2) / 2 + 1 / 2);
        }


        // s \
        // |  \
        // |   *._
        // 0一一一s

        /// <summary>
        /// <list>
        /// <item> Plug <paramref name="x"/> into the following function: </item>
        /// <c>
        /// <item>‏‏‎‎s‏‏‎ ‎\         </item>
        /// <item>‏‏‎:‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎\       </item>
        /// <item>‏‏‎:‏‏‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎*._   </item>
        /// <item>‏‏‎0 . . . . s </item>
        /// </c>
        /// </list>
        /// </summary>
        /// <param name="x">input</param>
        /// <param name="scale">Size of the wave (Half wave)</param>
        /// <returns> <c>f(<paramref name="x"/>)</c> </returns>
        public static double SmoothEnd_Decreasing(double x, double scale) {
            var X = x.ForcedInRange(0, scale); // force x between 0一一一s
            return scale * (-Math.Sin(X * (Math.PI / 2) / scale) + 1);
        }


        // s --.
        // |    \
        // |     \
        // 0一一一s

        /// <summary>
        /// <list>
        /// <item> Plug <paramref name="x"/> into the following function: </item>
        /// <c>
        /// <item>‏‏‎‎s‏‏‎‏‏‎ ‎--.       </item>
        /// <item>‏‏‎:‏‏‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎\     </item>
        /// <item>‏‏‎:‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎\   </item>
        /// <item>‏‏‎0 . . . . s </item>
        /// </c>
        /// </list>
        /// </summary>
        /// <param name="x">input</param>
        /// <param name="scale">Size of the wave (Half wave)</param>
        /// <returns> <c>f(<paramref name="x"/>)</c> </returns>
        public static double SmoothStart_Decreasing(double x, double scale) {
            var X = x.ForcedInRange(0, scale); // force x between 0一一一s
            return scale * Math.Sin(X * (Math.PI / (2 * scale)) + Math.PI / 2);
        }
    }
}