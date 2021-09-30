using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScreenFIRE.Modules.Companion;
using System.Text;

namespace ScreenFIRE.Assets {
    public static partial class Strings {

        private static readonly string LocaleCachePath = Path.Combine(Common.SF_Data, "Locale");

        private static Dictionary<IStrings, string> Storage = new();
        public static bool RebuildStorage(ILanguages language) {
            //! No saved locales were found
            if (!Directory.Exists(LocaleCachePath))
                return false; //? Report failure

            //! Check if the locale file exists
            string stringsFilePath = Path.Combine(LocaleCachePath, $"{language}.json");
            if (!File.Exists(stringsFilePath))
                //! Fallback to English
                stringsFilePath = Path.Combine(LocaleCachePath, $"{language = ILanguages.English}.json");

            //! Recheck & Cancel if no English
            if (!File.Exists(stringsFilePath))
                return false; //? Report failure

            //! Pull from cache
            using StreamReader stream_stringsFromCache
                    = File.OpenText(Path.Combine(Common.SF_Data, "Locale", $"{language}.json"));
            JObject stringsFromCache
                = (JObject)JToken.ReadFrom(new JsonTextReader(stream_stringsFromCache));

            //! Rebuild runtime dictionary
            Storage = stringsFromCache.ToObject<Dictionary<IStrings, string>>();

            return true; //? Report success
        }

        public static void SaveStorage(ILanguages language = ILanguages.System) {
            //! Make sure the Locale dir exists
            if (!Directory.Exists(LocaleCachePath))
                Directory.CreateDirectory(LocaleCachePath);

            //! Get system language if generic (System)
            if (language == ILanguages.System) language = Languages.SystemLanguage();
            //! Save last used language
            Common.Settings.Language = language;

            //! Build the json object
            JObject stringsAsJson = new(from item in Storage
                                        orderby item.Key
                                        select new JProperty(item.Key.ToString(), item.Value));
            //! Write json into locale file
            File.WriteAllText(Path.Combine(Common.SF_Data, "Locale", $"{language}.json"),
                                stringsAsJson.ToString(), Encoding.UTF8);
        }

    }
}
