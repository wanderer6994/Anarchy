using Newtonsoft.Json;

namespace Discord
{
    public class MessageProperties
    {
        public MessageProperties()
        {
            Nonce = "";
        }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        [JsonProperty("tts")]
        public bool Tts { get; set; }

        [JsonProperty("embed")]
        public Embed Embed { get; set; }
    }
}
