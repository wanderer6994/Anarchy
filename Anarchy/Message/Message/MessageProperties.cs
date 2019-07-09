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
#pragma warning disable 0414
        private readonly string _nonce;
#pragma warning restore 0414


        [JsonProperty("tts")]
        public bool Tts { get; set; }


        [JsonProperty("embed")]
        public Embed Embed { get; set; }
    }
}
