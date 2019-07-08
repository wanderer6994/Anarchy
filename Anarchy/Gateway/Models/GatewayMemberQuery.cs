﻿using Newtonsoft.Json;

namespace Discord.Gateway
{
    internal class GatewayMemberQuery
    {
        public GatewayMemberQuery()
        {
            Query = "";
        }

        [JsonProperty("guild_id")]
        public ulong GuildId { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("limit")]
        public uint Limit { get; set; }


        public override string ToString()
        {
            return GuildId.ToString();
        }
    }
}