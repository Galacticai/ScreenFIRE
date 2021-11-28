using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static ScreenFIRE.Modules.Companion.math.Vision.SVG_Tools.Companion.PathdElement;

namespace ScreenFIRE.Modules.Companion.math.Vision.SVG_Tools.Companion {

    /// <summary> Parse||Convert various <see cref="PathdElement"/>-related things</summary>
    public static class SVGHelper {

        //? https://regex101.com/r/EFxDdq/1
        //? [ZzAaRrMmLlHhVvCcSsGgEeFfDdJjQqTt]((\s{0,})([-+]{0,})(\d+\.|\.|)\d+)+

        private static readonly Regex RealNumber_Regex = new(@"([-+]{0,})(\d+\.|\.|)\d+"),
                                      ZeroAndNumber_Regex = new(@"( 0)(\d)"),
                                      Spaces_Regex = new(@"\s+"),
                                      PathdElement_Type_Regex = new("[ZzAaRrMmLlHhVvCcSsGgEeFfDdJjQqTt]"),
                                      PathdElement_Values_Regex = new($"((\\s{{0,}}){RealNumber_Regex})+"),
                                      PathdElement_Regex = new($"{PathdElement_Type_Regex}{PathdElement_Values_Regex}");

        internal static char GetElementType(this PathdElement pathdElement)
            => pathdElement.Data_Raw[IPathdElement_Data.Type];

        //  internal static int ValueCount(this PathdElement pathdElement)
        //      => ValueCount(pathdElement.Data[0]);
        //  internal static int ValueCount(char PathdElement_Type) {
        //      return char.ToLower(PathdElement_Type) switch {
        //          'm' => 2,
        //          'l' => 2,
        //          'h' => 1,
        //          'v' => 1,
        //          'c' => 6,
        //          's' => 4,
        //          'q' => 4,
        //          't' => 2,
        //          'a' => 7,
        //          'z' => 0,
        //          _ => throw new ArgumentException("Path d element not recognized")
        //      };
        //  }
        //  internal static List<Range> GenerateValueRanges(PathdElement element) {
        //      if (element.Data is null)
        //          throw new InvalidDataException("Path d element is invalid");
        //      List<Range> valueRanges = new();
        //      int[] indexes = element.Data.IndexesOf(" ").ToArray();
        //      int i = 0;
        //      foreach (int index in indexes) {
        //          i++;
        //          int endindex;
        //          try {
        //              endindex = indexes[i];
        //          } catch { endindex = element.Data.Length; }
        //          valueRanges.Add(new(index + 1, endindex));
        //      }
        //      return valueRanges;
        //  }

        internal static string CleanupPathdElement(string pathdElement_Raw, bool multiple = false) {
            char PathdElement_Type = pathdElement_Raw[0];

            if (!PathdElement_Type_Regex.IsMatch(PathdElement_Type.ToString()))
                throw new ArgumentException("Path d element not recognized");

            //? Remove any ,
            pathdElement_Raw = pathdElement_Raw.Replace(',', ' ');
            Console.WriteLine(pathdElement_Raw);

            //? Fix space for " 0xyz"
            while (ZeroAndNumber_Regex.IsMatch(pathdElement_Raw))
                pathdElement_Raw = ZeroAndNumber_Regex.Replace(pathdElement_Raw, "$1 $2");

            if (multiple) return pathdElement_Raw.Trim();


            //? Fix space after element type
            //"M1.2 3 4 ..."  >> "M 1.2 3 4 ..."
            if (pathdElement_Raw.Length > 1) //!? Avoid OutOfRangeException
                if (Regex.IsMatch(pathdElement_Raw[..2], @$"{PathdElement_Type_Regex}\d"))
                    pathdElement_Raw = PathdElement_Type + " " + pathdElement_Raw[1..];

            //? Remove extra spaces
            pathdElement_Raw = Spaces_Regex.Replace(pathdElement_Raw, " ").Trim();

            //? Specific for type A (Elliptical Arc)
            if (char.ToLower(PathdElement_Type) == 'a') {

                //? | init new string
                string pathdElement_Raw_WithSpacesBetweenNumbers = $"{PathdElement_Type} ";

                //? | Pull numbers to clean later
                MatchCollection nbs = RealNumber_Regex.Matches(pathdElement_Raw);

                //? | Parse into the new string with clean numbers
                List<string> fixedElementsList = new();
                foreach (Match nb_m in nbs) {
                    string nb = nb_m.Value;
                    int CurrentNbIndex = fixedElementsList.Count + 1;

                    pathdElement_Raw_WithSpacesBetweenNumbers += $"{Convert.ToSingle(nb)} ";
                }
                pathdElement_Raw = pathdElement_Raw_WithSpacesBetweenNumbers;
            }

            return pathdElement_Raw.Trim();
        }

        /// <param name="pathd"> Raw value of <c>&lt;path d="...this..." /&gt;</c> </param>
        /// <returns> List&lt;<see cref="PathdElement"/>&gt; containing <br/>
        /// the the path d elements from <paramref name="pathd"/> <see cref="string"/> </returns>
        /// <exception cref="ArgumentException"/>
        public static List<PathdElement> ToPathd(string pathd) {
            //? Example path d="..."
            // M 12.75     2.75 a .75 .75 0 00       -1.5 0 V 4.5 H 9.276      a 1.75 1.75 0 00 -.985 .303 L6.596 5.957A.25.25 0 016.455 6H2.353a.75.75 0 100 1.5H3.93L.563 15.18a.762.762 0 00.21.88 c .08 .064 .161 .125 .309 .221 .186 .121 .452 .278 .792 .433 .68 .311 1.662 .62 2.876 .62 a6 .919 6.919 0 002.876 - .62c.34 - .155.606 - .312.792 - .433.15 - .097.23 - .158.31 - .223a.75.75 0 00.209 - .878L5.569 7.5h.886c.351 0 .694 - .106.984 - .303l1.696 - 1.154A.25.25 0 019.275 6h1.975v14.5H6.763a.75.75 0 000 1.5h10.474a.75.75 0 000 - 1.5H12.75V6h1.974c.05 0 .1.015.14.043l1.697 1.154c.29.197.633.303.984.303h.886l - 3.368 7.68a.75.75 0 00.23.896c.012.009 0 0 .002 0a3.154 3.154 0 00.31.206c.185.112.45.256.79.4a7.343 7.343 0 002.855.568 7.343 7.343 0 002.856 - .569c.338 - .143.604 - .287.79 - .399a3.5 3.5 0 00.31 - .206.75.75 0 00.23 - .896L20.07 7.5h1.578a.75.75 0 000 - 1.5h - 4.102a.25.25 0 01 - .14 - .043l - 1.697 - 1.154a1.75 1.75 0 00 - .984 - .303H12.75V2.75zM2.193 15.198a5.418 5.418 0 002.557.635 5.418 5.418 0 002.557 - .635L4.75 9.368l - 2.557 5.83zm14.51 - .024c.082.04.174.083.275.126.53.223 1.305.45 2.272.45a5.846 5.846 0 002.547 - .576L19.25 9.367l - 2.547 5.807z";

            //? Clean a bit
            pathd = CleanupPathdElement(pathd, multiple: true); //? Skip element-specific cleaning

            List<PathdElement> pathData_List = new();

            //? Identify pathd elements
            MatchCollection pathdElement_Collection_Raw
                = Regex.Matches(pathd, PathdElement_Regex.ToString());

            //! No pathd elements were found
            if (pathdElement_Collection_Raw.Count == 0)
                throw new ArgumentException("pathd not recognized");

            //? Add pathd elements to list
            foreach (Match pathdElement_Raw_Match in pathdElement_Collection_Raw) {

                MatchCollection values = Regex.Matches(pathdElement_Raw_Match.Value, RealNumber_Regex.ToString());

                //  int valuesCount = Regex.Matches(pathdElement_Raw_Match.Value, RealNumber_Regex.ToString()).Count,
                //      requiredValuesCount = ValueCount(pathdElement_Raw_Match.Value[0]),
                //      pathdElement_Raw_ValuesParts_Count = valuesCount / requiredValuesCount;
                //
                //  List<string> pathdElement_Raw_Parts = new();
                //  //!? Example:
                //  // values: 1 2 3 4 5 6 1 2 3 4 5 6 1 2 3 4 5 6
                //  //      |> Count: 18
                //  // Required: 6
                //  //      |> Elements == (Count / Required) ==  (18 / 6)  == 3
                //  //? (1 2 3 4 5 6) (1 2 3 4 5 6) (1 2 3 4 5 6)
                //  for (int i = 0; i < pathdElement_Raw_ValuesParts_Count; i++) {
                //
                //      //? (i = current element) * (requiredValuesCount)
                //      int offset = i * requiredValuesCount;
                //
                //      //? >>A<< 1 2 3 ...
                //      string pathdElement_Raw_Part = pathdElement_Raw_Match.Value[0].ToString();
                //
                //      //? (1) (2) (3) (4) (5) (6)
                //      for (int j = 0; j < requiredValuesCount; j++)
                //          pathdElement_Raw_Part += $" {values[j + offset]}";
                //
                //      //!? ⚠️ Must clean string before adding to values list
                //      //!? ⚠️ BUT Cannot  clean string (unsupported) if
                //      //!?     values count is more than `requiredValuesCount`
                //      pathdElement_Raw_Part = CleanupPathdElement(pathdElement_Raw_Part);
                //      pathdElement_Raw_Parts.Add(pathdElement_Raw_Part);
                //  }

                //  foreach (string pathdElement_Raw_Part in pathdElement_Raw_Parts) {
                //? Parse
                PathdElement pathdElement
                    = ToPathdElement(pathdElement_Raw_Match.Value);//pathdElement_Raw_Part);

                //? Add to list
                pathData_List.Add(pathdElement);
                //  }
            }
            return pathData_List;
        }

        /// <summary>
        /// Convert a <see cref="string"/>[] (Containing point data) to the corresponding type in <see cref="Pathd"/> </summary>
        /// <param name="pathdElement_Clean"> Raw path d element <see cref="string"/>[] to be processed </param>
        /// <returns> <see cref="PathdElement"/> from <paramref name="pathdElement_Clean"/> <see cref="string"/>[] (Parts array) </returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="FormatException"/> <exception cref="OverflowException"/>
        public static PathdElement ToPathdElement(string pathdElement_Clean) {
            string pathdElement_Raw_Cleaned = CleanupPathdElement(pathdElement_Clean);

            string[] values = Array.Empty<string>();


            //? Ignore empty values
            var values_NotProcessed = Regex.Matches(pathdElement_Raw_Cleaned[1..], RealNumber_Regex.ToString());
            foreach (Match match in values_NotProcessed)
                if (!string.IsNullOrEmpty(match.Value) | match.Value != " ")
                    values = values.Append(match.Value).ToArray();

            char PathdElement_Type = pathdElement_Raw_Cleaned[0];

            //? A 1 2 3 006 7
            if (char.ToLower(PathdElement_Type) == 'a' & values.Length < 7) {
                if (values.Length == 5) {
                    values = new[] { values[0], values[1], values[2],           //? A 1 2 3
                                     (values[3])[0].ToString(),                 //? (0)06
                                     (values[3])[1].ToString(),                 //? 0(0)6
                                     (values[3])[2..],                          //? 00(6...)
                                     values[4],                                 //? 7
                                   };
                } else if (values.Length == 6) {
                    values = new[] { values[0], values[1], values[2], values[3],//? A 1 2 3 0
                                     (values[4])[0].ToString(),                 //? (0)6
                                     (values[4])[1..],                          //? 0(6...)
                                     values[5]                                  //? 7
                                   };
                }
            }

            //? Upper = Absolute | Lower = Relaive
            bool IsRelative = char.IsLower(PathdElement_Type);

            PathdElement pathdElement = char.ToLower(PathdElement_Type) switch {
                #region Convertion to Type
                'm' => PathdElement.MoveTo(                          //! M
                            Convert.ToSingle(values[0]),             //? x
                            Convert.ToSingle(values[1]),             //? y
                            IsRelative),
                'l' => PathdElement.LineTo.Free(                     //! L
                            Convert.ToSingle(values[0]),             //? x
                            Convert.ToSingle(values[1]),             //? y
                            IsRelative),
                'h' => PathdElement.LineTo.Horizontal(               //! H
                            Convert.ToSingle(values[0]),             //? x
                            IsRelative),
                'v' => PathdElement.LineTo.Vertical(                 //! V
                            Convert.ToSingle(values[0]),             //? y
                            IsRelative),
                'c' => PathdElement.CurveTo.Regular(                 //! C
                            Convert.ToSingle(values[0]),             //? x1
                            Convert.ToSingle(values[1]),             //? y1
                            Convert.ToSingle(values[2]),             //? x2
                            Convert.ToSingle(values[3]),             //? y2
                            Convert.ToSingle(values[4]),             //? x
                            Convert.ToSingle(values[5]),             //? y
                            IsRelative),
                's' => PathdElement.CurveTo.Smooth(                  //! S
                            Convert.ToSingle(values[0]),             //? x2
                            Convert.ToSingle(values[1]),             //? y2
                            Convert.ToSingle(values[2]),             //? x
                            Convert.ToSingle(values[3]),             //? y
                            IsRelative),
                'q' => PathdElement.CurveTo.QuadraticBezier(         //! Q
                            Convert.ToSingle(values[0]),             //? x1
                            Convert.ToSingle(values[1]),             //? y1
                            Convert.ToSingle(values[2]),             //? x
                            Convert.ToSingle(values[3]),             //? y
                            IsRelative),
                't' => PathdElement.CurveTo.ShorthandQuadraticBezier(//! T
                            Convert.ToSingle(values[1]),             //? x
                            Convert.ToSingle(values[2]),             //? y
                            IsRelative),
                'a' => PathdElement.EllipticalArc(                   //! A
                            Convert.ToSingle(values[0]),             //? rx
                            Convert.ToSingle(values[1]),             //? ry
                            Convert.ToSingle(values[2]),             //? rotation
                            values[3] == "1",                        //? largeArc?
                            values[4] == "1",                        //? sweepArc?
                            Convert.ToSingle(values[5]),             //? x
                            Convert.ToSingle(values[6]),             //? y
                            IsRelative),
                'z' => PathdElement.ClosePath(),                     //! Z
                #endregion
                _ => throw new ArgumentException("Path d element not recognized")
            };

            return pathdElement;
        }
    }
}
