using Newtonsoft.Json;

namespace Discord
{
    public class GuildTextChannel : GuildChannel
    {
        [JsonProperty("topic")]
        public string Topic { get; private set; }

        [JsonProperty("nsfw")]
        public bool Nsfw { get; private set; }


        public override void Update()
        {
            GuildTextChannel channel = Client.GetGuildTextChannel(Id);
            Name = channel.Name;
            Topic = channel.Topic;
            Nsfw = channel.Nsfw;
            Position = channel.Position;
            ParentId = channel.ParentId;
            PermissionOverwrites = channel.PermissionOverwrites;
        }


        public void Modify(GuildTextChannelProperties properties)
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

            GuildTextChannel channel = Client.ModifyGuildTextChannel(Id, properties);
            Name = channel.Name;
            Topic = channel.Topic;
            Nsfw = channel.Nsfw;
            Position = channel.Position;
            ParentId = channel.ParentId;
            PermissionOverwrites = channel.PermissionOverwrites;
        }
    }
}
