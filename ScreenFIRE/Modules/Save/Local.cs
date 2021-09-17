using GLib;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.math;
using System.IO;
using gtk = Gtk;

namespace ScreenFIRE.Modules.Save {

    partial class Save {

        private static Gdk.Pixbuf img(Gdk.Rectangle rect) => Vision.Screenshot(rect);


        /// <summary> ••• GUI ••• <br/>
        /// Save a <see cref="Screenshot"/> locally </summary>
        public static void Local(Screenshot screenshot, ISaveFormat saveFormat = ISaveFormat.png) {


            gtk.FileChooserDialog save = new("Save As", null, gtk.FileChooserAction.Save);
            save.AddButton(gtk.Stock.Cancel, gtk.ResponseType.Cancel);
            save.AddButton(gtk.Stock.Save, gtk.ResponseType.Ok);
            save.DefaultResponse = gtk.ResponseType.Ok;
            save.SelectMultiple = false;
            save.SetCurrentFolder(Common.SF);

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
                    warn.Add(new gtk.Label(save.Filename + $"already exists.{Common.n}Do you want to replace the existing file?"));

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

            }
            save.Destroy();
        }

        /// <summary> ••• Specific ••• <br/>
        /// Save a <see cref="Screenshot"/> locally </summary>
        public static void Local(Screenshot screenshot, IFile file, ISaveFormat saveFormat = ISaveFormat.png) {

            file.MakeDirectory(new()); //! NOT DONE


            Gdk.Pixbuf ss = Vision.Screenshot(screenshot.ImageRectangle);

            ss.Save(
                        //! Folder + File - extension
                        Path.Combine(
                            file.Parent.ParsedName,
                            Path.GetFileNameWithoutExtension(file.Basename) ?? "ScreenFIRE")

                        //! _yyMMdd-HHmmff
                        + (replacing ? ($"_{screenshot.Time:yyMMdd-HHmmff}") : string.Empty)

                        //! .extension
                        + $".{saveFormat}",

                        $"{saveFormat}");
        }

        /// <summary> ••• AUTO ••• <br/>
        /// Save a <see cref="Screenshot"/> locally </summary>
        public static void Local_Auto(Screenshot screenshot, ISaveFormat saveFormat = ISaveFormat.png) {
            //! Local(screenshot, ("", "")); //! PLACEHOLDER
        }
    }
}
