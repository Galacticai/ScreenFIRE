using Gtk;
using ScreenFIRE.Assets;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using System.IO;

namespace ScreenFIRE.Modules.Save {


    partial class Save {

        private static string[] s
                = Strings.Fetch(IStrings.SaveAs___, //0
                                IStrings.Yes, //1
                                IStrings.No, //2
                                IStrings.Cancel,//3
                                IStrings.FileAlreadyExists_,//4
                                IStrings.WouldYouLikeToReplaceTheExistingFile_//5
                                );

        /// <summary> ••• GUI ••• Save a <see cref="Screenshot"/> locally </summary>
        public static bool Local(Screenshot screenshot, Window parentWindow) {

            //! Parameters to be passed
            //Screenshot screenshot;
            string path;
            bool replaceExisting = false;
            ISaveFormat saveFormat = ISaveFormat.png;
            ///////////////////////////

            //! Let the user choose a path to the file
            FileChooserDialog choose
                    = new(s[0],
                          parentWindow,
                          FileChooserAction.Save);
            choose.SelectMultiple = false;
            choose.AddButton(Stock.Ok, ResponseType.Ok);
            choose.AddButton(Stock.Cancel, ResponseType.Cancel);
            choose.SetCurrentFolder(Common.SF);

            ResponseType chooseResponse = (ResponseType)choose.Run();

            //! User closed the dialog
            if (chooseResponse == ResponseType.Ok) {

                path = choose.Filename;

                //! User chose a file that already exists
                if (choose.File.Exists) {
                    //! Warn about replacing the file
                    MessageDialog fileExistsDialog
                            = new(parentWindow,
                                  DialogFlags.DestroyWithParent,
                                  MessageType.Question,
                                  ButtonsType.YesNo,
                                  s[4] + Common.nn + s[5]);
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
                         path: Path.Combine(Common.SF,
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
    }
}
