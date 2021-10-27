using static ScreenFIRE.Modules.Companion.math.Vision.SVGd_Tools.SVGd.PathData;

namespace ScreenFIRE.Modules.Companion.math.Vision.SVGd_Tools.Companion {

    /// <summary> SVG <c>&lt;path d="..."&gt;</c> property manipulation <br/><br/>
    /// Static part of <see cref="SVGd"/> <br/>
    /// >> Easier to use <br/>
    /// >> More descriptive </summary>
    public partial class SVGd {

        #region Static || Alternative

        /// <summary>
        /// Close the current subpath by drawing a straight line from the current <br/>
        /// point to current subpath's most recent starting point (usually, the   <br/>
        /// most recent moveto point). <br/><br/>
        /// Data example: <br/>
        /// <list type="bullet">
        /// <item> <c> z </c> </item>
        /// </list> </summary>
        public static z ClosePath() => new();
        /// <summary>
        /// An A can appear anywhere between coordinates within any path data     <br/>
        /// command to indicate that all subsequent coordinates are absolute. <br/><br/>
        /// Data example: <br/>
        /// <list type="bullet">
        /// <item> <c> A </c> </item>
        /// </list> </summary>
        public static A AbsoluteCoordinates() => new();
        /// <summary>
        /// An r can appear anywhere between coordinates within any path data     <br/>
        /// command to indicate that all subsequent coordinates are relative to the <br/>
        /// current point at the start of the command. <br/><br/>
        /// Data example: <br/>
        /// <list type="bullet">
        /// <item> <c> r </c> </item>
        /// </list> </summary>
        public static r RelativeCoordinates() => new();

        /// <summary> Start a new sub-path at the given (x,y) coordinate. <br/><br/>
        /// Data example: <br/>
        /// <list type="bullet">
        /// <item> <c> M X Y </c> ••• Absolute </item>
        /// <item> <c> m X Y </c> ••• Relative </item>
        /// </list> </summary>
        public static M MoveTo(double x, double y, bool Relative)
            => new(x, y, Relative);

        /// <summary> <c> { L || H || V } { X Y || X || Y } </c> </summary>
        public struct LineTo {
            /// <summary>
            /// Draw a line from the current point to the given (x,y) coordinate which <br/>
            /// becomes the new current point.  <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> L X Y </c> ••• Absolute </item>
            /// <item> <c> l X Y </c> ••• Relative </item>
            /// </list> </summary>
            public static L Free(double x, double y, bool Relative)
                => new(x, y, Relative);
            /// Draws a horizontal line from the current point (cpx, cpy) to (x, cpy). <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> H X </c> ••• Absolute </item>
            /// <item> <c> h X </c> ••• Relative </item>
            /// </list> </summary>
            public static H Horizontal(double x, bool Relative)
                => new(x, Relative);
            /// <summary>
            /// Draws a vertical line from the current point (cpx, cpy) to (cpx, y).   <br/><br/>
            /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> V Y </c> ••• Absolute </item>
            /// <item> <c> v Y </c> ••• Relative </item>
            /// </list> </summary>
            public static V Vertical(double y, bool Relative)
                    => new(y, Relative);
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
            /// <item> <c> C X1 Y1 X2 Y2 X Y </c> ••• Absolute </item>
            /// <item> <c> c X1 Y1 X2 Y2 X Y </c> ••• Relative </item>
            /// </list> </summary>
            public static C Normal(double x1, double y1, double x2, double y2, double x, double y, bool Relative)
            => new(x1, y1, x2, y2, x, y, Relative);
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
            public static S Smooth(double x2, double y2, double x, double y, bool Relative)
                => new(x2, y2, x, y, Relative);

            /// <summary>
            /// A quarter ellipse is drawn from the current point to the given end point. <br/>
            /// Draws a quadratic bezier curve from the current point to (x,y) using
            /// (x1,y1) as the control point. <br/><br/>
            /// /// Data example: <br/>
            /// <list type="bullet">
            /// <item> <c> Q X1 Y1 X Y </c> ••• Absolute </item>
            /// <item> <c> q X1 Y1 X Y </c> ••• Relative </item>
            /// </list> </summary>
            public static Q QuadraticBezier(double x1, double y1, double x, double y, bool Relative)
                => new(x1, y1, x, y, Relative);

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
            public static T TrueTypeQuadraticBezier(double x1, double y1, bool Relative)
                => new(x1, y1, Relative);
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
            public static T TrueTypeQuadraticBezier(double x1, double y1, double x, double y, bool Relative)
                => new(x1, y1, x, y, Relative);
        }

        /// <summary> <c> {[G||E]||[F||D]} CX CY A1 A2 R1 R2 A3 </c> </summary>
        public struct EllipticalArc {
            /// <summary> <c> {G||E} CX CY A1 A2 R1 R2 A3 </c> </summary>
            public struct LineTo {
                /// <summary>
                /// Same as <see cref="Counterclockwise"/>, except that the arc is drawn in a <br/>
                /// clockwise direction.  <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> G CX CY A1 A2 R1 R2 A3 </c> ••• Absolute </item>
                /// <item> <c> g CX CY A1 A2 R1 R2 A3 </c> ••• Relative </item>
                /// </list> </summary>
                public static G Clockwise(double cx, double cy, double a1, double a2, double r1, double r2, double a3, bool Relative)
                    => new(cx, cy, a1, a2, r1, r2, a3, Relative);
                /// <summary>
                /// Same as <see cref="MoveTo.Counterclockwise"/>, except that there is an   <br/>
                /// implicit lineto from the current point to the start of the first arc.  <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> E CX CY A1 A2 R1 R2 A3 </c> ••• Absolute </item>
                /// <item> <c> e CX CY A1 A2 R1 R2 A3 </c> ••• Relative </item>
                /// </list> </summary>
                public static E Counterclockwise(double cx, double cy, double a1, double a2, double r1, double r2, double a3, bool Relative)
                    => new(cx, cy, a1, a2, r1, r2, a3, Relative);
            }

            /// <summary> <c> {F||D} CX CY A1 A2 R1 R2 A3 </c> </summary>
            public struct MoveTo {
                /// <summary>
                /// Same as <see cref="Counterclockwise"/>, except that the arc is drawn in a <br/>
                /// clockwise direction.  <br/><br/>
                /// Data example: <br/>
                /// <list type="bullet">
                /// <item> <c> F CX CY A1 A2 R1 R2 A3 </c> ••• Absolute </item>
                /// <item> <c> f CX CY A1 A2 R1 R2 A3 </c> ••• Relative </item>
                /// </list> </summary>
                public static F Clockwise(double cx, double cy, double a1, double a2, double r1, double r2, double a3, bool Relative)
                    => new(cx, cy, a1, a2, r1, r2, a3, Relative);
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
                public static D Counterclockwise(double cx, double cy, double a1, double a2, double r1, double r2, double a3, bool Relative)
                    => new(cx, cy, a1, a2, r1, r2, a3, Relative);
            }
        }

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
        public static J EllipticalQuadrantX(double x, double y, double angle, bool Relative)
            => new(x, y, angle, Relative);

        #endregion
    }
}