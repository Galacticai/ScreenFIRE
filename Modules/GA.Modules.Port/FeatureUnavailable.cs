
namespace GeekAssistant.Modules.General {
    internal static class FeatureUnavailable {
        public static void Run(string code, string title) {
            inf.detail.code = code;
            string state = c.version.Revision switch {
                1 => "beta phase",
                2 => "testing phase",
                3 => "development phase",
                _ => "development phase"
            };
            inf.Run(inf.lvls.Information, title,
                    $"Geek Assistant is still in {state}... {title} might be added in future updates. Stay tuned!");
        }
    }
}
