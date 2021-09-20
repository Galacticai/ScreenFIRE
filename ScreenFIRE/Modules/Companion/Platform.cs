using System;
using System.Runtime.InteropServices;

namespace ScreenFIRE.Modules.Companion {

    /// <summary> Common class for general ScreenFIRE stuff </summary>
    public static class Platform {

        //!? TESTS 
        //! >> $ID is common (All)
        //! >> $VERSION_ID is common (Except Arch || CentOS 6~5)

        //! >> Can be used
        //! $ . /etc/*-release && echo $name
        //? (output: value of name)

        //!? Ubuntu 20.04 ===================================
        //! $ cat /etc/*-release
        //  DISTRIB_ID=Ubuntu
        //  DISTRIB_RELEASE=20.04
        //  DISTRIB_CODENAME=focal
        //  DISTRIB_DESCRIPTION = "Ubuntu 20.04 LTS"
        //  NAME="Ubuntu"
        //  VERSION="20.04 LTS (Focal Fossa)"
        //  ID=ubuntu
        //  ID_LIKE = debian
        //  PRETTY_NAME="Ubuntu 20.04 LTS"
        //  VERSION_ID="20.04"
        //  HOME_URL="https://www.ubuntu.com/"
        //  SUPPORT_URL="https://help.ubuntu.com/"
        //  BUG_REPORT_URL="https://bugs.launchpad.net/ubuntu/"
        //  PRIVACY_POLICY_URL="https://www.ubuntu.com/legal/terms-and-policies/privacy-policy"
        //  VERSION_CODENAME=focal
        //  UBUNTU_CODENAME = focal
        //
        //! $ cat /etc/*-release | grep "DISTRIB_ID="
        //  DISTRIB_ID=Ubuntu
        //
        //! $ cat /etc/*-release | grep "ID_LIKE="
        //  ID_LIKE=debian
        //
        //! $ cat /etc/*-release | grep "VERSION_ID="
        //  VERSION_ID="20.04"
        //!? ================================================

        //!? Arch v?? ======================================= 
        //! $ cat /etc/os-release
        //  NAME="Arch Linux"
        //  ID=arch
        //  PRETTY_NAME="Arch Linux"
        //  ANSI_COLOR="0;36"
        //  HOME_URL="https://www.archlinux.org/"
        //  SUPPORT_URL="https://bbs.archlinux.org/"
        //  BUG_REPORT_URL="https://bugs.archlinux.org/" 
        //
        //? Not installed by default! `lsb-release`
        //! $ cat /etc/lsb-release
        //  LSB_VERSION=1.4-14
        //  DISTRIB_ID=Arch
        //  DISTRIB_RELEASE = rolling
        //  DISTRIB_DESCRIPTION="Arch Linux"
        //
        //? Empty: `/etc/arch-version`
        //!? ================================================

        //!? Amazon Linux 2016.09 =========================== 
        //! $ cat /etc/os-release
        //  NAME="Amazon Linux AMI"
        //  VERSION="2016.09"
        //  ID="amzn"
        //  ID_LIKE="rhel fedora"
        //  VERSION_ID="2016.09"
        //  PRETTY_NAME="Amazon Linux AMI 2016.09"
        //  ANSI_COLOR="0;33"
        //  CPE_NAME="cpe:/o:amazon:linux:2016.09:ga"
        //  HOME_URL="http://aws.amazon.com/amazon-linux-ami/"
        //
        //? Empty: cat /etc/lsb-release 
        //
        //! $ cat /etc/system-release
        //  Amazon Linux AMI release 2016.09
        //!? ================================================

        //!? CentOS 7 ======================================= 
        //! $ cat /etc/os-release
        //  NAME="CentOS Linux"                                                                                                        
        //  VERSION="7 (Core)"
        //  ID="centos"
        //  ID_LIKE="rhel fedora"
        //  VERSION_ID="7"
        //  PRETTY_NAME="CentOS Linux 7 (Core)"
        //  ANSI_COLOR="0;31"
        //  CPE_NAME="cpe:/o:centos:centos:7"
        //  HOME_URL="https://www.centos.org/"
        //  BUG_REPORT_URL="https://bugs.centos.org/"
        //  
        //  CENTOS_MANTISBT_PROJECT="CentOS-7"
        //  CENTOS_MANTISBT_PROJECT_VERSION="7"
        //  REDHAT_SUPPORT_PRODUCT="centos"
        //  REDHAT_SUPPORT_PRODUCT_VERSION="7" 
        //
        //? Does not exist: /etc/lsb-release
        //!? ================================================

        //!? CentOS 6 and 5 =================================
        //? Does not exist: /etc/os-release  
        //
        //! $ cat /etc/lsb-release
        //  LSB_VERSION=base-4.0-amd64:base-4.0-noarch:core-4.0-amd64:core-4.0-noarch 
        //
        //! $ cat /etc/centos-release
        //  CentOS release 6.7 (Final)
        //!? ================================================

        //!? Fedora 22 ======================================
        //! $ cat  /etc/os-release  
        //  NAME=Fedora
        //  VERSION="22 (Twenty Two)"
        //  ID=fedora
        //  VERSION_ID = 22
        //  PRETTY_NAME="Fedora 22 (Twenty Two)"
        //  ANSI_COLOR="0;34"   
        //  CPE_NAME="cpe:/o:fedoraproject:fedora:22"
        //  HOME_URL="https://fedoraproject.org/"
        //  BUG_REPORT_URL="https://bugzilla.redhat.com/"
        //  REDHAT_BUGZILLA_PRODUCT="Fedora"
        //  REDHAT_BUGZILLA_PRODUCT_VERSION=22
        //  REDHAT_SUPPORT_PRODUCT="Fedora"
        //  REDHAT_SUPPORT_PRODUCT_VERSION=22
        //  PRIVACY_POLICY_URL=https://fedoraproject.org/wiki/Legal:PrivacyPolicy
        //
        //? Does not exist: /etc/lsb-release 
        //
        //! $ cat /etc/fedora-release
        //  Fedora release 22 (Twenty Two)
        //!? ================================================

        //!? openSUSE Tumbleweed ============================
        //! $ cat  /etc/os-release  
        //  NAME=openSUSE
        //  VERSION="20150725 (Tumbleweed)"
        //  VERSION_ID="20150725"
        //  PRETTY_NAME="openSUSE 20150725 (Tumbleweed) (x86_64)"
        //  ID=opensuse
        //  ANSI_COLOR = "0;32"
        //  CPE_NAME="cpe:/o:opensuse:opensuse:20150725"
        //  BUG_REPORT_URL="https://bugs.opensuse.org"
        //  HOME_URL="https://opensuse.org/"
        //  ID_LIKE="suse"
        //
        //? Does not exist: /etc/lsb-release 
        //
        //! $ cat /etc/SuSE-release
        //  openSUSE 20150725 (x86_64)
        //  VERSION = 20150725
        //  CODENAME = Tumbleweed
        //  # /etc/SuSE-release is deprecated and will be removed in the future, use /etc/os-release instead
        //!? ================================================

        public record ILinuxVersion_PLACEHOLDER { //! ... PLACEHOLDER
            //!                            Name        Version(Major, Minor)        // PLATFORM ID
            public static readonly Version Ubuntu = new Version(10, 0);          // Win32NT
            public static readonly Version Windows81 = new Version(6, 3);           // Win32NT
        }

        /// <summary> Indicate if the current platform is Unix based </summary>
        public static bool RunningLinux
                => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);




        /// <summary> Windows versions </summary>
        public record IWindowsVersion {
            //!                            Windows     Version(Major, Minor)        // PLATFORM ID
            public static readonly Version Windows10 = new Version(10, 0);          // Win32NT
            public static readonly Version Windows81 = new Version(6, 3);           // Win32NT
            public static readonly Version Windows8 = new Version(6, 2);            // Win32NT
            public static readonly Version Windows7_2008r2 = new Version(6, 1);     // Win32NT
            public static readonly Version WindowsVista_2008 = new Version(6, 0);   // Win32NT
            public static readonly Version Windows2003 = new Version(5, 2);         // Win32NT
            public static readonly Version WindowsXP = new Version(5, 1);           // Win32NT
            public static readonly Version Windows2000 = new Version(5, 0);         // Win32NT
            public static readonly Version WindowsMe = new Version(4, 90);          // Win32Windows
            public static readonly Version Windows98 = new Version(4, 10);          // Win32Windows
            public static readonly Version Windows95_NT40 = new Version(4, 0);      // Win32Windows
        }

        /// <summary> Indicate if the current platform is Win32NT based (Windows NT and above) </summary>
        public static bool RunningWindows
                => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        /// <summary> Indicates if Windows 10 (10.0...) is the current OS</summary>
        public static bool RunningWindows10
            => RunningWindows
                && //! Step into only if running Windows 
                    (Environment.OSVersion.Version.ToString(2)
                        == IWindowsVersion.Windows10.ToString(2));


        /// <summary> Indicates whether the current platform is supported by ScreenFIRE (Linux or Windows) </summary>
        public static bool IsSupported
                => Platform.RunningLinux | Platform.RunningWindows;
    }
}
