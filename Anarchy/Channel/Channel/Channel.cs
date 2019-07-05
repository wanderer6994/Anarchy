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


        public virtual void Update()
        {
            Name = Client.GetChannel(Id).Name;
        }


        public void Modify(ChannelProperties properties)
        {
            if (!properties.NameProperty.Set)
                return;

            Name = Client.ModifyChannel(Id, properties).Name;
        }


        public Channel Delete()
        {
            return Client.DeleteChannel(Id);
        }


        #region messages
        public void TriggerTyping()
        {
            Client.TriggerTyping(Id);
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


        public void PinMessage(long messageId)
        {
            Client.PinChannelMessage(Id, messageId);
        }


        public void PinMessage(Message message)
        {
            PinMessage(message.Id);
        }


        public void UnpinMessage(long messageId)
        {
            Client.UnpinChannelMessage(Id, messageId);
        }


        public void UnpinMessage(Message message)
        {
            Client.UnpinChannelMessage(Id, message.Id);
        }
        #endregion


        public override string ToString()
        {
            return Name;
        }
    }
}