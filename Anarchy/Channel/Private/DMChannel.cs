using System.Collections.Generic;
using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// Represents a <see cref="Channel"/> specific to direct message channels
    /// </summary>
    public class DMChannel : Channel
    {
        public DMChannel()
        {
            OnClientUpdated += (sender, e) => Recipients.SetClientsInList(Client);
        }


        [JsonProperty("recipients")]
        public IReadOnlyList<User> Recipients { get; protected set; }


        /// <summary>
        /// Updates the channel's info
        /// </summary>
        public override void Update()
        {
            Recipients = Client.GetDMChannel(Id).Recipients;
        }


        /// <summary>
        /// Closes the DM
        /// </summary>
        public void Leave()
        {
            Client.CloseDM(Id);
        }


        #region messages
        /// <summary>
        /// Triggers a 'user typing...'
        /// </summary>
        public void TriggerTyping()
        {
            Client.TriggerTyping(Id);
        }


        /// <summary>
        /// Sends a message to the channel
        /// </summary>
        /// <param name="message">Content of the message</param>
        /// <param name="tts">Whether the message should be TTS or not</param>
        /// <returns>The message</returns>
        public Message SendMessage(string message, bool tts = false, Embed embed = null)
        {
            return Client.SendMessage(Id, message, tts, embed);
        }


        /// <summary>
        /// Gets a list of messages in the channel
        /// </summary>
        /// <param name="limit">The max amount of messages to return</param>
        /// <param name="afterId">The ID to offset from</param>
        public IReadOnlyList<Message> GetMessages(uint limit = 100, uint afterId = 0)
        {
            return Client.GetChannelMessages(Id, limit, afterId);
        }


        /// <summary>
        /// Gets the channel's pinned messages
        /// </summary>
        public IReadOnlyList<Message> GetPinnedMessages()
        {
            return Client.GetChannelPinnedMessages(Id);
        }


        /// <summary>
        /// Pins a message to the channel
        /// </summary>
        /// <param name="messageId">ID of the message</param>
        public void PinMessage(ulong messageId)
        {
            Client.PinChannelMessage(Id, messageId);
        }


        /// <summary>
        /// Pins a message to the channel
        /// </summary>
        public void PinMessage(Message message)
        {
            PinMessage(message.Id);
        }


        /// <summary>
        /// Unpins a message from the channel
        /// </summary>
        /// <param name="messageId">ID of the message</param>
        public void UnpinMessage(ulong messageId)
        {
            Client.UnpinChannelMessage(Id, messageId);
        }


        /// <summary>
        /// Unpins a message from the channel
        /// </summary>
        public void UnpinMessage(Message message)
        {
            Client.UnpinChannelMessage(Id, message.Id);
        }
        #endregion
    }
}
