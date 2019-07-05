using Newtonsoft.Json;

namespace Discord
{
    public static class MessageExtensions
    {
        #region management
        public static Message SendMessage(this DiscordClient client, long channelId, MessageProperties properties)
        {
            return client.HttpClient.Post($"/channels/{channelId}/messages", 
                                JsonConvert.SerializeObject(properties)).Deserialize<Message>().SetClient(client);
        }


        public static Message SendMessage(this DiscordClient client, long channelId, string message, bool tts = false)
        {
            return client.SendMessage(channelId, new MessageProperties() { Content = message, Tts = tts });
        }


        public static Message EditMessage(this DiscordClient client, long channelId, long messageId, string msg)
        {
            return client.HttpClient.Patch($"/channels/{channelId}/messages/{messageId}", "{\"content\":\"" + msg + "\"}")
                                .Deserialize<Message>().SetClient(client);
        }


        public static void DeleteMessage(this DiscordClient client, long channelId, long messageId)
        {
            client.HttpClient.Delete($"/channels/{channelId}/messages/{messageId}");
        }
        #endregion


        public static void TriggerTyping(this DiscordClient client, long channelId)
        {
            var resp = client.HttpClient.Post($"/channels/{channelId}/typing");

            if (resp.Content.ReadAsStringAsync().Result.Contains("cooldown"))
                throw new TooManyRequestsException(client,  resp.Deserialize<MessageRateLimit>().Cooldown);
        }
    }
}