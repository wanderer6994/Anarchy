using Newtonsoft.Json;

namespace Discord
{
    public class EmbedImage
    {
        [JsonProperty("url")]
        public string Url { get; private set; }

        [JsonProperty("width")]
        public int Width { get; private set; }

        [JsonProperty("height")]
        public int Height { get; private set; }
    }
}
