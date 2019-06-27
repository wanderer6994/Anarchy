using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// Options for modifying a reaction
    /// </summary>
    public class ReactionModProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
