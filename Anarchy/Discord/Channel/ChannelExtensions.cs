using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class ChannelExtensions
    {
        public static Channel CreateChannel(this DiscordClient client, long guildId, ChannelCreationProperties properties)
        {
            var resp = client.HttpClient.Post($"/guilds/{guildId}/channels", JsonConvert.SerializeObject(properties));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);
            
            Channel channel = JsonConvert.DeserializeObject<Channel>(resp.Content.ReadAsStringAsync().Result);
            channel.Client = client;
            return channel;
        }


        public static Channel ModifyChannel(this DiscordClient client, long channelId, ChannelModProperties properties)
        {
            var resp = client.HttpClient.Patch($"/channels/{channelId}", JsonConvert.SerializeObject(properties));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            Channel channel = JsonConvert.DeserializeObject<Channel>(resp.Content.ReadAsStringAsync().Result);
            channel.Client = client;
            return channel;
        }


        public static Channel DeleteChannel(this DiscordClient client, long channelId)
        {
            var resp = client.HttpClient.Delete($"/channels/{channelId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            Channel channel = JsonConvert.DeserializeObject<Channel>(resp.Content.ReadAsStringAsync().Result);
            channel.Client = client;
            return channel;
        }


        public static Channel GetChannel(this DiscordClient client, long channelId)
        {
            var resp = client.HttpClient.Get($"/channels/{channelId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            Channel channel = JsonConvert.DeserializeObject<Channel>(resp.Content.ReadAsStringAsync().Result);
            channel.Client = client;
            return channel;
        }

        public static bool AddChannelPermissionOverwrite(this DiscordClient client, long channelId, PermissionOverwrite permission)
        {
            var resp = client.HttpClient.Put($"/channels/{channelId}/permissions/{permission.Id}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }

        public static List<Message> GetChannelMessages(this DiscordClient client, long channelId, int limit = 100, int afterId = 0)
        {
            var resp = client.HttpClient.Get($"/channels/{channelId}/messages?limit={limit}&after={afterId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);
            
            List<Message> messages = JsonConvert.DeserializeObject<List<Message>>(resp.Content.ReadAsStringAsync().Result);
            foreach (var message in messages) message.Client = client;
            return messages;
        }


        #region pinning
        public static List<Message> GetChannelPinnedMessages(this DiscordClient client, long channelId)
        {
            var resp = client.HttpClient.Get($"/channels/{channelId}/pins");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            List<Message> messages = JsonConvert.DeserializeObject<List<Message>>(resp.Content.ReadAsStringAsync().Result);
            foreach (var message in messages) message.Client = client;
            return messages;
        }


        public static bool PinChannelMessage(this DiscordClient client, long channelId, long messageId)
        {
            var resp = client.HttpClient.Put($"/channels/{channelId}/pins/{messageId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new MessageNotFoundException(client, messageId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }

        public static bool UnpinChannelMessage(this DiscordClient client, long channelId, long messageId)
        {
            var resp = client.HttpClient.Delete($"/channels/{channelId}/pins/{messageId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new MessageNotFoundException(client, messageId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
        #endregion


        public static Channel CreateDM(this DiscordClient client, long recipientId)
        {
            var resp = client.HttpClient.Post("/users/@me/channels", "{\"recipient_id\":\"" + recipientId + "\"}");
            
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