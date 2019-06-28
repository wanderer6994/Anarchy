using Newtonsoft.Json;
using System.Collections.Generic;
using Discord.Webhook;

namespace Discord
{
    public class Guild : Controllable
    {
        public Guild()
        {
            OnClientUpdated += (sender, e) =>
            {
                if (Roles != null) Roles.SetClientsInList(Client);

                if (Reactions != null) Reactions.SetClientsInList(Client);
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
        public GuildVerificationLevel VerificationLevel { get; private set; }

        [JsonProperty("default_message_notifications")]
        public GuildDefaultNotifications DefaultNotifications { get; set; }

        [JsonProperty("owner_id")]
        public long? OwnerId { get; private set; }


        public void Update()
        {
            Guild guild = Client.GetGuild(Id);
            Name = guild.Name;
            IconId = guild.IconId;
            Region = guild.Region;
            Roles = guild.Roles;
            Reactions = guild.Reactions;
            VerificationLevel = guild.VerificationLevel;
            DefaultNotifications = guild.DefaultNotifications;
            OwnerId = guild.OwnerId;
        }


        public Guild Modify(GuildModProperties properties)
        {
            if (properties.Name == null)
                properties.Name = Name;
            if (properties.Region == null)
                properties.Region = Region;

            Guild guild = Client.ModifyGuild(Id, properties);
            Region = guild.Region;
            IconId = guild.IconId;
            VerificationLevel = guild.VerificationLevel;
            DefaultNotifications = guild.DefaultNotifications;
            OwnerId = guild.OwnerId;
            Roles = guild.Roles;
            Reactions = guild.Reactions;
            return guild;
        }


        public bool Delete()
        {
            return Client.DeleteGuild(Id);
        }


        public bool KickMember(long userId)
        {
            return Client.KickGuildMember(Id, userId);
        }


        public bool KickMember(User user)
        {
            return KickMember(user.Id);
        }


        public List<Ban> GetBans(long guildId)
        {
            return Client.GetGuildBans(guildId);
        }


        public Ban GetBan(long guildId, long userId)
        {
            return Client.GetGuildBan(guildId, userId);
        }


        public bool BanMember(long userId, int deleteMessageDays, string reason)
        {
            return Client.BanGuildMember(Id, userId, deleteMessageDays, reason);
        }


        public bool BanMember(User user, int deleteMessageDays, string reason)
        {
            return BanMember(user.Id, deleteMessageDays, reason);
        }


        public bool UnbanMember(long userId)
        {
            return Client.UnbanGuildMember(Id, userId);
        }


        public bool UnbanMember(User user)
        {
            return UnbanMember(user.Id);
        }


        public bool ChangeNickname(string nickname)
        {
            return Client.ChangeNickname(Id, nickname);
        }


        public List<Channel> GetChannels()
        {
            return Client.GetGuildChannels(Id);
        }


        public List<Reaction> GetReactions()
        {
            List<Reaction> reactions = Client.GetGuildReactions(Id);
            Reactions = reactions;
            return reactions;
        }


        public Reaction GetReaction(long reactionId)
        {
            return Client.GetGuildReaction(Id, reactionId);
        }


        public List<Role> GetRoles()
        {
            List<Role> roles = Client.GetGuildRoles(Id);
            Roles = roles;
            return roles;
        }


        public List<Invite> GetInvites()
        {
            return Client.GetGuildInvites(Id);
        }


        public Channel CreateChannel(ChannelCreationProperties properties)
        {
            return Client.CreateGuildChannel(Id, properties);
        }


        public Reaction CreateReaction(ReactionCreationProperties properties)
        {
            return Client.CreateGuildReaction(Id, properties);
        }


        public Role CreateRole(RoleProperties properties = null)
        {
            return Client.CreateGuildRole(Id, properties);
        }


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