using System.Net;
using Newtonsoft.Json;

namespace Discord.Webhook
{
    public class Hook : Controllable
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
        public string AvatarId { get; private set; }


        public void Update()
        {
            Hook hook = Client.GetWebhook(Id, Token);
            Name = hook.Name;
            Creator = hook.Creator;
            ChannelId = hook.ChannelId;
            AvatarId = hook.AvatarId;
        }


        public Hook Modify(WebhookProperties properties)
        {
            if (properties.Name == null)
                properties.Name = Name;
            if (properties.ChannelId == null)
                properties.ChannelId = ChannelId;

            Hook hook = Client.ModifyChannelWebhook(Id, properties);
            Name = hook.Name;
            ChannelId = hook.ChannelId;
            AvatarId = hook.AvatarId;
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
                Avatar = $"https://cdn.discordapp.com/avatars/{Id}/{AvatarId}.jpg"
            };

            return SendMessage(message);
        }


        public override string ToString()
        {
            return $"{Id} ({Token})";
        }
    }
}