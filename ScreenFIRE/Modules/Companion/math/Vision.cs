using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using gdk = Gdk;

namespace ScreenFIRE.Modules.Companion.math {

    /// <summary> Visual math </summary>
    internal class Vision {

        /// <param name="byteArr">input to be converted</param>
        /// <returns><see cref="new gdk.Pixbuf(byteArr)"/></returns>
        [Obsolete("Just directly use `new gdk.Pixbuf(byteArr);`")]
        public static gdk.Pixbuf ByteArrayToPixbuf(byte[] byteArr)
            => new(byteArr);

        /// <param name="bitmap"> <see cref="Bitmap"/> input to be converted </param>
        /// <returns> <see cref="Bitmap"/> as <see cref="byte"/>[] </returns>
        public static byte[] BitmapToByteArray(Bitmap bitmap)
            => (byte[])new ImageConverter().ConvertTo(bitmap, typeof(byte[]));

        /// <summary>  Blends the specified <see cref="gdk.RGBA"/> colors together. </summary>
        /// <param name="foreColor">Color to blend onto the background color.</param>
        /// <param name="backColor">Color to blend the other color onto.</param>
        /// <param name="amountIn255">
        ///	 <list type="bullet">
        ///	  <item> (0&lt;=&lt;1) How much of <paramref name="foreColor"/> to keep, “on top of” <paramref name="backColor"/>. </item>
        ///	  <item> -1 to use Alpha of <paramref name="foreColor"/> </item>
        ///	 </list>
        /// </param>
        /// <returns>The blended color.</returns>
        public static gdk.RGBA BlendColors(gdk.RGBA foreColor, gdk.RGBA backColor, double amountFactor = -1) {
            //if amount not set, Use  foreColor.A  [ 0 >=> 1 ]
            if (amountFactor == -1)
                amountFactor = foreColor.Alpha / 255; // convert alpha 0<=<255 to 0<=<1
            //mathMisc.ForcedInRange(out amountIn255, 0, 255); //failsafe

            gdk.RGBA result;
            result.Alpha = backColor.Alpha;
            result.Red = (byte)((foreColor.Red * amountFactor) + backColor.Red * (1 - amountFactor));
            result.Green = (byte)((foreColor.Green * amountFactor) + backColor.Green * (1 - amountFactor));
            result.Blue = (byte)((foreColor.Blue * amountFactor) + backColor.Blue * (1 - amountFactor));
            return result;
        }

        public static gdk.Pixbuf InvertColors(gdk.Pixbuf input) {
            //var test = input.
            return null; //! PLACEHOLDER
        }


        /// <returns>true if brightness matrix hashes are of the images are equal</returns>
        public static bool CompareImagesBritghtnessMatrix(Image input1, Image input2)
                => GetImageHash(input1) == GetImageHash(input2);
        private static List<bool> GetImageHash(Image input) {
            List<bool> result = new();
            Bitmap bmpMin = new(input, new Size(16, 16));
            for (int j = 0; j < bmpMin.Height; j++)
                for (int i = 0; i < bmpMin.Width; i++)
                    result.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f); //reduce colors to true / false

            return result;
        }


        /// <param name = "rect" > Rectangle to be captured</param>
        /// <returns>Screenshot<see cref="Image"/> of the<paramref name="rect"/></returns>
        public static gdk.Pixbuf Screenshot(gdk.Rectangle rect)
            => new(gdk.Global.DefaultRootWindow, rect.X, rect.Y, rect.Width, rect.Height);


        [Obsolete]
        public static Bitmap Gradient(Size size, Color color1, Color color2, Color color3) {
            Image topHalf = Gradient(size, color1, color2);
            Image bottomHalf = Gradient(size, color2, color3);
            TextureBrush topG = new(topHalf);
            TextureBrush bottomG = new(bottomHalf);
            //  https://stackoverflow.com/questions/465172/merging-two-images-in-c-net
            return null; //! PLACEHOLDER

        }

        /// <summary> Create a gradient from <paramref name="color1"/> to <paramref name="color2"/> </summary>
        /// <param name="size"> Size of the <see cref="Bitmap"/> </param>
        /// <param name="color1"> Initial color </param>
        /// <param name="color2"> Destination color </param>
        /// <returns> <see cref="Bitmap"/> image of the result gradient </returns>
        public static Bitmap Gradient(Size size, Color color1, Color color2) {
            using Bitmap bitmap = new(size.Width, size.Height);
            using Graphics graphics = Graphics.FromImage(bitmap);
            using LinearGradientBrush brush = new(
                        new Rectangle(new Point(0, 0), size),
                        color1, color2,
                        LinearGradientMode.Vertical);
            brush.SetSigmaBellShape(.5f);
            graphics.FillRectangle(brush, new Rectangle(new Point(0, 0), size));
            return bitmap;
        }

        /// <summary> Change the opacity of an image </summary>
        /// <param name="input"><see cref="Image"/> to process</param>
        /// <param name="opacity">new opacity value</param>
        /// <returns>Modified version of <paramref name="input"/> treated with the new <paramref name="opacity"/></returns>
        public static Image SetOpacity(Image input, float opacity) {
            Image result = new Bitmap(input.Width, input.Height);
            Graphics g = Graphics.FromImage(result);
            ColorMatrix colormatrix = new();
            colormatrix.Matrix33 = opacity;
            ImageAttributes imgAttribute = new();
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            g.DrawImage(input,
                        new Rectangle(0, 0, result.Width, result.Height),
                        0, 0, input.Width, input.Height,
                        GraphicsUnit.Pixel, imgAttribute);
            g.Dispose();
            return result;
        }


        /// <summary> Shapes and stuff math </summary>
        public struct Geometry {

            /// <summary> Find the bounding rectangle of several rectangles </summary>
            /// <param name="rectangles">Rectangles to process</param>
            /// <returns><see cref="gdk.Rectangle"/> which contains all <paramref name="rectangles"/>[]</returns>
            public static gdk.Rectangle BoundingRectangle(gdk.Rectangle[] rectangles) {
                int xMin = rectangles.Min(s => s.X),
                    yMin = rectangles.Min(s => s.Y),
                    xMax = rectangles.Max(s => s.X + s.Width),
                    yMax = rectangles.Max(s => s.Y + s.Height);
                return new gdk.Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);
            }

            /// <summary> Convert 2 points to a rectangle </summary>
            /// <returns> <see cref="gdk.Rectangle"/> built from <paramref name="point1"/> and <paramref name="point2"/> </returns>
            public static gdk.Rectangle PointsToRectangle(gdk.Point point1, gdk.Point point2) {
                int left = Math.Min(point1.X, point2.X),
                    right = Math.Max(point1.X, point2.X),
                    top = Math.Min(point1.Y, point2.Y),
                    bottom = Math.Max(point1.Y, point2.Y),

                    width = right - left,
                    height = bottom - top;

                return new gdk.Rectangle(left, top, width, height);
            }


            /// <param name="boundsRect">Original rectangle to convert</param>
            /// <param name="radius">Radius of rounded corners</param>
            /// <returns>Rounded rectangle as <see cref="GraphicsPath"/></returns>
            public static GraphicsPath RoundedRect(Rectangle boundsRect, double radius) {
                // Bound radius by half the height and width of boundsRect
                mathMisc.ForcedInRange(ref radius, 0, boundsRect.Height / 2);
                mathMisc.ForcedInRange(ref radius, 0, boundsRect.Width / 2);

                int diameter = (int)radius * 2;
                Rectangle arc = new(boundsRect.Location, new Size(diameter, diameter));
                GraphicsPath path = new();
                path.StartFigure();
                if (radius == 0) {
                    path.AddRectangle(boundsRect);
                    return path;
                }
                // top left arc
                path.AddArc(arc, 180, 90);
                // top right arc
                arc.X = boundsRect.Right - diameter;
                path.AddArc(arc, 270, 90);
                // bottom right arc
                arc.Y = boundsRect.Bottom - diameter;
                path.AddArc(arc, 0, 90);
                // bottom left arc
                arc.X = boundsRect.Left;
                path.AddArc(arc, 90, 90);

                path.CloseFigure();
                return path;
            }

            /// <summary>
            /// Draws a circle (outline) using <see cref="Graphics"/> <paramref name="g"/> and <see cref="Brush"/> <paramref name="brush"/>
            /// </summary>
            /// <param name="g"> Target <see cref="Graphics"/> </param>
            /// <param name="pen"> Target <see cref="Pen"/> </param>
            /// <param name="centerX">X of center point</param>
            /// <param name="centerY">Y of center point</param>
            /// <param name="radius">radius of the circle</param>
            public static void DrawCircle(Graphics g, Pen pen, float centerX, float centerY, float radius) {
                g.DrawEllipse(pen, centerX - radius, centerY - radius,
                              radius + radius, radius + radius);
            }
            /// <summary>
            /// Draws a filled circle using <see cref="Graphics"/> <paramref name="g"/> and <see cref="Brush"/> <paramref name="brush"/>
            /// </summary>
            /// <param name="g"> Target <see cref="Graphics"/> </param>
            /// <param name="brush"></param>
            /// <param name="centerX">X of center point</param>
            /// <param name="centerY">Y of center point</param>
            /// <param name="radius">radius of the circle</param>
            public static void FillCircle(Graphics g, Brush brush, float centerX, float centerY, float radius) {
                g.FillEllipse(brush, centerX - radius, centerY - radius,
                              radius + radius, radius + radius);
            }
            /// <param name="centerX">X of center point</param>
            /// <param name="centerY">Y of center point</param>
            /// <param name="radius">radius of the circle</param>
            /// <returns>Circle path as <see cref="GraphicsPath"/></returns>
            public static GraphicsPath Circle(float centerX, float centerY, float radius) {
                GraphicsPath result = new();
                result.AddEllipse(centerX - radius,
                                  centerY - radius,
                                  radius + radius,
                                  radius + radius);
                return result;
            }

            /// <param name="boundsRect">Parent rectangle</param>
            /// <returns>Circle path as <see cref="GraphicsPath"/> bounded by <paramref name="boundsRect"/></returns>
            public static GraphicsPath EllipseInRect(Rectangle boundsRect) {
                GraphicsPath result = new();
                result.AddEllipse(boundsRect);
                return result;
            }
        }
    }
}