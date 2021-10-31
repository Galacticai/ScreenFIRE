using ScreenFIRE.Modules.Companion.math.Vision.SVG_Tools.Companion;
using System;
using System.Collections.Generic;

namespace ScreenFIRE.Modules.Companion.math.Vision.SVG_Tools {
    //! Move to (0,0)
    //?    >> (x|y) - Min(All(x|y))
    //  M  =>       x    ,  y
    //  L  =>       x    ,  y
    //  V  =>       y    ,
    //  H  =>       x    ,
    //  C  =>       x1   ,  y1   ,  x2   ,  y2       ,  x     ,  y
    //  S  =>       x2   ,  y2   ,  x    ,  y        ,
    //  Q  =>       x1   ,  y1   ,  x    ,  y        ,
    //  T  =>       x    ,  y
    //  A  =>       rx   ,  ry   ,  a    , large-arc ,  sweep ,  x   ,  y  ,

    /// <summary> SVG <c>&lt;path d="..."&gt;</c> property manipulation </summary>
    public partial record SVG_PathdElement {

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
        public record M : SVG_PathdElement {
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

                DataValueRanges = SVGHelper.Parse.ValueRanges(this);
            }
        }
        public record L : SVG_PathdElement {
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

                DataValueRanges = SVGHelper.Parse.ValueRanges(this);
            }
        }
        public record V : SVG_PathdElement {
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

                DataValueRanges = SVGHelper.Parse.ValueRanges(this);
            }
        }
        public record H : SVG_PathdElement {
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

                DataValueRanges = SVGHelper.Parse.ValueRanges(this);
            }
        }
        public record C : SVG_PathdElement {
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

                DataValueRanges = SVGHelper.Parse.ValueRanges(this);
            }
        }
        public record S : SVG_PathdElement {
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

                DataValueRanges = SVGHelper.Parse.ValueRanges(this);
            }
        }
        public record Q : SVG_PathdElement {
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

                DataValueRanges = SVGHelper.Parse.ValueRanges(this);
            }
        }
        public record T : SVG_PathdElement {
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

                DataValueRanges = SVGHelper.Parse.ValueRanges(this);
            }
        }
        public record A : SVG_PathdElement {
            /// <summary>
            /// No Description available.  <br/><br/>
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

                DataValueRanges = SVGHelper.Parse.ValueRanges(this);
            }
        }

        public record Z : SVG_PathdElement {
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

        //#region Types

        //public record Z : SVG_PathdElement {
        //    internal int ValueCount => 0;
        //    /// <summary>
        //    /// Close the current subpath by drawing a straight line from the current <br/>
        //    /// point to current subpath's most recent starting point (usually, the   <br/>
        //    /// most recent moveto point). <br/><br/>
        //    /// Data example: <br/>
        //    /// <list type="bullet">
        //    /// <item> <c> z </c> </item>
        //    /// </list> </summary>
        //    public Z() { Data = $"Z"; }
        //}
        //public record A : SVG_PathdElement {
        //    internal int ValueCount => 0;
        //    /// <summary>
        //    /// An A can appear anywhere between coordinates within any path data     <br/>
        //    /// command to indicate that all subsequent coordinates are absolute. <br/><br/>
        //    /// Data example: <br/>
        //    /// <list type="bullet">
        //    /// <item> <c> A </c> </item>
        //    /// </list> </summary>
        //    public A() { Data = $"A"; }
        //}

        //public record r : SVG_PathdElement {
        //    internal int ValueCount => 0;
        //    /// <summary>
        //    /// An r can appear anywhere between coordinates within any path data     <br/>
        //    /// command to indicate that all subsequent coordinates are relative to the <br/>
        //    /// current point at the start of the command. <br/><br/>
        //    /// Data example: <br/>
        //    /// <list type="bullet">
        //    /// <item> <c> r </c> </item>
        //    /// </list> </summary>
        //    public r() { Data = $"r"; }
        //}

        //public record M : SVG_PathdElement {
        //    internal int ValueCount => 2;
        //    /// <summary> Start a new sub-path at the given (x,y) coordinate. <br/><br/>
        //    /// Data example: <br/>
        //    /// <list type="bullet">
        //    /// <item> <c> M X Y </c> ••• Absolute </item>
        //    /// <item> <c> m X Y </c> ••• relative </item>
        //    /// </list> </summary>
        //    public M(double x, double y, bool relative) {
        //        X = x; Y = y;
        //        Data = $"{(relative ? "m" : "M")} {X} {Y}";

        //        DataValueRanges = SVGHelper.Parse.ValueRanges(this);
        //        // X_DataIndex = indexes[0]; Y_DataIndex = indexes[1];
        //    }
        //}

        //public record L : SVG_PathdElement {
        //    internal int ValueCount => 2;
        //    /// <summary>
        //    /// Draw a line from the current point to the given (x,y) coordinate which <br/>
        //    /// becomes the new current point.  <br/><br/>
        //    /// Data example: <br/>
        //    /// <list type="bullet">
        //    /// <item> <c> L X Y </c> ••• Absolute </item>
        //    /// <item> <c> l X Y </c> ••• relative </item>
        //    /// </list> </summary>
        //    public L(double x, double y, bool relative) {
        //        X = x; Y = y;
        //        Data = $"{(relative ? "l" : "L")} {X} {Y}";

        //        DataValueRanges = SVGHelper.Parse.ValueRanges(this);
        //        // X_DataIndex = indexes[0]; Y_DataIndex = indexes[1];
        //    }
        //}
        //public record H : SVG_PathdElement {
        //    internal int ValueCount => 1;
        //    /// Draws a horizontal line from the current point (cpx, cpy) to (x, cpy). <br/><br/>
        //    /// Data example: <br/>
        //    /// <list type="bullet">
        //    /// <item> <c> H X </c> ••• Absolute </item>
        //    /// <item> <c> h X </c> ••• relative </item>
        //    /// </list> </summary>
        //    public H(double x, bool relative) {
        //        X = x;
        //        Data = $"{(relative ? "h" : "H")} {X}";

        //        DataValueRanges = SVGHelper.Parse.ValueRanges(this);
        //        //  X_DataIndex = 2;
        //    }
        //}
        //public record V : SVG_PathdElement {
        //    internal int ValueCount => 1;
        //    /// <summary>
        //    /// Draws a vertical line from the current point (cpx, cpy) to (cpx, y).   <br/><br/>
        //    /// Data example: <br/>
        //    /// <list type="bullet">
        //    /// <item> <c> V Y </c> ••• Absolute </item>
        //    /// <item> <c> v Y </c> ••• relative </item>
        //    /// </list> </summary>
        //    public V(double y, bool relative) {
        //        Y = y;
        //        Data = $"{(relative ? "v" : "V")} {Y}";
        //        DataValueRanges = SVGHelper.Parse.ValueRanges(this);
        //        // Y_DataIndex = 2;
        //    }
        //}


        //public record C : SVG_PathdElement {
        //    internal int ValueCount => 6;
        //    /// <summary>
        //    /// Draws a cubic bezier curve from the current point to (x,y) using (x1,y1) <br/>
        //    /// as the control point at the beginning of the curve and (x2,y2) as the    <br/>
        //    /// control point at the end of the curve.    <br/><br/>
        //    /// Data example: <br/>
        //    /// <list type="bullet">
        //    /// <item> <c> C X1 Y1 X2 Y2 X Y </c> ••• Absolute </item>
        //    /// <item> <c> c X1 Y1 X2 Y2 X Y </c> ••• relative </item>
        //    /// </list> </summary>
        //    public C(double x1, double y1, double x2, double y2, double x, double y, bool relative) {
        //        X1 = x1; Y1 = y1; X2 = x2; Y2 = y2; X = x; Y = y;
        //        Data = $"{(relative ? "c" : "C")} {X1} {Y1} {X2} {Y2} {X} {Y}";

        //        DataValueRanges = SVGHelper.Parse.ValueRanges(this);
        //        // X1_DataIndex = indexes[0]; Y1_DataIndex = indexes[1];
        //        // X2_DataIndex = indexes[2]; Y2_DataIndex = indexes[3];
        //        // X_DataIndex = indexes[4]; Y_DataIndex = indexes[5];
        //    }
        //}
        //public record S : SVG_PathdElement {
        //    internal int ValueCount => 4;
        //    /// <summary>
        //    /// Draws a cubic bezier curve from the current point to (x,y). The first    <br/>
        //    /// control point is assumed to be the reflection of the second control      <br/>
        //    /// point on the previous command relative to the current point. (If there   <br/>
        //    /// is no previous command or if the previous command was not a curve, assume <br/>
        //    /// the first control point is coincident with the current point.) (x2,y2) is <br/>
        //    /// the second control point (i.e., the control point at the end of the curve).     <br/><br/>
        //    /// Data example: <br/>
        //    /// <list type="bullet">
        //    /// <item> <c> S X2 Y2 X Y </c> ••• Absolute </item>
        //    /// <item> <c> s X2 Y2 X Y </c> ••• relative </item>
        //    /// </list> </summary>
        //    public S(double x2, double y2, double x, double y, bool relative) {
        //        X2 = x2; Y2 = y2; X = x; Y = y;
        //        Data = $"{(relative ? "s" : "S")} {X2} {Y2} {X} {Y}";

        //        DataValueRanges = SVGHelper.Parse.ValueRanges(this);
        //        // X2_DataIndex = indexes[0]; Y2_DataIndex = indexes[1];
        //        // X_DataIndex = indexes[2]; Y_DataIndex = indexes[3];
        //    }
        //}

        //public record Q : SVG_PathdElement {
        //    internal int ValueCount => 4;
        //    /// <summary>
        //    /// A quarter ellipse is drawn from the current point to the given end point. <br/>
        //    /// Draws a quadratic bezier curve from the current point to (x,y) using
        //    /// (x1,y1) as the control point. <br/><br/>
        //    /// /// Data example: <br/>
        //    /// <list type="bullet">
        //    /// <item> <c> Q X1 Y1 X Y </c> ••• Absolute </item>
        //    /// <item> <c> q X1 Y1 X Y </c> ••• relative </item>
        //    /// </list> </summary>
        //    public Q(double x1, double y1, double x, double y, bool relative) {
        //        X1 = x1; Y1 = y1; X = x; Y = y;
        //        Data = $"{(relative ? "q" : "Q")} {X1} {Y1} {X} {Y}";

        //        DataValueRanges = SVGHelper.Parse.ValueRanges(this);
        //        // X1_DataIndex = indexes[0]; Y1_DataIndex = indexes[1];
        //        // X_DataIndex = indexes[2]; Y_DataIndex = indexes[3];
        //    }
        //}

        //public record T : SVG_PathdElement {
        //    internal int ValueCount => 2;
        //    /// <summary>
        //    /// Draws one or more quadratic bezier curves by means of multiple control    <br/>
        //    /// points (can be as few as one set) and a single end point. Intermediate    <br/>
        //    /// (on-curve) points are obtained by interpolation between successive control <br/>
        //    /// points as in the TrueType documentation within the OpenType font specification. <br/>
        //    /// (??? More details needed.) The subpath need not be started in which case  <br/>
        //    /// the subpath will be closed. In this case the last point of the subpath    <br/>
        //    /// defines the start point of the quadratic bezier.  <br/><br/>
        //    /// Data example: <br/>
        //    /// <list type="bullet">
        //    /// <item> <c> T X Y </c> ••• Absolute </item>
        //    /// <item> <c> t X Y </c> ••• relative </item>
        //    /// </list> </summary>
        //    public T(double x, double y, bool relative) {
        //        X = x; Y = y;
        //        Data = $"{(relative ? "t" : "T")} {X} {Y}";

        //        DataValueRanges = SVGHelper.Parse.ValueRanges(this);
        //        // X1_DataIndex = indexes[0]; Y1_DataIndex = indexes[1];
        //    }
        //}

        ////private static void SetIndexes(SVG_Pathd PathData_Instance) {
        ////    var indexes = SVGHelper.PathData_ValueIndexes(PathData_Instance.Data);
        ////    PathData_Instance.CX_DataIndex = indexes[0]; PathData_Instance.CY_DataIndex = indexes[1];
        ////    PathData_Instance.A1_DataIndex = indexes[2]; PathData_Instance.A2_DataIndex = indexes[3];
        ////    PathData_Instance.R1_DataIndex = indexes[4]; PathData_Instance.R2_DataIndex = indexes[5];
        ////    PathData_Instance.A3_DataIndex = indexes[6];
        ////}
        //public record G : SVG_PathdElement {
        //    internal int ValueCount => 7;
        //    /// <summary>
        //    /// Same as <see cref="E"/>, except that the arc is drawn in a <br/>
        //    /// clockwise direction.  <br/><br/>
        //    /// Data example: <br/>
        //    /// <list type="bullet">
        //    /// <item> <c> G CX CY A1 A2 R1 R2 A3 </c> ••• Absolute </item>
        //    /// <item> <c> g CX CY A1 A2 R1 R2 A3 </c> ••• relative </item>
        //    /// </list> </summary>
        //    public G(double cx, double cy, double a1, double a2, double r1, double r2, double a3, bool relative) {
        //        CX = cx; CY = cy; A1 = a1; A2 = a2; R1 = r1; R2 = r2; A3 = a3;
        //        Data = $"{(relative ? "g" : "G")} {CX} {CY} {A1} {A2} {R1} {R2} {A3}";

        //        DataValueRanges = SVGHelper.Parse.ValueRanges(this);
        //        DataValueRanges = SVGHelper.Parse.ValueRanges(this);
        //    }
        //}
        //public record E : SVG_PathdElement {
        //    internal int ValueCount => 7;
        //    /// <summary>
        //    /// Same as <see cref="D"/>, except that there is an   <br/>
        //    /// implicit lineto from the current point to the start of the first arc.  <br/><br/>
        //    /// Data example: <br/>
        //    /// <list type="bullet">
        //    /// <item> <c> E CX CY A1 A2 R1 R2 A3 </c> ••• Absolute </item>
        //    /// <item> <c> e CX CY A1 A2 R1 R2 A3 </c> ••• relative </item>
        //    /// </list> </summary>
        //    public E(double cx, double cy, double a1, double a2, double r1, double r2, double a3, bool relative) {
        //        CX = cx; CY = cy; A1 = a1; A2 = a2; R1 = r1; R2 = r2; A3 = a3;
        //        Data = $"{(relative ? "e" : "E")} {CX} {CY} {A1} {A2} {R1} {R2} {A3}";

        //        DataValueRanges = SVGHelper.Parse.ValueRanges(this);
        //    }
        //}

        //public record F : SVG_PathdElement {
        //    internal int ValueCount => 7;
        //    /// <summary>
        //    /// Same as <see cref="Counterclockwise"/>, except that the arc is drawn in a <br/>
        //    /// clockwise direction.  <br/><br/>
        //    /// Data example: <br/>
        //    /// <list type="bullet">
        //    /// <item> <c> F CX CY A1 A2 R1 R2 A3 </c> ••• Absolute </item>
        //    /// <item> <c> f CX CY A1 A2 R1 R2 A3 </c> ••• relative </item>
        //    /// </list> </summary>
        //    public F(double cx, double cy, double a1, double a2, double r1, double r2, double a3, bool relative) {
        //        CX = cx; CY = cy; A1 = a1; A2 = a2; R1 = r1; R2 = r2; A3 = a3;
        //        Data = $"{(relative ? "f" : "F")} {CX} {CY} {A1} {A2} {R1} {R2} {A3}";

        //        DataValueRanges = SVGHelper.Parse.ValueRanges(this);
        //    }
        //}
        //public record D : SVG_PathdElement {
        //    internal int ValueCount => 7;
        //    /// <summary>
        //    /// Draws a counterclockwise elliptical arc described by its center (cx, cy), <br/>
        //    /// start angle in degrees (a1), end angle in degrees (a2), radius along X-axis <br/>
        //    /// (r1), radius along Y-axis (r2), and ellipse rotate angle in degrees (a3). <br/>
        //    /// All angles are converted to modula 360 degrees before processing, after   <br/>
        //    /// which if a1=a2, then a complete ellipse is drawn. If r2 is not provided,  <br/>
        //    /// it is assumed to be equal to r1 (circular arc). If a3 is not provided, it <br/>
        //    /// is assumed that the ellipse aligns with the axes of the current coordinate <br/>
        //    /// system. There is an implicit moveto from the current point to the start of <br/>
        //    /// the first arc.   <br/><br/>
        //    /// Data example: <br/>
        //    /// <list type="bullet">
        //    /// <item> <c> D CX CY A1 A2 R1 R2 A3 </c> ••• Absolute </item>
        //    /// <item> <c> d CX CY A1 A2 R1 R2 A3 </c> ••• relative </item>
        //    /// </list> </summary>
        //    public D(double cx, double cy, double a1, double a2, double r1, double r2, double a3, bool relative) {
        //        CX = cx; CY = cy; A1 = a1; A2 = a2; R1 = r1; R2 = r2; A3 = a3;
        //        Data = $"{(relative ? "d" : "D")} {CX} {CY} {A1} {A2} {R1} {R2} {A3}";

        //        DataValueRanges = SVGHelper.Parse.ValueRanges(this);
        //    }
        //}

        //public record J : SVG_PathdElement {
        //    internal int ValueCount => 3;
        //    /// <summary>
        //    /// A quarter ellipse is drawn from the current point to the given end point. <br/>
        //    /// The elliptical segment is initially tangential to a line pointing in      <br/>
        //    /// direction <angle> at the current point and ends up tangential to a        <br/>
        //    /// perpendicular line to <angle> going through (x,y).   <br/><br/>
        //    /// Data example: <br/>
        //    /// <list type="bullet">
        //    /// <item> <c> J X Y Angle </c> ••• Absolute </item>
        //    /// <item> <c> j X Y Angle </c> ••• relative </item>
        //    /// </list> </summary>
        //    public J(double x, double y, double angle, bool relative) {
        //        X = x; Y = y; Angle = angle;
        //        Data = $"{(relative ? "j" : "J")} {X} {Y} {Angle}";

        //        DataValueRanges = SVGHelper.Parse.ValueRanges(this);
        //        // X_DataIndex = indexes[0]; Y_DataIndex = indexes[1];
        //        // Angle_DataIndex = indexes[2];
        //    }
        //}
        //#endregion
    }


    #region Static || Easy Alternative
    /// <summary> SVG <c>&lt;path d="..."&gt;</c> property manipulation <br/><br/>
    /// Static part of <see cref="SVG_Pathd"/> <br/>
    /// >> Easier to use <br/>
    /// >> More descriptive </summary>
    public partial record SVG_PathdElement {

        /// <summary> Start a new sub-path at the given (x,y) coordinate. <br/><br/>
        /// Data example: <br/>
        /// <list type="bullet">
        /// <item> <c> M <paramref name="x"/> <paramref name="y"/> </c> ••• Absolute </item>
        /// <item> <c> m <paramref name="x"/> <paramref name="y"/> </c> ••• relative </item>
        /// </list> </summary>
        public static M MoveTo(double x, double y, bool relative)
            => new(x, y, relative);

        /// <summary> <c> { L || H || V } { X Y || X || Y } </c> </summary>
        public struct LineTo {
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
            public static T TrueTypeQuadraticBezier(double x, double y, bool relative)
                => new(x, y, relative);
        }


        /// <summary>
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
