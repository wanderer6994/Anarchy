using Newtonsoft.Json;

namespace Discord
{
    public class EmbedField
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("value")]
        public string Content { get; private set; }

        [JsonProperty("inline")]
        public bool Inline { get; private set; }
    }
}
