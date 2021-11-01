using ScreenFIRE.Modules.Companion.math.Vision.SVG_Tools.Companion;
using System;
using System.Collections.Generic;

namespace ScreenFIRE.Modules.Companion.math.Vision.SVG_Tools {
    //! Move to (0,0)
    //? |> (x|y) - Min(All(x|y))
    //  M   =>   x    y
    //  L   =>   x    y
    //  V   =>   y
    //  H   =>   x
    //  C   =>   x1   y1   x2  y2  x  y
    //  S   =>   x2   y2   x   y
    //  Q   =>   x1   y1   x   y
    //  T   =>   x    y
    //  A   =>   rx   ry   a   large-arc  sweep  x  y

    /// <summary> SVG <c>&lt;path d="..."&gt;</c> property manipulation </summary>
    public partial class PathdElement {

        /// <summary> SVG point as <see cref="string"/> <br/>
        /// Example: <br/>
        /// • <c>&lt;path d="<see cref="Data"/> <see cref="Data"/>...etc" /&gt;</c>) </summary>
        public string Data { get; private set; }
        /// <summary> Location of the start and finish (List of <see cref="Range"/>s) of each variable in <see cref="Data"/> </summary>
        public List<Range> DataValueRanges { get; private set; }
        /// <summary> Amount of variables this element sports <br/><br/>
        /// Example: <br/>
        /// C x1 y1 x2 y2 x y <br/>
        /// >> ValueCount = 6
        /// </summary>
        public int ValueCount { get; private set; }

        #region Pathd Element variables
        public bool Relative { get; set; }
        public double X { get; set; }
        public double X1 { get; set; }
        public double X2 { get; set; }
        public double Y { get; set; }
        public double Y1 { get; set; }
        public double Y2 { get; set; }
        public double RX { get; set; }
        public double RY { get; set; }
        public double Rotation { get; set; }
        public bool LargeArc { get; set; }
        public bool SweepArc { get; set; }
        #endregion

        #region Pathd Element Types
        public class M : PathdElement {
            /// <summary> Start a new sub-path at the given (x,y) coordinate. <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> M <paramref name="x"/> <paramref name="y"/> </c> ••• Absolute </item>
            /// <item> <c> m <paramref name="x"/> <paramref name="y"/> </c> ••• relative </item>
            /// </list> </summary>
            public M(double x, double y, bool relative) {
                ValueCount = 2;

                Relative = relative;

                X = x; Y = y;

                Data = $"{(relative ? "m" : "M")} {X} {Y}";

                DataValueRanges = SVGHelper.GenerateValueRanges(this);
            }
        }
        public class L : PathdElement {
            /// <summary>
            /// Draw a line from the current point to the given (x,y) coordinate which <br/>
            /// becomes the new current point.  <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> L <paramref name="x"/> <paramref name="y"/> </c> ••• Absolute </item>
            /// <item> <c> l <paramref name="x"/> <paramref name="y"/> </c> ••• relative </item>
            /// </list> </summary>
            public L(double x, double y, bool relative) {
                ValueCount = 2;

                Relative = relative;

                X = x; Y = y;

                Data = $"{(relative ? "l" : "L")} {X} {Y}";

                DataValueRanges = SVGHelper.GenerateValueRanges(this);
            }
        }
        public class V : PathdElement {
            /// <summary>
            /// Draws a vertical line from the current point (cpx, cpy) to (cpx, y).   <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> V <paramref name="y"/> </c> ••• Absolute </item>
            /// <item> <c> v <paramref name="y"/> </c> ••• relative </item>
            /// </list> </summary>
            public V(double y, bool relative) {
                ValueCount = 1;

                Relative = relative;

                Y = y;

                Data = $"{(relative ? "v" : "V")} {Y}";

                DataValueRanges = SVGHelper.GenerateValueRanges(this);
            }
        }
        public class H : PathdElement {
            /// <summary>
            /// Draws a horizontal line from the current point (cpx, cpy) to (x, cpy). <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> H <paramref name="x"/> </c> ••• Absolute </item>
            /// <item> <c> h <paramref name="x"/> </c> ••• relative </item>
            /// </list> </summary>
            public H(double x, bool relative) {
                ValueCount = 1;

                Relative = relative;

                X = x;

                Data = $"{(relative ? "h" : "H")} {X}";

                DataValueRanges = SVGHelper.GenerateValueRanges(this);
            }
        }
        public class C : PathdElement {
            /// <summary>
            /// Draws a cubic bezier curve from the current point to (x,y) using (x1,y1) <br/>
            /// as the control point at the beginning of the curve and (x2,y2) as the    <br/>
            /// control point at the end of the curve.    <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> C <paramref name="x1"/> <paramref name="y1"/> <paramref name="x2"/> <paramref name="y2"/> <paramref name="x"/> <paramref name="y"/> </c> ••• Absolute </item>
            /// <item> <c> c <paramref name="x1"/> <paramref name="y1"/> <paramref name="x2"/> <paramref name="y2"/> <paramref name="x"/> <paramref name="y"/> </c> ••• relative </item>
            /// </list> </summary>
            public C(double x1, double y1, double x2, double y2, double x, double y, bool relative) {
                ValueCount = 6;

                Relative = relative;

                X1 = x1; Y1 = y1; X2 = x2; Y2 = y2; X = x; Y = y;

                Data = $"{(relative ? "c" : "C")} {X1} {Y1} {X2} {Y2} {X} {Y}";

                DataValueRanges = SVGHelper.GenerateValueRanges(this);
            }
        }
        public class S : PathdElement {
            /// <summary>
            /// Draws a cubic bezier curve from the current point to (x,y). The first    <br/>
            /// control point is assumed to be the reflection of the second control      <br/>
            /// point on the previous command relative to the current point. (If there   <br/>
            /// is no previous command or if the previous command was not a curve, assume <br/>
            /// the first control point is coincident with the current point.) (x2,y2) is <br/>
            /// the second control point (i.e., the control point at the end of the curve).     <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> S <paramref name="x2"/> <paramref name="y2"/> <paramref name="x"/> <paramref name="y"/> </c> ••• Absolute </item>
            /// <item> <c> s <paramref name="x2"/> <paramref name="y2"/> <paramref name="x"/> <paramref name="y"/> </c> ••• relative </item>
            /// </list> </summary>
            public S(double x2, double y2, double x, double y, bool relative) {
                ValueCount = 4;

                Relative = relative;

                X2 = x2; Y2 = y2; X = x; Y = y;

                Data = $"{(relative ? "s" : "S")} {X2} {Y2} {X} {Y}";

                DataValueRanges = SVGHelper.GenerateValueRanges(this);
            }
        }
        public class Q : PathdElement {
            /// <summary>
            /// A quarter ellipse is drawn from the current point to the given end point. <br/>
            /// Draws a quadratic bezier curve from the current point to (x,y) using
            /// (x1,y1) as the control point. <br/><br/>
            /// /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> Q <paramref name="x1"/> <paramref name="y1"/> <paramref name="x"/> <paramref name="y"/> </c> ••• Absolute </item>
            /// <item> <c> q <paramref name="x1"/> <paramref name="y1"/> <paramref name="x"/> <paramref name="y"/> </c> ••• relative </item>
            /// </list> </summary>
            public Q(double x1, double y1, double x, double y, bool relative) {
                ValueCount = 4;
                Relative = relative;

                X1 = x1; Y1 = y1; X = x; Y = y;

                Data = $"{(relative ? "q" : "Q")} {X1} {Y1} {X} {Y}";

                DataValueRanges = SVGHelper.GenerateValueRanges(this);
            }
        }
        public class T : PathdElement {
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
            /// <item> <c> T <paramref name="x"/> <paramref name="y"/> </c> ••• Absolute </item>
            /// <item> <c> t <paramref name="x"/> <paramref name="y"/> </c> ••• relative </item>
            /// </list> </summary>
            public T(double x, double y, bool relative) {
                ValueCount = 2;

                Relative = relative;

                X = x; Y = y;

                Data = $"{(relative ? "t" : "T")} {X} {Y}";

                DataValueRanges = SVGHelper.GenerateValueRanges(this);
            }
        }
        public class A : PathdElement {
            /// <summary>
            /// Draws an elliptical arc <br/>
            /// No Description available.  <br/><br/>
            /// <br/><br/>
            /// >> Note: Variables marked with ? are <see cref="bool"/> <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> A <paramref name="rx"/> <paramref name="ry"/> <paramref name="rotation"/> <paramref name="largeArc"/>? <paramref name="sweepArc"/>? <paramref name="x"/> <paramref name="y"/> </c> ••• Absolute </item>
            /// <item> <c> a <paramref name="rx"/> <paramref name="ry"/> <paramref name="rotation"/> <paramref name="largeArc"/>? <paramref name="sweepArc"/>? <paramref name="x"/> <paramref name="y"/> </c> ••• relative </item>
            /// </list> </summary>
            public A(double rx, double ry, double rotation, bool largeArc, bool sweepArc, double x, double y, bool relative) {
                ValueCount = 7;

                Relative = relative;

                RX = rx; RY = ry; Rotation = rotation;
                LargeArc = largeArc; SweepArc = sweepArc;
                X = x; Y = y;

                Data = $"{(relative ? "a" : "A")} {RX} {RY} {Rotation} {Convert.ToInt16(LargeArc)} {Convert.ToInt16(SweepArc)} {X} {Y}";

                DataValueRanges = SVGHelper.GenerateValueRanges(this);
            }
        }

        public class Z : PathdElement {
            /// <summary>
            /// Close the current subpath by drawing a straight line from the current <br/>
            /// point to current subpath's most recent starting point (usually, the   <br/>
            /// most recent moveto point). <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> Z </c> </item>
            /// </list> </summary>
            public Z() {
                ValueCount = 0;
                Relative = true; //? Z is always relative
                Data = "Z";
            }
        }
        #endregion

    }


    #region Static || Easy Alternative
    /// <summary> SVG <c>&lt;path d="..."&gt;</c> property manipulation <br/><br/>
    /// Static part of <see cref="SVG_Pathd"/> <br/>
    /// >> Easier to use <br/>
    /// >> More descriptive </summary>
    public partial class PathdElement {

        /// <summary> Start a new sub-path at the given (x,y) coordinate. <br/><br/>
        /// Data example: <br/>
        /// <list type="bullet">
        /// <item> <c> M <paramref name="x"/> <paramref name="y"/> </c> ••• Absolute </item>
        /// <item> <c> m <paramref name="x"/> <paramref name="y"/> </c> ••• relative </item>
        /// </list> </summary>
        public static M MoveTo(double x, double y, bool relative)
            => new(x, y, relative);
        /// <summary> <c> { L || H || V } { X Y || X || Y } </c> </summary>
        public class LineTo {
            /// <summary>
            /// Draw a line from the current point to the given (x,y) coordinate which <br/>
            /// becomes the new current point.  <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> L <paramref name="x"/> <paramref name="y"/> </c> ••• Absolute </item>
            /// <item> <c> l <paramref name="x"/> <paramref name="y"/> </c> ••• relative </item>
            /// </list> </summary>
            public static L Free(double x, double y, bool relative)
                => new(x, y, relative);
            /// <summary>
            /// Draws a vertical line from the current point (cpx, cpy) to (cpx, y).   <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> V <paramref name="y"/> </c> ••• Absolute </item>
            /// <item> <c> v <paramref name="y"/> </c> ••• relative </item>
            /// </list> </summary>
            public static V Vertical(double y, bool relative)
                    => new(y, relative);
            /// <summary>
            /// Draws a horizontal line from the current point (cpx, cpy) to (x, cpy). <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> H <paramref name="x"/> </c> ••• Absolute </item>
            /// <item> <c> h <paramref name="x"/> </c> ••• relative </item>
            /// </list> </summary>
            public static H Horizontal(double x, bool relative)
                => new(x, relative);
        }
        /// <summary>
        /// <c> { C || S } CX CY A1 A2 R1 R2 A3 </c> <br/>
        /// <c> Q X1 Y1 X Y </c> <br/>
        /// <c> T { X1 Y1 || X1 Y1 X Y } </c>
        /// </summary>
        public struct CurveTo {
            /// <summary>
            /// Draws a cubic bezier curve from the current point to (x,y) using (x1,y1) <br/>
            /// as the control point at the beginning of the curve and (x2,y2) as the    <br/>
            /// control point at the end of the curve.    <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> C <paramref name="x1"/> <paramref name="y1"/> <paramref name="x2"/> <paramref name="y2"/> <paramref name="x"/> <paramref name="y"/> </c> ••• Absolute </item>
            /// <item> <c> c <paramref name="x1"/> <paramref name="y1"/> <paramref name="x2"/> <paramref name="y2"/> <paramref name="x"/> <paramref name="y"/> </c> ••• relative </item>
            /// </list> </summary>
            public static C Regular(double x1, double y1, double x2, double y2, double x, double y, bool relative)
            => new(x1, y1, x2, y2, x, y, relative);
            /// <summary>
            /// Draws a cubic bezier curve from the current point to (x,y). The first    <br/>
            /// control point is assumed to be the reflection of the second control      <br/>
            /// point on the previous command relative to the current point. (If there   <br/>
            /// is no previous command or if the previous command was not a curve, assume <br/>
            /// the first control point is coincident with the current point.) (x2,y2) is <br/>
            /// the second control point (i.e., the control point at the end of the curve).     <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> S <paramref name="x2"/> <paramref name="y2"/> <paramref name="x"/> <paramref name="y"/> </c> ••• Absolute </item>
            /// <item> <c> s <paramref name="x2"/> <paramref name="y2"/> <paramref name="x"/> <paramref name="y"/> </c> ••• relative </item>
            /// </list> </summary>
            public static S Smooth(double x2, double y2, double x, double y, bool relative)
                => new(x2, y2, x, y, relative);

            /// <summary>
            /// A quarter ellipse is drawn from the current point to the given end point. <br/>
            /// Draws a quadratic bezier curve from the current point to (x,y) using
            /// (x1,y1) as the control point. <br/><br/>
            /// /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> Q <paramref name="x1"/> <paramref name="y1"/> <paramref name="x"/> <paramref name="y"/> </c> ••• Absolute </item>
            /// <item> <c> q <paramref name="x1"/> <paramref name="y1"/> <paramref name="x"/> <paramref name="y"/> </c> ••• relative </item>
            /// </list> </summary>
            public static Q QuadraticBezier(double x1, double y1, double x, double y, bool relative)
                => new(x1, y1, x, y, relative);

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
            /// <item> <c> T <paramref name="x"/> <paramref name="y"/> </c> ••• Absolute </item>
            /// <item> <c> t <paramref name="x"/> <paramref name="y"/> </c> ••• relative </item>
            /// </list> </summary>
            public static T ShorthandQuadraticBezier(double x, double y, bool relative)
                => new(x, y, relative);
        }
        /// <summary>
        /// Draws an elliptical arc <br/>
        /// No Description available.  <br/><br/>
        /// <br/><br/>
        /// >> Note: Variables marked with ? are <see cref="bool"/> <br/><br/>
        /// Data example: <br/>
        /// <list type="bullet">
        /// <item> <c> A <paramref name="rx"/> <paramref name="ry"/> <paramref name="rotation"/> <paramref name="largeArc"/>? <paramref name="sweepArc"/>? <paramref name="x"/> <paramref name="y"/> </c> ••• Absolute </item>
        /// <item> <c> a <paramref name="rx"/> <paramref name="ry"/> <paramref name="rotation"/> <paramref name="largeArc"/>? <paramref name="sweepArc"/>? <paramref name="x"/> <paramref name="y"/> </c> ••• relative </item>
        /// </list> </summary>
        public static A EllipticalArc(double rx, double ry, double rotation, bool largeArc, bool sweepArc, double x, double y, bool relative)
            => new(rx, ry, rotation, largeArc, sweepArc, x, y, relative);
        /// <summary>
        /// Close the current subpath by drawing a straight line from the current <br/>
        /// point to current subpath's most recent starting point (usually, the   <br/>
        /// most recent moveto point). <br/><br/>
        /// Data example: <br/>
        /// <list type="bullet">
        /// <item> <c> Z </c> </item>
        /// </list> </summary>
        public static Z ClosePath() => new();
    }
    #endregion
}
