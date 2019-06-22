using Newtonsoft.Json;
using System.Net;

namespace Discord
{
    public static class MessageExtensions
    {
        public static void TriggerTyping(this DiscordClient client, long channelId)
        {
            var resp = client.HttpClient.PostAsync($"/channels/{channelId}/typing").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);
            
            string content = resp.Content.ReadAsStringAsync().Result;
            if (content.Contains("cooldown"))
                throw new TooManyRequestsException(client, JsonConvert.DeserializeObject<MessageRateLimit>(content).Cooldown);
        }
        
        public static Message SendMessage(this DiscordClient client, long channelId, string message, bool tts = false)
        {
            var resp = client.HttpClient.PostAsync($"/channels/{channelId}/messages", JsonConvert.SerializeObject(new MessageProperties() { Content = message, Tts = tts })).Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            Message msg = JsonConvert.DeserializeObject<Message>(resp.Content.ReadAsStringAsync().Result);
            msg.Client = client;
            return msg;
        }
        
        public static Message EditMessage(this DiscordClient client, long channelId, long messageId, string msg)
        {
            var resp = client.HttpClient.PatchAsync($"/channels/{channelId}/messages/{messageId}", "{\"content\":\"" + msg + "\"}").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new MessageNotFoundException(client, messageId);

            Message edited = JsonConvert.DeserializeObject<Message>(resp.Content.ReadAsStringAsync().Result);
            edited.Client = client;
            return edited;
        }
        
        public static bool DeleteMessage(this DiscordClient client, long channelId, long messageId)
        {
            var resp = client.HttpClient.DeleteAsync($"/channels/{channelId}/messages/{messageId}").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new MessageNotFoundException(client, messageId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
    }
}