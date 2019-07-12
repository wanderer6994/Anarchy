using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Discord.Gateway
{
    /// <summary>
    /// A <see cref="Guild"/> with extra information (currently only available through a <see cref="Login"/>)
    /// </summary>
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
#pragma warning disable CS0649
        private readonly string _joinedAt;
#pragma warning restore CS0659
        public DateTime JoinedAt
        {
            get { return DiscordTimestamp.FromString(_joinedAt); }
        }


        /// <summary>
        /// Gets the guild's channels
        /// </summary>
        public override IReadOnlyList<GuildChannel> GetChannels()
        {
            return Channels = base.GetChannels();
        }
    }
}
