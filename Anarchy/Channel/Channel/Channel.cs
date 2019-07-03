using Discord.Webhook;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public class Channel : Controllable
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; protected set; }

        [JsonProperty("type")]
        public ChannelType Type { get; private set; }


        public GuildChannel Delete()
        {
            return Client.DeleteChannel(Id);
        }


        public bool TriggerTyping()
        {
            return Client.TriggerTyping(Id);
        }


        public Message SendMessage(MessageProperties properties)
        {
            return Client.SendMessage(Id, properties);
        }


        public Message SendMessage(string message, bool tts = false)
        {
            return Client.SendMessage(Id, message, tts);
        }


        public IReadOnlyList<Message> GetMessages(int limit = 100, int afterId = 0)
        {
            return Client.GetChannelMessages(Id, limit, afterId);
        }


        public IReadOnlyList<Message> GetPinnedMessages()
        {
            return Client.GetChannelPinnedMessages(Id);
        }


        public bool PinMessage(long messageId)
        {
            return Client.PinChannelMessage(Id, messageId);
        }


        public bool PinMessage(Message message)
        {
            return PinMessage(message.Id);
        }


        public bool UnpinMessage(long messageId)
        {
            return Client.UnpinChannelMessage(Id, messageId);
        }


        public bool UnpinMessage(Message message)
        {
            return Client.UnpinChannelMessage(Id, message.Id);
        }


        public Invite CreateInvite(InviteProperties properties = null)
        {
            return Client.CreateInvite(Id, properties);
        }


        public IReadOnlyList<Hook> GetWebhooks()
        {
            return Client.GetChannelWebhooks(Id);
        }


        public Hook CreateWebhook(WebhookProperties properties)
        {
            return Client.CreateChannelWebhook(Id, properties);
        }


        public override string ToString()
        {
            return Name;
        }
    }
}