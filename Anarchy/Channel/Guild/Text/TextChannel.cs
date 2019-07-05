using Newtonsoft.Json;

namespace Discord
{
    public class TextChannel : GuildChannel
    {
        [JsonProperty("topic")]
        public string Topic { get; private set; }

        [JsonProperty("nsfw")]
        public bool Nsfw { get; private set; }


        public override void Update()
        {
            TextChannel channel = Client.GetTextChannel(Id);
            Name = channel.Name;
            Topic = channel.Topic;
            Nsfw = channel.Nsfw;
            Position = channel.Position;
            ParentId = channel.ParentId;
            PermissionOverwrites = channel.PermissionOverwrites;
        }


        public void Modify(TextChannelProperties properties)
        {
            if (!properties.NameProperty.Set)
                properties.Name = Name;
            if (!properties.TopicProperty.Set)
                properties.Topic = Topic;
            if (!properties.NsfwProperty.Set)
                properties.Nsfw = Nsfw;
            if (!properties.PositionProperty.Set)
                properties.Position = Position;
            if (!properties.ParentProperty.Set)
                properties.ParentId = ParentId;

            TextChannel channel = Client.ModifyTextChannel(Id, properties);
            Name = channel.Name;
            Topic = channel.Topic;
            Nsfw = channel.Nsfw;
            Position = channel.Position;
            ParentId = channel.ParentId;
            PermissionOverwrites = channel.PermissionOverwrites;
        }
    }
}
