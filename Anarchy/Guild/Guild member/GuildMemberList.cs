using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord.Gateway
{
    public class GuildMemberList
    {
        [JsonProperty("members")]
        public List<GuildMember> Members { get; private set; }
    }
}