using Newtonsoft.Json;

namespace Discord
{
    public class EmbedFooter
    {
        [JsonProperty("text")]
        public string Text { get; set; }


        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }


        [JsonProperty("proxy_icon_url")]
        public string IconProxyUrl { get; private set; }


        public override string ToString()
        {
            return Text;
        }
    }
}
