using Newtonsoft.Json;

namespace Discord
{
    public class Role : ClientClassBase
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public int? Color { get; set; }

        [JsonProperty("position")]
        public int Position { get; private set; }

        [JsonIgnore]
        public long GuildId { get; internal set; }

        [JsonProperty("hoist")]
        public bool? Seperated { get; set; }

        [JsonProperty("mentionable")]
        public bool? Mentionable { get; set; }

        [JsonProperty("permissions")]
        public int? Permissions { get; set; }


        public Role Modify(RoleProperties properties)
        {
            Role role = Client.ModifyGuildRole(GuildId, Id, properties);
            Name = role.Name;
            Permissions = role.Permissions;
            Color = role.Color;
            Seperated = role.Seperated;
            Position = role.Position;
            return role;
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
