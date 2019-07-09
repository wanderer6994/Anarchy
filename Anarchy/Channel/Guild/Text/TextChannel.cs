using Discord.Webhook;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public class TextChannel : GuildChannel
    {
        [JsonProperty("topic")]
        public string Topic { get; private set; }


        [JsonProperty("nsfw")]
        public bool Nsfw { get; private set; }


        //this is in seconds btw
        [JsonProperty("rate_limit_per_user")]
        public int SlowMode { get; private set; }


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
            if (!properties.SlowModeProperty.Set)
                properties.SlowMode = SlowMode;
            if (!properties.PositionProperty.Set)
                properties.Position = Position;
            if (!properties.ParentProperty.Set)
                properties.ParentId = ParentId;

            TextChannel channel = Client.ModifyTextChannel(Id, properties);
            Name = channel.Name;
            Topic = channel.Topic;
            Nsfw = channel.Nsfw;
            SlowMode = channel.SlowMode;
            Position = channel.Position;
            ParentId = channel.ParentId;
            PermissionOverwrites = channel.PermissionOverwrites;
        }


        public IReadOnlyList<Hook> GetWebhooks()
        {
            return Client.GetChannelWebhooks(Id);
        }


        public Hook CreateWebhook(WebhookProperties properties)
        {
            return Client.CreateChannelWebhook(Id, properties);
        }
    }
}
