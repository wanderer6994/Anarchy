using Newtonsoft.Json;

namespace Discord
{
    public class NameDiscriminator
    {
        [JsonProperty("username")]
        public string Username { get; internal set; }

        [JsonProperty("discriminator")]
        public int Discriminator { get; internal set; }


        public override string ToString()
        {
            return $"{Username}#{Discriminator}";
        }
    }
}