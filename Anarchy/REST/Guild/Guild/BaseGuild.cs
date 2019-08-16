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
        public GuildChannel CreateChannel(GuildChannelCreationProperties properties)
        {
            return Client.CreateGuildChannel(Id, properties);
        }


        /// <summary>
        /// Creates a text channel
        /// </summary>
        /// <param name="properties">Options for creating the channel</param>
        /// <returns>The created channel</returns>
        public TextChannel CreateTextChannel(GuildChannelCreationProperties properties)
        {
            return Client.CreateTextChannel(Id, properties);
        }


        /// <summary>
        /// Creates a voice channel
        /// </summary>
        /// <param name="properties">Options for creating the channel</param>
        /// <returns>The created channel</returns>
        public VoiceChannel CreateVoiceChannel(GuildChannelCreationProperties properties)
        {
            return Client.CreateVoiceChannel(Id, properties);
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
        public IReadOnlyList<Webhook.Hook> GetWebhooks()
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
