using Newtonsoft.Json;

namespace Discord.Gateway
{
    internal class GatewayMembers
    {
        public GatewayMembers()
        {
            Query = "";
        }

        [JsonProperty("guild_id")]
        public long GuildId { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }


        public override string ToString()
        {
            return $"Guild: {GuildId} Limit: {Limit}";
        }
    }
}
