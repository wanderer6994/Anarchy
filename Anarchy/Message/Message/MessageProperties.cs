using Newtonsoft.Json;

namespace Discord
{
    public class MessageProperties
    {
        public MessageProperties()
        {
            _nonce = "";
        }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("nonce")]
        private readonly string _nonce;

        [JsonProperty("tts")]
        public bool Tts { get; set; }

        [JsonProperty("embed")]
        public Embed Embed { get; set; }
    }
}
