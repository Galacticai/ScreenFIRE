namespace ScreenFIRE.Modules.Companion {
    internal class Theme {
        //public static bool IsDarkTheme => Application.Current.RequestedTheme == ApplicationTheme.Dark;
        public static void placeholder() {
            Gtk.CssProvider css_provider = new();
            css_provider.LoadFromData("themes/DeLorean-Dark-3.14/gtk-3.0/gtk.css");
            Gtk.StyleContext.AddProviderForScreen(Gdk.Screen.Default, css_provider, 800);
        }
    }
}
