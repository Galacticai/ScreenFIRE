using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.math;
using System.Drawing;
using System.IO;
using gtk = Gtk;

namespace ScreenFIRE.Modules.Save {

    partial class Save {

        /// <summary> ••• AUTO ••• <br/>
        /// Save a <see cref="Screenshot"/> locally </summary>
        public static void Local_Auto(Screenshot screenshot, ISaveFormat saveFormat = ISaveFormat.png) {
            Local(screenshot, ("", "")); //! PLACEHOLDER
        }

        /// <summary> ••• Specific ••• <br/>
        /// Save a <see cref="Screenshot"/> locally </summary>
        public static void Local(Screenshot screenshot, (string folder, string file) name, ISaveFormat saveFormat = ISaveFormat.png) {
            if (!Directory.Exists(name.folder)) ; //! NOT DONE
        }

        /// <summary> ••• GUI ••• <br/>
        /// Save a <see cref="Screenshot"/> locally </summary>
        public static void Local(Screenshot screenshot, ISaveFormat saveFormat = ISaveFormat.png) {
            Image ss = Vision.Screenshot(screenshot.ImageRectangle);

            gtk.FileChooserDialog save = new("Save As", null, gtk.FileChooserAction.Save);
            save.AddButton(gtk.Stock.Cancel, gtk.ResponseType.Cancel);
            save.AddButton(gtk.Stock.Save, gtk.ResponseType.Ok);
            save.DefaultResponse = gtk.ResponseType.Ok;
            save.SelectMultiple = false;
            save.SetCurrentFolder(c.SF);

            gtk.ResponseType response = (gtk.ResponseType)save.Run();


            bool replacing = false;
            if (response == gtk.ResponseType.Ok
               & Directory.Exists(save.CurrentFolder)
               & ss != null) {
                if (save.File.Exists) {

                    gtk.MessageDialog warn = new(null, gtk.DialogFlags.DestroyWithParent, gtk.MessageType.Warning, gtk.ButtonsType.YesNo, "File already exists.");
                    warn.Resize(100, 200);
                    warn.Resizable = false;
                    warn.DefaultResponse = gtk.ResponseType.No;
                    //warn.AddButton(gtk.Stock.Cancel, gtk.ResponseType.Cancel);
                    //warn.AddButton(gtk.Stock.No, gtk.ResponseType.No);
                    //warn.AddButton(gtk.Stock.Yes, gtk.ResponseType.Yes); 

                    //? not working
                    warn.Add(new gtk.Label(save.Filename + $"already exists.{c.n}Do you want to replace the existing file?"));

                    gtk.ResponseType warnResponse = (gtk.ResponseType)warn.Run();
                    if (warnResponse == gtk.ResponseType.No) { // Don't replace   
                        replacing = true;
                        warn.Destroy();
                    } else if (warnResponse == gtk.ResponseType.Cancel) {
                        warn.Destroy();
                        save.Destroy();
                        return;
                    } // Cancel
                }
                ss.Save(Path.Combine(
                            Path.GetDirectoryName(save.Filename),
                            Path.GetFileNameWithoutExtension(save.File.Basename)
                        )
                        + (replacing ? ("-" + screenshot.UID.ToString()[..6]) : string.Empty)
                        + "." + saveFormat.ToString(),
                        SaveFormat.ToImageFormat(saveFormat));
            }
            save.Destroy();
        }
    }
}