using System.Threading.Tasks;
using System.Windows.Forms;
using GeekAssistant.Controls.Material;
using GeekAssistant.Modules.General.Companion;
using GeekAssistant.Modules.General.Companion.Style;

namespace GeekAssistant.Modules.General.SetTheme {
    internal class SetThemeCompanion {

        public static void SetControlsArrTheme(Control[] control) {
            foreach (var ctrl in control) SetControlTheme(ctrl);
        }
        public static void SetControlTheme(Control control) {
            Animate.Run(control, nameof(control.ForeColor), colors.UI.fg());
            Animate.Run(control, nameof(control.BackColor), colors.UI.bg());
        }

        public static void SetControlsArrTheme_Metro(MetroFramework.Interfaces.IMetroControl[] control) {
            foreach (var ctrl in control) SetControlTheme_Metro(ctrl);
        }
        public static void SetControlTheme_Metro(MetroFramework.Interfaces.IMetroControl control)
          => control.Theme = colors.UI.Metro();  //Note: Cannot animate "Theme"   

    }
}
