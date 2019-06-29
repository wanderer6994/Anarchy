using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord.Gateway
{
    public class GatewayLogin : Controllable
    {
        public GatewayLogin()
        {
            OnClientUpdated += (sender, e) =>
            {
                if (User != null)
                    User.SetClient(Client);

                if (Guilds != null)
                    Guilds.SetClientsInList(Client);
            };
        }

        [JsonProperty("session_id")]
        internal string SessionId { get; private set; }

        [JsonProperty("user")]
        public ClientUser User { get; private set; }

        [JsonProperty("guilds")]
        public List<UnavailableGuild> Guilds { get; private set; }


        public override string ToString()
        {
            return $"{User} ({User.Id})";
        }
    }
}