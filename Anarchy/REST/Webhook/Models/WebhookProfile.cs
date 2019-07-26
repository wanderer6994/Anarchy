using Newtonsoft.Json;

namespace Discord.Webhook
{
    public class WebhookProfile
    {
        [JsonProperty("username")]
        public string Name { get; set; }


        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
    }
}
