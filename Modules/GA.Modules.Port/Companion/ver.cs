
namespace GeekAssistant.Modules.General.Companion {
    internal static class ver {
        public enum VerType { log, Home, HomeTitle, ToU }

        /// <summary>
        /// Create a customized version string accordingly or vX.x if not specified
        /// </summary>
        /// <param name="level">Place where this is being called at</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>log = Geek Assistant vX.x #Phase ©20XX By NHKomaiha.</item>
        /// <item>Main = vX.x #Phase ©20XX By NHKomaiha.</item>
        /// <item>MainTitle = Geek Assistant — vX.x #Phase</item>
        /// <item>ToU() = ©2021 Geek Assistant By NHKomaiha. vX.x #Phase"</item>
        /// <item>(Other) = vX.x</item>
        /// </list>
        /// </returns>
        public static string Run(VerType verType) {
            string cDateByNHKomaiha = "©2021 By NHKomaiha";
            string result = $"v{c.version.Major}.{c.version.Minor}";
            switch (c.version.Revision) {
                case 1: result += " #Beta"; break;
                case 2: result += " #Test"; break;
                case 3: result += " #Dev"; break;
            }
            switch (verType) {
                case VerType.log: result = $"Geek Assistant {result} {cDateByNHKomaiha}."; break;
                case VerType.Home: result += $" {cDateByNHKomaiha}."; break;
                case VerType.HomeTitle: result = $"Geek Assistant — {result}"; break;
                case VerType.ToU: result = $"{c.copyright}. {result}"; break;
            }
            return result;
        }
    }
}
