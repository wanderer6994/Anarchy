using Newtonsoft.Json;

namespace Discord
{
    public class Role : ControllableModel
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("color")]
        public int Color { get; private set; }

        [JsonProperty("position")]
        public int Position { get; private set; }

        [JsonIgnore]
        public long GuildId { get; internal set; }

        [JsonProperty("hoist")]
        public bool? Seperated { get; private set; }

        [JsonProperty("mentionable")]
        public bool? Mentionable { get; private set; }

        [JsonProperty("permissions")]
        public int Permissions { get; set; }


        public Role Modify(RoleProperties properties)
        {
            if (properties.Name == null)
                properties.Name = Name;
            if (properties.Color == null)
                properties.Color = Color;
            if (properties.Seperated == null)
                properties.Seperated = Seperated;
            if (properties.Mentionable == null)
                properties.Mentionable = Mentionable;
            if (properties.Permissions == null)
                properties.Permissions = Permissions;

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
