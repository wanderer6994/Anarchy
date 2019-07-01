using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class ChannelExtensions
    {
        #region management
        public static Channel CreateGuildChannel(this DiscordClient client, long guildId, ChannelCreationProperties properties)
        {
            var resp = client.HttpClient.Post($"/guilds/{guildId}/channels", JsonConvert.SerializeObject(properties));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            return resp.Deserialize<Channel>().SetClient(client);
        }


        public static Channel ModifyChannel(this DiscordClient client, long channelId, ChannelModProperties properties)
        {
            var resp = client.HttpClient.Patch($"/channels/{channelId}", JsonConvert.SerializeObject(properties));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            return resp.Deserialize<Channel>().SetClient(client);
        }


        public static Channel DeleteChannel(this DiscordClient client, long channelId)
        {
            var resp = client.HttpClient.Delete($"/channels/{channelId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            return resp.Deserialize<Channel>().SetClient(client);
        }
        #endregion


        public static Channel GetChannel(this DiscordClient client, long channelId)
        {
            var resp = client.HttpClient.Get($"/channels/{channelId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            return resp.Deserialize<Channel>().SetClient(client);
        }


        #region messages
        public static IReadOnlyList<Message> GetChannelMessages(this DiscordClient client, long channelId, int limit = 100, int afterId = 0)
        {
            var resp = client.HttpClient.Get($"/channels/{channelId}/messages?limit={limit}&after={afterId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            string content = resp.Content.ReadAsStringAsync().Result;

            return resp.Deserialize<IReadOnlyList<Message>>().SetClientsInList(client);
        }


        public static IReadOnlyList<Message> GetChannelPinnedMessages(this DiscordClient client, long channelId)
        {
            var resp = client.HttpClient.Get($"/channels/{channelId}/pins");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            return resp.Deserialize<IReadOnlyList<Message>>().SetClientsInList(client);
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


        public static Channel LeaveGroup(this DiscordClient client, long groupId)
        {
            return client.DeleteChannel(groupId);
        }
    }
}