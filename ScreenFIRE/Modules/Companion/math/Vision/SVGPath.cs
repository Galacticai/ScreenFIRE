using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ScreenFIRE.Modules.Companion.math.Vision {

    public class SVGPath {
        private static readonly string PathData_Type_Regex = @"([Zz]|[Aa]|[Rr]|[Mm]|[Ll]|[Hh]|[Vv]|[Cc]|[Ss]|[Gg]|[Ee]|[Ff]|[Dd]|[Jj]|[Qq]|[Tt])",
                                       PathData_Value_Regex = @"((\s+|)(\d+\.\d+|\d+)\s+(\d+\.\d+|\d+)(\s+(\d+\.\d+|\d+)\s+(\d+\.\d+|\d+)(\s+(\d+\.\d+|\d+)\s+(\d+\.\d+|\d+)|)|)|)",
                                       PathData_Regex = PathData_Type_Regex + PathData_Value_Regex;
        public record PathData {
            /// <summary> SVG point as <see cref="string"/> <br/>
            /// Example: <br/>
            /// • <c>&lt;path d="<see cref="Data"/> <see cref="Data"/>...etc" /&gt;</c>) </summary>
            public string Data { get; private set; }
            public double X { get; private set; }
            public double X1 { get; private set; }
            public double X2 { get; private set; }
            public double Y { get; private set; }
            public double Y1 { get; private set; }
            public double Y2 { get; private set; }
            public double CX { get; private set; }
            public double CY { get; private set; }
            public double A1 { get; private set; }
            public double A2 { get; private set; }
            public double A3 { get; private set; }
            public double R1 { get; private set; }
            public double R2 { get; private set; }
            public double Angle { get; private set; }

            /// <summary>
            /// Convert a <see cref="string"/>[] (Containing point data) to the corresponding type in <see cref="PathData"/>
            /// </summary>
            /// <param name="pointData"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"/>
            public static PathData ParsePathData(params string[] pointData) {
                //Type type = GetSVGDataType(char.Parse(pointData[0]));

                bool Relative = char.IsLower(char.Parse(pointData[0]));

                return pointData[0].ToLower() switch {

                    "z" => new ClosePath(),

                    "a" => new AbsoluteCoordinates(),

                    "r" => new RelativeCoordinates(),

                    //? x y
                    "m" => new MoveTo(Convert.ToDouble(pointData[1]),
                                      Convert.ToDouble(pointData[2]),
                                      Relative),

                    //? x y
                    "l" => new LineTo.Free(Convert.ToDouble(pointData[1]),
                                           Convert.ToDouble(pointData[2]),
                                           Relative),

                    //? x
                    "h" => new LineTo.Horizontal(Convert.ToDouble(pointData[1]), Relative),

                    //? y
                    "v" => new LineTo.Vertical(Convert.ToDouble(pointData[1]), Relative),

                    //? x1 y1    x2 y2    x y
                    "c" => new CurveTo.Normal(Convert.ToDouble(pointData[1]), Convert.ToDouble(pointData[2]),
                                              Convert.ToDouble(pointData[3]), Convert.ToDouble(pointData[4]),
                                              Convert.ToDouble(pointData[5]), Convert.ToDouble(pointData[6]),
                                              Relative),

                    //? x2 y2    x y
                    "s" => new CurveTo.Smooth(Convert.ToDouble(pointData[1]), Convert.ToDouble(pointData[2]),
                                              Convert.ToDouble(pointData[3]), Convert.ToDouble(pointData[4]),
                                              Relative),

                    //? x1 y1    x y
                    "q" => new CurveTo.QuadraticBezier(Convert.ToDouble(pointData[1]), Convert.ToDouble(pointData[2]),
                                                       Convert.ToDouble(pointData[3]), Convert.ToDouble(pointData[4]),
                                                       Relative),

                    "t" => (pointData.Length == 3)
                           //? x1 y1
                           ? new CurveTo.TrueTypeQuadraticBezier(Convert.ToDouble(pointData[1]), Convert.ToDouble(pointData[2]),
                                                                 Relative)
                           //? x1 y1    x y
                           : new CurveTo.TrueTypeQuadraticBezier(Convert.ToDouble(pointData[1]), Convert.ToDouble(pointData[2]),
                                                                 Convert.ToDouble(pointData[3]), Convert.ToDouble(pointData[4]),
                                                                 Relative),

                    //? cx cy    a1 a2    r1 r2    a3
                    "g" => new EllipticalArc.LineTo.Clockwise(Convert.ToDouble(pointData[1]), Convert.ToDouble(pointData[2]),
                                                              Convert.ToDouble(pointData[3]), Convert.ToDouble(pointData[4]),
                                                              Convert.ToDouble(pointData[5]), Convert.ToDouble(pointData[6]),
                                                              Convert.ToDouble(pointData[7]),
                                                              Relative),

                    //? cx cy    a1 a2    r1 r2    a3
                    "e" => new EllipticalArc.LineTo.Counterclockwise(Convert.ToDouble(pointData[1]), Convert.ToDouble(pointData[2]),
                                                                     Convert.ToDouble(pointData[3]), Convert.ToDouble(pointData[4]),
                                                                     Convert.ToDouble(pointData[5]), Convert.ToDouble(pointData[6]),
                                                                     Convert.ToDouble(pointData[7]),
                                                                     Relative),

                    //? cx cy    a1 a2    r1 r2    a3
                    "f" => new EllipticalArc.MoveTo.Clockwise(Convert.ToDouble(pointData[1]), Convert.ToDouble(pointData[2]),
                                                              Convert.ToDouble(pointData[3]), Convert.ToDouble(pointData[4]),
                                                              Convert.ToDouble(pointData[5]), Convert.ToDouble(pointData[6]),
                                                              Convert.ToDouble(pointData[7]),
                                                              Relative),

                    //? cx cy    a1 a2    r1 r2    a3
                    "d" => new EllipticalArc.MoveTo.Counterclockwise(Convert.ToDouble(pointData[1]), Convert.ToDouble(pointData[2]),
                                                                     Convert.ToDouble(pointData[3]), Convert.ToDouble(pointData[4]),
                                                                     Convert.ToDouble(pointData[5]), Convert.ToDouble(pointData[6]),
                                                                     Convert.ToDouble(pointData[7]),
                                                                     Relative),

                    //? x y    angle
                    "j" => new EllipticalQuadrantX(Convert.ToDouble(pointData[1]), Convert.ToDouble(pointData[2]),
                                                   Convert.ToDouble(pointData[3]),
                                                   Relative),

                    _ => throw new ArgumentException("Path not recognized")
                };

            }

            //public static Type GetSVGDataType(char SVGDataType) {
            //    return char.ToLower(SVGDataType) switch {
            //        'z' => typeof(ClosePath),
            //        'a' => typeof(AbsoluteCoordinates),
            //        'r' => typeof(RelativeCoordinates),
            //        'm' => typeof(MoveTo),
            //        'l' => typeof(LineTo.Free),
            //        'h' => typeof(LineTo.Horizontal),
            //        'v' => typeof(LineTo.Vertical),
            //        'c' => typeof(CurveTo),
            //        's' => typeof(SmoothCurveTo),
            //        'g' => typeof(EllipticalArc.LineTo.Clockwise),
            //        'e' => typeof(EllipticalArc.LineTo.Counterclockwise),
            //        'f' => typeof(EllipticalArc.MoveTo.Clockwise),
            //        'd' => typeof(EllipticalArc.MoveTo.Counterclockwise),
            //        'j' => typeof(EllipticalQuadrantX),
            //        'q' => typeof(QuadraticBezier_CurveTo),
            //        't' => typeof(TrueTypeQuadraticBezier_CurveTo),
            //        _ => throw new ArgumentException("Path type unrecognized")
            //    };
            //}

            #region Types

            /// <summary> <c> z </c> </summary>
            public record ClosePath : PathData {
                /// <summary>
                /// Close the current subpath by drawing a straight line from the current <br/>
                /// point to current subpath's most recent starting point (usually, the   <br/>
                /// most recent moveto point). <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> z </c> </item>
                /// </list> </summary>
                public ClosePath() { Data = $"z"; }
            }

            /// <summary> <c> A </c> </summary>
            public record AbsoluteCoordinates : PathData {
                /// <summary>
                /// An A can appear anywhere between coordinates within any path data     <br/>
                /// command to indicate that all subsequent coordinates are absolute. <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> A </c> </item>
                /// </list> </summary>
                public AbsoluteCoordinates() { Data = $"A"; }
            }

            /// <summary> <c> r </c> </summary>
            public record RelativeCoordinates : PathData {
                /// <summary>
                /// An r can appear anywhere between coordinates within any path data     <br/>
                /// command to indicate that all subsequent coordinates are relative to the <br/>
                /// current point at the start of the command. <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> r </c> </item>
                /// </list> </summary>
                public RelativeCoordinates() { Data = $"r"; }
            }

            /// <summary> <c> M X Y </c> </summary>
            public record MoveTo : PathData {
                /// <summary> Start a new sub-path at the given (x,y) coordinate. <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> M X Y </c> ••• Absolute </item>
                /// <item> <c> m X Y </c> ••• Relative </item>
                /// </list> </summary>
                public MoveTo(double x, double y, bool Relative) {
                    X = x; Y = y;
                    Data = $"{(Relative ? "m" : "M")} {X} {Y}";
                }
            }

            /// <summary> <c> { L || H || V } { X Y || X || Y } </c> </summary>
            public struct LineTo {
                /// <summary> <c> L X Y </c> </summary>
                public record Free : PathData {
                    /// <summary>
                    /// Draw a line from the current point to the given (x,y) coordinate which <br/>
                    /// becomes the new current point.  <br/><br/>
                    /// Data example: <br/>
                    /// <list type="bullet">
                    /// <item> <c> L X Y </c> ••• Absolute </item>
                    /// <item> <c> l X Y </c> ••• Relative </item>
                    /// </list> </summary>
                    public Free(double x, double y, bool Relative) {
                        X = x; Y = y;
                        Data = $"{(Relative ? "l" : "L")} {X} {Y}";
                    }
                }
                /// <summary> <c> H X </c> </summary>
                public record Horizontal : PathData {
                    /// Draws a horizontal line from the current point (cpx, cpy) to (x, cpy). <br/><br/>
                    /// Data example: <br/>
                    /// <list type="bullet">
                    /// <item> <c> H X </c> ••• Absolute </item>
                    /// <item> <c> h X </c> ••• Relative </item>
                    /// </list> </summary>
                    public Horizontal(double x, bool Relative) {
                        X = x;
                        Data = $"{(Relative ? "h" : "H")} {X}";
                    }
                }
                /// <summary> <c> V Y </c> </summary>
                public record Vertical : PathData {
                    /// <summary>
                    /// Draws a vertical line from the current point (cpx, cpy) to (cpx, y).   <br/><br/>
                    /// Data example: <br/>
                    /// <list type="bullet">
                    /// <item> <c> V Y </c> ••• Absolute </item>
                    /// <item> <c> v Y </c> ••• Relative </item>
                    /// </list> </summary>
                    public Vertical(double y, bool Relative) {
                        Y = y;
                        Data = $"{(Relative ? "v" : "V")} {Y}";
                    }
                }
            }

            /// <summary>
            /// <c> { C || S } CX CY A1 A2 R1 R2 A3 </c> <br/>
            /// <c> Q X1 Y1 X Y </c> <br/>
            /// <c> T { X1 Y1 || X1 Y1 X Y } </c>
            /// </summary>
            public struct CurveTo {
                /// <summary> <c> C CX CY A1 A2 R1 R2 A3 </c> </summary>
                public record Normal : PathData {
                    /// <summary>
                    /// Draws a cubic bezier curve from the current point to (x,y) using (x1,y1) <br/>
                    /// as the control point at the beginning of the curve and (x2,y2) as the    <br/>
                    /// control point at the end of the curve.    <br/><br/>
                    /// Data example: <br/>
                    /// <list type="bullet">
                    /// <item> <c> C X1 Y1 X2 Y2 X Y </c> ••• Absolute </item>
                    /// <item> <c> c X1 Y1 X2 Y2 X Y </c> ••• Relative </item>
                    /// </list> </summary>
                    public Normal(double x1, double y1, double x2, double y2, double x, double y, bool Relative) {
                        X1 = x1; Y1 = y1; X2 = x2; Y2 = y2; X = x; Y = y;
                        Data = $"{(Relative ? "c" : "C")} {X1} {Y1} {X2} {Y2} {X} {Y}";
                    }
                }
                /// <summary> <c> S CX CY A1 A2 R1 R2 A3 </c> </summary>
                public record Smooth : PathData {
                    /// <summary>
                    /// Draws a cubic bezier curve from the current point to (x,y). The first    <br/>
                    /// control point is assumed to be the reflection of the second control      <br/>
                    /// point on the previous command relative to the current point. (If there   <br/>
                    /// is no previous command or if the previous command was not a curve, assume <br/>
                    /// the first control point is coincident with the current point.) (x2,y2) is <br/>
                    /// the second control point (i.e., the control point at the end of the curve).     <br/><br/>
                    /// Data example: <br/>
                    /// <list type="bullet">
                    /// <item> <c> S X2 Y2 X Y </c> ••• Absolute </item>
                    /// <item> <c> s X2 Y2 X Y </c> ••• Relative </item>
                    /// </list> </summary>
                    public Smooth(double x2, double y2, double x, double y, bool Relative) {
                        X2 = x2; Y2 = y2; X = x; Y = y;
                        Data = $"{(Relative ? "s" : "S")} {X2} {Y2} {X} {Y}";
                    }
                }

                /// <summary> <c> Q X1 Y1 X Y </c> </summary>
                public record QuadraticBezier : PathData {
                    /// <summary>
                    /// A quarter ellipse is drawn from the current point to the given end point. <br/>
                    /// Draws a quadratic bezier curve from the current point to (x,y) using
                    /// (x1,y1) as the control point. <br/><br/>
                    /// /// Data example: <br/>
                    /// <list type="bullet">
                    /// <item> <c> Q X1 Y1 X Y </c> ••• Absolute </item>
                    /// <item> <c> q X1 Y1 X Y </c> ••• Relative </item>
                    /// </list> </summary>
                    public QuadraticBezier(double x1, double y1, double x, double y, bool Relative) {
                        X1 = x1; Y1 = y1; X = x; Y = y;
                        Data = $"{(Relative ? "q" : "Q")} {X1} {Y1} {X} {Y}";
                    }
                }

                /// <summary> <c> T { X1 Y1 || X1 Y1 X Y } </c> </summary>
                public record TrueTypeQuadraticBezier : PathData {
                    /// <summary>
                    /// Draws one or more quadratic bezier curves by means of multiple control    <br/>
                    /// points (can be as few as one set) and a single end point. Intermediate    <br/>
                    /// (on-curve) points are obtained by interpolation between successive control <br/>
                    /// points as in the TrueType documentation within the OpenType font specification. <br/>
                    /// (??? More details needed.) The subpath need not be started in which case  <br/>
                    /// the subpath will be closed. In this case the last point of the subpath    <br/>
                    /// defines the start point of the quadratic bezier.  <br/><br/>
                    /// Data example: <br/>
                    /// <list type="bullet">
                    /// <item> <c> T X1 Y1 </c> ••• Absolute </item>
                    /// <item> <c> t X1 Y1 </c> ••• Relative </item>
                    /// </list> </summary>
                    public TrueTypeQuadraticBezier(double x1, double y1, bool Relative) {
                        X1 = x1; Y1 = y1;
                        Data = $"{(Relative ? "t" : "T")} {X1} {Y1}";
                    }
                    /// <summary>
                    /// Draws one or more quadratic bezier curves by means of multiple control    <br/>
                    /// points (can be as few as one set) and a single end point. Intermediate    <br/>
                    /// (on-curve) points are obtained by interpolation between successive control <br/>
                    /// points as in the TrueType documentation within the OpenType font specification. <br/>
                    /// (??? More details needed.) The subpath need not be started in which case  <br/>
                    /// the subpath will be closed. In this case the last point of the subpath    <br/>
                    /// defines the start point of the quadratic bezier.  <br/><br/>
                    /// Data example: <br/>
                    /// <list type="bullet">
                    /// <item> <c> T X1 Y1 X Y </c> ••• Absolute </item>
                    /// <item> <c> t X1 Y1 X Y </c> ••• Relative </item>
                    /// </list> </summary>
                    public TrueTypeQuadraticBezier(double x1, double y1, double x, double y, bool Relative) {
                        X1 = x1; Y1 = y1; X = x; Y = y;
                        Data = $"{(Relative ? "t" : "T")} {X1} {Y1} {X} {Y}";
                    }
                }
            }

            /// <summary> <c> {[G||E]||[F||D]} CX CY A1 A2 R1 R2 A3 </c> </summary>
            public struct EllipticalArc {
                /// <summary> <c> {G||E} CX CY A1 A2 R1 R2 A3 </c> </summary>
                public struct LineTo {
                    public record Clockwise : PathData {
                        /// <summary>
                        /// Same as <see cref="Counterclockwise"/>, except that the arc is drawn in a <br/>
                        /// clockwise direction.  <br/><br/>
                        /// Data example: <br/>
                        /// <list type="bullet">
                        /// <item> <c> G CX CY A1 A2 R1 R2 A3 </c> ••• Absolute </item>
                        /// <item> <c> g CX CY A1 A2 R1 R2 A3 </c> ••• Relative </item>
                        /// </list> </summary>
                        public Clockwise(double cx, double cy, double a1, double a2, double r1, double r2, double a3, bool Relative) {
                            CX = cx; CY = cy; A1 = a1; A2 = a2; R1 = r1; R2 = r2; A3 = a3;
                            Data = $"{(Relative ? "g" : "G")} {CX} {CY} {A1} {A2} {R1} {R2} {A3}";
                        }
                    }
                    public record Counterclockwise : PathData {
                        /// <summary>
                        /// Same as <see cref="MoveTo.Counterclockwise"/>, except that there is an   <br/>
                        /// implicit lineto from the current point to the start of the first arc.  <br/><br/>
                        /// Data example: <br/>
                        /// <list type="bullet">
                        /// <item> <c> E CX CY A1 A2 R1 R2 A3 </c> ••• Absolute </item>
                        /// <item> <c> e CX CY A1 A2 R1 R2 A3 </c> ••• Relative </item>
                        /// </list> </summary>
                        public Counterclockwise(double cx, double cy, double a1, double a2, double r1, double r2, double a3, bool Relative) {
                            CX = cx; CY = cy; A1 = a1; A2 = a2; R1 = r1; R2 = r2; A3 = a3;
                            Data = $"{(Relative ? "e" : "E")} {CX} {CY} {A1} {A2} {R1} {R2} {A3}";
                        }
                    }
                }
                /// <summary> <c> {F||D} CX CY A1 A2 R1 R2 A3 </c> </summary>
                public struct MoveTo {
                    public record Clockwise : PathData {
                        /// <summary>
                        /// Same as <see cref="Counterclockwise"/>, except that the arc is drawn in a <br/>
                        /// clockwise direction.  <br/><br/>
                        /// Data example: <br/>
                        /// <list type="bullet">
                        /// <item> <c> F CX CY A1 A2 R1 R2 A3 </c> ••• Absolute </item>
                        /// <item> <c> f CX CY A1 A2 R1 R2 A3 </c> ••• Relative </item>
                        /// </list> </summary>
                        public Clockwise(double cx, double cy, double a1, double a2, double r1, double r2, double a3, bool Relative) {
                            CX = cx; CY = cy; A1 = a1; A2 = a2; R1 = r1; R2 = r2; A3 = a3;
                            Data = $"{(Relative ? "f" : "F")} {CX} {CY} {A1} {A2} {R1} {R2} {A3}";
                        }
                    }
                    public record Counterclockwise : PathData {
                        /// <summary>
                        /// Draws a counterclockwise elliptical arc described by its center (cx, cy), <br/>
                        /// start angle in degrees (a1), end angle in degrees (a2), radius along X-axis <br/>
                        /// (r1), radius along Y-axis (r2), and ellipse rotate angle in degrees (a3). <br/>
                        /// All angles are converted to modula 360 degrees before processing, after   <br/>
                        /// which if a1=a2, then a complete ellipse is drawn. If r2 is not provided,  <br/>
                        /// it is assumed to be equal to r1 (circular arc). If a3 is not provided, it <br/>
                        /// is assumed that the ellipse aligns with the axes of the current coordinate <br/>
                        /// system. There is an implicit moveto from the current point to the start of <br/>
                        /// the first arc.   <br/><br/>
                        /// Data example: <br/>
                        /// <list type="bullet">
                        /// <item> <c> D CX CY A1 A2 R1 R2 A3 </c> ••• Absolute </item>
                        /// <item> <c> d CX CY A1 A2 R1 R2 A3 </c> ••• Relative </item>
                        /// </list> </summary>
                        public Counterclockwise(double cx, double cy, double a1, double a2, double r1, double r2, double a3, bool Relative) {
                            CX = cx; CY = cy; A1 = a1; A2 = a2; R1 = r1; R2 = r2; A3 = a3;
                            Data = $"{(Relative ? "d" : "D")} {CX} {CY} {A1} {A2} {R1} {R2} {A3}";
                        }
                    }
                }
            }

            public record EllipticalQuadrantX : PathData {
                /// <summary>
                /// A quarter ellipse is drawn from the current point to the given end point. <br/>
                /// The elliptical segment is initially tangential to a line pointing in      <br/>
                /// direction <angle> at the current point and ends up tangential to a        <br/>
                /// perpendicular line to <angle> going through (x,y).   <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> J X Y Angle </c> ••• Absolute </item>
                /// <item> <c> j X Y Angle </c> ••• Relative </item>
                /// </list> </summary>
                public EllipticalQuadrantX(double x, double y, double angle, bool Relative) {
                    X = x; Y = y; Angle = angle;
                    Data = $"{(Relative ? "j" : "J")} {X} {Y} {Angle}";
                }
            }


            #endregion
        }

        public static List<PathData> ParseSVGPoints(string path) {
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
                if (pointData_Raw.Length > 1)
                    if (Regex.IsMatch(pointData_Raw[..2], @$"{PathData_Type_Regex}\d"))
                        pointData_Raw = pointData_Raw[0] + ' ' + pointData_Raw[1..];

                //? Trim and split at spaces
                string[] pointData_Raw_Split = pointData_Raw.Trim().Split(' ');

                //? Parse
                PathData pathData = PathData.ParsePathData(pointData_Raw_Split);

                //? Add to list
                pathData_List.Add(pathData);
            }
            return pathData_List;
        }
    }
}