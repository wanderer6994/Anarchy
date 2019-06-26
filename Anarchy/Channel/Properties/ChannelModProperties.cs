using Newtonsoft.Json;

namespace Discord
{
    public class ChannelModProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("nsfw")]
        public bool? Nsfw { get; set; }

        [JsonProperty("parent_id")]
        public long? ParentId { get; set; }

        [JsonProperty("position")]
        public int? Position { get; set; }
    }
}