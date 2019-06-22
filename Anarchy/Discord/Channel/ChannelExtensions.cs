using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class ChannelExtensions
    {
        public static Channel CreateChannel(this DiscordClient client, long guildId, ChannelProperties properties)
        {
            var resp = client.HttpClient.PostAsync($"/guilds/{guildId}/channels", JsonConvert.SerializeObject(properties)).Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);
            
            Channel channel = JsonConvert.DeserializeObject<Channel>(resp.Content.ReadAsStringAsync().Result);
            channel.Client = client;
            return channel;
        }

        public static Channel ModifyChannel(this DiscordClient client, long channelId, ChannelProperties properties)
        {
            var resp = client.HttpClient.PatchAsync($"/channels/{channelId}", JsonConvert.SerializeObject(properties)).Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            Channel channel = JsonConvert.DeserializeObject<Channel>(resp.Content.ReadAsStringAsync().Result);
            channel.Client = client;
            return channel;
        }

        public static Channel DeleteChannel(this DiscordClient client, long channelId)
        {
            var resp = client.HttpClient.DeleteAsync($"/channels/{channelId}").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            Channel channel = JsonConvert.DeserializeObject<Channel>(resp.Content.ReadAsStringAsync().Result);
            channel.Client = client;
            return channel;
        }

        public static Channel GetChannel(this DiscordClient client, long channelId)
        {
            var resp = client.HttpClient.GetAsync($"/channels/{channelId}").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            Channel channel = JsonConvert.DeserializeObject<Channel>(resp.Content.ReadAsStringAsync().Result);
            channel.Client = client;
            return channel;
        }

        public static List<Message> GetChannelMessages(this DiscordClient client, long channelId, int limit = 100, int afterId = 0)
        {
            var resp = client.HttpClient.GetAsync($"/channels/{channelId}/messages?limit={limit}&after={afterId}").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);
            
            List<Message> messages = JsonConvert.DeserializeObject<List<Message>>(resp.Content.ReadAsStringAsync().Result);
            foreach (var message in messages) message.Client = client;
            return messages;
        }

        #region pinning
        public static List<Message> GetChannelPinnedMessages(this DiscordClient client, long channelId)
        {
            var resp = client.HttpClient.GetAsync($"/channels/{channelId}/pins").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            List<Message> messages = JsonConvert.DeserializeObject<List<Message>>(resp.Content.ReadAsStringAsync().Result);
            foreach (var message in messages) message.Client = client;
            return messages;
        }

        public static bool PinChannelMessage(this DiscordClient client, long channelId, long messageId)
        {
            var resp = client.HttpClient.PutAsync($"/channels/{channelId}/pins/{messageId}").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new MessageNotFoundException(client, messageId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }

        public static bool UnpinChannelMessage(this DiscordClient client, long channelId, long messageId)
        {
            var resp = client.HttpClient.DeleteAsync($"/channels/{channelId}/pins/{messageId}").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new MessageNotFoundException(client, messageId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
        #endregion

        public static Channel CreateDM(this DiscordClient client, long recipientId)
        {
            var resp = client.HttpClient.PostAsync("/users/@me/channels", "{\"recipient_id\":\"" + recipientId + "\"}").Result;
            
            Channel channel = JsonConvert.DeserializeObject<Channel>(resp.Content.ReadAsStringAsync().Result);
            channel.Client = client;
            return channel;
        }
        
        public static Channel LeaveGroup(this DiscordClient client, long groupId)
        {
            return client.DeleteChannel(groupId);
        }
    }
}