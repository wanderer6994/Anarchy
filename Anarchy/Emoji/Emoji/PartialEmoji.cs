using Newtonsoft.Json;

namespace Discord
{
    public class PartialEmoji : Controllable
    {
        [JsonProperty("id")]
        public long? Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("animated")]
        public bool Animated { get; private set; }

        public long GuildId { get; internal set; }


        public void Update()
        {
            if (Id == null)
                return;

            Name = Client.GetGuildEmoji(GuildId, (long)Id).Name;
        }


        public void Modify(string name)
        {
            if (Id == null)
                return;

            Name = Client.ModifyGuildEmoji(GuildId, (long)Id, name).Name;
        }


        public void Delete()
        {
            if (Id == null)
                return;

            Client.DeleteGuildEmoji(GuildId, (long)Id);
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
