using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public static class ChannelExtensions
    {
        private static T getChannel<T>(this DiscordClient client, ulong channelId) where T : Channel
        {
            return client.HttpClient.Get($"/channels/{channelId}")
                                .Deserialize<T>().SetClient(client);
        }


        private static Treturn modifyChannel<Treturn, TProperty>(this DiscordClient client, ulong channelId, TProperty properties) where TProperty : ChannelProperties where Treturn : Channel
        {
            return client.HttpClient.Patch($"/channels/{channelId}", 
                                JsonConvert.SerializeObject(properties)).Deserialize<Treturn>().SetClient(client);
        }


        #region channel
        public static Channel GetChannel(this DiscordClient client, ulong channelId)
        {
            return client.getChannel<Channel>(channelId);
        }


        public static Channel ModifyChannel(this DiscordClient client, ulong channelId, ChannelProperties properties)
        {
            return client.modifyChannel<Channel, ChannelProperties>(channelId, properties);
        }


        public static Channel DeleteChannel(this DiscordClient client, ulong channelId)
        {
            return client.HttpClient.Delete($"/channels/{channelId}")
                .Deserialize<Channel>().SetClient(client);
        }
        #endregion


        #region guild channel
        public static GuildChannel GetGuildChannel(this DiscordClient client, ulong channelId)
        {
            return client.getChannel<GuildChannel>(channelId);
        }


        public static TextChannel GetTextChannel(this DiscordClient client, ulong channelId)
        {
            return client.getChannel<TextChannel>(channelId);
        }


        public static VoiceChannel GetVoiceChannel(this DiscordClient client, ulong channelId)
        {
            return client.getChannel<VoiceChannel>(channelId);
        }


        private static T createGuildChannel<T>(this DiscordClient client, ulong guildId, ChannelCreationProperties properties) where T : GuildChannel
        {
            return client.HttpClient.Post($"/guilds/{guildId}/channels", 
                                JsonConvert.SerializeObject(properties)).Deserialize<T>().SetClient(client);
        }


        public static GuildChannel CreateGuildChannel(this DiscordClient client, ulong guildId, ChannelCreationProperties properties)
        {
            return client.createGuildChannel<GuildChannel>(guildId, properties);
        }


        public static TextChannel CreateTextChannel(this DiscordClient client, ulong guildId, ChannelCreationProperties properties)
        {
            properties.Type = ChannelType.Text;
            return client.createGuildChannel<TextChannel>(guildId, properties);
        }


        public static VoiceChannel CreateVoiceChannel(this DiscordClient client, ulong guildId, ChannelCreationProperties properties)
        {
            properties.Type = ChannelType.Voice;
            return client.createGuildChannel<VoiceChannel>(guildId, properties);
        }


        public static GuildChannel ModifyGuildChannel(this DiscordClient client, ulong channelId, GuildChannelProperties properties)
        {
            return client.modifyChannel<GuildChannel, GuildChannelProperties>(channelId, properties);
        }


        public static TextChannel ModifyTextChannel(this DiscordClient client, ulong channelId, TextChannelProperties properties)
        {
            return client.modifyChannel<TextChannel, TextChannelProperties>(channelId, properties);
        }


        public static VoiceChannel ModifyVoiceChannel(this DiscordClient client, ulong channelId, VoiceChannelProperties properties)
        {
            return client.modifyChannel<VoiceChannel, VoiceChannelProperties>(channelId, properties);
        }


        public static void AddPermissionOverwrite(this DiscordClient client, ulong channelId, PermissionOverwrite overwrite)
        {
            client.HttpClient.Put($"/channels/{channelId}/permissions/{overwrite.Id}", JsonConvert.SerializeObject(overwrite));
        }


        public static void RemovePermissionOverwrite(this DiscordClient client, ulong channelId, ulong id)
        {
            client.HttpClient.Delete($"/channels/{channelId}/permissions/{id}");
        }
        #endregion


        #region group
        public static PartialInvite JoinGroup(this DiscordClient client, string inviteCode)
        {
            return client.HttpClient.Post($"/invites/{inviteCode}").Deserialize<PartialInvite>().SetClient(client);
        }


        public static Group GetGroup(this DiscordClient client, ulong groupId)
        {
            return client.getChannel<Group>(groupId);
        }


        public static Group CreateGroup(this DiscordClient client, List<ulong> recipients)
        {
            return client.HttpClient.Post($"/users/@me/channels", JsonConvert.SerializeObject(new RecipientList() { Recipients = recipients })).Deserialize<Group>().SetClient(client);
        }


        public static Group ModifyGroup(this DiscordClient client, ulong groupId, GroupProperties properties)
        {
            return client.modifyChannel<Group, GroupProperties>(groupId, properties);
        }


        public static Group LeaveGroup(this DiscordClient client, ulong groupId)
        {
            return client.HttpClient.Delete($"/channels/{groupId}")
                                        .Deserialize<Group>().SetClient(client);
        }


        public static void AddUserToGroup(this DiscordClient client, ulong groupId, ulong userId)
        {
            client.HttpClient.Put($"/channels/{groupId}/recipients/{userId}");
        }


        public static void RemoveUserFromGroup(this DiscordClient client, ulong groupId, ulong userId)
        {
            client.HttpClient.Delete($"/channels/{groupId}/recipients/{userId}");
        }
        #endregion


        #region messages
        public static IReadOnlyList<Message> GetChannelMessages(this DiscordClient client, ulong channelId, uint limit = 100, ulong afterId = 0)
        {
            return client.HttpClient.Get($"/channels/{channelId}/messages?limit={limit}&after={afterId}")
                                .Deserialize<IReadOnlyList<Message>>().SetClientsInList(client);
        }


        public static IReadOnlyList<Message> GetChannelPinnedMessages(this DiscordClient client, ulong channelId)
        {
            return client.HttpClient.Get($"/channels/{channelId}/pins")
                                .Deserialize<IReadOnlyList<Message>>().SetClientsInList(client);
        }


        public static void PinChannelMessage(this DiscordClient client, ulong channelId, ulong messageId)
        {
            client.HttpClient.Put($"/channels/{channelId}/pins/{messageId}");
        }


        public static void UnpinChannelMessage(this DiscordClient client, ulong channelId, ulong messageId)
        {
            client.HttpClient.Delete($"/channels/{channelId}/pins/{messageId}");
        }
        #endregion
    }
}