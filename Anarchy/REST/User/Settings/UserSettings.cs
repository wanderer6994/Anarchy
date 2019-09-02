using Newtonsoft.Json;
using System;

namespace Discord
{
    public class UserSettings
    {
        public UserSettings()
        {
            AllowFriendRequestsFrom = new FriendSourceFlags();
        }


        private readonly Property<string> ThemeProperty = new Property<string>();
        [JsonProperty("theme")]
        private string _theme;

        public Theme Theme
        {
            get { return (Theme)Enum.Parse(typeof(Theme), _theme, true); }
            set { _theme = value.ToString().ToLower(); }
        }


        public bool ShouldSerializeTheme()
        {
            return ThemeProperty.Set;
        }


        private readonly Property<ExplicitContentFilter> ExplicityProperty = new Property<ExplicitContentFilter>();
        [JsonProperty("explicit_content_filter")]
        public ExplicitContentFilter ExplicitContentFilter
        {
            get { return ExplicityProperty; }
            set
            { ExplicityProperty.Value = value; }
        }


        public bool ShouldSerializeExplicitContentFilter()
        {
            return ExplicityProperty.Set;
        }


        private readonly Property<bool> DevProperty = new Property<bool>();
        [JsonProperty("developer_mode")]
        public bool DeveloperMode
        {
            get { return DevProperty; }
            set { DevProperty.Value = value; }
        }


        public bool ShouldSerializeDeveloperMode()
        {
            return DevProperty.Set;
        }


        private readonly Property<bool> CompactProperty = new Property<bool>();
        [JsonProperty("message_display_compact")]
        public bool CompactMessages
        {
            get { return CompactProperty; }
            set { CompactProperty.Value = value; }
        }


        public bool ShouldSerializeCompactMessages()
        {
            return CompactProperty.Set;
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


        private readonly Property<bool> TtsProperty = new Property<bool>();
        [JsonProperty("enable_tts_playback")]
        public bool EnableTts
        {
            get { return TtsProperty; }
            set { TtsProperty.Value = value; }
        }


        public bool ShouldSerializeEnableTts()
        {
            return TtsProperty.Set;
        }


        private readonly Property<bool> GifProperty = new Property<bool>();
        [JsonProperty("gif_auto_play")]
        public bool PlayGifsAutomatically
        {
            get { return GifProperty; }
            set { GifProperty.Value = value; }
        }


        public bool ShouldSerializePlayGifsAutomatically()
        {
            return GifProperty.Set;
        }


        public FriendSourceFlags AllowFriendRequestsFrom { get; set; }
    }
}
