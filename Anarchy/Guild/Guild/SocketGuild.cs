using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public class SocketGuild : Guild
    {
        public SocketGuild()
        {
            OnClientUpdated += (sender, e) => Channels.SetClientsInList(Client);
        }

        [JsonProperty("large")]
        public bool Large { get; private set; }

        [JsonProperty("member_count")]
        public uint MemberCount { get; private set; }

        [JsonProperty("channels")]
        public IReadOnlyList<GuildChannel> Channels { get; private set; }

        [JsonProperty("joined_at")]
        public string CreatedAt { get; private set; }


        public override IReadOnlyList<GuildChannel> GetChannels()
        {
            Channels = base.GetChannels();
            return Channels;
        }
    }
}
