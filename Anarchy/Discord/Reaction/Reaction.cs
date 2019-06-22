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

        public bool Delete()
        {
            return Client.DeleteReaction(GuildId, Id);
        }
    }
}
