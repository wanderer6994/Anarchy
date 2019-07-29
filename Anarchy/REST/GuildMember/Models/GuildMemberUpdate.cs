using System.Collections.Generic;
using Newtonsoft.Json;

namespace Discord
{
    internal class GuildMemberUpdate : Controllable
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


#pragma warning disable IDE0051
        [JsonProperty("guild_id")]
        private ulong GuildId
        {
            get { return Member.GuildId; }
            set { Member.GuildId = value; }
        }


        [JsonProperty("nick")]
        private string Nickname
        {
            get { return Member.Nickname; }
            set { Member.Nickname = value; }
        }


        [JsonProperty("roles")]
        private IReadOnlyList<ulong> Roles
        {
            get { return Member.Roles; }
            set { Member.Roles = value; }
        }


        [JsonProperty("user")]
        private User User
        {
            get { return Member.User; }
            set { Member.User = value; }
        }
#pragma warning restore IDE0051
    }
}
