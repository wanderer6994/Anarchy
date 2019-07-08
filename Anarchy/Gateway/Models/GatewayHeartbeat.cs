using Newtonsoft.Json;

namespace Discord.Gateway
{
    internal class GatewayHeartbeat
    {
        [JsonProperty("heartbeat_interval")]
        public uint Interval { get; private set; }


        public override string ToString()
        {
            return Interval.ToString();
        }
    }
}