using Newtonsoft.Json;

namespace Discord
{
    internal class MessageProperties
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
    }
}
