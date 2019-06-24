using Newtonsoft.Json;

namespace Discord.Gateway
{
    internal class GatewayHeartbeat
    {
        [JsonProperty("heartbeat_interval")]
        public int Interval { get; set; }
    }
}