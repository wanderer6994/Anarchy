using Newtonsoft.Json;

namespace Discord
{
    public class EmbedAuthor
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("url")]
        public string Url { get; private set; }

        [JsonProperty("icon_url")]
        public string IconUrl { get; private set; }

        [JsonProperty("proxy_icon_url")]
        public string IconProxyUrl { get; private set; }
    }
}
