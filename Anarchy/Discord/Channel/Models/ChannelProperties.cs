using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public class ChannelProperties
    {
        public ChannelProperties()
        {
            PermissionOverwrites = new List<PermissionOverwrite>();
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parent_id")]
        public long? ParentId { get; set; }

        [JsonProperty("type")]
        public ChannelType Type { get; set; }
        
        [JsonProperty("permission_overwrites")]
        public List<PermissionOverwrite> PermissionOverwrites { get; set; }
    }
}
