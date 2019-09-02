using Discord.Webhook;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;

namespace Discord
{
    public abstract class BaseGuild : Controllable
    {
        [JsonProperty("id")]
        public ulong Id { get; private set; }


        [JsonProperty("name")]
        public string Name { get; protected set; }


        [JsonProperty("icon")]
        public string IconId { get; protected set; }


        /// <summary>
        /// Updates the guild's info
        /// </summary>
        public void Update()
        {
            Guild guild = Client.GetGuild(Id);
            Name = guild.Name;
            IconId = guild.IconId;
        }


        /// <summary>
        /// Modifies the guild
        /// </summary>
        /// <param name="properties">Options for modifying the guild</param>
        public void Modify(GuildProperties properties)
        {
            if (!properties.IconSet)
                properties.IconId = IconId;

            Guild guild = Client.ModifyGuild(Id, properties);
            Name = guild.Name;
            IconId = guild.IconId;
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
        /// Creates a text channel
        /// </summary>
        /// <param name="properties">Options for creating the channel</param>
        /// <returns>The created channel</returns>
        public TextChannel CreateTextChannel(string name, ulong? parentId = null)
        {
            return Client.CreateTextChannel(Id, name, parentId);
        }


        /// <summary>
        /// Creates a voice channel
        /// </summary>
        /// <param name="properties">Options for creating the channel</param>
        /// <returns>The created channel</returns>
        public VoiceChannel CreateVoiceChannel(string name, ulong? parentId = null)
        {
            return Client.CreateVoiceChannel(Id, name, parentId);
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


        /// <summary>
        /// Gets the guild's icon
        /// </summary>
        /// <returns>The guild's icon (returns null if IconId is null)</returns>
        public Image GetIcon()
        {
            if (IconId == null)
                return null;

            return (Bitmap)new ImageConverter()
                        .ConvertFrom(new HttpClient().GetByteArrayAsync($"https://cdn.discordapp.com/icons/{Id}/{IconId}.png").Result);
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
