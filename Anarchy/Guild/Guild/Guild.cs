using Newtonsoft.Json;
using System.Collections.Generic;
using Discord.Webhook;

namespace Discord
{
    public class Guild : ControllableModel
    {
        public Guild()
        {
            OnClientUpdated += (sender, e) =>
            {
                if (Roles != null)
                    Roles.SetClientsInList(Client);

                if (Reactions != null)
                    Reactions.SetClientsInList(Client);
            };
        }

        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("icon")]
        public string IconId { get; private set; }

        [JsonProperty("region")]
        public string Region { get; private set; }

        private List<Role> _roles;
        [JsonProperty("roles")]
        public List<Role> Roles
        {
            get { return _roles; }
            private set
            {
                if (value != null)
                    foreach (var role in value) role.GuildId = Id;

                _roles = value;
            }
        }

        private List<Reaction> _reactions;
        [JsonProperty("emojis")]
        public List<Reaction> Reactions
        {
            get { return _reactions; }
            private set
            {
                if (value != null)
                    foreach (var reaction in value) reaction.GuildId = Id;

                _reactions = value;
            }
        }

        [JsonProperty("verification_level")]
        public GuildVerificationLevel Verification { get; private set; }

        [JsonProperty("default_message_notifications")]
        public GuildDefaultNotifications DefaultNotifications { get; set; }

        [JsonProperty("owner_id")]
        public long? OwnerId { get; private set; }


        /// <summary>
        /// Modifies the guild
        /// </summary>
        public Guild Modify(GuildModProperties properties)
        {
            if (properties.Name == null)
                properties.Name = Name;
            if (properties.Region == null)
                properties.Region = Region;

            Guild guild = Client.ModifyGuild(Id, properties);
            Region = guild.Region;
            IconId = guild.IconId;
            Verification = guild.Verification;
            DefaultNotifications = guild.DefaultNotifications;
            OwnerId = guild.OwnerId;
            Roles = guild.Roles;
            Reactions = guild.Reactions;
            return guild;
        }


        /// <summary>
        /// Deletes the guild
        /// </summary>
        public bool Delete()
        {
            return Client.DeleteGuild(Id);
        }


        /// <summary>
        /// Kicks a member
        /// </summary>
        public bool KickMember(long userId)
        {
            return Client.KickGuildMember(Id, userId);
        }


        /// <summary>
        /// Kicks a member
        /// </summary>
        public bool KickMember(User user)
        {
            return KickMember(user.Id);
        }


        /// <summary>
        /// Gets all bans
        /// </summary>
        public List<Ban> GetBans(long guildId)
        {
            return Client.GetGuildBans(guildId);
        }


        /// <summary>
        /// Gets a specific ban
        /// </summary>
        public Ban GetBan(long guildId, long userId)
        {
            return Client.GetGuildBan(guildId, userId);
        }


        /// <summary>
        /// Bans a member
        /// </summary>
        public bool BanMember(long userId, int deleteMessageDays, string reason)
        {
            return Client.BanGuildMember(Id, userId, deleteMessageDays, reason);
        }


        /// <summary>
        /// Bans a member
        /// </summary>
        public bool BanMember(User user, int deleteMessageDays, string reason)
        {
            return BanMember(user.Id, deleteMessageDays, reason);
        }


        /// <summary>
        /// Unbans a member
        /// </summary>
        public bool UnbanMember(long userId)
        {
            return Client.UnbanGuildMember(Id, userId);
        }


        /// <summary>
        /// Unbans a member
        /// </summary>
        public bool UnbanMember(User user)
        {
            return UnbanMember(user.Id);
        }


        /// <summary>
        /// Gets all channels
        /// </summary>
        public List<Channel> GetChannels()
        {
            return Client.GetGuildChannels(Id);
        }


        /// <summary>
        /// Gets all reactions
        /// </summary>
        public List<Reaction> GetReactions()
        {
            List<Reaction> reactions = Client.GetGuildReactions(Id);
            Reactions = reactions;
            return reactions;
        }


        /// <summary>
        /// Gets a specific reaction
        /// </summary>
        public Reaction GetReaction(long reactionId)
        {
            return Client.GetGuildReaction(Id, reactionId);
        }


        /// <summary>
        /// Gets all roles
        /// </summary>
        public List<Role> GetRoles()
        {
            List<Role> roles = Client.GetGuildRoles(Id);
            Roles = roles;
            return roles;
        }


        /// <summary>
        /// Gets all invites
        /// </summary>
        public List<Invite> GetInvites()
        {
            return Client.GetGuildInvites(Id);
        }


        /// <summary>
        /// Creates a channel
        /// </summary>
        public Channel CreateChannel(ChannelCreationProperties properties)
        {
            return Client.CreateGuildChannel(Id, properties);
        }


        /// <summary>
        /// Creates a reaction
        /// </summary>
        public Reaction CreateReaction(ReactionCreationProperties properties)
        {
            return Client.CreateGuildReaction(Id, properties);
        }


        /// <summary>
        /// Creates a role
        /// </summary>
        public Role CreateRole(RoleProperties properties = null)
        {
            return Client.CreateGuildRole(Id, properties);
        }


        /// <summary>
        /// Gets all webhooks
        /// </summary>
        public List<Hook> GetWebhooks()
        {
            return Client.GetGuildWebhooks(Id);
        }


        public override string ToString()
        {
            return $"{Name} ({Id})";
        }
    }
}