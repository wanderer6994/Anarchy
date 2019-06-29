using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public class Channel : BaseChannel
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
        public List<PermissionOverwrite> PermissionOverwrites { get; private set; }


        public void Update()
        {
            Channel channel = Client.GetChannel(Id);
            Name = channel.Name;
            Topic = channel.Topic;
            Position = channel.Position;
            Nsfw = channel.Nsfw;
            ParentId = channel.ParentId;
            PermissionOverwrites = channel.PermissionOverwrites;
        }


        public Channel Modify(ChannelModProperties properties)
        {
            if (properties.Name == null)
                properties.Name = Name;
            if (properties.Nsfw == null)
                properties.Nsfw = Nsfw;
            if (properties.Position == null)
                properties.Position = Position;
            if (properties.ParentId == null)
                properties.ParentId = ParentId;

            Channel channel = Client.ModifyChannel(Id, properties);
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
