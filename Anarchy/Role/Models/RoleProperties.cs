using Newtonsoft.Json;
using System.Drawing;

namespace Discord
{
    public class RoleProperties
    {
        internal Property<string> NameProperty = new Property<string>();
        [JsonProperty("name")]
        public string Name
        {
            get { return NameProperty; }
            set { NameProperty.Value = value; }
        }


        internal Property<EditablePermissions> PermissionsProperty = new Property<EditablePermissions>();
        [JsonProperty("permissions")]
        private uint _permissions
        {
            get { return Permissions; }
        }
        public EditablePermissions Permissions
        {
            get { return PermissionsProperty; }
            set { PermissionsProperty.Value = value; }
        }


        internal Property<uint> ColorProperty = new Property<uint>();
        [JsonProperty("color")]
        private uint _color
        {
            get { return ColorProperty; }
            set { ColorProperty.Value = value; }
        }
        public Color Color
        {
            get { return Color.FromArgb((int)_color); }
            set { _color = (uint)Color.FromArgb(0, value.R, value.G, value.B).ToArgb(); }
        }


        internal Property<bool> SeperatedProperty = new Property<bool>();
        [JsonProperty("hoist")]
        public bool Seperated
        {
            get { return SeperatedProperty; }
            set { SeperatedProperty.Value = value; }
        }


        internal Property<bool> MentionableProperty = new Property<bool>();
        [JsonProperty("mentionable")]
        public bool Mentionable
        {
            get { return MentionableProperty; }
            set { MentionableProperty.Value = value; }
        }


        public override string ToString()
        {
            return Name;
        }
    }
}