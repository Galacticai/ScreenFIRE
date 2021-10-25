using ScreenFIRE.Modules.Companion.math;
using System;
using System.IO;
using System.Text;
using g = Gdk;

namespace ScreenFIRE.Modules.Companion {

    public static class Txt {

        public static bool IsLetter(this char ch) {
            return char.IsLower(ch) | char.IsUpper(ch);
            //return Regex.IsMatch(ch.ToString(), "(A-Z|a-z)");
        }
        public static bool IsNumber(this char ch) {
            return char.IsDigit(ch);
        }

        public static string ParseSVG(string SVGpath,
                                      (int Width, int Height) Size,
                                      g.RGBA fillColor) {
            //fillColor ??= new() { Alpha = 1, Red = 0.5, Green = 0.5, Blue = 0.5 };
            string fillColorHex = ((int)(fillColor.Alpha * 255)).ToString("X2")
                                + ((int)(fillColor.Red * 255)).ToString("X2")
                                + ((int)(fillColor.Green * 255)).ToString("X2")
                                + ((int)(fillColor.Blue * 255)).ToString("X2");

            //return $"<svg width=\"1024\" height=\"1024\" viewBox=\"0 0 1024 1024\" fill=\"none\" xmlns=\"http://www.w3.org/2000/svg\"><path d=\"{SVGpath}\" transform=\"scale(64)\" fill=\"#1B1F23\"/></svg>";

            //return "<svg width=\"1024\" height=\"1024\" viewBox=\"0 0 1024 1024\" fill=\"none\" xmlns=\"http://www.w3.org/2000/svg\"><path fill-rule=\"evenodd\" clip-rule=\"evenodd\" d=\"M8 0C3.58 0 0 3.58 0 8C0 11.54 2.29 14.53 5.47 15.59C5.87 15.66 6.02 15.42 6.02 15.21C6.02 15.02 6.01 14.39 6.01 13.72C4 14.09 3.48 13.23 3.32 12.78C3.23 12.55 2.84 11.84 2.5 11.65C2.22 11.5 1.82 11.13 2.49 11.12C3.12 11.11 3.57 11.7 3.72 11.94C4.44 13.15 5.59 12.81 6.05 12.6C6.12 12.08 6.33 11.73 6.56 11.53C4.78 11.33 2.92 10.64 2.92 7.58C2.92 6.71 3.23 5.99 3.74 5.43C3.66 5.23 3.38 4.41 3.82 3.31C3.82 3.31 4.49 3.1 6.02 4.13C6.66 3.95 7.34 3.86 8.02 3.86C8.7 3.86 9.38 3.95 10.02 4.13C11.55 3.09 12.22 3.31 12.22 3.31C12.66 4.41 12.38 5.23 12.3 5.43C12.81 5.99 13.12 6.7 13.12 7.58C13.12 10.65 11.25 11.33 9.47 11.53C9.76 11.78 10.01 12.26 10.01 13.01C10.01 14.08 10 14.94 10 15.21C10 15.42 10.15 15.67 10.55 15.59C13.71 14.53 16 11.53 16 8C16 3.58 12.42 0 8 0Z\" transform=\"scale(64)\" fill=\"#1B1F23\"/></svg>";

            return $"<svg width=\"{Size.Width}\" height=\"{Size.Height}\" viewBox=\"0 0 {Size.Width} {Size.Height}\" xmlns=\"http://www.w3.org/2000/svg\">"
                     + $"<path d=\"{SVGpath}\" fill=\"#{fillColorHex}\" />"
                  + "</svg>";
        }
        /// <summary> Remove dashes ( <c>-</c> ) from <paramref name="input"/> <see cref="string"/> </summary>
        /// <param name="input"> Target <see cref="string"/> </param>
        /// <returns> <paramref name="input"/> <see cref="string"/> without dashes ( <c>-</c> ) </returns>
        /// <example> XXXXXX-YYYYYY-ZZZZZZ <br/>
        /// => XXXXXXYYYYYYZZZZZZ </example>
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