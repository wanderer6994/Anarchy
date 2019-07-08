using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public class GuildMember : Controllable
    {
        public GuildMember()
        {
            OnClientUpdated += (sender, e) => User.SetClient(Client);
        }

        public ulong GuildId { get; internal set; }

        [JsonProperty("user")]
        public User User { get; private set; }

        [JsonProperty("nick")]
        public string Nickname { get; private set; }

        [JsonProperty("roles")]
        public IReadOnlyList<ulong> Roles { get; private set; }

        [JsonProperty("joined_at")]
        public string JoinedAt { get; private set; }


        public void Kick()
        {
            Client.KickGuildMember(GuildId, User.Id);
        }


        public void Ban(string reason = null, uint deleteMessageDays = 0)
        {
            Client.BanGuildMember(GuildId, User.Id, reason, deleteMessageDays);
        }


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