using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Discord
{
    public static class MessageExtensions
    {
        #region management
        /// <summary>
        /// Sends a message to a channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="message">Content of the message</param>
        /// <param name="tts">Whether the message should be TTS or not</param>
        /// <returns>The message</returns>
        public static Message SendMessage(this DiscordClient client, ulong channelId, string message, bool tts = false, Embed embed = null)
        {
            return client.HttpClient.Post($"/channels/{channelId}/messages",
                                JsonConvert.SerializeObject(new MessageProperties() { Content = message, Tts = tts, Embed = embed })).Deserialize<Message>().SetClient(client);
        }


        /// <summary>
        /// Edits a message
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="messageId">ID of the channel</param>
        /// <param name="message">New content of the message</param>
        /// <returns>The edited message</returns>
        public static Message EditMessage(this DiscordClient client, ulong channelId, ulong messageId, string message)
        {
            return client.HttpClient.Patch($"/channels/{channelId}/messages/{messageId}", $"{{\"content\":\"{message}\"}}")
                                .Deserialize<Message>().SetClient(client);
        }


        /// <summary>
        /// Deletes a message
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="messageId">ID of the message</param>
        public static void DeleteMessage(this DiscordClient client, ulong channelId, ulong messageId)
        {
            client.HttpClient.Delete($"/channels/{channelId}/messages/{messageId}");
        }


        /// <summary>
        /// Bulk deletes messages (this is a bot only endpoint)
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="messages">IDs of the messages</param>
        public static void DeleteChannelMessages(this DiscordClient client, ulong channelId, List<ulong> messages)
        {
            client.HttpClient.Post($"/channels/{channelId}/messages/bulk-delete", $"{{\"messages\":{JsonConvert.SerializeObject(messages)}}}");
        }
        #endregion


        /// <summary>
        /// Triggers a 'user typing...'
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        public static void TriggerTyping(this DiscordClient client, ulong channelId)
        {
            var resp = client.HttpClient.Post($"/channels/{channelId}/typing");

            if (resp.Content.ReadAsStringAsync().Result.Contains("cooldown"))
                throw new RateLimitException(client, resp.Deserialize<JObject>().GetValue("message_send_cooldown_ms").ToObject<uint>());
        }


        /// <summary>
        /// Gets a list of messages in a channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="limit">The max amount of messages to return</param>
        /// <param name="afterId">The ID to offset from</param>
        public static IReadOnlyList<Message> GetChannelMessages(this DiscordClient client, ulong channelId, uint limit = 100, ulong afterId = 0)
        {
            return client.HttpClient.Get($"/channels/{channelId}/messages?limit={limit}{(afterId != 0 ? $"&after={afterId}" : "")}")
                                .Deserialize<IReadOnlyList<Message>>().SetClientsInList(client);
        }


        #region pins
        /// <summary>
        /// Gets a channel's pinned messages
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        public static IReadOnlyList<Message> GetChannelPinnedMessages(this DiscordClient client, ulong channelId)
        {
            return client.HttpClient.Get($"/channels/{channelId}/pins")
                                .Deserialize<IReadOnlyList<Message>>().SetClientsInList(client);
        }


        /// <summary>
        /// Pins a message to a channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="messageId">ID of the message</param>
        public static void PinChannelMessage(this DiscordClient client, ulong channelId, ulong messageId)
        {
            client.HttpClient.Put($"/channels/{channelId}/pins/{messageId}");
        }


        /// <summary>
        /// Unpins a message from a channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="messageId">ID of the message</param>
        public static void UnpinChannelMessage(this DiscordClient client, ulong channelId, ulong messageId)
        {
            client.HttpClient.Delete($"/channels/{channelId}/pins/{messageId}");
        }
        #endregion
    }
}