using Aspose.Svg;
using System;
using System.Collections.Generic;
using g = Gdk;
using paths = Aspose.Svg.Paths;
namespace ScreenFIRE.Modules.Companion.math.Vision {
    public static class SVG {

        private const string NamespaceURI = "http://www.w3.org/2000/svg";

        /// <summary> Create an equivelant <see cref="SVGPathElement"/> that is at 0,0 origin <br/>
        /// (Remove any extra offset)</summary>
        /// <param name="pathElement"> Target <see cref="SVGPathElement"/> </param>
        /// <returns> Equivelant <see cref="SVGPathElement"/> at 0,0 origin </returns>
        public static SVGPathElement GetPathAt00(this SVGPathElement pathElement) {
            var (offsetX, offsetY) = pathElement.GetPathXYoffset();

            foreach (var pathSeg in new List<paths.SVGPathSeg>(pathElement.PathSegList)) {
                //? M
                if (pathSeg is paths.SVGPathSegMovetoAbs) {
                    var seg = (paths.SVGPathSegMovetoAbs)pathSeg;
                    seg.X -= offsetX;
                    seg.Y -= offsetY;
                }
                //? L
                else if (pathSeg is paths.SVGPathSegLinetoAbs) {
                    var seg = (paths.SVGPathSegLinetoAbs)pathSeg;
                    seg.X -= offsetX;
                    seg.Y -= offsetY;
                }
                //? H
                else if (pathSeg is paths.SVGPathSegLinetoHorizontalAbs) {
                    var seg = (paths.SVGPathSegLinetoHorizontalAbs)pathSeg;
                    seg.X -= offsetX;
                }
                //? V
                else if (pathSeg is paths.SVGPathSegLinetoVerticalAbs) {
                    var seg = (paths.SVGPathSegLinetoVerticalAbs)pathSeg;
                    seg.Y -= offsetY;
                }
                //? C
                else if (pathSeg is paths.SVGPathSegCurvetoCubicAbs) {
                    var seg = (paths.SVGPathSegCurvetoCubicAbs)pathSeg;
                    seg.X -= offsetX;
                    seg.X1 -= offsetX;
                    seg.Y -= offsetY;
                    seg.Y1 -= offsetY;
                }
                //? Q
                else if (pathSeg is paths.SVGPathSegCurvetoQuadraticAbs) {
                    var seg = (paths.SVGPathSegCurvetoQuadraticAbs)pathSeg;
                    seg.X -= offsetX;
                    seg.X1 -= offsetX;
                    seg.Y -= offsetY;
                    seg.Y1 -= offsetY;
                }
                //? S
                else if (pathSeg is paths.SVGPathSegCurvetoCubicSmoothAbs) {
                    var seg = (paths.SVGPathSegCurvetoCubicSmoothAbs)pathSeg;
                    seg.X -= offsetX;
                    seg.X2 -= offsetX;
                    seg.Y -= offsetY;
                    seg.Y2 -= offsetY;
                }
                //? T
                else if (pathSeg is paths.SVGPathSegCurvetoQuadraticSmoothAbs) {
                    var seg = (paths.SVGPathSegCurvetoQuadraticSmoothAbs)pathSeg;
                    seg.X -= offsetX;
                    seg.Y -= offsetY;
                }
                //? A
                else if (pathSeg is paths.SVGPathSegArcAbs) {
                    var seg = (paths.SVGPathSegArcAbs)pathSeg;
                    seg.X -= offsetX;
                    seg.Y -= offsetY;
                }
            }
            return pathElement;
        }

        /// <summary> Calculate the amount of offset from 0,0 (origin) </summary>
        /// <param name="pathElement"> Target <see cref="SVGPathElement"/> </param>
        /// <returns> (<see cref="float"/> x, <see cref="float"/> y) amount of offset of this <see cref="SVGPathElement"/> </returns>
        internal static (float x, float y) GetPathXYoffset(this SVGPathElement pathElement) {
            float offsetX = 0, offsetY = 0;

            foreach (paths.SVGPathSeg pathSeg in pathElement.PathSegList) {
                //? M
                if (pathSeg is paths.SVGPathSegMovetoAbs) {
                    var seg = (paths.SVGPathSegMovetoAbs)pathSeg;
                    offsetX = Math.Min(offsetX, seg.X);
                    offsetY = Math.Max(offsetY, seg.Y);
                } else
                //? L
                if (pathSeg is paths.SVGPathSegLinetoAbs) {
                    var seg = (paths.SVGPathSegLinetoAbs)pathSeg;
                    offsetX = Math.Min(offsetX, seg.X);
                    offsetY = Math.Max(offsetY, seg.Y);
                } else
                //? H
                if (pathSeg is paths.SVGPathSegLinetoHorizontalAbs) {
                    var seg = (paths.SVGPathSegLinetoHorizontalAbs)pathSeg;
                    offsetX = Math.Min(offsetX, seg.X);
                } else
                //? V
                if (pathSeg is paths.SVGPathSegLinetoVerticalAbs) {
                    var seg = (paths.SVGPathSegLinetoVerticalAbs)pathSeg;
                    offsetY = Math.Max(offsetY, seg.Y);
                } else
                //? C
                if (pathSeg is paths.SVGPathSegCurvetoCubicAbs) {
                    var seg = (paths.SVGPathSegCurvetoCubicAbs)pathSeg;
                    offsetX = Math.Min(offsetX, seg.X1);
                    offsetX = Math.Min(offsetX, seg.X);
                    offsetY = Math.Max(offsetY, seg.Y1);
                    offsetY = Math.Max(offsetY, seg.Y);
                } else
                //? Q
                if (pathSeg is paths.SVGPathSegCurvetoQuadraticAbs) {
                    var seg = (paths.SVGPathSegCurvetoQuadraticAbs)pathSeg;
                    offsetX = Math.Min(offsetX, seg.X1);
                    offsetX = Math.Min(offsetX, seg.X);
                    offsetY = Math.Max(offsetY, seg.Y1);
                    offsetY = Math.Max(offsetY, seg.Y);
                } else
                //? S
                if (pathSeg is paths.SVGPathSegCurvetoCubicSmoothAbs) {
                    var seg = (paths.SVGPathSegCurvetoCubicSmoothAbs)pathSeg;
                    offsetX = Math.Min(offsetX, seg.X2);
                    offsetX = Math.Min(offsetX, seg.X);
                    offsetY = Math.Max(offsetY, seg.Y2);
                    offsetY = Math.Max(offsetY, seg.Y);
                } else
                //? T
                if (pathSeg is paths.SVGPathSegCurvetoQuadraticSmoothAbs) {
                    var seg = (paths.SVGPathSegCurvetoQuadraticSmoothAbs)pathSeg;
                    offsetX = Math.Min(offsetX, seg.X);
                    offsetY = Math.Max(offsetY, seg.Y);
                } else
                //? A
                if (pathSeg is paths.SVGPathSegArcAbs) {
                    var seg = (paths.SVGPathSegArcAbs)pathSeg;
                    offsetX = Math.Min(offsetX, seg.X);
                    offsetY = Math.Max(offsetY, seg.Y);
                }
            }

            return (offsetX, offsetY);
        }

        public static SVGDocument ParseSVGdoc(string d, (int Width, int Height) size, g.RGBA color)
            => ParseSVGdoc(("d", d),
                           ("fill", color.ToHexString()),
                           ("Width", size.Width.ToString()),
                           ("Height", size.Height.ToString()));

        public static SVGDocument ParseSVGdoc(params (string property, string value)[] Attributes) {
            using SVGDocument svgDoc = new();
            SVGSVGElement svgElement = svgDoc.RootElement;
            SVGPathElement pathElement = svgDoc.CreateElementNS(NamespaceURI, "path") as SVGPathElement;

            foreach (var (property, value) in Attributes)
                pathElement.SetAttribute(property, value);

            pathElement = pathElement.GetPathAt00();
            svgElement.InsertBefore(pathElement, svgElement.FirstChild);
            return svgDoc;
        }
    }
}