using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// Options for creating a message
    /// </summary>
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
