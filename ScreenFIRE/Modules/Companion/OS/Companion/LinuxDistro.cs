using Shell.NET;
using System;

namespace ScreenFIRE.Modules.Companion.OS.Companion {
	public enum ILinuxDistro {
		AlpineLinux,
		AmazonLinux,
		Arch,
		CentOS7,
		Debian,
		ElementaryOS,
		Fedora,
		LinuxMint,
		OpenSUSE,
		OracleLinux,
		SparkyLinux,
		SUSE,
		Ubuntu,
		ZorinOS,

		Other
	}

	public class LinuxDistro {
		public static ILinuxDistro FromID(string linuxDistro)
			=> linuxDistro.ToLower() switch {

				"alpine" => ILinuxDistro.AlpineLinux,
				"amzn" => ILinuxDistro.AmazonLinux,
				"arch" => ILinuxDistro.Arch,
				"centos" => ILinuxDistro.CentOS7,
				"debian" => ILinuxDistro.Debian,
				"elementary" => ILinuxDistro.ElementaryOS,
				"fedora" => ILinuxDistro.Fedora,
				"linuxmint" => ILinuxDistro.LinuxMint,
				"ol" => ILinuxDistro.OracleLinux,

				"opensuse" => ILinuxDistro.OpenSUSE,
				"opensuse-leap" => ILinuxDistro.OpenSUSE,
				"suse opensuse" => ILinuxDistro.OpenSUSE,

				"rhel fedora" => ILinuxDistro.Fedora,
				"sparky" => ILinuxDistro.SparkyLinux,
				"suse" => ILinuxDistro.SUSE,
				"ubuntu" => ILinuxDistro.Ubuntu,
				"zorin" => ILinuxDistro.ZorinOS,


				_ => ILinuxDistro.Other
			};


		/// <summary> Information about the currently running Linux OS </summary>
		public record struct Current {

			private static Bash _Bash = null;
			private static Bash Bash => _Bash ??= new();


			private static string _KernelString => Bash.Command("uname -r").Output;

			/// <summary> Indicates whether this linux is running in WSL (Windows Subsystem for Linux) </summary>
			public static bool IsWSL => _KernelString.Contains("Microsoft");

			/// <summary> <c> uname -r </c> >> (Trimmed down to version only) <br/>
			///     Kernel version <br/><br/>
			///     # Example: 5.13.0 (as <see cref="System.Version"/>)
			/// </summary>
			public static Version Kernel {
				get {
					string output = _KernelString;
					return Version.Parse(output[..output.IndexOf("-")]);
				}
			}

			private static string ParseReleaseCommandString(string variable)
				=> ". /etc/*-release && echo $" + variable;


			/// <summary> <c> >> /etc/*-release >> $ID </c> <br/>
			///     ID of this distro <br/><br/>
			///     # Example: ubuntu
			/// </summary>
			public static ILinuxDistro ID
				=> FromID(Bash.Command(ParseReleaseCommandString("ID")).Output);

			/// <summary> <c> >> /etc/*-release >> $ID_LIKE </c> <br/>
			///     ID of the distro this is based on <br/><br/>
			///     # Example: debian (<see cref="string"/>)
			/// </summary>
			public static ILinuxDistro BaseID
				=> FromID(Bash.Command(ParseReleaseCommandString("ID_LIKE")).Output);

			/// <summary> <c> >> /etc/*-release >> $VERSION_ID </c> <br/>
			///     Version presented in a numerical way <br/><br/>
			///     # Example: 20.04 (as <see cref="System.Version"/>) <br/>
			///     <br/>
			///  !!!  Except Arch and CentOS 5~6 AND maybe others ¯\_(ツ)_/¯
			/// </summary>
			public static Version Version
				=> Version.Parse(Bash.Command(ParseReleaseCommandString("VERSION_ID")).Output);

			/// <summary> <c> >> /etc/*-release >> $PRETTY_NAME </c> <br/>
			///     Name of this distro <br/><br/>
			///     # Example: Ubuntu 20.04 LTS
			/// </summary>
			public static string Name
				=> Bash.Command(ParseReleaseCommandString("PRETTY_NAME")).Output;

			/// <summary> <c> >> /etc/*-release >> $HOME_URL </c> <br/>
			///     Home page URL (<see cref="string"/>) of this distro <br/><br/>
			///     # Example: https://www.ubuntu.com/ (as <see cref="string"/>)
			/// </summary>
			public static string Homepage
				=> Bash.Command(ParseReleaseCommandString("HOME_URL")).Output;

		}
	}
}


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