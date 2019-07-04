using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord.Webhook
{
    public class WebhookMessageProperties
    {
        public WebhookMessageProperties()
        {
            Embeds = new List<Embed>();
        }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("embeds")]
        public List<Embed> Embeds { get; set; }
    }
}