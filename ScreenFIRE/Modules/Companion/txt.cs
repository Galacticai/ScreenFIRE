using ScreenFIRE.Modules.Companion.math;
using System;

namespace ScreenFIRE.Modules.Companion {

    class txt {

        /// <summary>
        /// Remove everything except the first line of a string
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>[[0-----------]]\n</returns>
        public static string GetFirstLine(string input) {
            // Foolproof
            if (string.IsNullOrEmpty(input)) return null;
            // Failsafe: return if only 1 line
            if (input.IndexOf(Environment.NewLine) < 1) return input;
            // Do
            return input.Substring(0, input.IndexOf(Environment.NewLine));
        }

        /// <summary>
        /// Remove the first line of a string
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>0---------\n[[---------- ...]]</returns>
        public static string CutFirstLine(string input) {
            // Foolproof
            if (string.IsNullOrEmpty(input)) return null;
            // Failsafe: return if only 1 line
            if (input.IndexOf(Environment.NewLine) < 1) return input;

            // Do
            // ' Text Example>>(\n)blabla bla<<
            return input.Substring(input.IndexOf(Environment.NewLine) + 1);
        }

        /// <summary>
        /// Remove the first word of a string
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>0---- [[--- ------ --- ...]]</returns>
        public static string CutFirstWord(string input) {

            // Foolproof
            if (string.IsNullOrEmpty(input)) return null;
            // Failsafe: return if only 1 line
            if (input.IndexOf(" ") < 1) return input;

            // Remove double space if present
            int index = 1;
            if (input.ToCharArray()[input.IndexOf(" ")] == input.ToCharArray()[input.IndexOf(" ") + 1])
                index += 1;

            // Do
            return input.Substring(input.IndexOf(" ") + index, input.Length);
        }
        /// <returns>Random string from a preset String() (open to edit preset array)</returns>
        public static string RandomWorkText
            => mathMisc.Random.FromArray(new[] {
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
            });

        /// <returns>Random string from a preset String() (open to edit preset array)</returns>
        public static string RandomFactText
            => mathMisc.Random.FromArray(new[] {
            "The first oranges weren’t orange.",
            "Samsung uses a butt-shaped robot to test phone durability.",
            "Peanuts aren’t technically nuts.",
            "Titin protein name is 189,819 letters long.",
            "Cats have fewer toes on their back paws.",
            "Blue whales consume half a million calories in one monch.",
            "Cows have no top front teeth.",
            "NASA can email tools to astronauts to 3D print."
            });


        ///// <returns>Gets current error code initials (XX)-xx without "-" (From S.ErrorCode)</returns>
        //public static string GA_current_workCode
        //    => (string.IsNullOrEmpty(inf.detail.code) | (inf.detail.code.IndexOf("-") < 1))
        //       ? "??" : inf.detail.code[..inf.detail.code.IndexOf("-")];


    }
}