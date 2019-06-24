using Newtonsoft.Json;

namespace Discord.Gateway
{
    internal class GatewayIdentification : GatewayData
    {
        [JsonProperty("properties")]
        public SuperProperties Properties { get; set; }

        [JsonProperty("compress")]
        public bool Compress { get; set; }

        [JsonProperty("presence")]
        public GatewayPresence Presence { get; set; }
    }
}
