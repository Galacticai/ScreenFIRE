using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScreenFIRE.Modules.Companion;
using System.Text;

namespace ScreenFIRE.Assets {
    public static partial class Strings {

        private static readonly string LocalePath = Path.Combine(Common.SF_Data, "Locale");

        private static Dictionary<IStrings, string> StringsStore = new();

        /// <summary> Pull cached strings according to <paramref name="language"/> </summary>
        /// <param name="language">Target <see cref="ILanguages"/> </param>
        /// <returns> Strings from cache (json) as Dictionary&lt;<see cref="IStrings"/>, <see cref="string"/>&gt; </returns>
        private static Dictionary<IStrings, string> StringsStore_FromCache(ILanguages language) {
            //! Pull from cache
            using StreamReader stream_stringsFromCache
                    = File.OpenText(Path.Combine(Common.SF_Data, "Locale", $"{language}.json"));
            return JToken.ReadFrom(new JsonTextReader(stream_stringsFromCache))
                        .ToObject<Dictionary<IStrings, string>>();
        }

        /// <summary> Determine and get the strings store that contains more strings </summary>
        /// <param name="language">Target <see cref="ILanguages"/> </param>
        /// <returns> The store that has more strings </returns>
        private static Dictionary<IStrings, string> PreferredStore(ILanguages language) {
            //! Compare >> false if Storage contains more all+more strings that are in json
            Dictionary<IStrings, string> stringsDictionaryFromCache = StringsStore_FromCache(language);
            foreach (IStrings key in stringsDictionaryFromCache.Keys)
                if (!StringsStore.ContainsKey(key))
                    return stringsDictionaryFromCache;
            return StringsStore;
        }

        public static bool RebuildStorage(ILanguages language) {
            //! No saved locales were found
            if (!Directory.Exists(LocalePath))
                return false; //? Report failure

            //! Check if the locale file exists
            string stringsFilePath = Path.Combine(LocalePath, $"{language}.json");
            if (!File.Exists(stringsFilePath))
                //! Fallback to English
                stringsFilePath = Path.Combine(LocalePath, $"{language = ILanguages.English}.json");

            //! Recheck & Cancel if no English
            if (!File.Exists(stringsFilePath))
                return false; //? Report failure

            //! Rebuild runtime dictionary
            StringsStore = PreferredStore(language);

            return true; //? Report success
        }

        public static bool SaveStorage(ILanguages language = ILanguages.System) {
            //! Make sure the Locale dir exists
            if (!Directory.Exists(LocalePath))
                Directory.CreateDirectory(LocalePath);

            //! Get system language if generic (System)
            if (language == ILanguages.System) language = Languages.SystemLanguage();
            //! Save last used language
            Common.Settings.Language = language;

            //! Cancel if the cache is better than the runtime strings
            if (StringsStore != PreferredStore(language))
                return false;

            //! Build json object
            JObject stringsAsJson = new(from item in StringsStore
                                        orderby item.Key
                                        select new JProperty(item.Key.ToString(), item.Value));

            //! Write json into locale file
            File.WriteAllText(Path.Combine(Common.SF_Data, "Locale", $"{language}.json"),
                                stringsAsJson.ToString(), Encoding.UTF8);

            return true;
        }

    }
}
