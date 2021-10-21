using System.Net.Http;
using System.Threading.Tasks;
using cult = System.Globalization.CultureInfo;

namespace ScreenFIRE.Modules.Companion {

    /// <summary>
    /// <list type="bullet">
    /// <item> System • Current system language (<see cref="English"/> if system language not supported) </item>
    /// <item> (Google Translate API supported languages) </item>
    /// <item> Other • Any language not listed </item>
    /// </list>
    /// </summary>
    public enum ILanguages {
        System,

        Afrikaans, Albanian, Amharic, Arabic, Armenian, Azerbaijani,

        Basque, Belarusian, Bengali, Bosnian, Bulgarian,

        Catalan, Cebuano, Chichewa, ChineseSimplified, ChineseTraditional,
        Corsican, Croatian, Czech,

        Danish, Dutch,

        English, Esperanto, Estonian,

        Filipino, Finnish, French, Frisian,

        Gujarati, Galician, Georgian, German, Greek,

        HaitianCreole, Hausa, Hawaiian, Hindi, Hmong, Hungarian,

        Icelandic, Igbo, Indonesian, Irish, Italian,

        Japanese, Javanese,

        Kannada, Kazakh, Khmer, Korean, KurdishKurmanji, Kyrgyz,

        Latin, Luxembourgish, Lao, Lithuanian, Latvian,

        Macedonian, Malagasy, Malay, Malayalam, Maltese, Maori,
        Marathi, Mongolian, MyanmarBurmese,

        Nepali, Norwegian,

        Pashto, Persian, Polish, Portuguese, Punjabi,

        Romanian, Russian,

        Samoan, ScotsGaelic, Serbian, Sesotho, Shona, Sindhi, Sinhala, Slovak,
        Slovenian, Somali, Spanish, Sundanese, Swahili, Swedish,

        Tajik, Tamil, Telugu, Thai, Turkish,

        Ukrainian, Urdu, Uzbek,

        Vietnamese,

        Welsh,

        Xhosa,

        Yiddish, Yoruba,

        Zulu,

        Other, //? Will default to English
    }

    public static class Languages {
        public static ILanguages ToILanguages(this string language)
          => SystemLanguage(language);
        public static string ToGoogleLanguageID(this ILanguages language)
          => GoogleLanguageID(language);

        /// <returns> Current .NET system language <see cref="string"/> code in 2 or 3 letters</returns>
        public static string TwoLetterISOLanguageName
          => cult.InstalledUICulture.TwoLetterISOLanguageName;
        #region Shortcuts
        public static bool CurrentLangIsEnglish => TwoLetterISOLanguageName == "en";
        public static bool CurrentLangIsArabic => TwoLetterISOLanguageName == "ar";
        public static bool CurrentLangIsChinese => TwoLetterISOLanguageName == "zh";
        #endregion


        ///// <summary> Translate a string using Google Translate API </summary>
        ///// <param name="input"> <see cref="string"/> to be translated </param>
        ///// <param name="toLanguage"> Destination language </param>
        ///// <returns> Translated <see cref="string"/> + <see cref="out"/> Detected original language</returns>
        //public static string Translate(string input, ILanguages toLanguage) {
        //    TranslationClient translationclient = TranslationClient.Create();
        //    TranslationResult translationResult
        //            = translationclient.TranslateText(input, ILanguagesToGoogleLanguageCodes(toLanguage));
        //    return translationResult.TranslatedText;
        //}

        public static async Task<string> TranslateText(string input, ILanguages toLanguage = ILanguages.System) {
            if (toLanguage == ILanguages.System) toLanguage = SystemLanguage();

            return string.Empty; //? PLACEHOLDER

            string url = string.Format("https://translate.google.com/?text={0}&tl={1}",
                                        input, toLanguage.ToGoogleLanguageID());
            string result = await new HttpClient().GetStringAsync(url);
            result = result[(result.IndexOf("<span title=\"") + "<span title=\"".Length)..];
            result = result[(result.IndexOf(">") + 1)..];
            result = result[..result.IndexOf("</span>")];
            result = result.Trim();
            return result;
        }

        /// <summary> Convert .NET language code <see cref="string"/> to <see cref="ILanguages"/> readable by ScreenFIRE </summary>
        /// <param name="language"> Language to convert || Will use system language if null </param>
        /// <returns> ScreenFIRE <see cref="ILanguages"/> corresponding to the provided <paramref name="language"/></returns>
        public static ILanguages SystemLanguage(string language = null)
           => (language ?? TwoLetterISOLanguageName) switch {
              //?
              //? Commented = not supported by Google Translate API
              //?
              //// "iv" => ILanguages.InvariantLanguage_InvariantCountry,
              "af" => ILanguages.Afrikaans,
               "am" => ILanguages.Amharic,
               "ar" => ILanguages.Arabic,
              //"as" => ILanguages.Assamese,
              "az" => ILanguages.Azerbaijani,
               "be" => ILanguages.Belarusian,
               "bg" => ILanguages.Bulgarian,
              //"bn" => ILanguages.Bangla,
              //"bo" => ILanguages.Tibetan,
              //"br" => ILanguages.Breton,
              "bs" => ILanguages.Bosnian,
               "ca" => ILanguages.Catalan,
              //"chr" => ILanguages.Cherokee,
              "cs" => ILanguages.Czech,
               "cy" => ILanguages.Welsh,
               "da" => ILanguages.Danish,
               "de" => ILanguages.German,
              //"dsb" => ILanguages.LowerSorbian,
              //"dz" => ILanguages.Dzongkha_Bhutan,
              "el" => ILanguages.Greek,
               "en" => ILanguages.English,
               "es" => ILanguages.Spanish,
               "et" => ILanguages.Estonian,
               "eu" => ILanguages.Basque,
               "fa" => ILanguages.Persian,
              //"ff" => ILanguages.Fulah,
              "fi" => ILanguages.Finnish,
               "fil" => ILanguages.Filipino,
              //"fo" => ILanguages.Faroese,
              "fr" => ILanguages.French,
              //"fy" => ILanguages.WesternFrisian,
              "ga" => ILanguages.Irish,
              //"gd" => ILanguages.ScottishGaelic,
              "gl" => ILanguages.Galician,
              //"gsw" => ILanguages.SwissGerman,
              "gu" => ILanguages.Gujarati,
               "ha" => ILanguages.Hausa,
               "haw" => ILanguages.Hawaiian,
               "hi" => ILanguages.Hindi,
               "hr" => ILanguages.Croatian,
              //"hsb" => ILanguages.UpperSorbian,
              "hu" => ILanguages.Hungarian,
               "hy" => ILanguages.Armenian,
               "id" => ILanguages.Indonesian,
               "ig" => ILanguages.Igbo,
              //"ii" => ILanguages.SichuanYi,
              "is" => ILanguages.Icelandic,
               "it" => ILanguages.Italian,
               "ja" => ILanguages.Japanese,
               "ka" => ILanguages.Georgian,
               "kk" => ILanguages.Kazakh,
              //"kl" => ILanguages.Kalaallisut,
              "km" => ILanguages.Khmer,
               "kn" => ILanguages.Kannada,
               "ko" => ILanguages.Korean,
              //"kok" => ILanguages.Konkani,
              //"ks" => ILanguages.Kashmiri,
              //"ku" => ILanguages.Kurdish,
              "ky" => ILanguages.Kyrgyz,
               "lb" => ILanguages.Luxembourgish,
               "lo" => ILanguages.Lao,
               "lt" => ILanguages.Lithuanian,
               "lv" => ILanguages.Latvian,
               "mi" => ILanguages.Maori,
               "mk" => ILanguages.Macedonian,
               "ml" => ILanguages.Malayalam,
               "mn" => ILanguages.Mongolian,
               "mr" => ILanguages.Marathi,
               "ms" => ILanguages.Malay,
               "mt" => ILanguages.Maltese,
              //"my" => ILanguages.Burmese,
              //"nb" => ILanguages.NorwegianBokmål,
              "ne" => ILanguages.Nepali,
               "nl" => ILanguages.Dutch,
              //"nn" => ILanguages.NorwegianNynorsk,
              //"om" => ILanguages.Oromo,
              //"or" => ILanguages.Odia,
              "pa" => ILanguages.Punjabi,
               "pl" => ILanguages.Polish,
               "ps" => ILanguages.Pashto,
               "pt" => ILanguages.Portuguese,
              //"rm" => ILanguages.Romansh,
              "ro" => ILanguages.Romanian,
               "ru" => ILanguages.Russian,
              //"rw" => ILanguages.Kinyarwanda,
              //"sah" => ILanguages.Sakha,
              "sd" => ILanguages.Sindhi,
              //"se" => ILanguages.NorthernSami,
              "si" => ILanguages.Sinhala,
               "sk" => ILanguages.Slovak,
               "sl" => ILanguages.Slovenian,
              //"smn" => ILanguages.InariSami,
              "so" => ILanguages.Somali,
               "sq" => ILanguages.Albanian,
               "sr" => ILanguages.Serbian,
               "sv" => ILanguages.Swedish,
               "sw" => ILanguages.Swahili,
               "ta" => ILanguages.Tamil,
               "te" => ILanguages.Telugu,
               "tg" => ILanguages.Tajik,
               "th" => ILanguages.Thai,
              //"ti" => ILanguages.Tigrinya,
              //"tk" => ILanguages.Turkmen,
              "tr" => ILanguages.Turkish,
              //"tt" => ILanguages.Tatar,
              //"tzm" => ILanguages.CentralAtlasTamazight,
              //"ug" => ILanguages.Uyghur,
              "uk" => ILanguages.Ukrainian,
               "ur" => ILanguages.Urdu,
               "uz" => ILanguages.Uzbek,
               "vi" => ILanguages.Vietnamese,
              //"wo" => ILanguages.Wolof,
              "xh" => ILanguages.Xhosa,
               "yi" => ILanguages.Yiddish,
               "yo" => ILanguages.Yoruba,
              //"zh" => ILanguages.Chinese,
              "zh" => ILanguages.ChineseSimplified,
               "zu" => ILanguages.Zulu,


               _ => ILanguages.Other
           };


        /// <summary> Convert <see cref="ILanguages"/> to <see cref="string"/> readable by Google Translate API </summary>
        /// <param name="language"> Language to convert </param>
        /// <returns> Google Translate language code <see cref="string"/> corresponding to the provided <paramref name="language"/></returns>
        public static string GoogleLanguageID(ILanguages language = ILanguages.System)
          => (language == ILanguages.System
              ? SystemLanguage() : language) switch {
                  ILanguages.Afrikaans => "af",
                  ILanguages.Albanian => "sq",
                  ILanguages.Amharic => "am",
                  ILanguages.Arabic => "ar",
                  ILanguages.Armenian => "hy",
                  ILanguages.Azerbaijani => "az",
                  ILanguages.Basque => "eu",
                  ILanguages.Belarusian => "be",
                  ILanguages.Bengali => "bn",
                  ILanguages.Bosnian => "bs",
                  ILanguages.Bulgarian => "bg",
                  ILanguages.Catalan => "ca",
                  ILanguages.Cebuano => "ceb",
                  ILanguages.Chichewa => "ny",
                  ILanguages.ChineseSimplified => "zh",
                  ILanguages.ChineseTraditional => "zh-TW",
                  ILanguages.Corsican => "co",
                  ILanguages.Croatian => "hr",
                  ILanguages.Czech => "cs",
                  ILanguages.Danish => "da",
                  ILanguages.Dutch => "nl",
                  ILanguages.English => "en",
                  ILanguages.Esperanto => "eo",
                  ILanguages.Estonian => "et",
                  ILanguages.Filipino => "tl",
                  ILanguages.Finnish => "fi",
                  ILanguages.French => "fr",
                  ILanguages.Frisian => "fy",
                  ILanguages.Galician => "gl",
                  ILanguages.Georgian => "ka",
                  ILanguages.German => "de",
                  ILanguages.Greek => "el",
                  ILanguages.Gujarati => "gu",
                  ILanguages.HaitianCreole => "ht",
                  ILanguages.Hausa => "ha",
                  ILanguages.Hawaiian => "haw",
                  ILanguages.Hindi => "hi",
                  ILanguages.Hmong => "hmn",
                  ILanguages.Hungarian => "hu",
                  ILanguages.Icelandic => "is",
                  ILanguages.Igbo => "ig",
                  ILanguages.Indonesian => "id",
                  ILanguages.Irish => "ga",
                  ILanguages.Italian => "it",
                  ILanguages.Japanese => "ja",
                  ILanguages.Javanese => "jw",
                  ILanguages.Kannada => "kn",
                  ILanguages.Kazakh => "kk",
                  ILanguages.Khmer => "km",
                  ILanguages.Korean => "ko",
                  ILanguages.KurdishKurmanji => "ku",
                  ILanguages.Kyrgyz => "ky",
                  ILanguages.Lao => "lo",
                  ILanguages.Latin => "la",
                  ILanguages.Latvian => "lv",
                  ILanguages.Lithuanian => "lt",
                  ILanguages.Luxembourgish => "lb",
                  ILanguages.Macedonian => "mk",
                  ILanguages.Malagasy => "mg",
                  ILanguages.Malay => "ms",
                  ILanguages.Malayalam => "ml",
                  ILanguages.Maltese => "mt",
                  ILanguages.Maori => "mi",
                  ILanguages.Marathi => "mr",
                  ILanguages.Mongolian => "mn",
                  ILanguages.MyanmarBurmese => "my",
                  ILanguages.Nepali => "ne",
                  ILanguages.Norwegian => "no",
                  ILanguages.Pashto => "ps",
                  ILanguages.Persian => "fa",
                  ILanguages.Polish => "pl",
                  ILanguages.Portuguese => "pt",
                  ILanguages.Punjabi => "pa",
                  ILanguages.Romanian => "ro",
                  ILanguages.Russian => "ru",
                  ILanguages.Samoan => "sm",
                  ILanguages.ScotsGaelic => "gd",
                  ILanguages.Serbian => "sr",
                  ILanguages.Sesotho => "st",
                  ILanguages.Shona => "sn",
                  ILanguages.Sindhi => "sd",
                  ILanguages.Sinhala => "si",
                  ILanguages.Slovak => "sk",
                  ILanguages.Slovenian => "sl",
                  ILanguages.Somali => "so",
                  ILanguages.Spanish => "es",
                  ILanguages.Sundanese => "su",
                  ILanguages.Swahili => "sw",
                  ILanguages.Swedish => "sv",
                  ILanguages.Tajik => "tg",
                  ILanguages.Tamil => "ta",
                  ILanguages.Telugu => "te",
                  ILanguages.Thai => "th",
                  ILanguages.Turkish => "tr",
                  ILanguages.Ukrainian => "uk",
                  ILanguages.Urdu => "ur",
                  ILanguages.Uzbek => "uz",
                  ILanguages.Vietnamese => "vi",
                  ILanguages.Welsh => "cy",
                  ILanguages.Xhosa => "xh",
                  ILanguages.Yiddish => "yi",
                  ILanguages.Yoruba => "yo",
                  ILanguages.Zulu => "zu",

                  //? Other
                  _ => "en"
              };

    }
}
