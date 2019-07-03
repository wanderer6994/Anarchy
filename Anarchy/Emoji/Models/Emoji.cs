using Newtonsoft.Json;

namespace Discord
{
    public class Emoji : Controllable
    {
        public Emoji()
        {
            OnClientUpdated += (sender, e) => Creator.SetClient(Client);
        }

        [JsonProperty("id")]
        public long? Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }
        
        [JsonProperty("animated")]
        public bool Animated { get; private set; }

        [JsonProperty("user")]
        public User Creator { get; private set; }

        public long GuildId { get; internal set; }


        public void Update()
        {
            Name = Client.GetGuildEmoji(GuildId, (long)Id).Name;
        }


        public Emoji Modify(string name)
        {
            Emoji emoji = Client.ModifyGuildEmoji(GuildId, (long)Id, name);
            Name = emoji.Name;
            return emoji;
        }


        public bool Delete()
        {
            return Client.DeleteGuildEmoji(GuildId, (long)Id);
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
