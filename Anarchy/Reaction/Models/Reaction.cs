using Newtonsoft.Json;

namespace Discord
{
    public class Reaction : ControllableModel
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }
        
        [JsonProperty("animated")]
        public bool Animated { get; private set; }

        [JsonIgnore]
        public long GuildId { get; internal set; }


        /// <summary>
        /// Modifies the reaction
        /// </summary>
        public Reaction Modify(ReactionModProperties properties)
        {
            Reaction reaction = Client.ModifyGuildReaction(GuildId, Id, properties);
            Name = reaction.Name;
            return reaction;
        }


        /// <summary>
        /// Deletes the reaction
        /// </summary>
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
