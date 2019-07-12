using Newtonsoft.Json;

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
        #endregion


        /// <summary>
        /// Triggers a 'user typing...'
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        public static void TriggerTyping(this DiscordClient client, ulong channelId)
        {
            var resp = client.HttpClient.Post($"/channels/{channelId}/typing");

            if (resp.Content.ReadAsStringAsync().Result.Contains("cooldown"))
                throw new RateLimitException(client, resp.Deserialize<MessageRateLimit>().Cooldown);
        }
    }
}