using Newtonsoft.Json;

namespace Discord
{
    public class ChannelCreationProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parent_id")]
        public long? ParentId { get; set; }

        [JsonProperty("type")]
        public ChannelType Type { get; set; }


        public override string ToString()
        {
            return $"Type: {Type} Name: {Name}";
        }
    }
}