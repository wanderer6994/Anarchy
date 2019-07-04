using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class ChannelExtensions
    {
        private static T getChannel<T>(this DiscordClient client, long channelId) where T : Channel
        {
            return client.HttpClient.Get($"/channels/{channelId}")
                                .Deserialize<T>().SetClient(client);
        }


        private static Treturn modifyChannel<Treturn, TProperty>(this DiscordClient client, long channelId, TProperty properties) where TProperty : ChannelProperties where Treturn : Channel
        {
            return client.HttpClient.Patch($"/channels/{channelId}", 
                JsonConvert.SerializeObject(properties)).Deserialize<Treturn>().SetClient(client);
        }


        #region channel
        public static Channel GetChannel(this DiscordClient client, long channelId)
        {
            return client.getChannel<Channel>(channelId);
        }


        public static Channel ModifyChannel(this DiscordClient client, long channelId, ChannelProperties properties)
        {
            return client.modifyChannel<Channel, ChannelProperties>(channelId, properties);
        }


        public static Channel DeleteChannel(this DiscordClient client, long channelId)
        {
            return client.HttpClient.Delete($"/channels/{channelId}")
                .Deserialize<Channel>().SetClient(client);
        }
        #endregion


        #region guild channel
        public static GuildChannel GetGuildChannel(this DiscordClient client, long channelId)
        {
            return client.getChannel<GuildChannel>(channelId);
        }


        public static GuildTextChannel GetGuildTextChannel(this DiscordClient client, long channelId)
        {
            return client.getChannel<GuildTextChannel>(channelId);
        }


        public static GuildVoiceChannel GetGuildVoiceChannel(this DiscordClient client, long channelId)
        {
            return client.getChannel<GuildVoiceChannel>(channelId);
        }


        private static T createGuildChannel<T>(this DiscordClient client, long guildId, ChannelCreationProperties properties) where T : GuildChannel
        {
            return client.HttpClient.Post($"/guilds/{guildId}/channels", 
                JsonConvert.SerializeObject(properties)).Deserialize<T>().SetClient(client);
        }


        public static GuildChannel CreateGuildChannel(this DiscordClient client, long guildId, ChannelCreationProperties properties)
        {
            return client.createGuildChannel<GuildChannel>(guildId, properties);
        }


        public static GuildTextChannel CreateGuildTextChannel(this DiscordClient client, long guildId, ChannelCreationProperties properties)
        {
            return client.createGuildChannel<GuildTextChannel>(guildId, properties);
        }


        public static GuildVoiceChannel CreateGuildVoiceChannel(this DiscordClient client, long guildId, ChannelCreationProperties properties)
        {
            return client.createGuildChannel<GuildVoiceChannel>(guildId, properties);
        }


        public static GuildChannel ModifyGuildChannel(this DiscordClient client, long channelId, GuildChannelProperties properties)
        {
            return client.modifyChannel<GuildChannel, GuildChannelProperties>(channelId, properties);
        }


        public static GuildTextChannel ModifyGuildTextChannel(this DiscordClient client, long channelId, GuildTextChannelProperties properties)
        {
            return client.modifyChannel<GuildTextChannel, GuildTextChannelProperties>(channelId, properties);
        }


        public static GuildVoiceChannel ModifyGuildVoiceChannel(this DiscordClient client, long channelId, GuildVoiceChannelProperties properties)
        {
            return client.modifyChannel<GuildVoiceChannel, GuildVoiceChannelProperties>(channelId, properties);
        }
        #endregion


        #region group
        public static Group GetGroup(this DiscordClient client, long groupId)
        {
            return client.getChannel<Group>(groupId);
        }


        public static Group CreateGroup(this DiscordClient client, List<long> recipients)
        {
            return client.HttpClient.Post($"/users/@me/channels", JsonConvert.SerializeObject(new RecipientList() { Recipients = recipients })).Deserialize<Group>().SetClient(client);
        }


        public static Group ModifyGroup(this DiscordClient client, long groupId, GroupProperties properties)
        {
            return client.modifyChannel<Group, GroupProperties>(groupId, properties);
        }


        public static Group LeaveGroup(this DiscordClient client, long groupId)
        {
            return client.HttpClient.Delete($"/channels/{groupId}")
                                        .Deserialize<Group>().SetClient(client);
        }
        #endregion


        #region messages
        public static IReadOnlyList<Message> GetChannelMessages(this DiscordClient client, long channelId, int limit = 100, int afterId = 0)
        {
            return client.HttpClient.Get($"/channels/{channelId}/messages?limit={limit}&after={afterId}")
                                .Deserialize<IReadOnlyList<Message>>().SetClientsInList(client);
        }


        public static IReadOnlyList<Message> GetChannelPinnedMessages(this DiscordClient client, long channelId)
        {
            return client.HttpClient.Get($"/channels/{channelId}/pins")
                                .Deserialize<IReadOnlyList<Message>>().SetClientsInList(client);
        }


        public static void PinChannelMessage(this DiscordClient client, long channelId, long messageId)
        {
            client.HttpClient.Put($"/channels/{channelId}/pins/{messageId}");
        }


        public static void UnpinChannelMessage(this DiscordClient client, long channelId, long messageId)
        {
            client.HttpClient.Delete($"/channels/{channelId}/pins/{messageId}");
        }
        #endregion
    }
}