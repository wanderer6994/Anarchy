using Newtonsoft.Json;
using System.Drawing;

namespace Discord
{
    public class Role : Controllable
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("color")]
        private int _color;
        public Color Color
        {
            get { return Color.FromArgb(_color); }
            private set { _color = Color.FromArgb(0, value.R, value.G, value.B).ToArgb(); }
        }

        [JsonProperty("position")]
        public int Position { get; private set; }

        public long GuildId { get; internal set; }

        [JsonProperty("hoist")]
        public bool Seperated { get; private set; }

        [JsonProperty("mentionable")]
        public bool Mentionable { get; private set; }

        [JsonProperty("permissions")]
        private int _permissions
        {
            set { Permissions = new Permissions(value); }
        }
        public Permissions Permissions { get; private set; }


        public Role Modify(RoleProperties properties)
        {
            if (!properties.NameProperty.Set)
                properties.Name = Name;
            if (!properties.ColorProperty.Set)
                properties.Color = Color;
            if (!properties.SeperatedProperty.Set)
                properties.Seperated = Seperated;
            if (!properties.MentionableProperty.Set)
                properties.Mentionable = Mentionable;
            if (properties.Permissions == null)
                properties.Permissions = new EditablePermissions(Permissions);

            Role role = Client.ModifyGuildRole(GuildId, Id, properties);
            Name = role.Name;
            Permissions = role.Permissions;
            Color = role.Color;
            Seperated = role.Seperated;
            Position = role.Position;
            return role;
        }


        public bool Delete()
        {
            return Client.DeleteGuildRole(GuildId, Id);
        }


        public override string ToString()
        {
            return $"{Name} ({Id})";
        }
    }
}
