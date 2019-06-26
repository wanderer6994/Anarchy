using Newtonsoft.Json;

namespace Discord
{
    public class GuildModProperties : GuildCreationProperties
    {
        [JsonProperty("verification_level")]
        public GuildVerificationLevel Verification { get; set; }
        
        [JsonProperty("default_message_notifications")]
        public GuildDefaultNotifications DefaultNotifications { get; set; }
    }
}
