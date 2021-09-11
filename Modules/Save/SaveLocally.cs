using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion.math;
using System.Drawing;
using System.IO;
using gtk = Gtk;

namespace ScreenFIRE.Modules.Save {

    partial class Save {

        /// <summary> ••• AUTO ••• <br/>
        /// Save a <see cref="Screenshot"/> locally </summary>
        public static void Local_Auto(Screenshot screenshot,  ISaveFormat saveFormat =  ISaveFormat.png) {
            Local(screenshot, ("", ""));
        }

        /// <summary> ••• Specific ••• <br/>
        /// Save a <see cref="Screenshot"/> locally </summary>
        public static void Local(Screenshot screenshot, (string folder, string file) name,  ISaveFormat saveFormat =  ISaveFormat.png) {
            if (!Directory.Exists(name.folder)) ;
        }

        /// <summary> ••• GUI ••• <br/>
        /// Save a <see cref="Screenshot"/> locally </summary>
        public static void Local(Screenshot screenshot,  ISaveFormat saveFormat =  ISaveFormat.png) {
            Image ss;
            ss = Vision.Screenshot(screenshot.ImageRectangle);

            gtk.FileChooserDialog fcd = new("Save As", null, gtk.FileChooserAction.Save);
            fcd.AddButton(gtk.Stock.Cancel, gtk.ResponseType.Cancel);
            fcd.AddButton(gtk.Stock.Save, gtk.ResponseType.Ok);
            fcd.DefaultResponse = gtk.ResponseType.Ok;
            fcd.SelectMultiple = false;

            gtk.ResponseType response = (gtk.ResponseType)fcd.Run();

            if (response == gtk.ResponseType.Ok
               & Directory.Exists(fcd.CurrentFolder)
               & ss != null)
                ss.Save(fcd.Filename +
                            (fcd.Filename.ToLower()
                                [(fcd.Filename.ToLower().Length - 4)..(fcd.Filename.ToLower().Length)]
                                    == $".{saveFormat.ToString().ToLower()}"
                                ? "" : $".{saveFormat.ToString().ToLower()}"),
                        SaveFormat.ToImageFormat(saveFormat));

            fcd.Destroy();

        }
    }
}