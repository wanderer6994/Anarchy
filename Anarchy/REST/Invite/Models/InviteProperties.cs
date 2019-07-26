using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// Properties for creating an invite
    /// </summary>
    public class InviteProperties
    {
        [JsonProperty("max_age")]
        public uint MaxAge { get; set; }


        [JsonProperty("max_uses")]
        public uint MaxUses { get; set; }


        [JsonProperty("temporary")]
        public bool Temporary { get; set; }
    }
}