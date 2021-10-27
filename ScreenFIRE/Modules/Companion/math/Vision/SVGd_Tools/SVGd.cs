using ScreenFIRE.Modules.Companion.math.Vision.SVGd_Tools.Companion;
using System.Collections.Generic;

namespace ScreenFIRE.Modules.Companion.math.Vision.SVGd_Tools {

    /// <summary> SVG <c>&lt;path d="..."&gt;</c> property manipulation </summary>
    public partial class SVGd {
        public record PathData {
            /// <summary> SVG point as <see cref="string"/> <br/>
            /// Example: <br/>
            /// • <c>&lt;path d="<see cref="Data"/> <see cref="Data"/>...etc" /&gt;</c>) </summary>
            public string Data { get; private set; }

            #region PathData variables
            public double X { get; set; }
            public double X1 { get; set; }
            public double X2 { get; set; }
            public double Y { get; set; }
            public double Y1 { get; set; }
            public double Y2 { get; set; }
            public double CX { get; set; }
            public double CY { get; set; }
            public double A1 { get; set; }
            public double A2 { get; set; }
            public double A3 { get; set; }
            public double R1 { get; set; }
            public double R2 { get; set; }
            public double Angle { get; set; }
            public int X_DataIndex { get; private set; }
            public int X1_DataIndex { get; private set; }
            public int X2_DataIndex { get; private set; }
            public int Y_DataIndex { get; private set; }
            public int Y1_DataIndex { get; private set; }
            public int Y2_DataIndex { get; private set; }
            public int CX_DataIndex { get; private set; }
            public int CY_DataIndex { get; private set; }
            public int A1_DataIndex { get; private set; }
            public int A2_DataIndex { get; private set; }
            public int A3_DataIndex { get; private set; }
            public int R1_DataIndex { get; private set; }
            public int R2_DataIndex { get; private set; }
            public int Angle_DataIndex { get; private set; }
            #endregion


            #region Types

            public record z : PathData {
                /// <summary>
                /// Close the current subpath by drawing a straight line from the current <br/>
                /// point to current subpath's most recent starting point (usually, the   <br/>
                /// most recent moveto point). <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> z </c> </item>
                /// </list> </summary>
                public z() { Data = $"z"; }
            }
            public record A : PathData {
                /// <summary>
                /// An A can appear anywhere between coordinates within any path data     <br/>
                /// command to indicate that all subsequent coordinates are absolute. <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> A </c> </item>
                /// </list> </summary>
                public A() { Data = $"A"; }
            }

            public record r : PathData {
                /// <summary>
                /// An r can appear anywhere between coordinates within any path data     <br/>
                /// command to indicate that all subsequent coordinates are relative to the <br/>
                /// current point at the start of the command. <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> r </c> </item>
                /// </list> </summary>
                public r() { Data = $"r"; }
            }

            public record M : PathData {
                /// <summary> Start a new sub-path at the given (x,y) coordinate. <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> M X Y </c> ••• Absolute </item>
                /// <item> <c> m X Y </c> ••• Relative </item>
                /// </list> </summary>
                public M(double x, double y, bool Relative) {
                    X = x; Y = y;
                    Data = $"{(Relative ? "m" : "M")} {X} {Y}";

                    var indexes = Parse.PathData_ValueIndexes(X, Y);
                    X_DataIndex = indexes[0]; Y_DataIndex = indexes[1];
                }
            }

            public record L : PathData {
                /// <summary>
                /// Draw a line from the current point to the given (x,y) coordinate which <br/>
                /// becomes the new current point.  <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> L X Y </c> ••• Absolute </item>
                /// <item> <c> l X Y </c> ••• Relative </item>
                /// </list> </summary>
                public L(double x, double y, bool Relative) {
                    X = x; Y = y;
                    Data = $"{(Relative ? "l" : "L")} {X} {Y}";

                    var indexes = Parse.PathData_ValueIndexes(X, Y);
                    X_DataIndex = indexes[0]; Y_DataIndex = indexes[1];
                }
            }
            public record H : PathData {
                /// Draws a horizontal line from the current point (cpx, cpy) to (x, cpy). <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> H X </c> ••• Absolute </item>
                /// <item> <c> h X </c> ••• Relative </item>
                /// </list> </summary>
                public H(double x, bool Relative) {
                    X = x;
                    Data = $"{(Relative ? "h" : "H")} {X}";

                    X_DataIndex = 2;
                }
            }
            public record V : PathData {
                /// <summary>
                /// Draws a vertical line from the current point (cpx, cpy) to (cpx, y).   <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> V Y </c> ••• Absolute </item>
                /// <item> <c> v Y </c> ••• Relative </item>
                /// </list> </summary>
                public V(double y, bool Relative) {
                    Y = y;
                    Data = $"{(Relative ? "v" : "V")} {Y}";

                    Y_DataIndex = 2;
                }
            }


            public record C : PathData {
                /// <summary>
                /// Draws a cubic bezier curve from the current point to (x,y) using (x1,y1) <br/>
                /// as the control point at the beginning of the curve and (x2,y2) as the    <br/>
                /// control point at the end of the curve.    <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> C X1 Y1 X2 Y2 X Y </c> ••• Absolute </item>
                /// <item> <c> c X1 Y1 X2 Y2 X Y </c> ••• Relative </item>
                /// </list> </summary>
                public C(double x1, double y1, double x2, double y2, double x, double y, bool Relative) {
                    X1 = x1; Y1 = y1; X2 = x2; Y2 = y2; X = x; Y = y;
                    Data = $"{(Relative ? "c" : "C")} {X1} {Y1} {X2} {Y2} {X} {Y}";

                    List<int> indexes = Parse.PathData_ValueIndexes(X1, Y1, X2, Y2, X, Y);
                    X1_DataIndex = indexes[0]; Y1_DataIndex = indexes[1];
                    X2_DataIndex = indexes[2]; Y2_DataIndex = indexes[3];
                    X_DataIndex = indexes[4]; Y_DataIndex = indexes[5];
                }
            }
            public record S : PathData {
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
                public S(double x2, double y2, double x, double y, bool Relative) {
                    X2 = x2; Y2 = y2; X = x; Y = y;
                    Data = $"{(Relative ? "s" : "S")} {X2} {Y2} {X} {Y}";

                    List<int> indexes = Parse.PathData_ValueIndexes(X2, Y2, X, Y);
                    X2_DataIndex = indexes[0]; Y2_DataIndex = indexes[1];
                    X_DataIndex = indexes[2]; Y_DataIndex = indexes[3];
                }
            }

            public record Q : PathData {
                /// <summary>
                /// A quarter ellipse is drawn from the current point to the given end point. <br/>
                /// Draws a quadratic bezier curve from the current point to (x,y) using
                /// (x1,y1) as the control point. <br/><br/>
                /// /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> Q X1 Y1 X Y </c> ••• Absolute </item>
                /// <item> <c> q X1 Y1 X Y </c> ••• Relative </item>
                /// </list> </summary>
                public Q(double x1, double y1, double x, double y, bool Relative) {
                    X1 = x1; Y1 = y1; X = x; Y = y;
                    Data = $"{(Relative ? "q" : "Q")} {X1} {Y1} {X} {Y}";

                    List<int> indexes = Parse.PathData_ValueIndexes(X1, Y1, X, Y);
                    X1_DataIndex = indexes[0]; Y1_DataIndex = indexes[1];
                    X_DataIndex = indexes[2]; Y_DataIndex = indexes[3];
                }
            }

            public record T : PathData {
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
                public T(double x1, double y1, bool Relative) {
                    X1 = x1; Y1 = y1;
                    Data = $"{(Relative ? "t" : "T")} {X1} {Y1}";

                    List<int> indexes = Parse.PathData_ValueIndexes(X1, Y1);
                    X1_DataIndex = indexes[0]; Y1_DataIndex = indexes[1];
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
                public T(double x1, double y1, double x, double y, bool Relative) {
                    X1 = x1; Y1 = y1; X = x; Y = y;
                    Data = $"{(Relative ? "t" : "T")} {X1} {Y1} {X} {Y}";

                    List<int> indexes = Parse.PathData_ValueIndexes(X1, Y1, X, Y);
                    X1_DataIndex = indexes[0]; Y1_DataIndex = indexes[1];
                    X_DataIndex = indexes[2]; Y_DataIndex = indexes[3];
                }
            }

            private static void SetIndexes(PathData PathData_Instance) {
                List<int> indexes = Parse.PathData_ValueIndexes(PathData_Instance.CX, PathData_Instance.CY,
                                                       PathData_Instance.A1, PathData_Instance.A2,
                                                       PathData_Instance.R1, PathData_Instance.R2,
                                                       PathData_Instance.A3);
                PathData_Instance.CX_DataIndex = indexes[0]; PathData_Instance.CY_DataIndex = indexes[1];
                PathData_Instance.A1_DataIndex = indexes[2]; PathData_Instance.A2_DataIndex = indexes[3];
                PathData_Instance.R1_DataIndex = indexes[4]; PathData_Instance.R2_DataIndex = indexes[5];
                PathData_Instance.A3_DataIndex = indexes[6];
            }
            public record G : PathData {
                /// <summary>
                /// Same as <see cref="Counterclockwise"/>, except that the arc is drawn in a <br/>
                /// clockwise direction.  <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> G CX CY A1 A2 R1 R2 A3 </c> ••• Absolute </item>
                /// <item> <c> g CX CY A1 A2 R1 R2 A3 </c> ••• Relative </item>
                /// </list> </summary>
                public G(double cx, double cy, double a1, double a2, double r1, double r2, double a3, bool Relative) {
                    CX = cx; CY = cy; A1 = a1; A2 = a2; R1 = r1; R2 = r2; A3 = a3;
                    Data = $"{(Relative ? "g" : "G")} {CX} {CY} {A1} {A2} {R1} {R2} {A3}";

                    SetIndexes(this);
                }
            }
            public record E : PathData {
                /// <summary>
                /// Same as <see cref="MoveTo.Counterclockwise"/>, except that there is an   <br/>
                /// implicit lineto from the current point to the start of the first arc.  <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> E CX CY A1 A2 R1 R2 A3 </c> ••• Absolute </item>
                /// <item> <c> e CX CY A1 A2 R1 R2 A3 </c> ••• Relative </item>
                /// </list> </summary>
                public E(double cx, double cy, double a1, double a2, double r1, double r2, double a3, bool Relative) {
                    CX = cx; CY = cy; A1 = a1; A2 = a2; R1 = r1; R2 = r2; A3 = a3;
                    Data = $"{(Relative ? "e" : "E")} {CX} {CY} {A1} {A2} {R1} {R2} {A3}";

                    SetIndexes(this);
                }
            }

            public record F : PathData {
                /// <summary>
                /// Same as <see cref="Counterclockwise"/>, except that the arc is drawn in a <br/>
                /// clockwise direction.  <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> F CX CY A1 A2 R1 R2 A3 </c> ••• Absolute </item>
                /// <item> <c> f CX CY A1 A2 R1 R2 A3 </c> ••• Relative </item>
                /// </list> </summary>
                public F(double cx, double cy, double a1, double a2, double r1, double r2, double a3, bool Relative) {
                    CX = cx; CY = cy; A1 = a1; A2 = a2; R1 = r1; R2 = r2; A3 = a3;
                    Data = $"{(Relative ? "f" : "F")} {CX} {CY} {A1} {A2} {R1} {R2} {A3}";

                    SetIndexes(this);
                }
            }
            public record D : PathData {
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
                public D(double cx, double cy, double a1, double a2, double r1, double r2, double a3, bool Relative) {
                    CX = cx; CY = cy; A1 = a1; A2 = a2; R1 = r1; R2 = r2; A3 = a3;
                    Data = $"{(Relative ? "d" : "D")} {CX} {CY} {A1} {A2} {R1} {R2} {A3}";

                    SetIndexes(this);
                }
            }

            public record J : PathData {
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
                public J(double x, double y, double angle, bool Relative) {
                    X = x; Y = y; Angle = angle;
                    Data = $"{(Relative ? "j" : "J")} {X} {Y} {Angle}";

                    List<int> indexes = Parse.PathData_ValueIndexes(X, Y, Angle);
                    X_DataIndex = indexes[0]; Y_DataIndex = indexes[1];
                    Angle_DataIndex = indexes[2];
                }
            }

            #endregion
        }
    }
}
