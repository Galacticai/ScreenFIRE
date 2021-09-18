using Gtk;
using ScreenFIRE.Assets;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using System.IO;

namespace ScreenFIRE.Modules.Save {


    partial class Save {

        /// <summary> ••• GUI ••• Save a <see cref="Screenshot"/> locally </summary>
        public static bool Local(Screenshot screenshot, Window parentWindow) {

            FileChooserDialog save = new(Strings.Fetch(IStrings.SaveAs___), null, FileChooserAction.Save);
            save.AddButton(Stock.Cancel, ResponseType.Cancel);
            save.AddButton(Stock.Save, ResponseType.Ok);
            save.DefaultResponse = ResponseType.Ok;
            save.SelectMultiple = false;
            save.SetCurrentFolder(Common.SF);

            ResponseType response = (ResponseType)save.Run();


            if (response == ResponseType.Ok
               & Directory.Exists(save.CurrentFolder)) {
                if (save.File.Exists) {
                    string[] warnText
                        = Strings.Fetch(IStrings.FileAlreadyExists_, IStrings.WouldYouLikeToReplaceTheExistingFile_);
                    MessageDialog warn
                        = new(parentWindow, DialogFlags.DestroyWithParent,
                              MessageType.Warning, ButtonsType.YesNo,
                              warnText[0] + Common.nn + warnText[1]);
                    warn.Resize(100, 250);
                    warn.Resizable = false;
                    warn.DefaultResponse = ResponseType.No;

                    if (!Local(screenshot, save.Filename, warnDialog: warn))
                        save.Destroy();
                }
            } else {
                save.Destroy();
                return false;
            }
            save.Destroy();
            return true;
        }


        /// <summary> ••• AUTO ••• Save a <see cref="Screenshot"/> locally </summary>
        public static bool Local_Auto(Screenshot screenshot, ISaveFormat saveFormat = ISaveFormat.png) {
            //! Local(screenshot, ); //! PLACEHOLDER
            return false;
        }


        /// <summary> ••• Specific ••• Save a <see cref="Screenshot"/> locally </summary>
        /// <returns> false if cancelled of failed to save</returns>
        public static bool Local(Screenshot screenshot,
                                 string path,
                                 bool replaceExisting = false,
                                 ISaveFormat saveFormat = ISaveFormat.png,
                                 MessageDialog warnDialog = null) {


            bool replacing = File.Exists(path);
            if (warnDialog != null) {
                ResponseType warnResponse = (ResponseType)warnDialog.Run();
                if (warnResponse == ResponseType.No)   // Don't replace   
                    replacing = true;

                warnDialog.Destroy(); //return false; //Cancel signal } // Cancel
            }

            screenshot.Image.Save(
                        //! Folder + File - extension
                        Path.Combine(
                            Path.GetDirectoryName(path),
                            Path.GetFileNameWithoutExtension(path) ?? "ScreenFIRE")

                        //! + _yyMMdd-HHmmff
                        + (replacing & replaceExisting ? ($"_{screenshot.Time:yyMMdd-HHmmff}") : string.Empty)

                        //! + .extension
                        + $".{saveFormat}",
                        //
                        $"{saveFormat}");

            return true; // Save successful
        }
    }
}
