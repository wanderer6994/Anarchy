using Newtonsoft.Json;

namespace Discord
{
    public class EmbedFooter
    {
        [JsonProperty("text")]
        public string Text { get; private set; }

        [JsonProperty("icon_url")]
        public string IconUrl { get; private set; }

        [JsonProperty("proxy_icon_url")]
        public string IconProxyUrl { get; private set; }
    }
}
