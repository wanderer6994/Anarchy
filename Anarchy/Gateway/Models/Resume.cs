using Newtonsoft.Json;

namespace Discord.Gateway
{
    internal class GatewayResume
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("session_id")]
        public string SessionId { get; set; }

        [JsonProperty("seq")]
        public uint? Sequence { get; set; }
    }
}
