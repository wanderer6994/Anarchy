using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord.Gateway
{
    internal class GatewayPresence
    {
        public GatewayPresence()
        {
            Status = "online";
            Since = 0;
            Activities = new List<object>();
        }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("since")]
        public int Since { get; set; }

        //activities have not been implemented yet
        [JsonProperty("activities")]
        public List<object> Activities { get; set; }

        [JsonProperty("afk")]
        public bool Afk { get; set; }


        public override string ToString()
        {
            return Status;
        }
    }
}
