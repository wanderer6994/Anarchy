using Newtonsoft.Json;

namespace Discord.Webhook
{
    public class Webhook
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }
        
        [JsonProperty("user")]
        public User User { get; private set; }

        [JsonProperty("token")]
        public string Token { get; private set; }

        [JsonProperty("guild_id")]
        public long GuildId { get; private set; }

        [JsonProperty("channel_id")]
        public long ChannelId { get; private set; }

        [JsonProperty("avatar")]
        public string Avatar { get; private set; }
    }
}