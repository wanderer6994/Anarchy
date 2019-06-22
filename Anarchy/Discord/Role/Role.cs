using Newtonsoft.Json;

namespace Discord
{
    public class Role : RoleProperties
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("managed")]
        public bool Managed { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonIgnore]
        public long GuildId { get; set; }

        [JsonIgnore]
        internal DiscordClient Client { get; set; }

        public Role Modify(RoleProperties properties)
        {
            return Client.ChangeGuildRole(GuildId, Id, properties);
        }

        public void Delete()
        {
            Client.DeleteGuildRole(GuildId, Id);
        }

        public override string ToString()
        {
            return $"{Name} ({Id})";
        }
    }
}
