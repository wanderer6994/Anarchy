using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class ChannelExtensions
    {
        #region management
        /// <summary>
        /// Creates a guild channel
        /// </summary>
        public static Channel CreateGuildChannel(this DiscordClient client, long guildId, ChannelCreationProperties properties)
        {
            var resp = client.HttpClient.Post($"/guilds/{guildId}/channels", JsonConvert.SerializeObject(properties));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            return resp.Content.Json<Channel>().SetClient(client);
        }


        /// <summary>
        /// Modifies a channel
        /// </summary>
        public static Channel ModifyChannel(this DiscordClient client, long channelId, ChannelModProperties properties)
        {
            var resp = client.HttpClient.Patch($"/channels/{channelId}", JsonConvert.SerializeObject(properties));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            return resp.Content.Json<Channel>().SetClient(client);
        }


        /// <summary>
        /// Deletes a channel
        /// </summary>
        public static Channel DeleteChannel(this DiscordClient client, long channelId)
        {
            var resp = client.HttpClient.Delete($"/channels/{channelId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            return resp.Content.Json<Channel>().SetClient(client);
        }
        #endregion


        /// <summary>
        /// Gets a channel
        /// </summary>
        public static Channel GetChannel(this DiscordClient client, long channelId)
        {
            var resp = client.HttpClient.Get($"/channels/{channelId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            return resp.Content.Json<Channel>().SetClient(client);
        }


        /// <summary>
        /// Gets a list of messages in a channel
        /// </summary>
        public static List<Message> GetChannelMessages(this DiscordClient client, long channelId, int limit = 100, int afterId = 0)
        {
            var resp = client.HttpClient.Get($"/channels/{channelId}/messages?limit={limit}&after={afterId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            return resp.Content.Json<List<Message>>().SetClientsInList(client);
        }


        /// <summary>
        /// Gets a list of pinned channel messages
        /// </summary>
        public static List<Message> GetChannelPinnedMessages(this DiscordClient client, long channelId)
        {
            var resp = client.HttpClient.Get($"/channels/{channelId}/pins");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            return resp.Content.Json<List<Message>>().SetClientsInList(client);
        }


        /// <summary>
        /// Pins a message
        /// </summary>
        public static bool PinChannelMessage(this DiscordClient client, long channelId, long messageId)
        {
            var resp = client.HttpClient.Put($"/channels/{channelId}/pins/{messageId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new MessageNotFoundException(client, messageId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }

        /// <summary>
        /// Unpins a message
        /// </summary>
        public static bool UnpinChannelMessage(this DiscordClient client, long channelId, long messageId)
        {
            var resp = client.HttpClient.Delete($"/channels/{channelId}/pins/{messageId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new MessageNotFoundException(client, messageId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }


        /// <summary>
        /// Leaves a group
        /// </summary>
        public static Channel LeaveGroup(this DiscordClient client, long groupId)
        {
            return client.DeleteChannel(groupId);
        }
    }
}