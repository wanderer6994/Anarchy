using Newtonsoft.Json;
using System.Collections.Generic;
using Discord.Webhook;

namespace Discord
{
    public class Guild : ClientClassBase
    {
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
                {
                    foreach (var role in value)
                        role.GuildId = Id;
                }

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
                {
                    foreach (var reaction in value)
                        reaction.GuildId = Id;
                }

                _reactions = value;
            }
        }

        [JsonProperty("verification_level")]
        public GuildVerificationLevel Verification { get; private set; }

        [JsonProperty("default_message_notifications")]
        public GuildDefaultNotifications DefaultNotifications { get; set; }

        [JsonProperty("owner_id")]
        public long? OwnerId { get; private set; }

        private DiscordClient _client;
        internal override DiscordClient Client
        {
            get { return _client; }
            set
            {
                _client = value;

                if (Roles != null)
                    Roles.SetClientsInList(value);

                if (Reactions != null)
                    Reactions.SetClientsInList(value);
            }
        }


        public Guild Modify(GuildModProperties properties)
        {
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
            return Client.CreateChannel(Id, properties);
        }


        public Reaction CreateReaction(ReactionCreationProperties properties)
        {
            return Client.CreateGuildReaction(Id, properties);
        }


        public Role CreateRole()
        {
            return Client.CreateGuildRole(Id);
        }


        public List<Hook> GetWebhooks()
        {
            return Client.GetGuildWebhooks(Id);
        }


        //TODO: Make this able to convert the bytes into an image (problems with the .webp extension i reckon)
        public byte[] DownloadIcon()
        {
            if (IconId == null)
                return null;

            return Client.HttpClient.Get($"https://cdn.discordapp.com/icons/{Id}/{IconId}.webp")
                                    .Content.ReadAsByteArrayAsync().Result;
        }


        public override string ToString()
        {
            return $"{Name} ({Id})";
        }
    }
}