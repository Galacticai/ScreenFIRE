
using System.Drawing;

internal static class images {
    private static bool dark => c.S.DarkTheme;
    private static bool theme(bool anti) => dark & !anti;

    public struct GA {
        public static Image GeekAssistant(bool anti = false) => theme(anti) ? prop.GA.Geek_Assistant___25_ : prop.GA.Geek_Assistant___75_;
    }
    public struct x24 {
        public struct inf {
            public static Image From_inflvls(GeekAssistant.Modules.General.inf.lvls lvls)
                => lvls switch {
                    GeekAssistant.Modules.General.inf.lvls.Warn => Warn(), // 0
                    GeekAssistant.Modules.General.inf.lvls.Error => Error(), // 1 
                    GeekAssistant.Modules.General.inf.lvls.FatalError => Error(), // 10
                    GeekAssistant.Modules.General.inf.lvls.Question => Question(), // 2
                    _ => Information()  // -1
                };

            public static Image Information(bool anti = false) => theme(anti) ? prop.x24.Info_Blue_dark_24 : prop.x24.Info_Blue_24;
            public static Image Warn(bool anti = false) => theme(anti) ? prop.x24.Info_Yellow_dark_24 : prop.x24.Info_Yellow_24;
            public static Image Question(bool anti = false) => theme(anti) ? prop.x24.Question_Blue_dark_24 : prop.x24.Question_Blue_24;
            public static Image Error(bool anti = false) => theme(anti) ? prop.x24.Warning_Red_dark_24 : prop.x24.Warning_Red_24;

        }
        //public static Image AutoDetect(bool anti = false) => theme(anti) ? prop.x24.AutoDetect_dark_24 : prop.x24.AutoDetect_24;
        public static Image Donate(bool anti = false) => theme(anti) ? prop.x24.DonateHeart_dark_24 : prop.x24.DonateHeart_24;
        public static Image SwitchTheme(bool anti = false) => theme(anti) ? prop.x24.Theme_dark_24 : prop.x24.Theme_light_24;
        public static Image Smile(bool anti = false) => theme(anti) ? prop.x24.Smile_dark_24 : prop.x24.Smile_24;
        public static Image Settings(bool anti = false) => theme(anti) ? prop.x24.Settings_dark_24 : prop.x24.Settings_24;
        public static Image ToU(bool anti = false) => theme(anti) ? prop.x24.ToU_dark_24 : prop.x24.ToU_24;
        public static Image Commands(bool anti = false) => theme(anti) ? prop.x24.Commands_dark_24 : prop.x24.Commands_24;
    }

    public struct x64 {
        public struct inf {
            public static Image From_inflvls(GeekAssistant.Modules.General.inf.lvls lvls)
                => lvls switch {
                    GeekAssistant.Modules.General.inf.lvls.Warn => Warn(), // 0
                    GeekAssistant.Modules.General.inf.lvls.Error => Error(), // 1 
                    GeekAssistant.Modules.General.inf.lvls.FatalError => Error(), // 10
                    GeekAssistant.Modules.General.inf.lvls.Question => Question(), // 2
                    _ => Information()  // -1
                };

            public static Image Information(bool anti = false) => theme(anti) ? prop.x64.Info_Blue_dark_64 : prop.x64.Info_Blue_64;
            public static Image Warn(bool anti = false) => theme(anti) ? prop.x64.Info_Yellow_dark_64 : prop.x64.Info_Yellow_64;
            public static Image Question(bool anti = false) => theme(anti) ? prop.x64.Question_Blue_dark_64 : prop.x64.Question_Blue_64;
            public static Image Error(bool anti = false) => theme(anti) ? prop.x64.Warning_Red_dark_64 : prop.x64.Warning_Red_64;

        }

        public static Image AutoDetect(bool anti = false) => theme(anti) ? prop.x64.AutoDetect_dark_64 : prop.x64.AutoDetect_64;

        public static Image Donate(bool anti = false) => theme(anti) ? prop.x64.DonateHeart_dark_64 : prop.x64.DonateHeart_64;
        public static Image SwitchTheme(bool anti = false) => theme(anti) ? prop.x64.Theme_dark_64 : prop.x64.Theme_light_64;
        public static Image Smile(bool anti = false) => theme(anti) ? prop.x64.Smile_dark_64 : prop.x64.Smile_64;
        public static Image Settings(bool anti = false) => theme(anti) ? prop.x64.Settings_dark_64 : prop.x64.Settings_64;
        public static Image ToU(bool anti = false) => theme(anti) ? prop.x64.ToU_dark_64 : prop.x64.ToU_64;
        //public static Image Commands(bool anti = false) => theme(anti) ? prop.x64.Commands_dark_64 : prop.x64.Commands_64;
    }
}
