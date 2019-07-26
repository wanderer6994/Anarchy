using System.Collections.Generic;
using Newtonsoft.Json;

namespace Discord
{
    public class GuildMemberUpdate : Controllable
    {
        public GuildMemberUpdate()
        {
            Member = new GuildMember();

            OnClientUpdated += (sender, e) =>
            {
                User.SetClient(Client);
                Member.SetClient(Client);
            };
        }


        public GuildMember Member { get; private set; }


        [JsonProperty("guild_id")]
        public ulong GuildId
        {
            get { return Member.GuildId; }
            private set { Member.GuildId = value; }
        }


        [JsonProperty("nick")]
        public string Nickname
        {
            get { return Member.Nickname; }
            private set { Member.Nickname = value; }
        }


        [JsonProperty("roles")]
        public IReadOnlyList<ulong> Roles
        {
            get { return Member.Roles; }
            set { Member.Roles = value; }
        }


        [JsonProperty("user")]
        public User User
        {
            get { return Member.User; }
            set { Member.User = value; }
        }
    }
}
