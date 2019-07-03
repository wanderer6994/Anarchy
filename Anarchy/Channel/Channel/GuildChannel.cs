using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public class GuildChannel : Channel
    {
        [JsonProperty("topic")]
        public string Topic { get; private set; }
        
        [JsonProperty("guild_id")]
        public long GuildId { get; private set; }

        [JsonProperty("position")]
        public int Position { get; private set; }

        [JsonProperty("nsfw")]
        public bool Nsfw { get; private set; }
        
        [JsonProperty("parent_id")]
        public long? ParentId { get; private set; }

        [JsonProperty("permission_overwrites")]
        public IReadOnlyList<PermissionOverwrite> PermissionOverwrites { get; private set; }


        public void Update()
        {
            Channel channel = Client.GetChannel(Id);
            Name = channel.Name;
            //these have temporarily been removed due to the wrapper not having a 'GetGuildChannel()' method yet
            /*
            Topic = channel.Topic;
            Position = channel.Position;
            Nsfw = channel.Nsfw;
            ParentId = channel.ParentId;
            PermissionOverwrites = channel.PermissionOverwrites;*/
        }


        public GuildChannel Modify(ChannelModProperties properties)
        {
            if (!properties.NameProperty.Set)
                properties.Name = Name;
            if (!properties.NsfwProperty.Set)
                properties.Nsfw = Nsfw;
            if (!properties.PositionProperty.Set)
                properties.Position = Position;
            if (!properties.ParentProperty.Set)
                properties.ParentId = ParentId;

            GuildChannel channel = Client.ModifyChannel(Id, properties);
            Name = channel.Name;
            Topic = channel.Topic;
            Position = channel.Position;
            Nsfw = channel.Nsfw;
            ParentId = channel.ParentId;
            PermissionOverwrites = channel.PermissionOverwrites;
            return channel;
        }
    }
}
