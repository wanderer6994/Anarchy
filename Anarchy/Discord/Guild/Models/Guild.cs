using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public class Guild : BasicGuild
    {
        [JsonProperty("region")]
        public string Region { get; private set; }

        [JsonProperty("icon")]
        public string IconId { get; private set; }

        [JsonProperty("verification_level")]
        public GuildVerificationLevel Verification { get; private set; }

        [JsonProperty("default_message_notifications")]
        public GuildDefaultNotifications DefaultNotifications { get; set; }

        [JsonProperty("owner_id")]
        public long OwnerId { get; private set; }

        private List<Role> _roles;
        [JsonProperty("roles")]
        public List<Role> Roles
        {
            get { return _roles; }
            private set
            {
                foreach (var role in value)
                    role.GuildId = Id;

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
                foreach (var reaction in value)
                    reaction.GuildId = Id;

                _reactions = value;
            }
        }

        private DiscordClient _client;
        internal override DiscordClient Client
        {
            get { return _client; }
            set
            {
                _client = value;

                foreach (var role in Roles)
                    role.Client = Client;

                foreach (var reaction in Reactions)
                    reaction.Client = Client;
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


        public override List<Role> GetRoles()
        {
            Roles = base.GetRoles();
            return Roles;
        }


        public override List<Reaction> GetReactions()
        {
            Reactions = base.GetReactions();
            return Reactions;
        }


        //TODO: Make this able to convert the bytes into an image (problems with the .webp extension i reckon)
        public byte[] DownloadIcon()
        {
            if (IconId == null)
                return null;
                
            return Client.HttpClient.Get($"https://cdn.discordapp.com/icons/{Id}/{IconId}.webp")
                                    .Content.ReadAsByteArrayAsync().Result;
        }
    }
}
