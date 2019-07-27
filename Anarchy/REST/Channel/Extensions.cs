using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public static class ChannelExtensions
    {
#pragma warning disable IDE1006
        private static T getChannel<T>(this DiscordClient client, ulong channelId) where T : Channel
        {
            return client.HttpClient.Get($"/channels/{channelId}")
                                .Deserialize<T>().SetClient(client);
        }


        private static T createChannel<T>(this DiscordClient client, string json) where T : Channel
        {
            return client.HttpClient.Post($"/users/@me/channels", json)
                                .Deserialize<T>().SetClient(client);
        }


        private static Treturn modifyChannel<Treturn, TProperty>(this DiscordClient client, ulong channelId, TProperty properties) where TProperty : ChannelProperties where Treturn : Channel
        {
            return client.HttpClient.Patch($"/channels/{channelId}", 
                                JsonConvert.SerializeObject(properties)).Deserialize<Treturn>().SetClient(client);
        }

        private static T deleteChannel<T>(this DiscordClient client, ulong channelId) where T : Channel
        {
            return client.HttpClient.Delete($"/channels/{channelId}")
                .Deserialize<T>().SetClient(client);
        }


        #region channel
        /// <summary>
        /// Gets a channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        public static Channel GetChannel(this DiscordClient client, ulong channelId)
        {
            return client.getChannel<Channel>(channelId);
        }


        /// <summary>
        /// Modifies a channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="properties">Options for modifying the channel</param>
        /// <returns>The modified <see cref="Channel"/></returns>
        public static Channel ModifyChannel(this DiscordClient client, ulong channelId, ChannelProperties properties)
        {
            return client.modifyChannel<Channel, ChannelProperties>(channelId, properties);
        }


        /// <summary>
        /// Deletes a channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>The deleted channel</returns>
        public static Channel DeleteChannel(this DiscordClient client, ulong channelId)
        {
            return client.deleteChannel<Channel>(channelId);
        }
        #endregion


        #region guild channel
        /// <summary>
        /// Gets a guild channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        public static GuildChannel GetGuildChannel(this DiscordClient client, ulong channelId)
        {
            return client.getChannel<GuildChannel>(channelId);
        }


        /// <summary>
        /// Gets a guild text channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        public static TextChannel GetTextChannel(this DiscordClient client, ulong channelId)
        {
            return client.getChannel<TextChannel>(channelId);
        }


        /// <summary>
        /// Gets a guild voice channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        public static VoiceChannel GetVoiceChannel(this DiscordClient client, ulong channelId)
        {
            return client.getChannel<VoiceChannel>(channelId);
        }

        
        private static T createGuildChannel<T>(this DiscordClient client, ulong guildId, ChannelCreationProperties properties) where T : GuildChannel
        {
            return client.HttpClient.Post($"/guilds/{guildId}/channels", 
                                JsonConvert.SerializeObject(properties)).Deserialize<T>().SetClient(client);
        }


        /// <summary>
        /// Creates a guild channel
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        /// <param name="properties">Options for creating the channel</param>
        /// <returns>The created <see cref="GuildChannel"/></returns>
        public static GuildChannel CreateGuildChannel(this DiscordClient client, ulong guildId, ChannelCreationProperties properties)
        {
            return client.createGuildChannel<GuildChannel>(guildId, properties);
        }


        /// <summary>
        /// Creates a guild text channel
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        /// <param name="properties">Options for creating the channel</param>
        /// <returns>The created <see cref="TextChannel"/></returns>
        public static TextChannel CreateTextChannel(this DiscordClient client, ulong guildId, ChannelCreationProperties properties)
        {
            properties.Type = ChannelType.Text;
            return client.createGuildChannel<TextChannel>(guildId, properties);
        }


        /// <summary>
        /// Creates a guild voice channel
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        /// <param name="properties">Options for creating the channel</param>
        /// <returns>The created <see cref="VoiceChannel"/></returns>
        public static VoiceChannel CreateVoiceChannel(this DiscordClient client, ulong guildId, ChannelCreationProperties properties)
        {
            properties.Type = ChannelType.Voice;
            return client.createGuildChannel<VoiceChannel>(guildId, properties);
        }


        /// <summary>
        /// Modifies a guild channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="properties">Options for modifying the guild channel</param>
        /// <returns>The modified <see cref="GuildChannel"/></returns>
        public static GuildChannel ModifyGuildChannel(this DiscordClient client, ulong channelId, GuildChannelProperties properties)
        {
            return client.modifyChannel<GuildChannel, GuildChannelProperties>(channelId, properties);
        }


        /// <summary>
        /// Modifies a guild text channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="properties">Options for modifying the channel</param>
        /// <returns>The modified <see cref="TextChannel"/></returns>
        public static TextChannel ModifyTextChannel(this DiscordClient client, ulong channelId, TextChannelProperties properties)
        {
            return client.modifyChannel<TextChannel, TextChannelProperties>(channelId, properties);
        }


        /// <summary>
        /// Modifies a guild voice channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="properties">Options for modifying the channel</param>
        /// <returns>The modified <see cref="VoiceChannel"/></returns>
        public static VoiceChannel ModifyVoiceChannel(this DiscordClient client, ulong channelId, VoiceChannelProperties properties)
        {
            return client.modifyChannel<VoiceChannel, VoiceChannelProperties>(channelId, properties);
        }


        /// <summary>
        /// Deletes a guild channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>The deleted <see cref="GuildChannel"/></returns>
        public static GuildChannel DeleteGuildChannel(this DiscordClient client, ulong channelId)
        {
            return client.deleteChannel<GuildChannel>(channelId);
        }


        /// <summary>
        /// Adds/edits a permission overwrite to a channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="overwrite">The permission overwrite to add/edit</param>
        public static void AddPermissionOverwrite(this DiscordClient client, ulong channelId, PermissionOverwrite overwrite)
        {
            client.HttpClient.Put($"/channels/{channelId}/permissions/{overwrite.Id}", JsonConvert.SerializeObject(overwrite));
        }


        /// <summary>
        /// Removes a permission overwrite from a channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="id">ID of the role or member affected by the overwrite</param>
        public static void RemovePermissionOverwrite(this DiscordClient client, ulong channelId, ulong id)
        {
            client.HttpClient.Delete($"/channels/{channelId}/permissions/{id}");
        }
        #endregion


        #region group
        /// <summary>
        /// Joins a group
        /// </summary>
        /// <param name="inviteCode">Invite for the group</param>
        /// <returns>The invite used</returns>
        public static PartialInvite JoinGroup(this DiscordClient client, string inviteCode)
        {
            return client.HttpClient.Post($"/invites/{inviteCode}")
                                .Deserialize<PartialInvite>().SetClient(client);
        }


        /// <summary>
        /// Gets a group
        /// </summary>
        /// <param name="groupId">ID of the group</param>
        public static Group GetGroup(this DiscordClient client, ulong groupId)
        {
            return client.getChannel<Group>(groupId);
        }


        /// <summary>
        /// Creates a group
        /// </summary>
        /// <param name="recipients">The IDs of the recipients to add</param>
        /// <returns>The created <see cref="Group"/></returns>
        public static Group CreateGroup(this DiscordClient client, List<ulong> recipients)
        {
            return client.createChannel<Group>(JsonConvert.SerializeObject(new RecipientList() { Recipients = recipients }));
        }


        /// <summary>
        /// Modifies a group
        /// </summary>
        /// <param name="groupId">ID of the group</param>
        /// <param name="properties">Options for modifying the group</param>
        /// <returns>The modified <see cref="Group"/></returns>
        public static Group ModifyGroup(this DiscordClient client, ulong groupId, GroupProperties properties)
        {
            return client.modifyChannel<Group, GroupProperties>(groupId, properties);
        }


        /// <summary>
        /// Leaves a group
        /// </summary>
        /// <param name="groupId">ID of the group</param>
        /// <returns>The leaved <see cref="Group"/></returns>
        public static Group LeaveGroup(this DiscordClient client, ulong groupId)
        {
            return client.deleteChannel<Group>(groupId);
        }


        /// <summary>
        /// Adds a user to a group
        /// </summary>
        /// <param name="groupId">ID of the group</param>
        /// <param name="userId">ID of the user</param>
        public static void AddUserToGroup(this DiscordClient client, ulong groupId, ulong userId)
        {
            client.HttpClient.Put($"/channels/{groupId}/recipients/{userId}");
        }


        /// <summary>
        /// Removes a user from a group
        /// </summary>
        /// <param name="groupId">ID of the group</param>
        /// <param name="userId">ID of the user</param>
        public static void RemoveUserFromGroup(this DiscordClient client, ulong groupId, ulong userId)
        {
            client.HttpClient.Delete($"/channels/{groupId}/recipients/{userId}");
        }
        #endregion


        #region DM
        /// <summary>
        /// Gets the account's private channels
        /// </summary>
        /// <returns>A <see cref="IReadOnlyList{Channel}"/> containing the client's private channels</returns>
        public static IReadOnlyList<Channel> GetPrivateChannels(this DiscordClient client)
        {
            return client.HttpClient.Get($"/users/@me/channels")
                                .Deserialize<IReadOnlyList<Channel>>().SetClientsInList(client);
        }


        /// <summary>
        /// Gets a direct messaging channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>A <see cref="DMChannel"/> representation of the channel</returns>
        public static DMChannel GetDMChannel(this DiscordClient client, ulong channelId)
        {
            return client.getChannel<DMChannel>(channelId);
        }


        /// <summary>
        /// Creates a direct messaging channel
        /// </summary>
        /// <param name="recipientId">ID of the user</param>
        /// <returns>The created <see cref="DMChannel"/></returns>
        public static DMChannel CreateDM(this DiscordClient client, ulong recipientId)
        {
            return client.createChannel<DMChannel>($"{{\"recipient_id\":\"{recipientId}\"}}");
        }


        /// <summary>
        /// Closes a direct messaging channel (does not delete the messages)
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>The closed <see cref="DMChannel"/></returns>
        public static DMChannel CloseDM(this DiscordClient client, ulong channelId)
        {
            return client.deleteChannel<DMChannel>(channelId);
        }
        #endregion
#pragma warning restore IDE1006
    }
}