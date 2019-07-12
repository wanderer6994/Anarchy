using Newtonsoft.Json;
using System.Drawing;

namespace Discord
{
    public class Role : Controllable
    {
        [JsonProperty("id")]
        public ulong Id { get; private set; }


        [JsonProperty("name")]
        public string Name { get; private set; }


        [JsonProperty("color")]
        private uint _color;
        public Color Color
        {
            get { return Color.FromArgb((int)_color); }
            private set { _color = (uint)Color.FromArgb(0, value.R, value.G, value.B).ToArgb(); }
        }


        [JsonProperty("position")]
        public int Position { get; private set; }


        public ulong GuildId { get; internal set; }


        [JsonProperty("hoist")]
        public bool Seperated { get; private set; }


        [JsonProperty("mentionable")]
        public bool Mentionable { get; private set; }


        [JsonProperty("permissions")]
        private uint _permissions
        {
            set { Permissions = new Permissions(value); }
        }
        public Permissions Permissions { get; private set; }


        /// <summary>
        /// Modifies the role
        /// </summary>
        /// <param name="properties">Options for modifying the role</param>
        public void Modify(RoleProperties properties)
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
        }


        /// <summary>
        /// Deletes the role
        /// </summary>
        public void Delete()
        {
            Client.DeleteGuildRole(GuildId, Id);
        }


        public override string ToString()
        {
            return Name;
        }
    }
}