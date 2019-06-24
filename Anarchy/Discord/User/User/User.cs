using Newtonsoft.Json;

namespace Discord
{
    public class User : Recipient
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("avatar")]
        public string Avatar { get; private set; }

        [JsonProperty("bot")]
        public bool Bot { get; private set; }


        public override string ToString()
        {
            return $"{base.ToString()} ({Id})";
        }
    }
}