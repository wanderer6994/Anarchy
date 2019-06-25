using Newtonsoft.Json;

namespace Discord
{
    //Not the best name. Might wanna come up with a better one
    public class ClientClassBase
    {
        [JsonIgnore]
        internal virtual DiscordClient Client { get; set; }
    }
}
