using Newtonsoft.Json;

namespace Discord
{
    public class DiscordHttpError
    {
        [JsonProperty("code")]
        public DiscordError Code { get; private set; }

        [JsonProperty("message")]
        public string Message { get; private set; }
    }
}
