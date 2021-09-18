using Gtk;
using ScreenFIRE.Assets;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using System.IO;
using System.Threading.Tasks;

namespace ScreenFIRE.Modules.Save {


    partial class Save {

        /// <summary> ••• GUI ••• Save a <see cref="Screenshot"/> locally </summary>
        public static async Task<bool> Local(Screenshot screenshot, Window parentWindow) {

            FileChooserDialog save = new(await Strings.Fetch(IStrings.SaveAs), null, FileChooserAction.Save);
            save.AddButton(Stock.Cancel, ResponseType.Cancel);
            save.AddButton(Stock.Save, ResponseType.Ok);
            save.DefaultResponse = ResponseType.Ok;
            save.SelectMultiple = false;
            save.SetCurrentFolder(Common.SF);

            ResponseType response = (ResponseType)save.Run();


            if (response == ResponseType.Ok
               & Directory.Exists(save.CurrentFolder)) {
                if (save.File.Exists) {

                    MessageDialog warn
                        = new(parentWindow, DialogFlags.DestroyWithParent,
                              MessageType.Warning, ButtonsType.YesNo,
                              Strings.Fetch(IStrings.FileAlreadyExists)
                              + Common.nn
                              + Strings.Fetch(IStrings.DoYouWantToReplaceTheExistingFile));
                    warn.Resize(100, 250);
                    warn.Resizable = false;
                    warn.DefaultResponse = ResponseType.No;

                    if (!await Local(screenshot, save.Filename, warnDialog: warn))
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
        public static async Task<bool> Local_Auto(Screenshot screenshot, ISaveFormat saveFormat = ISaveFormat.png) {
            //! Local(screenshot, ); //! PLACEHOLDER
            return false;
        }


        /// <summary> ••• Specific ••• Save a <see cref="Screenshot"/> locally </summary>
        /// <returns> false if cancelled of failed to save</returns>
        public static async Task<bool> Local(Screenshot screenshot,
                                 string path,
                                 bool replaceExisting = false,
                                 ISaveFormat saveFormat = ISaveFormat.png,
                                 MessageDialog warnDialog = null) {


            bool replacing = File.Exists(path);
            if (warnDialog != null) {
                ResponseType warnResponse = (ResponseType)warnDialog.Run();
                if (warnResponse == ResponseType.No) { // Don't replace   
                    replacing = true;
                    warnDialog.Destroy();
                } else if (warnResponse == ResponseType.Cancel) {
                    warnDialog.Destroy();
                    return false; //Cancel signal
                } // Cancel
            } else {

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
