using Newtonsoft.Json;

namespace Discord.Gateway
{
    public class GatewayRole
    {
        [JsonProperty("guild_id")]
        public long GuildId { get; private set; }

        [JsonProperty("role")]
        public Role Role { get; private set; }
    }
}
