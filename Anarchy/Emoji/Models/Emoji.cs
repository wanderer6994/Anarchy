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
        public long Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }
        
        [JsonProperty("animated")]
        public bool Animated { get; private set; }

        [JsonProperty("user")]
        public User Creator { get; private set; }

        public long GuildId { get; internal set; }


        public void Update()
        {
            Name = Client.GetGuildEmoji(GuildId, Id).Name;
        }


        public void Modify(string name)
        {
            Name = Client.ModifyGuildEmoji(GuildId, Id, name).Name;
        }


        public void Delete()
        {
            Client.DeleteGuildEmoji(GuildId, Id);
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
