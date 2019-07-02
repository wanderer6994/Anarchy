using Newtonsoft.Json;

namespace Discord.Gateway
{
    internal class GatewayPresence
    {
        public GatewayPresence()
        {
            Status = "online";
            Since = 0;
        }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("game")]
        public Activity Activity { get; set; }

        [JsonProperty("since")]
        public int Since { get; set; }

        [JsonProperty("afk")]
        public bool Afk { get; set; }


        public override string ToString()
        {
            return Status;
        }
    }
}
