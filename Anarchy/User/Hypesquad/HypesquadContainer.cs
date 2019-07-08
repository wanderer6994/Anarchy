using Newtonsoft.Json;

namespace Discord
{
    internal class HypesquadContainer
    {
        [JsonProperty("house_id")]
        public HypesquadHouse House { get; set; }


        public override string ToString()
        {
            return House.ToString();
        }
    }
}