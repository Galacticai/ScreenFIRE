using System;
using c = Cairo;
using g = Gdk;

namespace ScreenFIRE.Modules.Companion.math.Vision {

    internal static class VisionCommon {


        /// <param name="byteArr">input to be converted</param>
        /// <returns><see cref="new gdk.Pixbuf(byteArr)"/></returns>
        [Obsolete("Just directly use `new gdk.Pixbuf(byteArr);`")]
        public static g.Pixbuf ByteArrayToPixbuf(this byte[] byteArr)
            => new(byteArr);


        /// <summary>  Blends the specified <see cref="g.RGBA"/> colors together. </summary>
        /// <param name="foreColor">Color to blend onto the background color.</param>
        /// <param name="backColor">Color to blend the other color onto.</param>
        /// <param name="amountFactor">
        ///	 <list type="bullet">
        ///	  <item> (0&lt;=&lt;1) How much of <paramref name="foreColor"/> to keep, “on top of” <paramref name="backColor"/>. </item>
        ///	  <item> -1 to use Alpha of <paramref name="foreColor"/> </item>
        ///	 </list>
        /// </param>
        /// <returns>The blended color.</returns>
        public static g.RGBA BlendColors(g.RGBA foreColor, g.RGBA backColor, double amountFactor = -1) {
            //if amount not set, Use  foreColor.A  [ 0 >=> 1 ]
            if (amountFactor == -1)
                amountFactor = foreColor.Alpha / 255; // convert alpha 0<=<255 to 0<=<1
                                                      //mathMisc.ForcedInRange(out amountIn255, 0, 255); //failsafe

            g.RGBA result;
            result.Alpha = backColor.Alpha;
            result.Red = (byte)((foreColor.Red * amountFactor) + backColor.Red * (1 - amountFactor));
            result.Green = (byte)((foreColor.Green * amountFactor) + backColor.Green * (1 - amountFactor));
            result.Blue = (byte)((foreColor.Blue * amountFactor) + backColor.Blue * (1 - amountFactor));
            return result;
        }

        public static g.Pixbuf InvertColors(this g.Pixbuf input) {
            //var test = input.
            return null; //! PLACEHOLDER
        }

        /// <param name = "rectangle" > Rectangle to be captured</param>
        /// <returns>Screenshot <see cref="g.Pixbuf"/> of the <paramref name="rectangle"/></returns>
        public static g.Pixbuf Screenshot(g.Rectangle rectangle)
            => new(g.Global.DefaultRootWindow, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);


        public static g.Pixbuf PixbufSetAlpha(this g.Pixbuf image, double opacity) {
            using c.ImageSurface surface = new(c.Format.Argb32, image.Width, image.Height);
            using (c.Context context = new(surface)) {
                g.CairoHelper.SetSourcePixbuf(context, image, 0, 0);
                context.PaintWithAlpha(opacity);
            }
            return new g.Pixbuf(surface.Data, true, 8, surface.Width, surface.Height, surface.Stride);
        }
    }
}

// System.Drawing
///// <param name="bitmap"> <see cref="Bitmap"/> input to be converted </param>
///// <returns> <see cref="Bitmap"/> as <see cref="byte"/>[] </returns>
//public static byte[] BitmapToByteArray(this Bitmap bitmap)
//    => (byte[])new ImageConverter().ConvertTo(bitmap, typeof(byte[]));
///// <returns>true if brightness matrix hashes are of the images are equal</returns>
//public static bool CompareImagesBritghtnessMatrix(this Image input1, Image input2)
//        => input1.GetImageHash() == input2.GetImageHash();
//private static List<bool> GetImageHash(this Image input) {
//    List<bool> result = new();
//    Bitmap bmpMin = new(input, new Size(16, 16));
//    for (int j = 0; j < bmpMin.Height; j++)
//        for (int i = 0; i < bmpMin.Width; i++)
//            result.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f); //reduce colors to true / false
//
//    return result;
//}
///// <param name="pixbuf"> Target <see cref="g.Pixbuf"/> </param>
///// <param name="saveFormat"> Target format (<see cref="ISaveFormat"/>) </param>
///// <returns> <see cref="Bitmap"/> generated from <paramref name="pixbuf"/> bytes </returns>
//public static Bitmap PixbufToBitmap(this g.Pixbuf pixbuf, ISaveFormat saveFormat) {
//    return (Bitmap)TypeDescriptor.GetConverter(typeof(Bitmap))
//                .ConvertFrom(pixbuf.SaveToBuffer(saveFormat.ToString()));
//}
//[Obsolete]
//public static Bitmap Gradient(Size size, Color color1, Color color2, Color color3) {
//    Image topHalf = Gradient(size, color1, color2);
//    Image bottomHalf = Gradient(size, color2, color3);
//    TextureBrush topG = new(topHalf);
//    TextureBrush bottomG = new(bottomHalf);
//    //  https://stackoverflow.com/questions/465172/merging-two-images-in-c-net
//    return null; //! PLACEHOLDER
//
//}
//
///// <summary> Create a gradient from <paramref name="color1"/> to <paramref name="color2"/> </summary>
///// <param name="size"> Size of the <see cref="Bitmap"/> </param>
///// <param name="color1"> Initial color </param>
///// <param name="color2"> Destination color </param>
///// <returns> <see cref="Bitmap"/> image of the result gradient </returns>
//public static Bitmap Gradient(Size size, Color color1, Color color2) {
//    using Bitmap bitmap = new(size.Width, size.Height);
//    using Graphics graphics = Graphics.FromImage(bitmap);
//    using LinearGradientBrush brush = new(
//                new Rectangle(new Point(0, 0), size),
//                color1, color2,
//                LinearGradientMode.Vertical);
//    brush.SetSigmaBellShape(.5f);
//    graphics.FillRectangle(brush, new Rectangle(new Point(0, 0), size));
//    return bitmap;
//}
///// <summary> Change the opacity of an image </summary>
///// <param name="input"><see cref="Image"/> to process</param>
///// <param name="opacity">new opacity value</param>
///// <returns>Modified version of <paramref name="input"/> treated with the new <paramref name="opacity"/></returns>
//public static Image SetOpacity(this Image input, float opacity) {
//    Image result = new Bitmap(input.Width, input.Height);
//    Graphics g = Graphics.FromImage(result);
//    ColorMatrix colormatrix = new();
//    colormatrix.Matrix33 = opacity;
//    ImageAttributes imgAttribute = new();
//    imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
//    g.DrawImage(input,
//                new Rectangle(0, 0, result.Width, result.Height),
//                0, 0, input.Width, input.Height,
//                GraphicsUnit.Pixel, imgAttribute);
//    g.Dispose();
//    return result;
//}