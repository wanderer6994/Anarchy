namespace Discord
{
    public enum Language
    {
        Danish,
        German,
        EnglishUK,
        EnglishUS,
        Spanish,
        French,
        Croatian,
        Italian,
        Lithuanian,
        Hungarian,
        Dutch,
        Norwegian,
        Polish,
        Portuguese,
        Romanian,
        Finnish,
        Swedish,
        Viatnamese,
        Turkish,
        Czech,
        Greek,
        Bulgarian,
        Russian,
        Ukranian,
        Thai,
        Chinese,
        Japanese,
        ChineseThai,
        Korean
    }

    public static class LanguageUtils
    {
        public static string ConvertToString(Language lang)
        {
            switch (lang)
            {
                case Language.Danish:
                    return "da";
                case Language.German:
                    return "de";
                case Language.EnglishUK:
                    return "en-GB";
                case Language.EnglishUS:
                    return "en-US";
                case Language.Spanish:
                    return "es-ES";
                case Language.French:
                    return "fr";
                case Language.Croatian:
                    return "hr";
                case Language.Italian:
                    return "it";
                case Language.Lithuanian:
                    return "lt";
                case Language.Hungarian:
                    return "hu";
                case Language.Dutch:
                    return "nl";
                case Language.Norwegian:
                    return "no";
                case Language.Polish:
                    return "pl";
                case Language.Portuguese:
                    return "pt-BR";
                case Language.Romanian:
                    return "ro";
                case Language.Finnish:
                    return "fi";
                case Language.Swedish:
                    return "sv-SE";
                case Language.Viatnamese:
                    return "vi";
                case Language.Turkish:
                    return "tr";
                case Language.Czech:
                    return "cs";
                case Language.Greek:
                    return "el";
                case Language.Bulgarian:
                    return "bg";
                case Language.Russian:
                    return "ru";
                case Language.Ukranian:
                    return "uk";
                case Language.Thai:
                    return "th";
                case Language.Chinese:
                    return "zh-CN";
                case Language.Japanese:
                    return "ja";
                case Language.ChineseThai:
                    return "zh-TW";
                case Language.Korean:
                    return "ko";
                default:
                    return "en-US";
            }
        }
    }
}
