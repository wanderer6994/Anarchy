using Newtonsoft.Json;

namespace Discord
{
    public class Reaction
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }
        
        [JsonProperty("animated")]
        public bool Animated { get; private set; }

        [JsonIgnore]
        public long GuildId { get; internal set; }

        [JsonIgnore]
        internal DiscordClient Client { get; set; }


        public Reaction Modify(ReactionModProperties properties)
        {
            Reaction reaction = Client.ModifyGuildReaction(GuildId, Id, properties);
            Name = reaction.Name;
            return reaction;
        }


        public bool Delete()
        {
            return Client.DeleteGuildReaction(GuildId, Id);
        }


        public override string ToString()
        {
            return $"{Name} ({Id})";
        }
    }
}
