using Gtk;
using ScreenFIRE.Assets;
using ScreenFIRE.Assets.Embedded;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.math;
using ScreenFIRE.Modules.Save;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.GUI {

    class Config : ApplicationWindow {
        [UI] private readonly Image LogoImage = null;

        [UI] private readonly Label Screenshot_TabButton = null;
        [UI] private readonly Image Preview_Image_Screenshot_Box = null;
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
        [UI] private readonly Switch Switch_AutoSaveExisting_Box_SaveOptions_Box = null;

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

            Switch_AutoSaveExisting_Box_SaveOptions_Box.StateChanged += delegate {
                Common.LocalSave_Settings.AutoReplaceExisting
                    = Switch_AutoSaveExisting_Box_SaveOptions_Box.State;
            };


        }
        private void AssignStrings() {
            Title = Strings.Fetch(IStrings.ScreenFIREConfig).Result;

            Screenshot_TabButton.Text = Strings.Fetch(IStrings.Screenshot).Result;

            SaveOptions_TabButton.Text = Strings.Fetch(IStrings.SavingOptions).Result;

            Label_MenuButton_SaveOptions_Box.Text
                = SaveFormat.StringWithDesctiption_From_SaveOptionsFormat();
            bmp_Button_SaveFormat_Popover.Label = $"bmp ({Strings.Fetch(IStrings.Original).Result})";
            png_Button_SaveFormat_Popover.Label = $"png ({Strings.Fetch(IStrings.Quality).Result})";
            jpg_Button_SaveFormat_Popover.Label = $"jpg ({Strings.Fetch(IStrings.Efficiency).Result})";
            gif_Button_SaveFormat_Popover.Label = $"gif ({Strings.Fetch(IStrings.Animated).Result})";
            mp4_Button_SaveFormat_Popover.Label = $"mp4 ({Strings.Fetch(IStrings.Video).Result})";

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

            _label1.Text = Strings.Fetch(IStrings.ChooseHowYouWouldLikeToFireYourScreenshot_).Result;
            SF_Button_AllMonitors.Label = Strings.Fetch(IStrings.AllMonitors).Result;
            SF_Button_MonitorAtPointer.Label = Strings.Fetch(IStrings.MonitorAtPointer).Result;
            SF_Button_WindowAtPointer.Label = Strings.Fetch(IStrings.WindowAtPointer).Result;
            SF_Button_ActiveWindow.Label = Strings.Fetch(IStrings.ActiveWindow).Result;
            SF_Button_Custom.Label = Strings.Fetch(IStrings.FreeAreaSelection).Result;

        }
        private void AssignImages() {
            //! = LogoImage ==========================
            LogoImage.Pixbuf
                = new Gdk.Pixbuf(Vision.BitmapToByteArray(SF.Logo))
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
            System.Drawing.Size SF_repo_Button_Image_Size
                = mathMisc.Scale_Fit(new(SF_repo_Button_Image_Pixbuf.Width, SF_repo_Button_Image_Pixbuf.Height), 24);
            Image_SF_repo_Button_About_Box.Pixbuf
                = SF_repo_Button_Image_Pixbuf
                    .ScaleSimple(SF_repo_Button_Image_Size.Width,
                                 SF_repo_Button_Image_Size.Height,
                                 Gdk.InterpType.Bilinear);
            //! ======================================

            //! = Image_License_Button_About_Box =====
            Gdk.Pixbuf Image_License_Button_About_Box_Pixbuf = new(Vision.BitmapToByteArray(icons.Balance_png));
            System.Drawing.Size Image_License_Button_About_Box_Size
                = mathMisc.Scale_Fit(new(Image_License_Button_About_Box_Pixbuf.Width, Image_License_Button_About_Box_Pixbuf.Height), 24);
            Image_License_Button_About_Box.Pixbuf
                = Image_License_Button_About_Box_Pixbuf
                    .ScaleSimple(Image_License_Button_About_Box_Size.Width,
                                 Image_License_Button_About_Box_Size.Height,
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

        private int _counter;
        private async Task Capture(IScreenshotType screenshotType) {
            Hide();
            AcceptFocus = false;

            Thread.Sleep(1000); //! Temp (For Windows) make sure the window is fully hidden

            using var ss = new Screenshot(screenshotType);
            if (!await Save.Local(ss, this)) {
                MessageDialog failDialog = new(this,
                                               DialogFlags.Modal,
                                               MessageType.Warning,
                                               ButtonsType.Ok,
                                               await Strings.Fetch(IStrings.SomethingWentWrong___));
                failDialog.Run();
                failDialog.Destroy();
            } else {
                _label1.Text = await Strings.Fetch(IStrings.FiredAScreenshot_) + Common.nn
                             + await Strings.Fetch(IStrings.ThisButtonHasBeenClicked) + " " + (1 + _counter++) + " "
                             + (_counter <= 1 ? await Strings.Fetch(IStrings.times_1)
                                             : await Strings.Fetch(IStrings.times_2));
            }

            AcceptFocus = true;
            ShowAll();

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
