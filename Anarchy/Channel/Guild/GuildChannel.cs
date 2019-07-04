using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public class GuildChannel : Channel
    {   
        [JsonProperty("guild_id")]
        public long GuildId { get; private set; }

        [JsonProperty("position")]
        public int Position { get; protected set; }
        
        [JsonProperty("parent_id")]
        public long? ParentId { get; protected set; }

        [JsonProperty("permission_overwrites")]
        public IReadOnlyList<PermissionOverwrite> PermissionOverwrites { get; protected set; }


        public override void Update()
        {
            GuildChannel channel = Client.GetGuildChannel(Id);
            Name = channel.Name;
            Position = channel.Position;
            ParentId = channel.ParentId;
            PermissionOverwrites = channel.PermissionOverwrites;
        }


        public void Modify(GuildChannelProperties properties)
        {
            if (!properties.NameProperty.Set)
                properties.Name = Name;
            if (!properties.PositionProperty.Set)
                properties.Position = Position;
            if (!properties.ParentProperty.Set)
                properties.ParentId = ParentId;

            GuildChannel channel = Client.ModifyGuildChannel(Id, properties);
            Name = channel.Name;
            Position = channel.Position;
            ParentId = channel.ParentId;
            PermissionOverwrites = channel.PermissionOverwrites;
        }
    }
}
