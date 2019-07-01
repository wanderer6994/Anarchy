using Newtonsoft.Json;

namespace Discord
{
    public class MessageReaction : Controllable
    {
        public MessageReaction()
        {
            OnClientUpdated += (sender, e) =>
            {
                if (Reaction != null)
                    Reaction.Client = Client;
            };
        }

        [JsonProperty("emoji")]
        public Reaction Reaction { get; private set; }

        [JsonProperty("count")]
        public int Count { get; private set; }

        [JsonProperty("me")]
        public bool ClientHasReacted { get; private set; }


        public override string ToString()
        {
            return Reaction.ToString();
        }
    }
}