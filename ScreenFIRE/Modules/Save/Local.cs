using System;
using System.IO;
using System.Threading.Tasks;
using Gtk;
using ScreenFIRE.Assets;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;

namespace ScreenFIRE.Modules.Save {


    partial class Save {

        /// <summary> ••• GUI ••• Save a <see cref="Screenshot"/> locally </summary>
        public static async Task<bool> Local(Screenshot screenshot, Window parentWindow) {

            //! Parameters to be passed
            //Screenshot screenshot;
            string path;
            bool replaceExisting = false;
            ISaveFormat saveFormat = ISaveFormat.png;
            ///////////////////////////

            //! Let the user choose a path to the file
            FileChooserNative choose
                        = new(await Strings.Fetch(IStrings.SaveAs___),
                              parentWindow,
                              FileChooserAction.Save,
                              await Strings.Fetch(IStrings.OK), await Strings.Fetch(IStrings.Cancel));
            choose.SelectMultiple = false;
            choose.SetCurrentFolder(Path.Combine(Common.LocalSave_Settings.Location, MonthDir));

            ResponseType chooseResponse = (ResponseType)choose.Run();

            //! User closed the dialog
            if (chooseResponse == ResponseType.Accept) {

                path = choose.Filename;

                //! User chose a file that already exists
                if (choose.File.Exists) {
                    //! Warn about replacing the file
                    MessageDialog fileExistsDialog
                            = new(parentWindow,
                                  DialogFlags.DestroyWithParent,
                                  MessageType.Question,
                                  ButtonsType.YesNo,
                                  await Strings.Fetch(IStrings.FileAlreadyExists_) + Common.nn
                                  + await Strings.Fetch(IStrings.WouldYouLikeToReplaceTheExistingFile_));
                    fileExistsDialog.SetPosition(WindowPosition.CenterOnParent);
                    ResponseType fileExistsResponse = (ResponseType)fileExistsDialog.Run();
                    //! User chose to replace the file
                    if (fileExistsResponse == ResponseType.Yes)
                        replaceExisting = true;

                    fileExistsDialog.Destroy();
                }

            } else { //! User cancelled the screenshot saving operation
                choose.Destroy();
                return false;
            }
            choose.Destroy();

            //! Pass to ••• Specific ••• with the info provided by the user
            return Local(screenshot, path, replaceExisting, saveFormat);
        }


        /// <summary> ••• AUTO ••• Save a <see cref="Screenshot"/> locally </summary>
        public static bool Local(Screenshot screenshot, ISaveFormat saveFormat = ISaveFormat.png) {
            //! Pass to ••• Specific ••• but with auto generated info
            return Local(screenshot,
                         path: Path.Combine(Common.SF, MonthDir,
                                            Strings.Fetch(IStrings.ScreenFIRE)
                                            + $"_{screenshot.Time:yyMMdd-HHmmff}"
                                            + $".{saveFormat}"),
                         replaceExisting: false,
                         saveFormat: ISaveFormat.png);
        }


        /// <summary> ••• Specific ••• Save a <see cref="Screenshot"/> locally </summary>
        /// <returns> false if cancelled of failed to save</returns>
        public static bool Local(Screenshot screenshot,
                                 string path,
                                 bool replaceExisting = false,
                                 ISaveFormat saveFormat = ISaveFormat.png) {

            try {//! Try to use provided path

                //! Deal with file replacement
                if (File.Exists(path) & !replaceExisting) {
                    path = Path.Combine(
                                Path.GetDirectoryName(path),
                                Path.GetFileNameWithoutExtension(path)
                                + $"_{screenshot.Time:yyMMdd-HHmmff}"
                                + $".{saveFormat}");
                }

                //! Make sure the extension is added
                if (string.IsNullOrEmpty(Path.GetExtension(path)))
                    path += $".{saveFormat}";

                //! Save
                screenshot.Image.Save(path, saveFormat.ToString());
                return true;

            } catch {

                try { //! Try to use default path

                    return Local(screenshot, saveFormat); //! (Auto)

                } catch { return false; } //! Something went wrong

            }
        }

        public static string MonthDir => PrepareMonthDir();
        private static string PrepareMonthDir() {
            string path = Path.Combine(Common.LocalSave_Settings.Location,
                                       $"{DateTime.Now:MM-yyyy}");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }
    }
}
