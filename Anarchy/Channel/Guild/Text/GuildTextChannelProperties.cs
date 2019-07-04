using Newtonsoft.Json;

namespace Discord
{
    public class GuildTextChannelProperties : GuildChannelProperties
    {
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
    }
}
