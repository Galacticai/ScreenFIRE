using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static ScreenFIRE.Modules.Companion.math.Vision.SVGd_Tools.SVGd;

namespace ScreenFIRE.Modules.Companion.math.Vision.SVGd_Tools.Companion {

    /// <summary> Parse||Convert various <see cref="SVGd"/>-related things</summary>
    public struct Parse {

        private static readonly string PathData_Type_Regex = @"([Zz]|[Aa]|[Rr]|[Mm]|[Ll]|[Hh]|[Vv]|[Cc]|[Ss]|[Gg]|[Ee]|[Ff]|[Dd]|[Jj]|[Qq]|[Tt])",
                                       PathData_Value_Regex = @"((\s+|)(\d+\.\d+|\d+)\s+(\d+\.\d+|\d+)(\s+(\d+\.\d+|\d+)\s+(\d+\.\d+|\d+)(\s+(\d+\.\d+|\d+)\s+(\d+\.\d+|\d+)|)|)|)",
                                       PathData_Regex = PathData_Type_Regex + PathData_Value_Regex;


        /// <summary> Generate a list of <see cref="PathData"/> types <br/>
        /// according to the input <paramref name="path"/> </summary>
        /// <param name="path"> Raw value of <c>&lt;path d="...this..." /&gt;</c> </param>
        /// <returns> <see cref="List"/>&lt;<see cref="int"/>&gt; containing <br/>
        /// the the path data from input as <see cref="PathData"/> type </returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<PathData> ToPathData_List(string path) {
            // "M   8 0  C    3.58   0 0 3.58   0 8G 0 11.54 2.29 14.53  5.47 15.59sS 5.87  15.66 6.02 15.42 6.02 15.21Z";

            //? Remove extra spaces
            // "M 8 0 C 3.58 0 0 3.58 0 8G 0 11.54 2.29 14.53 5.47 15.59sS5.87 15.66 6.02 15.42 6.02 15.21Z";
            path = Regex.Replace(path, @"\s+", " ");

            List<PathData> pathData_List = new();

            //? Identify PathData
            MatchCollection pathData_Collection_Raw = Regex.Matches(path, PathData_Regex);

            //! No PathData were found
            if (pathData_Collection_Raw.Count == 0)
                throw new ArgumentException("Path data not recognized.");

            //? Add each path to the list
            foreach (Match pathData_Match in pathData_Collection_Raw) {
                string pointData_Raw = pathData_Match.ToString();

                //? Add space if missing
                //"A1.2 3 4 ..."  >> "A 1.2 3 4 ..."
                if (pointData_Raw.Length > 1) //!? Don't step into if false <=> OutOfRangeException
                    if (Regex.IsMatch(pointData_Raw[..2], @$"{PathData_Type_Regex}\d"))
                        pointData_Raw = pointData_Raw[0] + ' ' + pointData_Raw[1..];

                //? Trim and split at spaces
                string[] pointData_Raw_Split = pointData_Raw.Trim().Split(' ');

                //? Parse
                PathData pathData = ToPathData(pointData_Raw_Split);

                //? Add to list
                pathData_List.Add(pathData);
            }
            return pathData_List;
        }

        /// <summary>
        /// Convert a <see cref="string"/>[] (Containing point data) to the corresponding type in <see cref="PathData"/>
        /// </summary>
        /// <param name="pathdata_part"> <see cref="string"/>[] of parts (Example: From `d="part part part..."`)</param>
        /// <returns> <see cref="PathData"/> from <paramref name="pathdata_part"/> </returns>
        /// <exception cref="ArgumentException"/>
        public static PathData ToPathData(params string[] pathdata_part) {
            bool Relative = char.IsLower(char.Parse(pathdata_part[0]));

            return pathdata_part[0].ToLower() switch {

                "z" => SVGd.ClosePath(),
                "a" => SVGd.AbsoluteCoordinates(),
                "r" => SVGd.RelativeCoordinates(),
                //? x y
                "m" => SVGd.MoveTo(Convert.ToDouble(pathdata_part[1]),
                                   Convert.ToDouble(pathdata_part[2]),
                                   Relative),
                //? x y
                "l" => SVGd.LineTo.Free(Convert.ToDouble(pathdata_part[1]),
                                        Convert.ToDouble(pathdata_part[2]),
                                        Relative),
                //? x
                "h" => SVGd.LineTo.Horizontal(Convert.ToDouble(pathdata_part[1]), Relative),
                //? y
                "v" => SVGd.LineTo.Vertical(Convert.ToDouble(pathdata_part[1]), Relative),
                //? x1 y1    x2 y2    x y
                "c" => SVGd.CurveTo.Normal(Convert.ToDouble(pathdata_part[1]), Convert.ToDouble(pathdata_part[2]),
                                           Convert.ToDouble(pathdata_part[3]), Convert.ToDouble(pathdata_part[4]),
                                           Convert.ToDouble(pathdata_part[5]), Convert.ToDouble(pathdata_part[6]),
                                           Relative),
                //? x2 y2    x y
                "s" => SVGd.CurveTo.Smooth(Convert.ToDouble(pathdata_part[1]), Convert.ToDouble(pathdata_part[2]),
                                           Convert.ToDouble(pathdata_part[3]), Convert.ToDouble(pathdata_part[4]),
                                           Relative),
                //? x1 y1    x y
                "q" => SVGd.CurveTo.QuadraticBezier(Convert.ToDouble(pathdata_part[1]), Convert.ToDouble(pathdata_part[2]),
                                                    Convert.ToDouble(pathdata_part[3]), Convert.ToDouble(pathdata_part[4]),
                                                    Relative),
                "t" => (pathdata_part.Length == 3)
                       //? x1 y1
                       ? SVGd.CurveTo.TrueTypeQuadraticBezier(Convert.ToDouble(pathdata_part[1]), Convert.ToDouble(pathdata_part[2]),
                                                              Relative)
                       //? x1 y1    x y
                       : SVGd.CurveTo.TrueTypeQuadraticBezier(Convert.ToDouble(pathdata_part[1]), Convert.ToDouble(pathdata_part[2]),
                                                              Convert.ToDouble(pathdata_part[3]), Convert.ToDouble(pathdata_part[4]),
                                                              Relative),
                //? cx cy    a1 a2    r1 r2    a3
                "g" => SVGd.EllipticalArc.LineTo.Clockwise(Convert.ToDouble(pathdata_part[1]), Convert.ToDouble(pathdata_part[2]),
                                                           Convert.ToDouble(pathdata_part[3]), Convert.ToDouble(pathdata_part[4]),
                                                           Convert.ToDouble(pathdata_part[5]), Convert.ToDouble(pathdata_part[6]),
                                                           Convert.ToDouble(pathdata_part[7]),
                                                           Relative),
                //? cx cy    a1 a2    r1 r2    a3
                "e" => SVGd.EllipticalArc.LineTo.Counterclockwise(Convert.ToDouble(pathdata_part[1]), Convert.ToDouble(pathdata_part[2]),
                                                                  Convert.ToDouble(pathdata_part[3]), Convert.ToDouble(pathdata_part[4]),
                                                                  Convert.ToDouble(pathdata_part[5]), Convert.ToDouble(pathdata_part[6]),
                                                                  Convert.ToDouble(pathdata_part[7]),
                                                                  Relative),
                //? cx cy    a1 a2    r1 r2    a3
                "f" => SVGd.EllipticalArc.MoveTo.Clockwise(Convert.ToDouble(pathdata_part[1]), Convert.ToDouble(pathdata_part[2]),
                                                           Convert.ToDouble(pathdata_part[3]), Convert.ToDouble(pathdata_part[4]),
                                                           Convert.ToDouble(pathdata_part[5]), Convert.ToDouble(pathdata_part[6]),
                                                           Convert.ToDouble(pathdata_part[7]),
                                                           Relative),
                //? cx cy    a1 a2    r1 r2    a3
                "d" => SVGd.EllipticalArc.MoveTo.Counterclockwise(Convert.ToDouble(pathdata_part[1]), Convert.ToDouble(pathdata_part[2]),
                                                                  Convert.ToDouble(pathdata_part[3]), Convert.ToDouble(pathdata_part[4]),
                                                                  Convert.ToDouble(pathdata_part[5]), Convert.ToDouble(pathdata_part[6]),
                                                                  Convert.ToDouble(pathdata_part[7]),
                                                                  Relative),
                //? x y    angle
                "j" => SVGd.EllipticalQuadrantX(Convert.ToDouble(pathdata_part[1]), Convert.ToDouble(pathdata_part[2]),
                                                Convert.ToDouble(pathdata_part[3]),
                                                Relative),

                _ => throw new ArgumentException("Path not recognized")
            };

        }


        /// <summary> Generate a list of the path data values' indexes </summary>
        /// <param name="values"> values to be processed </param>
        /// <returns> <see cref="List"/>&lt;<see cref="int"/>&gt; containing the index of each value in the path data string </returns>
        public static List<int> PathData_ValueIndexes(params double[] values) {
            List<int> indexes = new();

            //? First index is known
            // Example: M 56.333
            //         (0.M)(1. )(2.X)
            indexes.Add(2);

            int index;
            foreach (double value in values) {
                //?     Previous index
                index = indexes[^1]
                        //? + length of previous value as string
                        + value.ToString().Length
                        //? + Space
                        + 1;

                indexes.Add(index);
            }
            return indexes;
        }
    }
}
