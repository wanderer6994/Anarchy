using Newtonsoft.Json;

namespace Discord.Gateway
{
    internal class GatewayPresence
    {
        public GatewayPresence()
        {
            Status = "online";
            Since = 0;
            Activities = new string[] { };
        }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("since")]
        public int Since { get; set; }

        [JsonProperty("activities")]
        public string[] Activities { get; set; }

        [JsonProperty("afk")]
        public bool Afk { get; set; }
    }
}
