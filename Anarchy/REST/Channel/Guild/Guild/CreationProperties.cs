using Newtonsoft.Json;

namespace Discord
{
    public class GuildChannelCreationProperties : ChannelCreationProperties
    {
        [JsonProperty("parent_id")]
        public ulong? ParentId { get; set; }
    }
}