using Newtonsoft.Json;

namespace Discord
{
    public class Reaction : Controllable
    {
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
            Name = Client.GetGuildReaction(GuildId, (long)Id).Name;
        }


        public Reaction Modify(ReactionModProperties properties)
        {
            Reaction reaction = Client.ModifyGuildReaction(GuildId, (long)Id, properties);
            Name = reaction.Name;
            return reaction;
        }


        public bool Delete()
        {
            return Client.DeleteGuildReaction(GuildId, (long)Id);
        }


        public override string ToString()
        {
            return $"{Name} ({Id})";
        }
    }
}
