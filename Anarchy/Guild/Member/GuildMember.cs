using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Discord
{
    public class GuildMember : Controllable
    {
        public GuildMember()
        {
            OnClientUpdated += (sender, e) => User.SetClient(Client);
        }


        [JsonProperty("guild_id")]
        public ulong GuildId { get; internal set; }


        [JsonProperty("user")]
        public User User { get; private set; }


        [JsonProperty("nick")]
        public string Nickname { get; private set; }


        [JsonProperty("roles")]
        public IReadOnlyList<ulong> Roles { get; private set; }


        [JsonProperty("joined_at")]
#pragma warning disable CS0649
        private readonly string _joinedAt;
        public DateTime? JoinedAt
        {
            get
            {
                if (_joinedAt == null)
                    return null;

                return DiscordTimestamp.FromString(_joinedAt);
            }
        }
#pragma warning restore CS0649


        /// <summary>
        /// Kicks the member from the guild
        /// </summary>
        public void Kick()
        {
            Client.KickGuildMember(GuildId, User.Id);
        }


        /// <summary>
        /// Bans the member from the guild
        /// </summary>
        /// <param name="reason">The reason for banning the user</param>
        /// <param name="deleteMessageDays">Amount of days to purge messages</param>
        public void Ban(string reason = null, uint deleteMessageDays = 0)
        {
            Client.BanGuildMember(GuildId, User.Id, reason, deleteMessageDays);
        }


        /// <summary>
        /// Unbans the user from the guild
        /// </summary>
        public void Unban()
        {
            Client.UnbanGuildMember(GuildId, User.Id);
        }


        public override string ToString()
        {
            return User.ToString();
        }
    }
}