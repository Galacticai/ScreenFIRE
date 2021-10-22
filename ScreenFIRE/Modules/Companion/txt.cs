using ScreenFIRE.Modules.Companion.math;
using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace ScreenFIRE.Modules.Companion {

    public static class Txt {

        public static byte[] ParseSVGBytes(this string pathSVG,
                                           (int Width, int Height) Size,
                                           Color fillColor = default) {
            return Encoding.UTF8.GetBytes(pathSVG.ParseSVG(Size, fillColor));
        }
        public static string ParseSVG(this string pathSVG,
                                      (int Width, int Height) Size,
                                      Color fillColor = default) {
            if (fillColor == default) fillColor = Color.Gray;
            string fillColorHex = fillColor.A.ToString("X2") + fillColor.R.ToString("X2") + fillColor.G.ToString("X2") + fillColor.B.ToString("X2");
            return $"<svg width=\"{Size.Width}\" height=\"{Size.Height}\" viewBox=\"0 0 {Size.Width} {Size.Height}\" fill=\"solid\" xmlns=\"http://www.w3.org/2000/svg\">" + Common.n
                     + $"<path d=\"{pathSVG}\" fill=\"{fillColorHex}\"/>" + Common.n
                  + "</svg>";
        }
        public static string NoDashes(this string input) {
            string[] guidSplit = input.ToString().Split('-');
            string result = string.Empty;
            foreach (string part in guidSplit)
                result += part;
            if (result == string.Empty)
                return input;
            return result;
        }

        public static string FileMakeUnique(this string path, Guid? GUID = null) {
            if (!File.Exists(path)) return path;
            GUID ??= Guid.NewGuid();
            string result = path;

            string suffix = "_";
            foreach (char ch in GUID.ToString().NoDashes()) {
                suffix += ch;
                result = path.FileAddSuffix(suffix);
                if (!File.Exists(result))
                    //? Stop once it becomes unique
                    break;
                //? Or keep going until full GUID is added
            }
            return result;
        }
        public static string[] DirectoryFileExt(this string path) {
            string directory = Path.GetDirectoryName(path),
                   name = Path.GetFileNameWithoutExtension(path),
                   extension = Path.GetExtension(path);
            return new[] { directory, name, extension };
        }

        public static string FileAddSuffix(this string path, string addition) {
            string[] dirfileext = path.DirectoryFileExt();

            //? No extension => Simple strings addition
            if (string.IsNullOrEmpty(dirfileext[2]))
                return path + addition;

            return Path.Combine(dirfileext[0], dirfileext[1] + addition + dirfileext[2]);
        }

        /// <summary> Remove everything except the first line of a string </summary>
        /// <param name="input"> Target </param>
        /// <returns> [[0-----------]]\n </returns>
        public static string GetFirstLine(this string input) {
            //! Foolproof
            if (string.IsNullOrEmpty(input)) return null;
            //! Failsafe: return if only 1 line
            if (!input.Contains('\n')) return input;
            //? Do
            string[] inputSplit = input.Split(Common.n);
            for (int index = 0; index < inputSplit.Length; index++) {
                if (string.IsNullOrEmpty(inputSplit[0])) continue;
                return inputSplit[index];
            }
            return input;
        }

        /// <summary> Remove the first line of a string </summary>
        /// <param name="input"> Target </param>
        /// <returns> 0---------\n[[---------- ...]] </returns>
        public static string CutFirstLine(this string input) {
            //! Foolproof
            if (string.IsNullOrEmpty(input)) return null;
            //! Failsafe: return if only 1 line
            if (input.IndexOf(Environment.NewLine) < 1) return input;

            //? Do
            // ---- ----[[\n---- ----]]
            return input[(input.IndexOf(Environment.NewLine) + 1)..];
        }

        /// <summary> Remove the first word of a string </summary>
        /// <param name="input"> Target </param>
        /// <returns> 0---- [[--- ------ --- ...]] </returns>
        public static string CutFirstWord(this string input) {

            //! Foolproof
            if (string.IsNullOrEmpty(input)) return null;
            //! Failsafe: return if only 1 line
            if (input.IndexOf(" ") < 1) return input;

            //? Remove double space if present
            int index = 1;
            if (input.ToCharArray()[input.IndexOf(" ")] == input.ToCharArray()[input.IndexOf(" ") + 1])
                index += 1;

            //? Do
            return input[(input.IndexOf(" ") + index)..];
        }
        /// <returns> Random work/loading string </returns>
        public static string RandomWorkText()
            => new string[] {
            "Hold on... We're doing magic!",
            "Stuff... Please be patient...",
            "Wait a second... Magic ongoing...",
            "Things are happening... Please wait.",
            "Things are happening... Hold on...",
            "Working... Please be patient.",
            "Progressing... Wait a moment.",
            "Hold on... Things are happening...",
            "Preparing... It shall take seconds.",
            "Sit tight! This won't take long.",
            "Magic rays everywhere! Please wait.",
            "Magic rays everywhere! Please be patient.",
            "Something is happening... Hold on...",
            "Progressing, we will finish soon",
            "Want a snack? Finishing soon..."
            }.PickRandom();

        /// <returns> Random fact string </returns>
        public static string RandomFactText
            => new string[] {
            "The first oranges weren’t orange.",
            "Samsung uses a butt-shaped robot to test phone durability.",
            "Peanuts aren’t technically nuts.",
            "Titin protein name is 189,819 letters long.",
            "Cats have fewer toes on their back paws.",
            "Blue whales consume half a million calories in one monch.",
            "Cows have no top front teeth.",
            "NASA can email tools to astronauts to 3D print."
            }.PickRandom();


        ///// <returns> Gets current error code initials (XX)-xx without "-" (From S.ErrorCode) </returns>
        //public static string GA_current_workCode
        //    => (string.IsNullOrEmpty(inf.detail.code) | (inf.detail.code.IndexOf("-") < 1))
        //       ? "??" : inf.detail.code[..inf.detail.code.IndexOf("-")];

        /// <summary> Generate SHA512 <see cref="string"/> from <paramref name="input"/> </summary>
        /// <param name="input"> Target </param>
        /// <returns> SHA512 <see cref="string"/> </returns>
        public static string ToSHA512(this string input) {
            using System.Security.Cryptography.SHA512 SHA512 = System.Security.Cryptography.SHA512.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = SHA512.ComputeHash(inputBytes);
            //? Convert the byte array to hexadecimal string
            StringBuilder sb = new();
            for (int i = 0; i < hashBytes.Length; i++)
                sb.Append(hashBytes[i].ToString("X2"));
            return sb.ToString();
        }
    }
}