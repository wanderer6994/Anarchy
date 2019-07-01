using Newtonsoft.Json;

namespace Discord
{
    public class EmbedField
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Content { get; set; }

        [JsonProperty("inline")]
        public bool Inline { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
