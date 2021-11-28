using System.Collections.Generic;

namespace ScreenFIRE.Modules.Companion.math.Vision.SVG_Tools.Companion {
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
    //  A   =>   rx   ry   rotation   largeArc?  sweepArc?  x  y

    /// <summary> SVG <c>&lt;path d="..."&gt;</c> property manipulation </summary>
    public partial class PathdElement {
        private PathdElement() { } //!? Prevent constructing superclass

        /// <summary> Determines whether this element is relative or not <br/><br/>
        /// Lowercase type >> Relative = true </summary>
        public bool Relative => char.IsLower(Data_Raw[IPathdElement_Data.Type][0]);

        internal SortedDictionary<IPathdElement_Data, dynamic> Data_Raw { get; set; }

        /// <summary> SVG Pathd Element as <see cref="string"/> <br/><br/>
        /// Example: <br/>
        /// • <c>&lt;path d="<see cref="Data"/> <see cref="Data"/>...etc" /&gt;</c>) </summary>
        public string Data {
            get {
                string data = $"{Data_Raw[IPathdElement_Data.Type]} ";
                foreach (IPathdElement_Data key in Data_Raw.Keys)
                    data += $"{Data_Raw[key]} ";
                return data.Trim();
            }
        }

        #region Pathd Element variables
        internal enum IPathdElement_Data {
            //? Type is the first character of Data string (type of element)
            Type,                   //!? char
            X, X1, X2,              //!? float
            Y, Y1, Y2,              //!? float
            RX, RY,                 //!? float
            Rotation,               //!? float
            LargeArc_, SweepArc_,   //!? bool
        }
        private float _X; public float X {
            get => _X;
            set {
                _X = value;
                Data_Raw[IPathdElement_Data.X] = value;
            }
        }
        private float _X1; public float X1 {
            get => _X1;
            set {
                _X1 = value;
                Data_Raw[IPathdElement_Data.X1] = value;
            }
        }
        private float _X2; public float X2 {
            get => _X2;
            set {
                _X2 = value;
                Data_Raw[IPathdElement_Data.X2] = value;
            }
        }
        private float _Y; public float Y {
            get => _Y;
            set {
                _Y = value;
                Data_Raw[IPathdElement_Data.Y] = value;
            }
        }
        private float _Y1; public float Y1 {
            get => _Y1;
            set {
                _Y1 = value;
                Data_Raw[IPathdElement_Data.Y1] = value;
            }
        }
        private float _Y2; public float Y2 {
            get => _Y2;
            set {
                _Y2 = value;
                Data_Raw[IPathdElement_Data.Y2] = value;
            }
        }
        private float _RX; public float RX {
            get => _RX;
            set {
                _RX = value;
                Data_Raw[IPathdElement_Data.RX] = value;
            }
        }
        private float _RY; public float RY {
            get => _RY;
            set {
                _RY = value;
                Data_Raw[IPathdElement_Data.RY] = value;
            }
        }
        private float _Rotation; public float Rotation {
            get => _Rotation;
            set {
                _Rotation = value;
                Data_Raw[IPathdElement_Data.Rotation] = value;
            }
        }
        private bool _LargeArc = false; public bool LargeArc {
            get => _LargeArc;
            set {
                _LargeArc = value;
                Data_Raw[IPathdElement_Data.LargeArc_] = value;
            }
        }
        private bool _SweepArc = false; public bool SweepArc {
            get => _SweepArc;
            set {
                _SweepArc = value;
                Data_Raw[IPathdElement_Data.SweepArc_] = value;
            }
        }
        #endregion

        #region Pathd Element Types
        public class M : PathdElement {
            /// <summary> Start a new sub-path at the given (x,y) coordinate. <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> M <paramref name="x"/> <paramref name="y"/> </c> ••• Absolute </item>
            /// <item> <c> m <paramref name="x"/> <paramref name="y"/> </c> ••• relative </item>
            /// </list> </summary>
            public M(float x, float y, bool relative) {
                X = x; Y = y;
                Data_Raw = new();
                Data_Raw.Add(IPathdElement_Data.Type,
                             relative ? 'm' : 'M');
                Data_Raw.Add(IPathdElement_Data.X, x);
                Data_Raw.Add(IPathdElement_Data.Y, y);
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
            public L(float x, float y, bool relative) {
                X = x; Y = y;
                Data_Raw = new();
                Data_Raw.Add(IPathdElement_Data.Type,
                             relative ? 'l' : 'L');
                Data_Raw.Add(IPathdElement_Data.X, x);
                Data_Raw.Add(IPathdElement_Data.Y, y);
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
            public V(float y, bool relative) {
                Y = y;
                Data_Raw = new();
                Data_Raw.Add(IPathdElement_Data.Type,
                             relative ? 'v' : 'V');
                Data_Raw.Add(IPathdElement_Data.Y, y);
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
            public H(float x, bool relative) {
                X = x;
                Data_Raw = new();
                Data_Raw.Add(IPathdElement_Data.Type,
                             relative ? 'h' : 'H');
                Data_Raw.Add(IPathdElement_Data.X, x);
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
            public C(float x1, float y1, float x2, float y2, float x, float y, bool relative) {
                X1 = x1; Y1 = y1; X2 = x2; Y2 = y2; X = x; Y = y;
                Data_Raw = new();
                Data_Raw.Add(IPathdElement_Data.Type,
                             relative ? 'c' : 'C');
                Data_Raw.Add(IPathdElement_Data.X1, x1);
                Data_Raw.Add(IPathdElement_Data.Y1, y1);
                Data_Raw.Add(IPathdElement_Data.X2, x2);
                Data_Raw.Add(IPathdElement_Data.Y2, y2);
                Data_Raw.Add(IPathdElement_Data.X, x);
                Data_Raw.Add(IPathdElement_Data.Y, y);
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
            public S(float x2, float y2, float x, float y, bool relative) {
                X2 = x2; Y2 = y2; X = x; Y = y;
                Data_Raw = new();
                Data_Raw.Add(IPathdElement_Data.Type,
                             relative ? 's' : 'S');
                Data_Raw.Add(IPathdElement_Data.X2, x2);
                Data_Raw.Add(IPathdElement_Data.Y2, y2);
                Data_Raw.Add(IPathdElement_Data.X, x);
                Data_Raw.Add(IPathdElement_Data.Y, y);
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
            public Q(float x1, float y1, float x, float y, bool relative) {
                X1 = x1; Y1 = y1; X = x; Y = y;
                Data_Raw = new();
                Data_Raw.Add(IPathdElement_Data.Type,
                             relative ? 'q' : 'Q');
                Data_Raw.Add(IPathdElement_Data.X1, x1);
                Data_Raw.Add(IPathdElement_Data.Y1, y1);
                Data_Raw.Add(IPathdElement_Data.X, x);
                Data_Raw.Add(IPathdElement_Data.Y, y);
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
            public T(float x, float y, bool relative) {
                X = x; Y = y;
                Data_Raw = new();
                Data_Raw.Add(IPathdElement_Data.Type,
                             relative ? 't' : 'T');
                Data_Raw.Add(IPathdElement_Data.X, x);
                Data_Raw.Add(IPathdElement_Data.Y, y);
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
            public A(float rx, float ry, float rotation, bool largeArc, bool sweepArc, float x, float y, bool relative) {
                RX = rx; RY = ry; Rotation = rotation;
                LargeArc = largeArc; SweepArc = sweepArc;
                X = x; Y = y;
                Data_Raw = new();
                Data_Raw.Add(IPathdElement_Data.Type,
                             relative ? 'a' : 'A');
                Data_Raw.Add(IPathdElement_Data.RX, rx);
                Data_Raw.Add(IPathdElement_Data.RY, ry);
                Data_Raw.Add(IPathdElement_Data.Rotation, rotation);
                Data_Raw.Add(IPathdElement_Data.LargeArc_, largeArc);
                Data_Raw.Add(IPathdElement_Data.SweepArc_, sweepArc);
                Data_Raw.Add(IPathdElement_Data.X, x);
                Data_Raw.Add(IPathdElement_Data.Y, y);
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
                Data_Raw = new();
                Data_Raw.Add(IPathdElement_Data.Type,
                             'z'); //? Z is always relative
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
        public static M MoveTo(float x, float y, bool relative)
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
            public static L Free(float x, float y, bool relative)
                => new(x, y, relative);
            /// <summary>
            /// Draws a vertical line from the current point (cpx, cpy) to (cpx, y).   <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> V <paramref name="y"/> </c> ••• Absolute </item>
            /// <item> <c> v <paramref name="y"/> </c> ••• relative </item>
            /// </list> </summary>
            public static V Vertical(float y, bool relative)
                    => new(y, relative);
            /// <summary>
            /// Draws a horizontal line from the current point (cpx, cpy) to (x, cpy). <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> H <paramref name="x"/> </c> ••• Absolute </item>
            /// <item> <c> h <paramref name="x"/> </c> ••• relative </item>
            /// </list> </summary>
            public static H Horizontal(float x, bool relative)
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
            public static C Regular(float x1, float y1, float x2, float y2, float x, float y, bool relative)
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
            public static S Smooth(float x2, float y2, float x, float y, bool relative)
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
            public static Q QuadraticBezier(float x1, float y1, float x, float y, bool relative)
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
            public static T ShorthandQuadraticBezier(float x, float y, bool relative)
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
        public static A EllipticalArc(float rx, float ry, float rotation, bool largeArc, bool sweepArc, float x, float y, bool relative)
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
