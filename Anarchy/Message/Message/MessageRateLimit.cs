using Newtonsoft.Json;

namespace Discord
{
    internal class MessageRateLimit
    {
        [JsonProperty("message_send_cooldown_ms")]
        public int Cooldown { get; private set; }
    }
}
