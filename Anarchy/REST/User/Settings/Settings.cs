using Newtonsoft.Json;
using System;

namespace Discord
{
    /// <summary>
    /// This is currently only used in OnUserUpdated, and is very unrealiable.
    /// For an example, you never know which properties have been changed.
    /// </summary>
    public class Settings
    {
        private readonly Property<string> ThemeProperty = new Property<string>();
        [JsonProperty("theme")]
        private string _theme
        {
            get { return ThemeProperty.Value; }
            set { ThemeProperty.Value = value; }
        }

        public Theme Theme
        {
            get { return (Theme)Enum.Parse(typeof(Theme), _theme, true); }
            set { ThemeProperty.Value = value.ToString().ToLower(); }
        }


        public bool ShouldSerializeTheme()
        {
            return ThemeProperty.Set;
        }


        [JsonProperty("explicit_content_filter")]
        public ExplicitContentFilter ExplicitContentFilter { get; internal set; }


        [JsonProperty("developer_mode")]
        public bool DeveloperMode { get; internal set; }

        [JsonProperty("message_display_compact")]
        public bool CompactMessages { get; internal set; }


        [JsonProperty("locale")]
        public string Language { get; internal set; }


        [JsonProperty("enable_tts_playback")]
        public bool EnableTts { get; internal set; }


        [JsonProperty("gif_auto_play")]
        public bool PlayGifsAutomatically { get; internal set; }
    }
}
