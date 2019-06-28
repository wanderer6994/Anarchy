using Newtonsoft.Json;

namespace Discord.Gateway
{
    internal class GatewayRole
    {
        [JsonProperty("guild_id")]
        public long GuildId { get; private set; }

        [JsonProperty("role")]
        public Role Role { get; private set; }


        public override string ToString()
        {
            return Role.ToString();
        }
    }
}
