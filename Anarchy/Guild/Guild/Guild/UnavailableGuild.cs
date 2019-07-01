using Newtonsoft.Json;

namespace Discord
{
    public class UnavailableGuild : Controllable
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        public Guild GetFullGuild()
        {
            return Client.GetGuild(Id);
        }
    }
}
