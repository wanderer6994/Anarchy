using Newtonsoft.Json;

namespace Discord
{
    public class ChannelProperties
    {
        internal Property<string> NameProperty = new Property<string>();
        [JsonProperty("name")]
        public string Name
        {
            get { return NameProperty; }
            set { NameProperty.Value = value; }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}