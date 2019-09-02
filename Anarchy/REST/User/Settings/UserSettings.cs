using Newtonsoft.Json;

namespace Discord
{
    public class UserSettings
    {
        private readonly Property<ExplicitContentFilter> ExplicityProperty = new Property<ExplicitContentFilter>();
        [JsonProperty("explicit_content_filter")]
        public ExplicitContentFilter ExplicitContentFilter
        {
            get { return ExplicityProperty.Value; }
            set
            { ExplicityProperty.Value = value; }
        }


        public bool ShouldSerializeExplicitContentFilter()
        {
            return ExplicityProperty.Set;
        }


        private readonly Property<string> LocaleProperty = new Property<string>();
        [JsonProperty("locale")]
        public string Language
        {
            get { return LocaleProperty; }
            set { LocaleProperty.Value = value; }
        }


        public bool ShouldSerializeLanguage()
        {
            return LocaleProperty.Set;
        }
    }
}
