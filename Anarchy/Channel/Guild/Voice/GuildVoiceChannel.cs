using Newtonsoft.Json;

namespace Discord
{
    public class GuildVoiceChannel : GuildChannel
    {
        [JsonProperty("bitrate")]
        public int Bitrate { get; private set; }

        [JsonProperty("user_limit")]
        public int UserLimit { get; private set; }


        public override void Update()
        {
            GuildVoiceChannel channel = Client.GetGuildVoiceChannel(Id);
            Name = channel.Name;
            Bitrate = channel.Bitrate;
            UserLimit = channel.UserLimit;
            Position = channel.Position;
            ParentId = channel.ParentId;
            PermissionOverwrites = channel.PermissionOverwrites;
        }


        public void Modify(GuildVoiceChannelProperties properties)
        {
            if (!properties.NameProperty.Set)
                properties.Name = Name;
            if (!properties.PositionProperty.Set)
                properties.Position = Position;
            if (!properties.ParentProperty.Set)
                properties.ParentId = ParentId;
            if (!properties.BitrateProperty.Set)
                properties.Bitrate = Bitrate;
            if (!properties.UserLimitProperty.Set)
                properties.UserLimit = UserLimit;

            GuildVoiceChannel channel = Client.ModifyGuildVoiceChannel(Id, properties);
            Name = channel.Name;
            Position = channel.Position;
            ParentId = channel.ParentId;
            PermissionOverwrites = channel.PermissionOverwrites;
            Bitrate = channel.Bitrate;
            UserLimit = channel.UserLimit;
        }
    }
}
