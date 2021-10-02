using Gtk;
using ScreenFIRE.Assets;
using ScreenFIRE.Assets.Embedded;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.math;
using ScreenFIRE.Modules.Save;
using System.Diagnostics;
using io = System.IO;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.GUI {

    class Config : ApplicationWindow {
        [UI] private readonly Image LogoImage = null;

        [UI] private readonly Label Screenshot_TabButton = null;
        [UI] private readonly Button ssPreview_Button_Screenshot_Box = null;
        [UI] private readonly Image Image_ssPreview_Button_Screenshot_Box = null;
        [UI] private readonly Label Label_ssPreview_Button_Screenshot_Box = null;
        [UI] private readonly Label _label1 = null;
        [UI] private readonly Button SF_Button_AllMonitors = null;
        [UI] private readonly Button SF_Button_MonitorAtPointer = null;
        [UI] private readonly Button SF_Button_WindowAtPointer = null;
        [UI] private readonly Button SF_Button_ActiveWindow = null;
        [UI] private readonly Button SF_Button_Custom = null;

        [UI] private readonly Label SaveOptions_TabButton = null;
        [UI] private readonly Label Label_MenuButton_SaveOptions_Box = null;
        [UI] private readonly FileChooserButton SaveLocation_FileChooserButton_SaveOptions_Box = null;
        [UI] private readonly Popover SaveFormat_Popover = null;
        [UI] private readonly Button bmp_Button_SaveFormat_Popover = null;
        [UI] private readonly Button png_Button_SaveFormat_Popover = null;
        [UI] private readonly Button jpg_Button_SaveFormat_Popover = null;
        [UI] private readonly Button gif_Button_SaveFormat_Popover = null;
        [UI] private readonly Button mp4_Button_SaveFormat_Popover = null;
        [UI] private readonly Label Label_AutoSaveExisting_Box_SaveOptions_Box = null;
        [UI] private readonly Gtk.Switch Switch_AutoDelete1MonthOldFiles_Box_SaveOptions_Box = null;

        [UI] private readonly Label About_TabButton = null;
        [UI] private readonly Label ScreenFIRE_Label_About_Box = null;
        [UI] private readonly Label VersionTitle_Label_About_Box = null;
        [UI] private readonly Label Version_Label_About_Box = null;
        //[UI] private readonly Box Version_Box_About_Box = null;
        //[UI] private readonly Box Phase_Box_About_Box = null;
        [UI] private readonly Label PhaseTitle_Label_About_Box = null;
        [UI] private readonly Label Phase_Label_About_Box = null;
        [UI] private readonly Button SF_repo_Button_About_Box = null;
        [UI] private readonly Image Image_SF_repo_Button_About_Box = null;
        [UI] private readonly Label Label_SF_repo_Button_About_Box = null;
        [UI] private readonly Button License_Button_About_Box = null;
        [UI] private readonly Image Image_License_Button_About_Box = null;
        [UI] private readonly Label Label_License_Button_About_Box = null;
        [UI] private readonly Label madeWith_Label_About_Box = null;

        private void AssignEvents() {
            DeleteEvent += delegate { Application.Quit(); };

            ssPreview_Button_Screenshot_Box.Clicked += delegate {
                Process.Start(new ProcessStartInfo() {
                    FileName = io.Path.Combine(Common.LocalSave_Settings.Location, Save.MonthDir),
                    UseShellExecute = true,
                    Verb = "open"
                });
            };

            SF_Button_AllMonitors.Clicked
                += async delegate { await Capture(IScreenshotType.AllMonitors); };
            SF_Button_MonitorAtPointer.Clicked
                += async delegate { await Capture(IScreenshotType.MonitorAtPointer); };
            SF_Button_WindowAtPointer.Clicked
                += async delegate { await Capture(IScreenshotType.WindowAtPointer); };
            SF_Button_ActiveWindow.Clicked
                += async delegate { await Capture(IScreenshotType.ActiveWindow); };
            SF_Button_Custom.Clicked += delegate { Program.ScreenFIRE.ShowAll(); };

            SF_repo_Button_About_Box.Clicked += delegate { Link.Open(Common.SF_GitRepo); };
            License_Button_About_Box.Clicked += delegate { Link.Open(Common.SF_License); };

            SaveLocation_FileChooserButton_SaveOptions_Box.CurrentFolderChanged
                += SaveLocation_FileChooserButton_SaveOptions_Box_CurrentFolderChanged;

            bmp_Button_SaveFormat_Popover.Clicked += bmp_Button_SaveFormat_Popover_Clicked;
            png_Button_SaveFormat_Popover.Clicked += png_Button_SaveFormat_Popover_Clicked;
            jpg_Button_SaveFormat_Popover.Clicked += jpg_Button_SaveFormat_Popover_Clicked;

            Switch_AutoDelete1MonthOldFiles_Box_SaveOptions_Box.StateChanged += delegate {
                Common.LocalSave_Settings.AutoDelete1MonthOldFiles
                    = Switch_AutoDelete1MonthOldFiles_Box_SaveOptions_Box.State;
            };


        }
        private void AssignStrings() {
            Title = Strings.Fetch(IStrings.ScreenFIREConfig).Result;

            Screenshot_TabButton.Text = Strings.Fetch(IStrings.Screenshot).Result;

            Label_ssPreview_Button_Screenshot_Box.Text = Strings.Fetch(IStrings.ViewScreenshots).Result;
            _label1.Text = Strings.Fetch(IStrings.ChooseHowYouWouldLikeToFireYourScreenshot_).Result;
            SF_Button_AllMonitors.Label = Strings.Fetch(IStrings.AllMonitors).Result;
            SF_Button_MonitorAtPointer.Label = Strings.Fetch(IStrings.MonitorAtPointer).Result;
            SF_Button_WindowAtPointer.Label = Strings.Fetch(IStrings.WindowAtPointer).Result;
            SF_Button_ActiveWindow.Label = Strings.Fetch(IStrings.ActiveWindow).Result;
            SF_Button_Custom.Label = Strings.Fetch(IStrings.FreeAreaSelection).Result;


            SaveOptions_TabButton.Text = Strings.Fetch(IStrings.SavingOptions).Result;

            Label_MenuButton_SaveOptions_Box.Text
                = SaveFormat.StringWithDesctiption();
            bmp_Button_SaveFormat_Popover.Label = $"{Strings.Fetch(IStrings.Original).Result} {Common.RangeDash} bmp";
            png_Button_SaveFormat_Popover.Label = $"{Strings.Fetch(IStrings.Quality).Result} {Common.RangeDash} png";
            jpg_Button_SaveFormat_Popover.Label = $"{Strings.Fetch(IStrings.Efficiency).Result} {Common.RangeDash} jpg";
            gif_Button_SaveFormat_Popover.Label = $"{Strings.Fetch(IStrings.Animated).Result} {Common.RangeDash} gif";
            mp4_Button_SaveFormat_Popover.Label = $"{Strings.Fetch(IStrings.Video).Result} {Common.RangeDash} mp4";

            Label_AutoSaveExisting_Box_SaveOptions_Box.Text = Strings.Fetch(IStrings.AutoDelete1MonthOldFiles).Result;

            About_TabButton.Text = Strings.Fetch(IStrings.About).Result;

            ScreenFIRE_Label_About_Box.Text = Strings.Fetch(IStrings.ScreenFIRE_Stylized).Result;

            VersionTitle_Label_About_Box.Text = Strings.Fetch(IStrings.Version).Result + ":";
            Version_Label_About_Box.Text = Common.VersionString(includePhase: false);
            PhaseTitle_Label_About_Box.Text = Strings.Fetch(IStrings.Phase).Result + ":";
            Phase_Label_About_Box.Text = Common.PhaseString();
            madeWith_Label_About_Box.Text = Strings.Fetch(IStrings.MadeWith_NET_GTK_).Result;

            Label_SF_repo_Button_About_Box.Text = Strings.Fetch(IStrings.ScreenFIRERepositoryAtGitHub).Result;
            Label_License_Button_About_Box.Text = Strings.Fetch(IStrings.GNUGeneralPublicLicensev3_0___).Result;

        }
        private void AssignImages() {
            //! = LogoImage ==========================
            LogoImage.Pixbuf
                = new Gdk.Pixbuf(SF.Logo)
                            .ScaleSimple(128, 128, Gdk.InterpType.Bilinear);
            //! ======================================

            ////! = About_svg_Image ====================
            //Gdk.Pixbuf footerPixbuf = new(SF.footer_svg);
            //System.Drawing.Size footerSize
            //    = mathMisc.ScaleToHeight(new(footerPixbuf.Width, footerPixbuf.Height), 128);
            //About_svg_Image.Pixbuf
            //    = footerPixbuf.ScaleSimple(footerSize.Width, footerSize.Height, Gdk.InterpType.Bilinear);
            ////! ======================================

            //! = Image_SF_repo_Button_About_Box =====
            Gdk.Pixbuf SF_repo_Button_Image_Pixbuf = new(icons.GitHub_svg);
            (double w, double h) SF_repo_Button_Image_Size
                = mathMisc.Scale.Fit((SF_repo_Button_Image_Pixbuf.Width, SF_repo_Button_Image_Pixbuf.Height), (24, 24));
            Image_SF_repo_Button_About_Box.Pixbuf
                = SF_repo_Button_Image_Pixbuf
                    .ScaleSimple((int)SF_repo_Button_Image_Size.w,
                                 (int)SF_repo_Button_Image_Size.h,
                                 Gdk.InterpType.Bilinear);
            //! ======================================

            //! = Image_License_Button_About_Box =====
            Gdk.Pixbuf Image_License_Button_About_Box_Pixbuf = new(icons.Balance_png);
            (double w, double h) Image_License_Button_About_Box_Size
                = mathMisc.Scale.Fit((Image_License_Button_About_Box_Pixbuf.Width, Image_License_Button_About_Box_Pixbuf.Height), (24, 24));
            Image_License_Button_About_Box.Pixbuf
                = Image_License_Button_About_Box_Pixbuf
                    .ScaleSimple((int)Image_License_Button_About_Box_Size.w,
                                 (int)Image_License_Button_About_Box_Size.h,
                                 Gdk.InterpType.Bilinear);
            //! ======================================

        }
        private void AssignEtc() {
            SaveLocation_FileChooserButton_SaveOptions_Box
                .SetCurrentFolder(Common.LocalSave_Settings.Location);
        }

        public Config() : this(new Builder("Config.glade")) { }
        private Config(Builder builder)
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
            Thread.Sleep(1000); //! Temp (For Windows) make sure the window is fully hidden

            using var ss = new Screenshot(screenshotType);
            if (await Save.Local(ss, this)) {
                _label1.Text = await Strings.Fetch(IStrings.FiredAScreenshot_);
                _ = new Timer(async (object obj) => {
                    _label1.Text = await Strings.Fetch(IStrings.ChooseHowYouWouldLikeToFireYourScreenshot_);
                }, null, 5000, Timeout.Infinite);

                var allocatedWidth = Image_ssPreview_Button_Screenshot_Box.AllocatedWidth;
                var (w, h) = mathMisc.Scale.Fit((ss.Image.Width, ss.Image.Height), ((allocatedWidth > 200 ? allocatedWidth : 270), 256));

                Label_ssPreview_Button_Screenshot_Box.Destroy();
                Image_ssPreview_Button_Screenshot_Box.Visible = true;
                Image_ssPreview_Button_Screenshot_Box.Pixbuf =
                     ss.Image.ScaleSimple((int)w, (int)h, Gdk.InterpType.Bilinear);
            } else {
                MessageDialog failDialog = new(this,
                                               DialogFlags.Modal,
                                               MessageType.Warning,
                                               ButtonsType.Ok,
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

        public void png_Button_SaveFormat_Popover_Clicked(object sender, EventArgs e) {
            Common.LocalSave_Settings.Format = ISaveFormat.png;
            Common.LocalSave_Settings.Save();
            Label_MenuButton_SaveOptions_Box.Text = png_Button_SaveFormat_Popover.Label;
            SaveFormat_Popover.Hide();
        }
        public void jpg_Button_SaveFormat_Popover_Clicked(object sender, EventArgs e) {
            Common.LocalSave_Settings.Format = ISaveFormat.jpeg;
            Common.LocalSave_Settings.Save();
            Label_MenuButton_SaveOptions_Box.Text = jpg_Button_SaveFormat_Popover.Label;
            SaveFormat_Popover.Hide();
        }
        public void bmp_Button_SaveFormat_Popover_Clicked(object sender, EventArgs e) {
            Common.LocalSave_Settings.Format = ISaveFormat.bmp;
            Common.LocalSave_Settings.Save();
            Label_MenuButton_SaveOptions_Box.Text = bmp_Button_SaveFormat_Popover.Label;
            SaveFormat_Popover.Hide();
        }


    }
}
