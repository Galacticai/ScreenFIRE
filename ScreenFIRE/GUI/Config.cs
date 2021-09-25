using Gtk;
using ScreenFIRE.Assets;
using ScreenFIRE.Assets.Embedded;
using ScreenFIRE.Modules.Capture;
using ScreenFIRE.Modules.Capture.Companion;
using ScreenFIRE.Modules.Companion;
using ScreenFIRE.Modules.Companion.math;
using ScreenFIRE.Modules.Save;
using System.Threading;
using System.Threading.Tasks;
using UI = Gtk.Builder.ObjectAttribute;

namespace ScreenFIRE.GUI {

    class Config : ApplicationWindow {
        [UI] private readonly Label _label1 = null;
        [UI] private readonly Image LogoImage = null;
        [UI] private readonly Label Screenshot_TabButton = null;
        [UI] private readonly Label SaveOptions_TabButton = null;
        [UI] private readonly Label About_TabButton = null;
        [UI] private readonly Label ScreenFIRE_Label_About_Box = null;
        [UI] private readonly Label VersionTitle_Label_About_Box = null;
        [UI] private readonly Label Version_Label_About_Box = null;
        [UI] private readonly Label PhaseTitle_Label_About_Box = null;
        [UI] private readonly Label Phase_Label_About_Box = null;
        [UI] private readonly Button SF_repo_Button_About_Box = null;
        [UI] private readonly Image Image_SF_repo_Button_About_Box = null;
        [UI] private readonly Label Label_SF_repo_Button_About_Box = null;
        [UI] private readonly Button License_Button_About_Box = null;
        [UI] private readonly Image Image_License_Button_About_Box = null;
        [UI] private readonly Label Label_License_Button_About_Box = null;
        [UI] private readonly Button SF_Button_AllMonitors = null;
        [UI] private readonly Button SF_Button_MonitorAtPointer = null;
        [UI] private readonly Button SF_Button_WindowAtPointer = null;
        [UI] private readonly Button SF_Button_ActiveWindow = null;
        [UI] private readonly Button SF_Button_Custom = null;
        [UI] private readonly Button bmp_Button_SaveFormat_Popover = null;
        [UI] private readonly Button png_Button_SaveFormat_Popover = null;
        [UI] private readonly Button jpg_Button_SaveFormat_Popover = null;
        [UI] private readonly Button gif_Button_SaveFormat_Popover = null;
        [UI] private readonly Button mp4_Button_SaveFormat_Popover = null;

        private static string[] txt_privatenameusedonlybythisfunction_238157203985ty9486t4 = null;
        private static async Task<string> txt(int index) {
            return (txt_privatenameusedonlybythisfunction_238157203985ty9486t4
                   ??= (await Strings.Fetch(IStrings.ScreenFIREConfig,//0
                                            IStrings.FiredAScreenshot_,//1
                                            IStrings.ThisButtonHasBeenClicked,//2
                                            IStrings.times_1,//3
                                            IStrings.times_2,//4
                                            IStrings.SomethingWentWrong___,//5
                                            IStrings.ChooseHowYouWouldLikeToFireYourScreenshot_,//6
                                            IStrings.AllMonitors,//7
                                            IStrings.MonitorAtPointer,//8
                                            IStrings.WindowAtPointer,//9
                                            IStrings.ActiveWindow,//10
                                            IStrings.FreeAreaSelection,//11
                                            IStrings.Screenshot,//12
                                            IStrings.SavingOptions,//13
                                            IStrings.About,//14
                                            IStrings.ScreenFIRERepositoryAtGitHub, //15
                                            IStrings.Original, //16
                                            IStrings.Quality, //17
                                            IStrings.Efficiency, //18
                                            IStrings.Animated, //19
                                            IStrings.Video, //20
                                            IStrings.Version, //21
                                            IStrings.Phase, //22
                                            IStrings.ScreenFIRE_Stylized, //23
                                            IStrings.GNUGeneralPublicLicensev3_0___ //24
                                            )
                        )
                    )[index];
        }
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

            SF_repo_Button_About_Box.Clicked += delegate { OpenLink.Run(Common.SF_GitRepo); };
            License_Button_About_Box.Clicked += delegate { OpenLink.Run(Common.SF_License); };
        }
        private void AssignStrings() {
            Title = txt(0).Result;

            Screenshot_TabButton.Text = txt(12).Result;
            SaveOptions_TabButton.Text = txt(13).Result;
            About_TabButton.Text = txt(14).Result;

            ScreenFIRE_Label_About_Box.Text = txt(23).Result;

            VersionTitle_Label_About_Box.Text = txt(21).Result;
            Version_Label_About_Box.Text = Common.VersionString(includePhase: false);
            PhaseTitle_Label_About_Box.Text = txt(22).Result;
            Phase_Label_About_Box.Text = Common.PhaseString();

            Label_SF_repo_Button_About_Box.Text = txt(15).Result;
            Label_License_Button_About_Box.Text = txt(24).Result;

            _label1.Text = txt(6).Result;
            SF_Button_AllMonitors.Label = txt(7).Result;
            SF_Button_MonitorAtPointer.Label = txt(8).Result;
            SF_Button_WindowAtPointer.Label = txt(9).Result;
            SF_Button_ActiveWindow.Label = txt(10).Result;
            SF_Button_Custom.Label = txt(11).Result;

            bmp_Button_SaveFormat_Popover.Label = $"bmp ({txt(16).Result})";
            png_Button_SaveFormat_Popover.Label = $"png ({txt(17).Result})";
            jpg_Button_SaveFormat_Popover.Label = $"jpg ({txt(18).Result})";
            gif_Button_SaveFormat_Popover.Label = $"gif ({txt(19).Result})";
            mp4_Button_SaveFormat_Popover.Label = $"mp4 ({txt(20).Result})";

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
                = mathMisc.ScaleToHeight(new(SF_repo_Button_Image_Pixbuf.Width, SF_repo_Button_Image_Pixbuf.Height), 24);
            Image_SF_repo_Button_About_Box.Pixbuf
                = SF_repo_Button_Image_Pixbuf
                    .ScaleSimple(SF_repo_Button_Image_Size.Width,
                                 SF_repo_Button_Image_Size.Height,
                                 Gdk.InterpType.Bilinear);
            //! ======================================

            //! = Image_License_Button_About_Box =====
            Gdk.Pixbuf Image_License_Button_About_Box_Pixbuf = new(icons.LicenseBalance_svg);
            System.Drawing.Size Image_License_Button_About_Box_Size
                = mathMisc.ScaleToHeight(new(Image_License_Button_About_Box_Pixbuf.Width, Image_License_Button_About_Box_Pixbuf.Height), 24);
            Image_License_Button_About_Box.Pixbuf
                = Image_License_Button_About_Box_Pixbuf
                    .ScaleSimple(Image_License_Button_About_Box_Size.Width,
                                 Image_License_Button_About_Box_Size.Height,
                                 Gdk.InterpType.Bilinear);
            //! ======================================

        }

        public Config() : this(new Builder("Config.glade")) { }

        private Config(Builder builder) : base(builder.GetRawOwnedObject("Config")) {
            builder.Autoconnect(this);

            AssignEvents();
            AssignStrings();
            AssignImages();
            //if (Languages.CurrentLangIsArabic) {  }
        }

        private int _counter;
        private async Task Capture(IScreenshotType screenshotType) {
            Hide();
            AcceptFocus = false;

            Thread.Sleep(1000); //! temprary to make sure the window is fully hidden

            using var ss = new Screenshot(screenshotType);
            if (!await Save.Local(ss, this)) {
                MessageDialog failDialog = new(this,
                                               DialogFlags.Modal,
                                               MessageType.Warning,
                                               ButtonsType.Ok,
                                               await txt(5));
                failDialog.Run();
                failDialog.Destroy();
            } else {
                _label1.Text = await txt(1) + Common.nn
                             + await txt(2) + " " + (1 + _counter++) + " "
                             + (_counter > 1 ? await txt(4) : await txt(3));
            }

            AcceptFocus = true;
            ShowAll();

        }
    }
}
