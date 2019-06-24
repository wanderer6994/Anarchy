using Newtonsoft.Json;

namespace Discord
{
    public class PermissionOverwrite
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("deny")]
        public int Deny { get; set; }

        [JsonProperty("allow")]
        public int Allow { get; set; }
    }
}