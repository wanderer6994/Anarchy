using System.Drawing;
using System.Net.Http;
using Newtonsoft.Json;

namespace Discord.Webhook
{
    public class Hook : Controllable
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("avatar")]
        public string AvatarId { get; private set; }

        [JsonProperty("user")]
        public User Creator { get; private set; }

        [JsonProperty("token")]
        public string Token { get; private set; }

        [JsonProperty("channel_id")]
        public long ChannelId { get; private set; }

        [JsonProperty("guild_id")]
        public long GuildId { get; private set; }

        public Hook()
        {
            OnClientUpdated += (sender, e) => Creator.SetClient(Client);
        }


        public void Update()
        {
            Hook hook = Client.GetWebhook(Id, Token);
            Name = hook.Name;
            AvatarId = hook.AvatarId;
            Creator = hook.Creator;
            ChannelId = hook.ChannelId;
        }


        public void Modify(WebhookProperties properties)
        {
            if (properties.Name == null)
                properties.Name = Name;
            if (!properties.AvatarSet)
                properties.Avatar = GetAvatar();
            if (!properties.ChannelProperty.Set)
                properties.ChannelId = ChannelId;

            Hook hook = Client.ModifyChannelWebhook(Id, properties);
            Name = hook.Name;
            AvatarId = hook.AvatarId;
            ChannelId = hook.ChannelId;
        }


        public void Delete()
        {
            Client.DeleteChannelWebhook(Id);
        }


        public void SendMessage(WebhookMessageProperties message)
        {
            Client.HttpClient.Post($"/webhooks/{Id}/{Token}", JsonConvert.SerializeObject(message));
        }


        public void SendMessage(string content)
        {
            WebhookMessageProperties message = new WebhookMessageProperties
            {
                Username = Name,
                Content = content,
                AvatarUrl = $"https://cdn.discordapp.com/avatars/{Id}/{AvatarId}.png"
            };

            SendMessage(message);
        }


        public Image GetAvatar()
        {
            if (AvatarId == null)
                return null;

            return (Bitmap)new ImageConverter().ConvertFrom(new HttpClient().GetAsync($"https://cdn.discordapp.com/avatars/{Id}/{AvatarId}.png")
                                                                    .Result.Content.ReadAsByteArrayAsync().Result);
        }


        public override string ToString()
        {
            return Token;
        }
    }
}