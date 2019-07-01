using Newtonsoft.Json;

namespace Discord
{
    public class Attachment
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("url")]
        public string Url { get; private set; }

        [JsonProperty("proxy_url")]
        public string ProxyUrl { get; private set; }

        [JsonProperty("size")]
        public int FileSize { get; private set; }


        public override string ToString()
        {
            return $"Size: {FileSize} Url: {Url}";
        }
    }
}