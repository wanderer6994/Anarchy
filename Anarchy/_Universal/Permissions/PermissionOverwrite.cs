using Newtonsoft.Json;

namespace Discord
{
    public class PermissionOverwrite
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("type")]
        public string Type { get; private set; }

        [JsonProperty("deny")]
        public int Deny { get; private set; }

        [JsonProperty("allow")]
        public int Allow { get; private set; }


        public override string ToString()
        {
            return $"{Type} permission | Allow: {Allow} Deny: {Deny}";
        }
    }
}