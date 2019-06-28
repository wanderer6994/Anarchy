using Newtonsoft.Json;

namespace Discord.Webhook
{
    public class WebhookMessage
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
    }
}
