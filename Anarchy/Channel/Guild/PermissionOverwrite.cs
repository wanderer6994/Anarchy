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
        private int _deny
        {
            set { Deny = new Permissions(value); }
        }
        public Permissions Deny { get; private set; }

        [JsonProperty("allow")]
        private int _allow
        {
            set { Allow = new Permissions(value); }
        }
        public Permissions Allow { get; private set; }


        public override string ToString()
        {
            return Type;
        }
    }
}