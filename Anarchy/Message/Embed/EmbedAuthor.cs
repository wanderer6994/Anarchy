using Newtonsoft.Json;

namespace Discord
{
    public class EmbedAuthor
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        [JsonProperty("proxy_icon_url")]
        public string IconProxyUrl { get; private set; }


        public override string ToString()
        {
            return $"{Name} ({Url})";
        }
    }
}
