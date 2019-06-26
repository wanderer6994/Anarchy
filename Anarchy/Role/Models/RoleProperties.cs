using Newtonsoft.Json;

namespace Discord
{
    public class RoleProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("permissions")]
        public int? Permissions { get; set; }

        [JsonProperty("color")]
        public int Color { get; set; }

        [JsonProperty("hoist")]
        public bool? Seperated { get; set; }

        [JsonProperty("mentionable")]
        public bool? Mentionable { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}