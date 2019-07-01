using Newtonsoft.Json;

namespace Discord
{
    public class InviteProperties
    {
        [JsonProperty("max_age")]
        public int MaxAge { get; set; }

        [JsonProperty("max_uses")]
        public int MaxUses { get; set; }

        [JsonProperty("temporary")]
        public bool Temporary { get; set; }


        public override string ToString()
        {
            return $"Max age: {MaxAge} Temporary: {Temporary}";
        }
    }
}