using System.Windows.Forms;

namespace GeekAssistant.Modules.General {
    internal static class HideAllForms {

        public static FormCollection HiddenForms;
        private static Form currentForm;
        /// <summary>
        /// Hides/Shows All forms currently opened by Geek Assistant
        /// </summary>
        /// <param name="Hide">Set if it should hide or not (show)</param>
        /// <param name="FormToFront">Bring this form to front when done</param>
        public static void Run(bool Hide) {
            // SET ERROR CODE WHEN CALLING, DO NOT SET HERE //

            currentForm = Form.ActiveForm; // Set before hiding to save
            if (Hide) {
                HiddenForms = Application.OpenForms;
                foreach (Form formname in Application.OpenForms) // Save all forms to HiddenFormsList then hide 
                    formname.Hide();

            } else {
                if (HiddenForms.Count == 0) { // failsafe
                    inf.detail.code += "-HF0";
                    inf.Run(inf.lvls.FatalError, "Something went wrong.", "We failed to revive hidden windows.");
                    if (inf.Run(inf.lvls.Question, "Refresh Geek Assistant?",
                                  "Refreshing will relaunch Geek Assistant to get back to working order. This will terminate any ongoing progress!",
                                ("Refresh", "No")))
                        Application.Restart();
                    return;
                }
                foreach (Form formname in HiddenForms)  // Show all forms saved in list
                    formname.Show();

                HiddenForms = null; //reset
                currentForm.BringToFront();
            }
        }
        //public static bool noHiddenForms() {
        //    if (HiddenForms == null) return false;
        //    if (HiddenForms.Count == 0) return false;
        //    return true;
        //}
    }

}
