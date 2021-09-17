using GeekAssistant.Forms;
using System;
using System.IO;
using System.Text.RegularExpressions;
using GeekAssistant.Modules.General;
using GeekAssistant.Modules.General.Companion;
using GeekAssistant.Modules.General.Companion.Style;

namespace GeekAssistant.Modules.General {
    internal static class log {
        private static string latestLogName;
        private static string latestLogPath;

        private static Home Home => c.Home;
        //private static void RefresHome() {
        //    foreach (Form h in Application.OpenForms)
        //        if (h.GetType() == typeof(Home))
        //            Home = (Home)h;
        //}
        public static void Create() {
            if (!Directory.Exists(c.GA)) Directory.CreateDirectory(c.GA);
            if (!Directory.Exists($@"{c.GA}\log")) Directory.CreateDirectory($@"{c.GA}\log");

            latestLogName = $"GA-log_{DateTime.Now:(yyy.MM.dd)-hh.mm.ss}.log";
            latestLogPath = $@"{c.GA}\log\{latestLogName}";
            File.Create(latestLogPath).Dispose();
            StreamWriter swriter = new(latestLogPath);
            swriter.WriteLine(Home.log_TextBox.Text);
            swriter.Close();
        }

        public static void Event(string EventName, int lines)
                => AppendText($"// {DateTime.Now:hhh:mm:ss.ff} // {EventName} //", lines);


        public static void ResetLog() {
            Event("Log Cleared", 3);
            Create();
            Home.log_TextBox.Text = ver.Run(ver.VerType.log);
            AppendText($"// Previous log saved //  {latestLogName}  //", 1);
            Event("Continue", 1);
        }

        public static void AppendText(string logText, int lines) {
            for (int newline = 1, loopTo = lines; newline <= loopTo; newline++)
                Home.log_TextBox.Text += c.n;
            Home.log_TextBox.Text += logText;
        }

        public static void StopNotifyIfLogSeen() {
            if (Home.log_TextBox.Visible & (Home.ShowLog_ErrorBlink_Timer.Enabled | Home.ShowLog_InfoBlink_Timer.Enabled)) {
                Home.ShowLog_ErrorBlink_Timer.Stop();
                Home.ShowLog_InfoBlink_Timer.Stop();
                Home.ShowLog_Button.Icon = images.x24.Commands();
                Home.ProgressBarLabel.ForeColor = colors.UI.fg();
            }
        }

        public static void ClearLog() {

        }
        public static void ClearIf30days() {
            if (!Directory.Exists(c.GA_logs)) return;

            //   file name:  GA-log_(yyyy.MM.dd)-hh.mm.ss.log
            //     example:  GA-log_(2018.04.24)-12.00.00.log
            Regex filenameRegex = new(@"GA-log_\(\d\d\d\d\.\d\d\.\d\d\)-\d\d\.\d\d\.\d\d\.log");
            foreach (FileInfo file in new DirectoryInfo(c.GA_logs).GetFiles())
                if (filenameRegex.Match($"{file.Name}.{file.Extension}").Success & (DateTime.Now - file.CreationTime).Days > 30)
                    file.Delete();
        }
    }
}
