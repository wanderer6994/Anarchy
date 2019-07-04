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

        public long GuildId { get; internal set; }

        [JsonProperty("user")]
        public User User { get; private set; }

        [JsonProperty("nick")]
        public string Nickname { get; private set; }

        [JsonProperty("roles")]
        public IReadOnlyList<long> Roles { get; private set; }

        [JsonProperty("joined_at")]
        public string JoinedAt { get; private set; }


        public bool Kick()
        {
            return Client.KickGuildMember(GuildId, User.Id);
        }


        public bool Ban(string reason = null, int deleteMessageDays = 0)
        {
            return Client.BanGuildMember(GuildId, User.Id, reason, deleteMessageDays);
        }


        public bool Unban()
        {
            return Client.UnbanGuildMember(GuildId, User.Id);
        }


        public override string ToString()
        {
            return User.ToString();
        }
    }
}