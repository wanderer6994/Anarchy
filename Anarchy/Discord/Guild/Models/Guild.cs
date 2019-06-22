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

        [JsonProperty("roles")]
        public List<Role> Roles { get; private set; }

        [JsonProperty("emojis")]
        public List<Reaction> Reactions { get; private set; }

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
                
            return Client.HttpClient.GetAsync($"https://cdn.discordapp.com/icons/{Id}/{IconId}.webp")
                                    .Result.Content.ReadAsByteArrayAsync().Result;
        }
    }
}
