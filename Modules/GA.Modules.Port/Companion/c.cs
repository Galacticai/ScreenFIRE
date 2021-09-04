using GeekAssistant.Forms;
using System;
using System.Linq;
using System.Windows.Forms;


internal class c {

    #region GA Directories
    ///<summary> Geek Assistant home directory ( ...\AppData\Roaming\Geek Assistant (Android AIO) ) </summary>
    public static readonly string GA = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}"
                                      + @"\Geek Assistant (Android AIO)";
    /// <summary> Geek Assistant tools directory (adb, fastboot, and others) ( {GA}\tools ) </summary>
    public static readonly string GA_tools = $@"{GA}\tools";
    /// <summary> Geek Assistant logs directory (Saved every session) ( {GA}\log ) </summary>
    public static readonly string GA_logs = $@"{GA}\log";
    #endregion


    #region Public variables

    /// <summary> <list>
    /// <item>Flagged on when a process is ongoing.</item>
    /// <item>Blocks other processes from starting while another is already ongoing</item>
    /// </list> </summary>
    public static bool Working = false;

    #endregion


    #region Forms  
    /// <typeparam name="form"><see cref="Form"/> to check</typeparam>
    /// <returns>True if <typeparamref name="form"/> is currently open</returns>
    public static bool FormisRunning<form>() => Application.OpenForms.OfType<form>().Any();

    /// <typeparam name="form"><see cref="Form"/> to check</typeparam>
    /// <returns>Current instance of <typeparamref name="form"/>  |  null if disposed or not found</returns>
    private static form Forms<form>() where form : Form {
        try {
            form f = Application.OpenForms.OfType<form>().First();
            if (f.IsDisposed) throw new ObjectDisposedException(f.Name);
            return f;
        } catch {
            return default;
        }
    }

    /// <returns>Current instance of <see cref="GeekAssistant.Forms.Preparing"/>  |  new instance if disposed or not found</returns>
    public static Preparing Preparing => Forms<Preparing>() ?? new();
    /// <returns>Current instance of <see cref="GeekAssistant.Forms.Wait"/>  |  new instance if disposed or not found</returns>
    public static Wait Wait => Forms<Wait>() ?? new();
    /// <returns>Current instance of <see cref="GeekAssistant.Forms.AppMode"/>  |  new instance if disposed or not found</returns>
    public static AppMode AppMode => Forms<AppMode>() ?? new();
    /// <returns>Current instance of <see cref="GeekAssistant.Forms.Donate"/>  |  new instance if disposed or not found</returns>
    public static Donate Donate => Forms<Donate>() ?? new();
    /// <returns>Current instance of <see cref="GeekAssistant.Forms.Home"/>  |  new instance if disposed or not found</returns>
    public static Home Home => Forms<Home>() ?? new();
    /// <returns>Current instance of <see cref="GeekAssistant.Forms.Info"/>  |  new instance if disposed or not found</returns>
    public static Info Info => Forms<Info>() ?? new();
    /// <returns>Current instance of <see cref="GeekAssistant.Forms.Settings"/>  |  new instance if disposed or not found</returns>
    public static Settings Settings => Forms<Settings>() ?? new();
    /// <returns>Current instance of <see cref="GeekAssistant.Forms.ToU"/>  |  new instance if disposed or not found</returns>
    public static ToU ToU => Forms<ToU>() ?? new();

    #endregion


    #region prop 

    private static prop.S _S; // Save to improve performance
    public static prop.S S => _S ??= new(); // Update if null
    public static bool theme(bool anti = false) => anti ? !S.DarkTheme : S.DarkTheme;

    #endregion


    #region Public Abbreviations 

    /// <summary> Get current copyright string </summary>
    public static readonly string copyright = System.Diagnostics.FileVersionInfo.GetVersionInfo(
                                          System.Reflection.Assembly.GetExecutingAssembly().Location
                                      ).LegalCopyright;
    /// <summary> Get current version field </summary>
    public static readonly Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
    /// <summary> Simple .NET new line (Environment.NewLine) </summary>
    public static readonly string n = Environment.NewLine;
    /// <summary> Simple html new line (&lt;br/&gt;) </summary>
    public static readonly string br = "<br/>";
    /// <summary> Tab: (Unicode) U+3000 | (HTML) And#12288; | (Description) Ideographic Space </summary>
    public static readonly string tab = @"　";
    /// <summary> Double Tab: (Unicode) U+3000 | (HTML) And#12288; | (Description) Ideographic Space </summary>
    public static readonly string tab2 = @"　　";

    #endregion
}

