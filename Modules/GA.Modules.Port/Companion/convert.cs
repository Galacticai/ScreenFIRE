

using System;

namespace GeekAssistant.Modules.General.Companion {
    internal static class convert {
        public struct Bool {
            /// <param name="value">Input</param>
            /// <returns>1 if true, else 0</returns>
            public static int ToInt(bool value) => value ? 1 : 0;
            /// <param name="value">Input</param>
            /// <returns>"Yes" if True, "No" if False</returns>
            public static string ToYesNo(bool value) => value ? "Yes" : "No";
        }
        public struct Int {
            /// <summary> <list type="bullet">
            /// <item>Use this instead: System.Convert.ToBoolean(value)</item>
            /// <item>Converts the value of the specified 32-bit signed integer to an equivalent Boolean value.</item>
            /// </list></summary>
            /// <param name="value">A string that contains the value of either System.Boolean.TrueString or System.Boolean.FalseString.</param>  
            /// <returns>
            ///     true if value equals System.Boolean.TrueString, or false if value equals System.Boolean.FalseString
            ///     or null.
            /// </returns>  
            public static bool ToBool(int value)
                => System.Convert.ToBoolean(value);
            /// <summary> <list type="bullet">
            /// <item>Use this instead: System.Convert.ToBoolean(value)</item>
            /// <item>Converts the specified string representation of a logical value to its Boolean
            ///     equivalent, using the specified culture-specific formatting information.</item>
            /// </list></summary> 
            /// <param name="value">A string that contains the value of either System.Boolean.TrueString or System.Boolean.FalseString.</param>  
            /// <returns> true if value is not zero; otherwise, false. </returns> 
            public static bool ToBool(float value)
                => System.Convert.ToBoolean(value);
        }
        public struct String {
            /// <param name="value">Input</param>
            /// <returns><list type="bullet">
            /// <item>yes | 1 | true | on => true</item>
            /// <item>(else) => false</item>
            /// </list></returns>
            public static bool ToBool(string value) => value.ToLower() switch {
                "yes" => true,
                "1" => true,
                "true" => true,
                "on" => true,
                _ => false
            };

            /// <summary> Split string to string[] of every line </summary>
            /// <param name="value">Input</param>
            /// <returns><see cref="string[]"/> containing every line of <paramref name="value"/> as an entry</returns>
            public static string[] ToLineArr(string value) {
                // Foolproof
                if (string.IsNullOrEmpty(value)) return null;
                // Do 
                return value.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries); ;
            }
        }
    }
}
