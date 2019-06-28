using Newtonsoft.Json;

namespace Discord
{
    public class PartialGuild : BaseGuild
    {
        [JsonProperty("icon")]
        public string IconId { get; private set; }

        [JsonProperty("owner")]
        public bool Owner { get; private set; }

        [JsonProperty("permissions")]
        public int Permissions { get; private set; }


        public Guild GetGuild()
        {
            return Client.GetGuild(Id);
        }
    }
}
