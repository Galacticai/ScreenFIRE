
using System;
using System.Diagnostics;

namespace GeekAssistant.Modules.General.Companion {
    internal static class GA_ProcessTime {
        public static DateTime StartTime => Process.GetCurrentProcess().StartTime;
        public static string StartTime_string => $"{StartTime:(yyy.MM.dd)-hh.mm.ss}";
        public static TimeSpan Uptime => DateTime.Now.Subtract(StartTime);
        public static string Uptime_string => $"{Uptime:(yyy.MM.dd)-hh.mm.ss}";
    }
}
