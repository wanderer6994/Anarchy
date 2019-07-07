using Newtonsoft.Json;

namespace Discord
{
    public class EmojiProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
