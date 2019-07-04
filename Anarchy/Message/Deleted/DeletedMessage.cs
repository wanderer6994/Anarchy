using Newtonsoft.Json;

namespace Discord
{
    public class DeletedMessage
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("channel_id")]
        public long ChannelId { get; private set; }

        [JsonProperty("guild_id")]
        public long GuildId { get; private set; }
    }
}
