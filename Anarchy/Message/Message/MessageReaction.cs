using Newtonsoft.Json;

namespace Discord
{
    public class MessageReaction : Controllable
    {
        public MessageReaction()
        {
            OnClientUpdated += (sender, e) => Emoji.SetClient(Client);
        }

        [JsonProperty("emoji")]
        public Emoji Emoji { get; private set; }

        [JsonProperty("count")]
        public int Count { get; private set; }

        [JsonProperty("me")]
        public bool ClientHasReacted { get; private set; }


        public override string ToString()
        {
            return Emoji.ToString();
        }
    }
}