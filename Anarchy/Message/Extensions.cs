using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class MessageExtensions
    {
        #region management
        public static Message SendMessage(this DiscordClient client, long channelId, string message, bool tts = false)
        {
            var resp = client.HttpClient.Post($"/channels/{channelId}/messages", JsonConvert.SerializeObject(new MessageProperties() { Content = message, Tts = tts }));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            return resp.Deserialize<Message>().SetClient(client);
        }


        public static Message EditMessage(this DiscordClient client, long channelId, long messageId, string msg)
        {
            var resp = client.HttpClient.Patch($"/channels/{channelId}/messages/{messageId}", "{\"content\":\"" + msg + "\"}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new MessageNotFoundException(client, messageId);

            return resp.Deserialize<Message>().SetClient(client);
        }


        public static bool DeleteMessage(this DiscordClient client, long channelId, long messageId)
        {
            var resp = client.HttpClient.Delete($"/channels/{channelId}/messages/{messageId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new MessageNotFoundException(client, messageId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
        #endregion


        public static bool TriggerTyping(this DiscordClient client, long channelId)
        {
            var resp = client.HttpClient.Post($"/channels/{channelId}/typing");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);
            
            if (resp.Content.ReadAsStringAsync().Result.Contains("cooldown"))
                throw new TooManyRequestsException(client,  resp.Deserialize<MessageRateLimit>().Cooldown);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
    }
}