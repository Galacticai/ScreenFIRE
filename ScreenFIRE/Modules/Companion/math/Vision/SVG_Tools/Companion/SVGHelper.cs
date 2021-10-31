using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ScreenFIRE.Modules.Companion.math.Vision.SVG_Tools.Companion {

    /// <summary> Parse||Convert various <see cref="SVG_PathdElement"/>-related things</summary>
    public static class SVGHelper {

        //? https://regex101.com/r/N8HOei/1
        //? [ZzAaRrMmLlHhVvCcSsGgEeFfDdJjQqTt]((\s+|)([-+]|)(\d+\.|\.|)\d+){1,}

        private static readonly Regex RealNumber_Regex = new(@"([-+]{0,})(\d+\.|\.|)\d+"),
                                      PathdElement_Type_Regex = new("[ZzAaRrMmLlHhVvCcSsGgEeFfDdJjQqTt]"),
                                      PathdElement_Values_Regex = new($"((\\s{{0,}}){RealNumber_Regex}){{1,}}"),
                                      PathdElement_Regex = new(PathdElement_Type_Regex.ToString() + PathdElement_Values_Regex.ToString());

        public static List<SVG_PathdElement> ToPathd(this string pathd_Raw) => Parse.Pathd(pathd_Raw);

        ///// <summary> Generate an <see cref="List"/> of the path data values' indexes </summary>
        ///// <param name="pathdElement"> data to be processed </param>
        ///// <returns> <see cref="List"/>&lt;<see cref="int"/>&gt; containing the index of each value in the path data string </returns>
        //private static (string[] Parts, List<Range> Ranges) PathdElement_Parts(string pathdElement) {
        //    List<Range> ranges = new();
        //    string[] parts = Array.Empty<string>();
        //
        //
        //    List<int> indexes = pathdElement.IndexesOf(" ").ToList();
        //
        //    int i = 0;
        //    foreach (int index in indexes) {
        //        i++;
        //
        //        int endindex;
        //        try {
        //            endindex = indexes[i];
        //        } catch { endindex = pathdElement.Length; }
        //        ranges.Add(new(index + 1, endindex));
        //
        //        parts[i] = pathdElement[(index + 1)..endindex];
        //    }
        //    return (parts, ranges);
        //}

        public struct Parse {

            public static List<Range> ValueRanges(SVG_PathdElement element) {
                List<Range> valueRanges = new();
                int[] indexes = element.Data.IndexesOf(" ").ToArray();
                int i = 0;
                foreach (int index in indexes) {
                    i++;
                    int endindex;
                    try {
                        endindex = indexes[i];
                    } catch { endindex = element.Data.Length; }
                    valueRanges.Add(new(index + 1, endindex));
                }

                return valueRanges;
            }


            /// <param name="pathd"> Raw value of <c>&lt;path d="...this..." /&gt;</c> </param>
            /// <returns> List&lt;<see cref="SVG_PathdElement"/>&gt; containing <br/>
            /// the the path d elements from <paramref name="pathd"/> <see cref="string"/> </returns>
            /// <exception cref="ArgumentException"/>
            public static List<SVG_PathdElement> Pathd(string pathd) {
                //? Example path d="..."
                // M 12.75     2.75 a .75 .75 0 00       -1.5 0 V 4.5 H 9.276      a 1.75 1.75 0 00 -.985 .303 L6.596 5.957A.25.25 0 016.455 6H2.353a.75.75 0 100 1.5H3.93L.563 15.18a.762.762 0 00.21.88 c .08 .064 .161 .125 .309 .221 .186 .121 .452 .278 .792 .433 .68 .311 1.662 .62 2.876 .62 a6 .919 6.919 0 002.876 - .62c.34 - .155.606 - .312.792 - .433.15 - .097.23 - .158.31 - .223a.75.75 0 00.209 - .878L5.569 7.5h.886c.351 0 .694 - .106.984 - .303l1.696 - 1.154A.25.25 0 019.275 6h1.975v14.5H6.763a.75.75 0 000 1.5h10.474a.75.75 0 000 - 1.5H12.75V6h1.974c.05 0 .1.015.14.043l1.697 1.154c.29.197.633.303.984.303h.886l - 3.368 7.68a.75.75 0 00.23.896c.012.009 0 0 .002 0a3.154 3.154 0 00.31.206c.185.112.45.256.79.4a7.343 7.343 0 002.855.568 7.343 7.343 0 002.856 - .569c.338 - .143.604 - .287.79 - .399a3.5 3.5 0 00.31 - .206.75.75 0 00.23 - .896L20.07 7.5h1.578a.75.75 0 000 - 1.5h - 4.102a.25.25 0 01 - .14 - .043l - 1.697 - 1.154a1.75 1.75 0 00 - .984 - .303H12.75V2.75zM2.193 15.198a5.418 5.418 0 002.557.635 5.418 5.418 0 002.557 - .635L4.75 9.368l - 2.557 5.83zm14.51 - .024c.082.04.174.083.275.126.53.223 1.305.45 2.272.45a5.846 5.846 0 002.547 - .576L19.25 9.367l - 2.547 5.807z";


                //? Remove extra spaces and convert , to space
                // M 12.75 2.75 a .75 .75 0 00 -1.5 0 V 4.5 H 9.276 a 1.75 1.75 0 00 -.985 .303 L6.596 5.957A.25.25 0 016.455 6H2.353a.75.75 0 100 1.5H3.93L.563 15.18a.762.762 0 00.21.88 c .08 .064 .161 .125 .309 .221 .186 .121 .452 .278 .792 .433 .68 .311 1.662 .62 2.876 .62 a6 .919 6.919 0 002.876 - .62c.34 - .155.606 - .312.792 - .433.15 - .097.23 - .158.31 - .223a.75.75 0 00.209 - .878L5.569 7.5h.886c.351 0 .694 - .106.984 - .303l1.696 - 1.154A.25.25 0 019.275 6h1.975v14.5H6.763a.75.75 0 000 1.5h10.474a.75.75 0 000 - 1.5H12.75V6h1.974c.05 0 .1.015.14.043l1.697 1.154c.29.197.633.303.984.303h.886l - 3.368 7.68a.75.75 0 00.23.896c.012.009 0 0 .002 0a3.154 3.154 0 00.31.206c.185.112.45.256.79.4a7.343 7.343 0 002.855.568 7.343 7.343 0 002.856 - .569c.338 - .143.604 - .287.79 - .399a3.5 3.5 0 00.31 - .206.75.75 0 00.23 - .896L20.07 7.5h1.578a.75.75 0 000 - 1.5h - 4.102a.25.25 0 01 - .14 - .043l - 1.697 - 1.154a1.75 1.75 0 00 - .984 - .303H12.75V2.75zM2.193 15.198a5.418 5.418 0 002.557.635 5.418 5.418 0 002.557 - .635L4.75 9.368l - 2.557 5.83zm14.51 - .024c.082.04.174.083.275.126.53.223 1.305.45 2.272.45a5.846 5.846 0 002.547 - .576L19.25 9.367l - 2.547 5.807z";
                pathd = Regex.Replace(pathd.Trim(), @"(\s+|[,])", " ");

                List<SVG_PathdElement> pathData_List = new();

                //? Identify pathd elements
                MatchCollection pathdElement_Collection_Raw
                    = Regex.Matches(pathd, PathdElement_Regex.ToString());

                //! No pathd elements were found
                if (pathdElement_Collection_Raw.Count == 0)
                    throw new ArgumentException("pathd not recognized");

                //? Add pathd elements to list
                foreach (Match pathdElement_Raw_Match in pathdElement_Collection_Raw) {
                    //? Parse
                    SVG_PathdElement pathdElement
                        = Parse.PathdElement(pathdElement_Raw_Match.Value);

                    //? Add to list
                    pathData_List.Add(pathdElement);
                }
                return pathData_List;
            }

            /// <summary>
            /// Convert a <see cref="string"/>[] (Containing point data) to the corresponding type in <see cref="Pathd"/> </summary>
            /// <param name="pathdElement_Raw"> Raw path d element <see cref="string"/>[] to be processed </param>
            /// <returns> <see cref="SVG_PathdElement"/> from <paramref name="pathdElement_Raw"/> <see cref="string"/>[] (Parts array) </returns>
            /// <exception cref="ArgumentException"/>
            /// <exception cref="FormatException"/> <exception cref="OverflowException"/>
            public static SVG_PathdElement PathdElement(string pathdElement_Raw, bool WithRanges = false) {
                pathdElement_Raw = pathdElement_Raw.Trim();

                //List<Range> valueRanges = new();
                string[] values = Array.Empty<string>();


                //? Fix space after element type
                //"M1.2 3 4 ..."  >> "M 1.2 3 4 ..."
                if (pathdElement_Raw.Length > 1) //!? Avoid OutOfRangeException
                    if (Regex.IsMatch(pathdElement_Raw[..2], @$"{PathdElement_Type_Regex}\d"))
                        pathdElement_Raw = pathdElement_Raw[0] + " " + pathdElement_Raw[1..];

                //? Fix space for 00s
                // "... 00 ..." ==> "... 0 0 ..."
                pathdElement_Raw = pathdElement_Raw.Replace("00", "0 0");


                //? Split values and find ranges
                // A >>1 >>2.6 >>3 ...
                // Ranges A >>1<< >>2.6<< >>3<< ...
                List<int> indexes = pathdElement_Raw.IndexesOf(" ").ToList();
                int i = 0;
                foreach (int index in indexes) {
                    i++;

                    int endindex;
                    try {
                        endindex = indexes[i];
                    } catch { endindex = pathdElement_Raw.Length; }
                    //valueRanges.Add(new(index + 1, endindex));

                    values = values.Append(pathdElement_Raw[(index + 1)..endindex]).ToArray();
                    //values[i] = pathdElement[(index + 1)..endindex];
                }


                char PathdElement_Type = pathdElement_Raw[0];

                //? To parts string[]
                string[] pointData_Parts = (new[] { PathdElement_Type.ToString() })
                                            .Add(values);

                //? Upper = Absolute | Lower = Relaive
                bool IsRelative = char.IsLower(PathdElement_Type);


                SVG_PathdElement pathdElement = char.ToLower(PathdElement_Type) switch {
                    #region Convertion to Type
                    'm' => SVG_PathdElement.MoveTo(                         //! M
                                Convert.ToDouble(pointData_Parts[1]),       //? x
                                Convert.ToDouble(pointData_Parts[2]),       //? y
                                IsRelative),
                    'l' => SVG_PathdElement.LineTo.Free(                    //! L
                                Convert.ToDouble(pointData_Parts[1]),       //? x
                                Convert.ToDouble(pointData_Parts[2]),       //? y
                                IsRelative),
                    'h' => SVG_PathdElement.LineTo.Horizontal(              //! H
                                Convert.ToDouble(pointData_Parts[1]),       //? x
                                IsRelative),
                    'v' => SVG_PathdElement.LineTo.Vertical(                //! V
                                Convert.ToDouble(pointData_Parts[1]),       //? y
                                IsRelative),
                    'c' => SVG_PathdElement.CurveTo.Regular(                //! C
                                Convert.ToDouble(pointData_Parts[1]),       //? x1
                                Convert.ToDouble(pointData_Parts[2]),       //? y1
                                Convert.ToDouble(pointData_Parts[3]),       //? x2
                                Convert.ToDouble(pointData_Parts[4]),       //? y2
                                Convert.ToDouble(pointData_Parts[5]),       //? x
                                Convert.ToDouble(pointData_Parts[6]),       //? y
                                IsRelative),
                    's' => SVG_PathdElement.CurveTo.Smooth(                 //! S
                                Convert.ToDouble(pointData_Parts[1]),       //? x2
                                Convert.ToDouble(pointData_Parts[2]),       //? y2
                                Convert.ToDouble(pointData_Parts[3]),       //? x
                                Convert.ToDouble(pointData_Parts[4]),       //? y
                                IsRelative),
                    'q' => SVG_PathdElement.CurveTo.QuadraticBezier(        //! Q
                                Convert.ToDouble(pointData_Parts[1]),       //? x1
                                Convert.ToDouble(pointData_Parts[2]),       //? y1
                                Convert.ToDouble(pointData_Parts[3]),       //? x
                                Convert.ToDouble(pointData_Parts[4]),       //? y
                                IsRelative),
                    't' => SVG_PathdElement.CurveTo.TrueTypeQuadraticBezier(//! T
                                Convert.ToDouble(pointData_Parts[1]),       //? x
                                Convert.ToDouble(pointData_Parts[2]),       //? y
                                IsRelative),
                    'a' => SVG_PathdElement.EllipticalArc(                  //! A
                                Convert.ToDouble(pointData_Parts[1]),       //? rx
                                Convert.ToDouble(pointData_Parts[2]),       //? ry
                                Convert.ToDouble(pointData_Parts[3]),       //? rotation
                                Convert.ToBoolean(pointData_Parts[4]),      //? largeArc?
                                Convert.ToBoolean(pointData_Parts[5]),      //? sweepArc?
                                Convert.ToDouble(pointData_Parts[6]),       //? x
                                Convert.ToDouble(pointData_Parts[7]),       //? y
                                IsRelative),
                    'z' => SVG_PathdElement.ClosePath(),                    //! Z
                    #endregion
                    _ => throw new ArgumentException("Path not recognized")
                };

                return pathdElement;
            }
        }
    }
}
