using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord.Gateway
{
    public class GateywayMemberChunk
    {
        [JsonProperty("members")]
        public List<GuildMember> Members { get; private set; }
    }
}
