using Newtonsoft.Json;

namespace Discord
{
    public class GuildChannelProperties : ChannelProperties
    {
        internal Property<ulong?> ParentProperty = new Property<ulong?>();
        [JsonProperty("parent_id")]
        public ulong? ParentId
        {
            get { return ParentProperty; }
            set { ParentProperty.Value = value; }
        }


        internal Property<uint> PositionProperty = new Property<uint>();
        [JsonProperty("position")]
        public uint Position
        {
            get { return PositionProperty; }
            set { PositionProperty.Value = value; }
        }
    }
}
