
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ScreenFire.Modules.Companion.math;

/// <summary> Visual math </summary>
internal class Vision {

    /// <summary>  Blends the specified colors together. </summary>
    /// <param name="foreColor">Color to blend onto the background color.</param>
    /// <param name="backColor">Color to blend the other color onto.</param>
    /// <param name="amount"> 
    ///  <list type="bullet"> <item>(0<=<1) How much of <paramref name="foreColor"/> to keep, “on top of” <paramref name="backColor"/>.</item>
    /// <item>-1 to use Alpha of <paramref name="foreColor"/> </item>
    /// </list> </param>
    /// <returns>The blended color.</returns>
    public static Color BlendColors(Color foreColor, Color backColor, float amount = -1) {
        //if amount not set, Use  foreColor.A  [ 0 >=> 1 ]
        if (amount == -1)
            amount = foreColor.A / 255; // convert alpha 0<=<255 to 0<=<1

        byte R = (byte)((foreColor.R * amount) + backColor.R * (1 - amount)),
             G = (byte)((foreColor.G * amount) + backColor.G * (1 - amount)),
             B = (byte)((foreColor.B * amount) + backColor.B * (1 - amount));
        return Color.FromArgb(R, G, B);
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

    public static Image Screenshot(Rectangle rect) {
        Bitmap bmp = new(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
        Graphics.FromImage(bmp).
            CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
        return bmp;
    }

    public static Bitmap Gradient(Size size, Color color1, Color color2, Color color3) {
        Image topHalf = Gradient(size, color1, color2);
        Image bottomHalf = Gradient(size, color2, color3);
        TextureBrush topG = new(topHalf);
        TextureBrush bottomG = new(bottomHalf);
        //  https://stackoverflow.com/questions/465172/merging-two-images-in-c-net
        return null;

    }
    public static Bitmap Gradient(Size size, Color color1, Color color2) {
        using (Bitmap bitmap = new(size.Width, size.Height))
        using (Graphics graphics = Graphics.FromImage(bitmap))
        using (LinearGradientBrush brush = new(
                    new Rectangle(new Point(0, 0), size),
                    color1, color2,
                    LinearGradientMode.Vertical)) {
            brush.SetSigmaBellShape(.5f);
            graphics.FillRectangle(brush, new Rectangle(new Point(0, 0), size));
            return bitmap;
        }
    }

    public static Bitmap ChangeOpacity(Image img, float opacityvalue) {
        Bitmap bmp = new Bitmap(img.Width, img.Height);
        Graphics graphics__1 = Graphics.FromImage(bmp);
        ColorMatrix colormatrix = new();
        colormatrix.Matrix33 = opacityvalue;
        ImageAttributes imgAttribute = new();
        imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        graphics__1.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
        graphics__1.Dispose();
        return bmp;
    }


    /// <summary> Shapes and stuff math </summary> 
    public struct Geometry {
        /// <param name="boundsRect">Original rectangle to convert</param>
        /// <param name="radius">Radius of rounded corners</param>
        /// <returns>Rounded rectangle as <see cref="GraphicsPath"/></returns>
        public static GraphicsPath RoundedRect(Rectangle boundsRect, int radius) {
            // Bound radius by half the height and width of boundsRect  
            radius = (int)mathMisc.ForcedInRange(radius, 0, boundsRect.Height / 2);
            radius = (int)mathMisc.ForcedInRange(radius, 0, boundsRect.Width / 2);

            int diameter = radius * 2;
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
        /// <param name="g"></param>
        /// <param name="brush"></param>
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
        /// <param name="g"></param>
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


