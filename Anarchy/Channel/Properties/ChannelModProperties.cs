using Newtonsoft.Json;

namespace Discord
{
    public class ChannelModProperties
    {
        internal Property<string> NameProperty = new Property<string>();
        [JsonProperty("name")]
        public string Name
        {
            get { return NameProperty; }
            set { NameProperty.Value = value; }
        }

        internal Property<string> TopicProperty = new Property<string>();
        [JsonProperty("topic")]
        public string Topic
        {
            get { return TopicProperty; }
            set { TopicProperty.Value = value; }
        }

        internal Property<bool> NsfwProperty = new Property<bool>();
        [JsonProperty("nsfw")]
        public bool Nsfw
        {
            get { return NsfwProperty; }
            set { NsfwProperty.Value = value; }
        }

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


        public override string ToString()
        {
            return $"Name: {Name} Position: {Position}";
        }
    }
}