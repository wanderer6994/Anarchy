using Newtonsoft.Json;

namespace Discord.Gateway
{
    public class Activity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public ActivityType Type { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
