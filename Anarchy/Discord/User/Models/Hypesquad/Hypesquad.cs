using Newtonsoft.Json;

namespace Discord
{
    internal class Hypesquad
    {
        [JsonProperty("house_id")]
        public HypesquadHouse House { get; set; }
    }
}
