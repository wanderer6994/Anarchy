using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord.Gateway
{
    public class GuildMemberList : Controllable
    {
        public GuildMemberList()
        {
            OnClientUpdated += (sender, e) => Members.SetClientsInList(Client);
        }

        [JsonProperty("guild_id")]
        public long GuildId { get; private set; }

        [JsonProperty("members")]
        public IReadOnlyList<GuildMember> Members { get; private set; }
    }
}