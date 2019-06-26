using Discord.Webhook;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public class Channel : ClientMember
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

        public Channel Modify(ChannelModProperties properties)
        {
            Channel channel = Client.ModifyChannel(Id, properties);
            Name = channel.Name;
            Topic = channel.Topic;
            Position = channel.Position;
            Nsfw = channel.Nsfw;
            ParentId = channel.ParentId;
            PermissionOverwrites = channel.PermissionOverwrites;
            return channel;
        }


        public Channel Delete()
        {
            return Client.DeleteChannel(Id);
        }


        public bool AddPermissionOverwrite(PermissionOverwrite permission)
        {
            if (Client.AddChannelPermissionOverwrite(Id, permission))
            {
                PermissionOverwrites.Add(permission);

                return true;
            }
            else
                return false;
        }


        public void TriggerTyping()
        {
            Client.TriggerTyping(Id);
        }


        public Message SendMessage(string message, bool tts = false)
        {
            return Client.SendMessage(Id, message, tts);
        }


        public List<Message> GetMessages(int limit = 100, int afterId = 0)
        {
            return Client.GetChannelMessages(Id, limit, afterId);
        }


        public List<Message> GetPinnedMessages()
        {
            return Client.GetChannelPinnedMessages(Id);
        }


        #region pin message
        public bool PinMessage(long messageId)
        {
            return Client.PinChannelMessage(Id, messageId);
        }


        public bool PinMessage(Message message)
        {
            return PinMessage(message.Id);
        }
        #endregion


        #region unpin message
        public bool UnpinMessage(long messageId)
        {
            return Client.UnpinChannelMessage(Id, messageId);
        }


        public bool UnpinMessagE(Message message)
        {
            return Client.UnpinChannelMessage(Id, message.Id);
        }
        #endregion


        public Invite CreateInvite(InviteProperties properties)
        {
            return Client.CreateInvite(Id, properties);
        }


        public List<Hook> GetWebhooks()
        {
            return Client.GetChannelWebhooks(Id);
        }


        public Hook CreateWebhook(WebhookProperties properties)
        {
            //awkward approach but it works xd
            properties.ChannelId = null;

            return Client.CreateChannelWebhook(Id, properties);
        }


        public override string ToString()
        {
            return $"#{Name} ({Id})";
        }
    }
}
