using Newtonsoft.Json;

namespace Discord
{
    public class GuildChannelProperties : ChannelProperties
    {
        internal Property<long?> ParentProperty = new Property<long?>();
        [JsonProperty("parent_id")]
        public long? ParentId
        {
            get { return ParentProperty; }
            set { ParentProperty.Value = value; }
        }

        internal Property<int> PositionProperty = new Property<int>();
        [JsonProperty("position")]
        public int Position
        {
            get { return PositionProperty; }
            set { PositionProperty.Value = value; }
        }
    }
}
