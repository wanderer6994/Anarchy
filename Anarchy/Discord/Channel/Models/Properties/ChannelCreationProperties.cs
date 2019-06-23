using Newtonsoft.Json;
using System.Collections.Generic;

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
    }
}