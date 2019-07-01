using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public class GuildMember : Controllable
    {
        public GuildMember()
        {
            OnClientUpdated += (sender, e) => User.Client = Client;
        }

        [JsonProperty("user")]
        public User User { get; private set; }

        [JsonProperty("nick")]
        public string Nickname { get; private set; }

        [JsonProperty("roles")]
        public IReadOnlyList<long> Roles { get; private set; }

        [JsonProperty("joined_at")]
        public string JoinedAt { get; private set; }

        public override string ToString()
        {
            return User.ToString();
        }
    }
}
