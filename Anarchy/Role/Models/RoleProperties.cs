using Newtonsoft.Json;

namespace Discord
{
    //TODO: Remove the need to instantiate Permissions from the user's side
    public class RoleProperties
    {
        internal Property<string> NameProperty = new Property<string>();
        [JsonProperty("name")]
        public string Name
        {
            get { return NameProperty; }
            set { NameProperty.Value = value; }
        }

        [JsonProperty("permissions")]
        private int _permissions
        {
            get { return Permissions; }
        }
        public EditablePermissions Permissions { get; set; }

        internal Property<int> ColorProperty = new Property<int>();
        [JsonProperty("color")]
        public int Color
        {
            get { return ColorProperty; }
            set { ColorProperty.Value = value; }
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