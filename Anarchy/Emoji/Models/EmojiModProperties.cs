using Newtonsoft.Json;

namespace Discord
{
    public class EmojiModProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
