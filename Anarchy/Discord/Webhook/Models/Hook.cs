using System.Net;
using Newtonsoft.Json;

namespace Discord.Webhook
{
    public class Hook
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }
        
        [JsonProperty("user")]
        public User Creator { get; private set; }

        [JsonProperty("token")]
        public string Token { get; private set; }

        [JsonProperty("guild_id")]
        public long GuildId { get; private set; }

        [JsonProperty("channel_id")]
        public long ChannelId { get; private set; }

        [JsonProperty("avatar")]
        public string Avatar { get; private set; }

        [JsonIgnore]
        internal DiscordClient Client { get; set; }

        public Hook Modify(WebhookProperties properties)
        {
            Hook hook = Client.ModifyChannelWebhook(Id, properties);
            Name = hook.Name;
            Creator = hook.Creator;
            ChannelId = hook.ChannelId;
            Avatar = hook.Avatar;
            return hook;
        }

        public bool Delete()
        {
            return Client.DeleteChannelWebhook(Id);
        }

        public bool SendMessage(WebhookMessage message)
        {
            var resp = Client.HttpClient.Post($"/webhooks/{Id}/{Token}", JsonConvert.SerializeObject(message));

            return resp.StatusCode == HttpStatusCode.NoContent;
        }

        public bool SendMessage(string content)
        {
            WebhookMessage message = new WebhookMessage
            {
                Username = Name,
                Content = content,
                Avatar = Avatar
            };

            return SendMessage(message);
        }
    }
}