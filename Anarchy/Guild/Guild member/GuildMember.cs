using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public class GuildMember
    {
        [JsonProperty("user")]
        public User User { get; private set; }

        [JsonProperty("nick")]
        public string Nickname { get; private set; }

        [JsonProperty("roles")]
        public List<long> Roles { get; private set; }

        [JsonProperty("joined_at")]
        public string JoinedAt { get; private set; }

        public override string ToString()
        {
            return User.ToString();
        }
    }
}
