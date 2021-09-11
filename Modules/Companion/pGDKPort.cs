using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using g = Gdk;

namespace pGDKPort  {
    //
    // Summary:
    //     ScreenshotCapture
    public static class ScreenshotCapture {
        //
        // Summary:
        //     Capture screenshot to Image object
        //
        // Parameters:
        //   onlyPrimaryScreen:
        //     Create screen only from primary screen
        public static Image TakeScreenshot(bool onlyPrimaryScreen = false) {
            try {
                return OsXCapture(onlyPrimaryScreen);
            } catch (Exception) {
                return WindowsCapture(onlyPrimaryScreen);
            }
        }

        private static Image OsXCapture(bool onlyPrimaryScreen) {
            return CombineBitmap(
                ExecuteCaptureProcess("screencapture",
                                      string.Format("{0} -T0 -tpng -S -x",
                                      onlyPrimaryScreen ? "-m" : ""),
                                      onlyPrimaryScreen ? 1 : 3));
        }

        //
        // Summary:
        //     Start execute process with parameters
        //
        // Parameters:
        //   execModule:
        //     Application name
        //
        //   parameters:
        //     Command line parameters
        //
        //   screensCounter:
        //
        // Returns:
        //     Bytes for destination image
        private static Image[] ExecuteCaptureProcess(string execModule, string parameters, int screensCounter) {
            List<string> list = new List<string>();
            for (int i = 0; i < screensCounter; i++) {
                list.Add(Path.Combine(Path.GetTempPath(), $"screenshot_{Guid.NewGuid()}.jpg"));
            }

            (Process.Start(execModule,
                           string.Format("{0} {1}",
                                         parameters,
                                         list.Aggregate((string x, string y) => x + " " + y)))
            ?? throw new InvalidOperationException($"Executable of '{execModule}' was not found"))
            .WaitForExit();

            for (int num = list.Count - 1; num >= 0; num--)
                if (!File.Exists(list[num]))
                    list.Remove(list[num]);

            try {
                return list.Select(new Func<string, Image>(Image.FromFile)).ToArray();
            } finally {
                list.ForEach(new Action<string>(File.Delete));
            }
        }

        //
        // Summary:
        //     Capture screenshot with .NET standard implementation
        //
        // Parameters:
        //   onlyPrimaryScreen:
        //
        // Returns:
        //     Return bytes of screenshot image
        private static Image WindowsCapture(bool onlyPrimaryScreen) {
            if (onlyPrimaryScreen) {
                return ScreenCapture(g.Display.Default.PrimaryMonitor);
            }
            int Count = g.Display.Default.NMonitors;
            g.Monitor[] gMonitor = new g.Monitor[Count];  // outside || Prevent redeclaring every loop  
            for (int i = 0; i < Count; i++)
                gMonitor[i] = g.Display.Default.GetMonitor(i);

            return CombineBitmap((from s in gMonitor
                                  orderby s.Geometry.Left
                                  select s).Select(new Func<g.Monitor, Bitmap>(ScreenCapture)).ToArray());
        }

        //
        // Summary:
        //     Create screenshot of single display
        //
        // Parameters:
        //   screen:
        private static Bitmap ScreenCapture(g.Monitor screen) {
            g.Rectangle rectangle = screen.Geometry;
            if (screen.Geometry.Width / screen.Workarea.Width > 1 || screen.Geometry.Height / screen.Workarea.Height > 1) {
                rectangle = new g.Rectangle(0, 0, screen.Workarea.Width + screen.Workarea.X, screen.Workarea.Height + screen.Workarea.Y);
            }

            PixelFormat pixelFormat = new Bitmap(1, 1, Graphics.FromHwnd(IntPtr.Zero)).PixelFormat;
            Bitmap bitmap = new(rectangle.Width, rectangle.Height, pixelFormat);

            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, new(rectangle.Width, rectangle.Height), CopyPixelOperation.SourceCopy);
            return bitmap;
        }

        //
        // Summary:
        //     Combime images collection in one bitmap
        //
        // Parameters:
        //   images:
        //
        // Returns:
        //     Combined image
        private static Image CombineBitmap(ICollection<Image> images) {
            if (images.Count == 1) {
                return images.First();
            }

            Image image = null;
            try {
                int num = 0;
                int num2 = 0;
                foreach (Image image2 in images) {
                    num += image2.Width;
                    num2 = ((image2.Height > num2) ? image2.Height : num2);
                }

                image = new Bitmap(num, num2);
                using (Graphics graphics = Graphics.FromImage(image)) {
                    graphics.Clear(Color.Black);
                    int num3 = 0;
                    foreach (Image image3 in images) {
                        graphics.DrawImage(image3, new Rectangle(num3, 0, image3.Width, image3.Height));
                        num3 += image3.Width;
                    }

                    return image;
                }
            } catch (Exception ex) {
                image?.Dispose();
                throw ex; //!? Rethrowing cought exception changes stack info!
            } finally {
                foreach (Image image4 in images) {
                    image4.Dispose();
                }
            }
        }
    }
}