using Newtonsoft.Json;

namespace Discord
{
    public class PartialGuild : BaseGuild
    {
        [JsonProperty("description")]
        public string Description { get; private set; }


        [JsonProperty("verification_level")]
        public GuildVerificationLevel VerificationLevel { get; private set; }


        [JsonProperty("vanity_url_code")]
        public string VanityInvite { get; private set; }


        [JsonProperty("owner")]
        public bool Owner { get; private set; }


        [JsonProperty("permissions")]
        private uint _permissions
        {
            set { Permissions = new Permissions(value); }
        }
        public Permissions Permissions { get; private set; }


        public Guild GetGuild()
        {
            return Client.GetGuild(Id);
        }
    }
}
