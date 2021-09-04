using GeekAssistant.Forms;
using System;
using System.Windows.Forms;

namespace GeekAssistant.Modules.General {
    internal class guiUpdate {

        public static void progressBar(int percent = 0) {
            ObjectInForm<Home, ProgressBar, int>(c.Home, c.Home.bar, nameof(c.Home.bar.Value), percent);
        }
        public static void SetProgressText(string txt, inf.lvls lvl = inf.lvls.Information) {
            ActionInForm(c.Home, new Action(() => GeekAssistant.Modules.General.SetProgressText.Run(txt, lvl)));
        }

        //log and log children


        public static void ObjectInForm<formType, objType, propType>(formType form, objType obj, string propName, propType value) where formType : Form {
            var propInfo = obj.GetType().GetProperty(propName);
            propInfo.SetValue(propName, value);

            ActionInForm(form, new Action(() => {
                var propInfo = obj.GetType().GetProperty(propName);
                propInfo.SetValue(propName, value);
            }));
        }
        public static void ActionInForm<formType>(formType form, Action action) where formType : Form {
            form.Invoke(action);
        }

        //public static guiUpdate Run() => throw new NotImplementedException();//new() {};



    }
}
