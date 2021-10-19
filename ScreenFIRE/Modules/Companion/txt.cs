﻿using ScreenFIRE.Modules.Companion.math;
using System;
using System.Text;

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
            return input[..input.IndexOf(Environment.NewLine)];
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
            return input[(input.IndexOf(Environment.NewLine) + 1)..];
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


        ///// <returns>Gets current error code initials (XX)-xx without "-" (From S.ErrorCode)</returns>
        //public static string GA_current_workCode
        //    => (string.IsNullOrEmpty(inf.detail.code) | (inf.detail.code.IndexOf("-") < 1))
        //       ? "??" : inf.detail.code[..inf.detail.code.IndexOf("-")];

        /// <summary> Generate SHA512 <see cref="string"/> from <paramref name="input"/> </summary>
        /// <param name="input"> Target <see cref="string"/> to be processed</param>
        /// <returns> SHA512 <see cref="string"/> </returns>
        public static string ToSHA512(string input) {
            using System.Security.Cryptography.SHA512 SHA512 = System.Security.Cryptography.SHA512.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = SHA512.ComputeHash(inputBytes);
            //! Convert the byte array to hexadecimal string
            StringBuilder sb = new();
            for (int i = 0; i < hashBytes.Length; i++)
                sb.Append(hashBytes[i].ToString("X2"));
            return sb.ToString();
        }
    }
}