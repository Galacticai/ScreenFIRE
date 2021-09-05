

using GeekAssistant.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace GeekAssistant.Modules.General {
    internal static class CenterToHomeBounds {
        public static void Run(Form f) {
            Point xy;

            Home home = c.Home;
            //foreach (Form h in Application.OpenForms)
            //    if (h is Home foundHome)
            //        home = foundHome;

            /*   _______________________                   
              *  |      |         |      |  <= Home
              *  |      |         | <= f |
              *  |      |_________|      |
              *  |                       |
              *  |_______________________|
              */
            if (c.FormisRunning<Home>())
                xy = new((home.Width / 2) - (f.Width / 2) + home.Location.X, home.Top);
            /*   _______________________                   
              *  |       _________       |  <= Screen
              *  |      |         | <= f |
              *  |      |         |      |
              *  |      |_________|      |
              *  |_______________________|
              */
            else {
                var currentScreenRect = Screen.FromControl(f).WorkingArea;
                xy = new((currentScreenRect.Width / 2) - (f.Width / 2) + currentScreenRect.Location.X,
                      (currentScreenRect.Height / 2) - (f.Height / 2));
            }

            f.SetBounds(xy.X, xy.Y, f.Width, f.Height);
        }
    }
}
