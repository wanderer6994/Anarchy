using Newtonsoft.Json;

namespace Discord
{
    public class Emoji : PartialEmoji
    {
        public Emoji()
        {
            OnClientUpdated += (sender, e) => Creator.SetClient(Client);
        }


        [JsonProperty("user")]
        public User Creator { get; private set; }
    }
}
