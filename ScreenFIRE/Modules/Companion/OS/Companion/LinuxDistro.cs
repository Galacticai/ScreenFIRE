using Shell.NET;
using System;

namespace ScreenFIRE.Modules.Companion.OS.Companion {
    public enum ILinuxDistro {
        Debian,
        SUSE,
        Arch,

        Ubuntu,
        LinuxMint,
        OpenSUSE,
        CentOS7,
        Fedora,
        ElementaryOS,
        ZorinOS,
        OracleLinux,
        SparkyLinux,
        AmazonLinux,

        Other
    }

    public class LinuxDistro {
        public static ILinuxDistro FromID(string linuxDistro)
            => linuxDistro.ToLower() switch {
                "debian" => ILinuxDistro.Debian,
                "suse" => ILinuxDistro.SUSE,
                "arch" => ILinuxDistro.Arch,

                "ubuntu" => ILinuxDistro.Ubuntu,
                "linuxmint" => ILinuxDistro.LinuxMint,
                "opensuse" => ILinuxDistro.OpenSUSE,
                "suse opensuse" => ILinuxDistro.OpenSUSE,
                "opensuse-leap" => ILinuxDistro.OpenSUSE,
                "centos" => ILinuxDistro.CentOS7,
                "fedora" => ILinuxDistro.Fedora,
                "rhel fedora" => ILinuxDistro.Fedora,
                "elementary" => ILinuxDistro.ElementaryOS,
                "zorin" => ILinuxDistro.ZorinOS,
                "ol" => ILinuxDistro.OracleLinux,
                "sparky" => ILinuxDistro.SparkyLinux,
                "amzn" => ILinuxDistro.AmazonLinux,

                _ => ILinuxDistro.Other
            };


        /// <summary> Information about the currently running Linux OS </summary>
        public record struct Current {

            private static Bash _bash = null;
            private static Bash bash => _bash ??= new();


            private static string ParseCommandString(string variable)
                => ". /etc/*-release && echo " + variable;

            /// <summary> <c> >> /etc/*-release >> $ID </c> <br/>
            ///     ID of this distro <br/><br/>
            ///     # Example: ubuntu
            /// </summary>
            public static ILinuxDistro ID
                => FromID(bash.Command(ParseCommandString("ID")).Output);

            /// <summary> <c> >> /etc/*-release >> $ID_LIKE </c> <br/>
            ///     ID of the distro this is based on <br/><br/>
            ///     # Example: debian (<see cref="string"/>)
            /// </summary>
            public static ILinuxDistro BaseID
                => FromID(bash.Command(ParseCommandString("ID_LIKE")).Output);

            /// <summary> <c> >> /etc/*-release >> $VERSION_ID </c> <br/>
            ///     Version presented in a numerical way <br/><br/>
            ///     # Example: 20.04 (<see cref="System.Version"/>) <br/>
            ///     <br/>
            ///  !!!  Except Arch and CentOS 5~6 AND maybe others ¯\_(ツ)_/¯ 
            /// </summary>
            public static Version Version
                => Version.Parse(bash.Command(ParseCommandString("VERSION_ID")).Output);

            /// <summary> <c> >> /etc/*-release >> $PRETTY_NAME </c> <br/>
            ///     Name of this distro <br/><br/>
            ///     # Example: Ubuntu 20.04 LTS
            /// </summary>
            public static string Name
                => bash.Command(ParseCommandString("PRETTY_NAME")).Output;

            /// <summary> <c> >> /etc/*-release >> $HOME_URL </c> <br/>
            ///     Home page URL (<see cref="string"/>) of this distro <br/><br/>
            ///     # Example: https://www.ubuntu.com/ (<see cref="string"/>)
            /// </summary>
            public static string Homepage
                => bash.Command(ParseCommandString("HOME_URL")).Output;

        }
    }
}
