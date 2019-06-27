using Discord.Webhook;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public class Channel : ControllableModel
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("topic")]
        public string Topic { get; private set; }
        
        [JsonProperty("type")]
        public ChannelType Type { get; private set; }

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


        /// <summary>
        /// Modifies the <see cref="Channel"/>
        /// </summary>
        public Channel Modify(ChannelModProperties properties)
        {
            if (properties.Name == null)
                properties.Name = Name;
            if (properties.Nsfw == null)
                properties.Nsfw = Nsfw;
            if (properties.Position == null)
                properties.Position = Position;

            Channel channel = Client.ModifyChannel(Id, properties);
            Name = channel.Name;
            Topic = channel.Topic;
            Position = channel.Position;
            Nsfw = channel.Nsfw;
            ParentId = channel.ParentId;
            PermissionOverwrites = channel.PermissionOverwrites;
            return channel;
        }


        /// <summary>
        /// Deletes the <see cref="Channel"/>
        /// </summary>
        /// <returns>The deleted <see cref="Channel"/></returns>
        public Channel Delete()
        {
            return Client.DeleteChannel(Id);
        }


        /// <summary>
        /// Triggers a 'user typing...' for the client user
        /// </summary>
        public bool TriggerTyping()
        {
            return Client.TriggerTyping(Id);
        }


        /// <summary>
        /// Sends a message to the channel
        /// </summary>
        public Message SendMessage(string message, bool tts = false)
        {
            return Client.SendMessage(Id, message, tts);
        }


        /// <summary>
        /// Gets a list of channel messages
        /// </summary>
        public List<Message> GetMessages(int limit = 100, int afterId = 0)
        {
            return Client.GetChannelMessages(Id, limit, afterId);
        }


        /// <summary>
        /// Gets all pinned messages in the channel
        /// </summary>
        public List<Message> GetPinnedMessages()
        {
            return Client.GetChannelPinnedMessages(Id);
        }


        /// <summary>
        /// Pins a message in the channel
        /// </summary>
        public bool PinMessage(long messageId)
        {
            return Client.PinChannelMessage(Id, messageId);
        }


        /// <summary>
        /// Pins a message in the channel
        /// </summary>
        public bool PinMessage(Message message)
        {
            return PinMessage(message.Id);
        }

        /// <summary>
        /// Unpins a message from the channel
        /// </summary>
        public bool UnpinMessage(long messageId)
        {
            return Client.UnpinChannelMessage(Id, messageId);
        }


        /// <summary>
        /// Unpins a message from the channel
        /// </summary>
        public bool UnpinMessagE(Message message)
        {
            return Client.UnpinChannelMessage(Id, message.Id);
        }


        /// <summary>
        /// Creates or gets an invite for the channel
        /// </summary>
        public Invite CreateInvite(InviteProperties properties)
        {
            return Client.CreateInvite(Id, properties);
        }


        /// <summary>
        /// Gets all the webhooks in the channel
        /// </summary>
        public List<Hook> GetWebhooks()
        {
            return Client.GetChannelWebhooks(Id);
        }


        /// <summary>
        /// Creates a webhook in the channel
        /// </summary>
        public Hook CreateWebhook(WebhookProperties properties)
        {
            //not gonna let the user change the channel when we're in this context
            properties.ChannelId = null;

            return Client.CreateChannelWebhook(Id, properties);
        }


        public override string ToString()
        {
            return $"#{Name} ({Id})";
        }
    }
}
