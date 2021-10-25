using Material.Icons;
using ScreenFIRE.Assets;
using ScreenFIRE.Assets.Embedded;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.math;
using ScreenFIRE.Modules.Companion.math.Vision;
using ScreenFIRE.Modules.Save;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using g = Gdk;
using gtk = Gtk;
using io = System.IO;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.GUI {

    class Config : gtk.ApplicationWindow {
        [UI] private readonly gtk.Image LogoImage = null;

        [UI] private readonly gtk.Label Screenshot_TabButton = null;
        [UI] private readonly gtk.Button ssPreview_Button_Screenshot_Box = null;
        [UI] private readonly gtk.Image Image_ssPreview_Button_Screenshot_Box = null;
        [UI] private readonly gtk.Label Label_ssPreview_Button_Screenshot_Box = null;
        [UI] private readonly gtk.Label _label1 = null;
        [UI] private readonly gtk.Button SS_Button_AllMonitors = null;
        [UI] private readonly gtk.Button SS_Button_MonitorAtPointer = null;
        [UI] private readonly gtk.Button SS_Button_WindowAtPointer = null;
        [UI] private readonly gtk.Button SS_Button_ActiveWindow = null;
        [UI] private readonly gtk.Button SS_Button_Custom = null;

        [UI] private readonly gtk.Label SaveOptions_TabButton = null;
        [UI] private readonly gtk.Label Label_Format_MenuButton_SaveOptions_Box = null;
        [UI] private readonly gtk.FileChooserButton SaveLocation_FileChooserButton_SaveOptions_Box = null;
        [UI] private readonly gtk.Popover SaveFormat_Popover = null;
        [UI] private readonly gtk.ToggleButton bmp_Button_SaveFormat_Popover = null;
        [UI] private readonly gtk.ToggleButton png_Button_SaveFormat_Popover = null;
        [UI] private readonly gtk.ToggleButton jpg_Button_SaveFormat_Popover = null;
        [UI] private readonly gtk.ToggleButton gif_Button_SaveFormat_Popover = null;
        [UI] private readonly gtk.ToggleButton mp4_Button_SaveFormat_Popover = null;
        [UI] private readonly gtk.Label Label_AutoSaveExisting_Box_SaveOptions_Box = null;
        [UI] private readonly gtk.EventBox AutoDelete1MonthOldFiles_EventBox_SaveOptions_Box = null;
        [UI] private readonly gtk.Switch Switch_AutoDelete1MonthOldFiles_Box_SaveOptions_Box = null;
        [UI] private readonly gtk.EventBox CopyToClipboard_EventBox_SaveOptions_Box = null;
        [UI] private readonly gtk.Switch Switch_CopyToClipboard_Box_SaveOptions_Box = null;

        [UI] private readonly gtk.Label About_TabButton = null;
        [UI] private readonly gtk.Label ScreenFIRE_Label_About_Box = null;
        [UI] private readonly gtk.Label VersionTitle_Label_About_Box = null;
        [UI] private readonly gtk.Label Version_Label_About_Box = null;
        [UI] private readonly gtk.EventBox VersionPhase_Box_About_Box = null;
        [UI] private readonly gtk.Label PhaseTitle_Label_About_Box = null;
        [UI] private readonly gtk.Label Phase_Label_About_Box = null;
        [UI] private readonly gtk.Button SF_repo_Button_About_Box = null;
        [UI] private readonly gtk.Image Image_SF_repo_Button_About_Box = null;
        [UI] private readonly gtk.Label Label_SF_repo_Button_About_Box = null;
        [UI] private readonly gtk.Button License_Button_About_Box = null;
        [UI] private readonly gtk.Image Image_License_Button_About_Box = null;
        [UI] private readonly gtk.Label Label_License_Button_About_Box = null;
        [UI] private readonly gtk.Label madeWith_Label_About_Box = null;

        private void AssignEvents() {
            DeleteEvent += delegate {
                //Strings.SaveStorage(Common.Settings.Language);
                gtk.Application.Quit();
            };

            ssPreview_Button_Screenshot_Box.Clicked += delegate {
                Process.Start(new ProcessStartInfo() { FileName = io.Path.Combine(Common.LocalSave_Settings.Location, Save.MonthDir), UseShellExecute = true, Verb = "open" });
            };

            SS_Button_AllMonitors.Clicked
                += async delegate { await Capture(IScreenshotType.AllMonitors); };
            SS_Button_MonitorAtPointer.Clicked
                += async delegate { await Capture(IScreenshotType.MonitorAtPointer); };
            SS_Button_WindowAtPointer.Clicked
                += async delegate { await Capture(IScreenshotType.WindowAtPointer); };
            SS_Button_ActiveWindow.Clicked
                += async delegate { await Capture(IScreenshotType.ActiveWindow); };
            SS_Button_Custom.Clicked += delegate { Program.ScreenFIRE.ShowAll(); };

            SF_repo_Button_About_Box.Clicked += delegate { Link.Open(Common.SF_GitRepo); };
            License_Button_About_Box.Clicked += delegate { Link.Open(Common.SF_License); };

            SaveLocation_FileChooserButton_SaveOptions_Box.CurrentFolderChanged
                += SaveLocation_FileChooserButton_SaveOptions_Box_CurrentFolderChanged;

            bmp_Button_SaveFormat_Popover.Toggled += delegate { Button_SaveFormat_Popover_Toggled(bmp_Button_SaveFormat_Popover); };
            png_Button_SaveFormat_Popover.Toggled += delegate { Button_SaveFormat_Popover_Toggled(png_Button_SaveFormat_Popover); };
            jpg_Button_SaveFormat_Popover.Toggled += delegate { Button_SaveFormat_Popover_Toggled(jpg_Button_SaveFormat_Popover); };

            Switch_AutoDelete1MonthOldFiles_Box_SaveOptions_Box.StateChanged += delegate {
                Common.LocalSave_Settings.AutoDelete1MonthOldFiles
                    = Switch_AutoDelete1MonthOldFiles_Box_SaveOptions_Box.State;
            };
            AutoDelete1MonthOldFiles_EventBox_SaveOptions_Box.ButtonReleaseEvent += delegate {
                Switch_AutoDelete1MonthOldFiles_Box_SaveOptions_Box.State
                  = !Switch_AutoDelete1MonthOldFiles_Box_SaveOptions_Box.State;
            };
            Switch_CopyToClipboard_Box_SaveOptions_Box.StateChanged += delegate {
                Common.LocalSave_Settings.CopyToClipboard
                = Switch_CopyToClipboard_Box_SaveOptions_Box.State;
            };
            CopyToClipboard_EventBox_SaveOptions_Box.ButtonReleaseEvent += delegate {
                Switch_CopyToClipboard_Box_SaveOptions_Box.State
                  = !Switch_CopyToClipboard_Box_SaveOptions_Box.State;
            };


            VersionPhase_Box_About_Box.ButtonReleaseEvent += async delegate {
                gtk.Clipboard.GetDefault(Gdk.Display.Default).Text
                        = $"{await Strings.Fetch(IStrings.ScreenFIRE)} {Common.VersionString()}";
            };

        }
        private void AssignStrings() {
            Title = Strings.Fetch(IStrings.ScreenFIREConfig).Result;

            Screenshot_TabButton.Text = Strings.Fetch(IStrings.Screenshot).Result;

            Label_ssPreview_Button_Screenshot_Box.Text = Strings.Fetch(IStrings.ViewScreenshots).Result;
            _label1.Text = Strings.Fetch(IStrings.ChooseHowYouWouldLikeToFireYourScreenshot_).Result;
            SS_Button_AllMonitors.Label = Strings.Fetch(IStrings.AllMonitors).Result;
            SS_Button_MonitorAtPointer.Label = Strings.Fetch(IStrings.MonitorAtPointer).Result;
            SS_Button_WindowAtPointer.Label = Strings.Fetch(IStrings.WindowAtPointer).Result;
            SS_Button_ActiveWindow.Label = Strings.Fetch(IStrings.ActiveWindow).Result;
            SS_Button_Custom.Label = Strings.Fetch(IStrings.FreeAreaSelection).Result;


            SaveOptions_TabButton.Text = Strings.Fetch(IStrings.SavingOptions).Result;

            Label_Format_MenuButton_SaveOptions_Box.Text = SaveFormat.StringWithDesctiption();
            bmp_Button_SaveFormat_Popover.Label = $"{Strings.Fetch(IStrings.Original).Result} {Common.RangeDash} bmp";
            png_Button_SaveFormat_Popover.Label = $"{Strings.Fetch(IStrings.Quality).Result} {Common.RangeDash} png";
            jpg_Button_SaveFormat_Popover.Label = $"{Strings.Fetch(IStrings.Efficiency).Result} {Common.RangeDash} jpg";
            gif_Button_SaveFormat_Popover.Label = $"{Strings.Fetch(IStrings.Animated).Result} {Common.RangeDash} gif";
            mp4_Button_SaveFormat_Popover.Label = $"{Strings.Fetch(IStrings.Video).Result} {Common.RangeDash} mp4";

            Label_AutoSaveExisting_Box_SaveOptions_Box.Text = Strings.Fetch(IStrings.AutoDelete1MonthOldFiles).Result;

            About_TabButton.Text = Strings.Fetch(IStrings.About).Result;

            ScreenFIRE_Label_About_Box.Text = Strings.Fetch(IStrings.ScreenFIRE_Stylized).Result;

            VersionPhase_Box_About_Box.TooltipText = Strings.Fetch(IStrings.ClicktoCopy___).Result;
            VersionTitle_Label_About_Box.Text = Strings.Fetch(IStrings.Version).Result + ":";
            Version_Label_About_Box.Text = Common.VersionString(includePhase: false);
            PhaseTitle_Label_About_Box.Text = Strings.Fetch(IStrings.Phase).Result + ":";
            Phase_Label_About_Box.Text = Common.PhaseString();
            madeWith_Label_About_Box.Text = Strings.Fetch(IStrings.MadeWith_NET_GTK_).Result;

            Label_SF_repo_Button_About_Box.Text = Strings.Fetch(IStrings.ScreenFIRERepositoryAtGitHub).Result;
            Label_License_Button_About_Box.Text = Strings.Fetch(IStrings.GNUGeneralPublicLicensev3_0___).Result;

        }
        private void AssignImages() {
            //? = LogoImage ==========================
            LogoImage.Pixbuf
                = new g.Pixbuf(SF.Logo).ScaleSimple(128, 128, g.InterpType.Bilinear);
            //? ======================================

            g.RGBA iconsColor = new() { Alpha = 1, Red = .05, Green = .06, Blue = .065 };
            int iconSize = 24;
            //? = Image_SF_repo_Button_About_Box =====
            Common.Cache.MaterialIcons.TryGetValue(MaterialIconKind.Github, out string github);
            Image_SF_repo_Button_About_Box.Pixbuf = github.ToPixbuf((iconSize, iconSize), iconsColor);
            //? ======================================

            //? = Image_License_Button_About_Box =====
            Common.Cache.MaterialIcons.TryGetValue(MaterialIconKind.ScaleBalance, out string scaleBalance);
            Image_License_Button_About_Box.Pixbuf = scaleBalance.ToPixbuf((iconSize, iconSize), iconsColor);
            //? ======================================

        }
        private void AssignEtc() {
            SaveLocation_FileChooserButton_SaveOptions_Box
                .SetCurrentFolder(Common.LocalSave_Settings.Location);

            SaveFormatButtons_Init();
        }

        public Config() : this(new gtk.Builder("Config.glade")) { }
        private Config(gtk.Builder builder)
                : base(builder.GetRawOwnedObject("Config")) {
            builder.Autoconnect(this);
            AssignEvents();
            AssignStrings();
            AssignImages();
            AssignEtc();
        }

        private async Task Capture(IScreenshotType screenshotType) {
            Visible = false;
            AcceptFocus = false;

            //_ = new Timer(async (object obj) => {
            Thread.Sleep(1000); //? Temp (For Windows) make sure the window is fully hidden

            using var ss = new Screenshot(screenshotType);
            bool saved = await Save.Local(ss, this);
            if (saved) {
                if (Common.LocalSave_Settings.CopyToClipboard)
                    Save.Clipboard(ss);

                _label1.Text = await Strings.Fetch(IStrings.FiredAScreenshot_);
                _ = new Timer(async (object obj) => {
                    _label1.Text = await Strings.Fetch(IStrings.ChooseHowYouWouldLikeToFireYourScreenshot_);
                }, null, 5000, Timeout.Infinite);

                var (w, h) = Scale.Fit((ss.Image.Width, ss.Image.Height), (270, 256));
                Label_ssPreview_Button_Screenshot_Box.Destroy();
                Image_ssPreview_Button_Screenshot_Box.Visible = true;
                Image_ssPreview_Button_Screenshot_Box.Pixbuf =
                     ss.Image.ScaleSimple((int)w, (int)h, Gdk.InterpType.Bilinear);

            } else {
                gtk.MessageDialog failDialog = new(this,
                                               gtk.DialogFlags.Modal,
                                               gtk.MessageType.Warning,
                                               gtk.ButtonsType.Ok,
                                               await Strings.Fetch(IStrings.SomethingWentWrong___));
                failDialog.Run();
                failDialog.Destroy();
            }

            AcceptFocus = true;
            Visible = true;
            //}, this, 2000, Timeout.Infinite);
        }

        private void SaveLocation_FileChooserButton_SaveOptions_Box_CurrentFolderChanged(object sender, EventArgs e) {
            if (PathIsRW.Run(SaveLocation_FileChooserButton_SaveOptions_Box
                                .CurrentFolder))
                Common.LocalSave_Settings.Location
                    = SaveLocation_FileChooserButton_SaveOptions_Box
                            .CurrentFolder;
            else SaveLocation_FileChooserButton_SaveOptions_Box
                    .SetCurrentFolder(Common.SF);
        }


        private void SaveFormatButtons_Init() {
            gtk.ToggleButton selectedButton = Common.LocalSave_Settings.Format switch {
                ISaveFormat.png => png_Button_SaveFormat_Popover,
                ISaveFormat.jpeg => jpg_Button_SaveFormat_Popover,
                _ => bmp_Button_SaveFormat_Popover
            };
            selectedButton.Active = true;
        }
        private void Button_SaveFormat_Popover_Toggled(gtk.ToggleButton selectedButton) {
            //? Avoid stack overflow - toggle each others
            if (!selectedButton.Active) { return; }
            ISaveFormat saveFormat = ISaveFormat.bmp;
            if (selectedButton == bmp_Button_SaveFormat_Popover) bmp_Button_SaveFormat_Popover.Active = true;
            if (selectedButton == png_Button_SaveFormat_Popover) { saveFormat = ISaveFormat.png; png_Button_SaveFormat_Popover.Active = true; }
            if (selectedButton == jpg_Button_SaveFormat_Popover) { saveFormat = ISaveFormat.bmp; jpg_Button_SaveFormat_Popover.Active = true; }
            if (selectedButton == gif_Button_SaveFormat_Popover) gif_Button_SaveFormat_Popover.Active = true;
            if (selectedButton == mp4_Button_SaveFormat_Popover) mp4_Button_SaveFormat_Popover.Active = true;
            Common.LocalSave_Settings.Format = saveFormat;
            Common.LocalSave_Settings.Save();
            Label_Format_MenuButton_SaveOptions_Box.Text = selectedButton.Label;
            SaveFormat_Popover.Hide();
        }
    }
}
