using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public class Guild : BaseGuild
    {
        public Guild()
        {
            OnClientUpdated += (sender, e) =>
            {
                if (Roles != null) Roles.SetClientsInList(Client);

                if (Reactions != null) Reactions.SetClientsInList(Client);
            };
        }

        [JsonProperty("description")]
        public string Description { get; private set; }

        [JsonProperty("region")]
        public string Region { get; private set; }

        [JsonProperty("premium_tier")]
        public GuildPremiumTier PremiumTier { get; private set; }

        private IReadOnlyList<Role> _roles;
        [JsonProperty("roles")]
        public IReadOnlyList<Role> Roles
        {
            get { return _roles; }
            private set
            {
                if (value != null)
                    foreach (var role in value) role.GuildId = Id;

                _roles = value;
            }
        }

        private IReadOnlyList<Reaction> _reactions;
        [JsonProperty("emojis")]
        public IReadOnlyList<Reaction> Reactions
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

        [JsonProperty("vanity_url_code")]
        public string VanityInvite { get; private set; }


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
            VanityInvite = guild.VanityInvite;
        }


        public Guild Modify(GuildModProperties properties)
        {
            if (!properties.NameProperty.Set)
                properties.Name = Name;
            if (!properties.RegionProperty.Set)
                properties.Region = Region;
            if (!properties.VerificationProperty.Set)
                properties.Verification = VerificationLevel;
            if (!properties.NotificationsProperty.Set)
                properties.DefaultNotifications = DefaultNotifications;

            Guild guild = Client.ModifyGuild(Id, properties);
            Name = guild.Name;
            Region = guild.Region;
            IconId = guild.IconId;
            VerificationLevel = guild.VerificationLevel;
            DefaultNotifications = guild.DefaultNotifications;
            OwnerId = guild.OwnerId;
            Roles = guild.Roles;
            Reactions = guild.Reactions;
            return guild;
        }


        public override IReadOnlyList<Role> GetRoles()
        {
            Roles = base.GetRoles();
            return Roles;
        }


        public override IReadOnlyList<Reaction> GetReactions()
        {
            Reactions = base.GetReactions();
            return Reactions;
        }
    }
}