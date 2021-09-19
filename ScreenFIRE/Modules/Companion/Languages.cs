
using System;
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

        Arabic, Afrikaans, Amharic, Azerbaijani, Armenian, Albanian,

        Belarusian, Bulgarian, Bengali, Bosnian, Basque,

        Catalan, Cebuano, Corsican, Czech, Croatian, Chichewa,
        ChineseSimplified, ChineseTraditional,

        Danish, Dutch,

        English, Esperanto, Estonian,

        Finnish, French, Frisian, Filipino,

        German, Greek, Galician, Gujarati, Georgian,

        Hausa, Hawaiian, Hindi, Hmong, HaitianCreole, Hungarian,

        Irish, Indonesian, Igbo, Icelandic, Italian,

        Japanese, Javanese,

        Kazakh, Khmer, Kannada, Korean, KurdishKurmanji, Kyrgyz,

        Latin, Luxembourgish, Lao, Lithuanian, Latvian,

        Malagasy, Maori, Macedonian, Malayalam, Mongolian, Marathi,
        Malay, Maltese, MyanmarBurmese,

        Nepali, Norwegian,

        Persian, Punjabi, Polish, Pashto, Portuguese,

        Romanian, Russian,

        Spanish, ScotsGaelic, Sindhi, Sinhala, Slovak, Slovenian,
        Samoan, Shona, Somali, Serbian, Sesotho, Sundanese, Swedish, Swahili,

        Tamil, Telugu, Tajik, Thai, Turkish,

        Ukrainian, Urdu, Uzbek,

        Welsh,

        Vietnamese,

        Xhosa,

        Yiddish, Yoruba,

        Zulu,

        Other, //! Will default to English
    }

    public class Languages {

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

        public static async Task<string> TranslateText(string input, ILanguages toLanguage) {
            return string.Empty; //! PLACEHOLDER

            string url = string.Format("https://translate.google.com/?text={0}&tl={1}",
                                        input, ILanguagesToGoogleLanguageCodes(toLanguage));
            string result = await new HttpClient().GetStringAsync(url);
            result = result[(result.IndexOf("<span title=\"") + "<span title=\"".Length)..];
            result = result[(result.IndexOf(">") + 1)..];
            result = result[..result.IndexOf("</span>")];
            result = result.Trim();
            return result;
        }

        /// <returns> <see cref="DotNetToILanguages()"/> </returns>
        [Obsolete("GetSystemLanguage is useless. Just use DotNetToILanguages()")]
        public static ILanguages GetSystemLanguage()
             => DotNetToILanguages();

        /// <summary> Convert .NET language code <see cref="string"/> to <see cref="ILanguages"/> readable by ScreenFIRE </summary>
        /// <param name="language"> Language to convert || Will use system language if null </param>
        /// <returns> ScreenFIRE <see cref="ILanguages"/> corresponding to the provided <paramref name="language"/></returns>
        public static ILanguages DotNetToILanguages(string language = null)
            => (language ?? TwoLetterISOLanguageName) switch {
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
        public static string ILanguagesToGoogleLanguageCodes(ILanguages language)
            => language switch {
                ILanguages.Afrikaans => "af",
                ILanguages.Amharic => "am",
                ILanguages.Arabic => "ar",
                ILanguages.Azerbaijani => "az",
                ILanguages.Belarusian => "be",
                ILanguages.Bulgarian => "bg",
                ILanguages.Bengali => "bn",
                ILanguages.Bosnian => "bs",
                ILanguages.Catalan => "ca",
                ILanguages.Cebuano => "ceb",
                ILanguages.Corsican => "co",
                ILanguages.Czech => "cs",
                ILanguages.Chichewa => "ny",
                ILanguages.ChineseSimplified => "zh",
                ILanguages.ChineseTraditional => "zh-TW",
                ILanguages.Welsh => "cy",
                ILanguages.Danish => "da",
                ILanguages.German => "de",
                ILanguages.Greek => "el",
                ILanguages.English => "en",
                ILanguages.Esperanto => "eo",
                ILanguages.Spanish => "es",
                ILanguages.Estonian => "et",
                ILanguages.Basque => "eu",
                ILanguages.Persian => "fa",
                ILanguages.Finnish => "fi",
                ILanguages.French => "fr",
                ILanguages.Frisian => "fy",
                ILanguages.Irish => "ga",
                ILanguages.ScotsGaelic => "gd",
                ILanguages.Galician => "gl",
                ILanguages.Gujarati => "gu",
                ILanguages.Hausa => "ha",
                ILanguages.Hawaiian => "haw",
                ILanguages.Hindi => "hi",
                ILanguages.Hmong => "hmn",
                ILanguages.Croatian => "hr",
                ILanguages.HaitianCreole => "ht",
                ILanguages.Hungarian => "hu",
                ILanguages.Armenian => "hy",
                ILanguages.Indonesian => "id",
                ILanguages.Igbo => "ig",
                ILanguages.Icelandic => "is",
                ILanguages.Italian => "it",
                ILanguages.Japanese => "ja",
                ILanguages.Javanese => "jw",
                ILanguages.Georgian => "ka",
                ILanguages.Kazakh => "kk",
                ILanguages.Khmer => "km",
                ILanguages.Kannada => "kn",
                ILanguages.Korean => "ko",
                ILanguages.KurdishKurmanji => "ku",
                ILanguages.Kyrgyz => "ky",
                ILanguages.Latin => "la",
                ILanguages.Luxembourgish => "lb",
                ILanguages.Lao => "lo",
                ILanguages.Lithuanian => "lt",
                ILanguages.Latvian => "lv",
                ILanguages.Malagasy => "mg",
                ILanguages.Maori => "mi",
                ILanguages.Macedonian => "mk",
                ILanguages.Malayalam => "ml",
                ILanguages.Mongolian => "mn",
                ILanguages.Marathi => "mr",
                ILanguages.Malay => "ms",
                ILanguages.Maltese => "mt",
                ILanguages.MyanmarBurmese => "my",
                ILanguages.Nepali => "ne",
                ILanguages.Dutch => "nl",
                ILanguages.Norwegian => "no",
                ILanguages.Punjabi => "pa",
                ILanguages.Polish => "pl",
                ILanguages.Pashto => "ps",
                ILanguages.Portuguese => "pt",
                ILanguages.Romanian => "ro",
                ILanguages.Russian => "ru",
                ILanguages.Sindhi => "sd",
                ILanguages.Sinhala => "si",
                ILanguages.Slovak => "sk",
                ILanguages.Slovenian => "sl",
                ILanguages.Samoan => "sm",
                ILanguages.Shona => "sn",
                ILanguages.Somali => "so",
                ILanguages.Albanian => "sq",
                ILanguages.Serbian => "sr",
                ILanguages.Sesotho => "st",
                ILanguages.Sundanese => "su",
                ILanguages.Swedish => "sv",
                ILanguages.Swahili => "sw",
                ILanguages.Tamil => "ta",
                ILanguages.Telugu => "te",
                ILanguages.Tajik => "tg",
                ILanguages.Thai => "th",
                ILanguages.Filipino => "tl",
                ILanguages.Turkish => "tr",
                ILanguages.Ukrainian => "uk",
                ILanguages.Urdu => "ur",
                ILanguages.Uzbek => "uz",
                ILanguages.Vietnamese => "vi",
                ILanguages.Xhosa => "xh",
                ILanguages.Yiddish => "yi",
                ILanguages.Yoruba => "yo",
                ILanguages.Zulu => "zu",

                //! Other
                _ => "en"
            };

    }
}
