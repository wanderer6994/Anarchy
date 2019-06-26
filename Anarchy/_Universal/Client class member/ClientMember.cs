using Newtonsoft.Json;

namespace Discord
{
    public class ClientMember
    {
        [JsonIgnore]
        internal virtual DiscordClient Client { get; set; }
    }
}