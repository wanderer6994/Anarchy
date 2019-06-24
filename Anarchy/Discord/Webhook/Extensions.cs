using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace Discord.Webhook
{
    public static class WebhookExtensions
    {
        #region management
        public static Hook CreateChannelWebhook(this DiscordClient client, long channelId, WebhookProperties properties)
        {
            var resp = client.HttpClient.Post($"/channels/{channelId}/webhooks", JsonConvert.SerializeObject(properties));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            Hook hook = JsonConvert.DeserializeObject<Hook>(resp.Content.ReadAsStringAsync().Result);
            hook.Client = client;
            hook.Modify(properties);
            return hook;
        }

        public static Hook ModifyChannelWebhook(this DiscordClient client, long webhookId, WebhookProperties properties)
        {
            var resp = client.HttpClient.Patch($"/webhooks/{webhookId}", JsonConvert.SerializeObject(properties));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new WebhookNotFoundException(client, webhookId);

            Hook hook = JsonConvert.DeserializeObject<Hook>(resp.Content.ReadAsStringAsync().Result);
            hook.Client = client;
            return hook;
        }

        public static bool DeleteChannelWebhook(this DiscordClient client, long webhookId)
        {
            var resp = client.HttpClient.Delete($"/webhooks/{webhookId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new WebhookNotFoundException(client, webhookId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
        #endregion

        public static Hook GetWebhook(this DiscordClient client, long webhookId, string token = "")
        {
            var resp = client.HttpClient.Get($"/webhooks/{webhookId}/{token}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new WebhookNotFoundException(client, webhookId);

            Hook hook = JsonConvert.DeserializeObject<Hook>(resp.Content.ReadAsStringAsync().Result);
            hook.Client = client;
            return hook;
        }

        public static List<Hook> GetGuildWebhooks(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.Get($"/guilds/{guildId}/webhooks");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            return JsonConvert.DeserializeObject<List<Hook>>(resp.Content.ReadAsStringAsync().Result);
        }

        public static List<Hook> GetChannelWebhooks(this DiscordClient client, long channelId)
        {
            var resp = client.HttpClient.Get($"/channels/{channelId}/webhooks");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, channelId);

            List<Hook> hooks = JsonConvert.DeserializeObject<List<Hook>>(resp.Content.ReadAsStringAsync().Result);
            foreach (var hook in hooks) hook.Client = client;
            return hooks;
        }
    }
}
