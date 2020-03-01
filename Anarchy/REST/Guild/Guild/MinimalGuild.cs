﻿using Discord.Webhook;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public class MinimalGuild : ControllableEx // the only reason this has Ex is bcuz of LoginGuild, lol
    {
        [JsonProperty("id")]
        public ulong Id { get; private set; }

        public MinimalGuild()
        { }

        public MinimalGuild(ulong guildId)
        {
            Id = guildId;
        }


        /// <summary>
        /// Deletes the guild
        /// </summary>
        public void Delete()
        {
            Client.DeleteGuild(Id);
        }


        /// <summary>
        /// Leaves the guild
        /// </summary>
        public void Leave()
        {
            Client.LeaveGuild(Id);
        }


        /// <summary>
        /// Acknowledges all messages and pings in the guild
        /// </summary>
        public void AcknowledgeMessages()
        {
            Client.AcknowledgeGuildMessages(Id);
        }


        /// <summary>
        /// Changes the client's nickname for this guild
        /// </summary>
        /// <param name="nickname">New nickname</param>
        public void ChangeClientNickname(string nickname)
        {
            Client.ChangeClientNickname(Id, nickname);
        }


        /// <summary>
        /// Gets the guild's channels
        /// </summary>
        public virtual IReadOnlyList<GuildChannel> GetChannels()
        {
            return Client.GetGuildChannels(Id);
        }


        /// <summary>
        /// Creates a channel
        /// </summary>
        /// <param name="properties">Options for creating the channel</param>
        /// <returns>The created channel</returns>
        public GuildChannel CreateChannel(string name, ChannelType type, ulong? parentId = null)
        {
            return Client.CreateGuildChannel(Id, name, type, parentId);
        }


        /// <summary>
        /// Gets the guild's emojis
        /// </summary>
        public virtual IReadOnlyList<Emoji> GetEmojis()
        {
            return Client.GetGuildEmojis(Id);
        }


        /// <summary>
        /// Gets an emoji in the guild
        /// </summary>
        /// <param name="emojiId">ID of the emoji</param>
        public Emoji GetEmoji(ulong emojiId)
        {
            return Client.GetGuildEmoji(Id, emojiId);
        }


        /// <summary>
        /// Creates an emoji
        /// </summary>
        /// <param name="properties">Options for creating the emoji</param>
        public Emoji CreateEmoji(EmojiProperties properties)
        {
            return Client.CreateGuildEmoji(Id, properties);
        }


        /// <summary>
        /// Gets the guild's roles
        /// </summary>
        public virtual IReadOnlyList<Role> GetRoles()
        {
            return Client.GetGuildRoles(Id);
        }


        /// <summary>
        /// Creates a role
        /// </summary>
        /// <param name="properties">Options for modifying the role after creating it</param>
        /// <returns>The created role</returns>
        public Role CreateRole(RoleProperties properties = null)
        {
            return Client.CreateGuildRole(Id, properties);
        }


        /// <summary>
        /// Gets an invite
        /// </summary>
        public IReadOnlyList<GuildInvite> GetInvites()
        {
            return Client.GetGuildInvites(Id);
        }


        /// <summary>
        /// Gets the guild's webhooks
        /// </summary>
        public IReadOnlyList<Hook> GetWebhooks()
        {
            return Client.GetGuildWebhooks(Id);
        }


        /// <summary>
        /// Gets the guild's audit log
        /// </summary>
        /// <param name="filters"></param>
        public IReadOnlyList<AuditLogEntry> GetAuditLog(AuditLogFilters filters = null)
        {
            return Client.GetAuditLog(Id, filters);
        }


        /// <summary>
        /// Gets a member from the server
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <returns></returns>
        public GuildMember GetMember(ulong userId)
        {
            return Client.GetGuildMember(Id, userId);
        }


        /// <summary>
        /// Gets a list of members from the server
        /// </summary>
        /// <param name="limit">Max amount of members to receive</param>
        /// <param name="afterId">ID to offset from</param>
        public IReadOnlyList<GuildMember> GetMembers(uint limit = 100, ulong afterId = 0)
        {
            return Client.GetGuildMembers(Id, limit, afterId);
        }


        /// <summary>
        /// Gets all members from the guild.
        /// Warning: This is an incredibly slow method. If you have a <see cref="DiscordSocketClient"/> use client.GetAllGuildMembers() instead.
        /// </summary>
        public IReadOnlyList<GuildMember> GetAllMembers()
        {
            return Client.GetAllGuildMembers(Id);
        }


        /// <summary>
        /// Gets the guild's banned users
        /// </summary>
        public IReadOnlyList<Ban> GetBans()
        {
            return Client.GetGuildBans(Id);
        }


        /// <summary>
        /// Gets a banned member
        /// </summary>
        /// <param name="userId">ID of the user</param>
        public Ban GetBan(ulong userId)
        {
            return Client.GetGuildBan(Id, userId);
        }


        public static implicit operator ulong(MinimalGuild instance)
        {
            return instance.Id;
        }
    }
}
