using Newtonsoft.Json;

namespace Discord.Gateway
{
    internal class GatewayData
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
