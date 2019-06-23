using Newtonsoft.Json;

namespace Discord.Gateway
{
    internal class GatewayGuildRequestMembers : GatewayData
    {
        public GatewayGuildRequestMembers()
        {
            Query = "";
        }

        [JsonProperty("guild_id")]
        public long GuildId { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }
    }
}
