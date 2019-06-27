using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace Discord.Webhook
{
    public static class WebhookExtensions
    {
        #region management
        /// <summary>
        /// Creates a webhook
        /// </summary>
        public static Hook CreateChannelWebhook(this DiscordClient client, long channelId, WebhookProperties properties)
        {
            var resp = client.HttpClient.Post($"/channels/{channelId}/webhooks", JsonConvert.SerializeObject(properties));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            Hook hook = resp.Content.Json<Hook>().SetClient(client);
            if (properties.ChannelId != null) hook.Modify(properties);
            return hook;
        }


        /// <summary>
        /// Modifies a webhook
        /// </summary>
        public static Hook ModifyChannelWebhook(this DiscordClient client, long webhookId, WebhookProperties properties)
        {
            var resp = client.HttpClient.Patch($"/webhooks/{webhookId}", JsonConvert.SerializeObject(properties));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new WebhookNotFoundException(client, webhookId);

            return resp.Content.Json<Hook>().SetClient(client);
        }

        /// <summary>
        /// Deletes a webhook
        /// </summary>
        public static bool DeleteChannelWebhook(this DiscordClient client, long webhookId)
        {
            var resp = client.HttpClient.Delete($"/webhooks/{webhookId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new WebhookNotFoundException(client, webhookId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
        #endregion


        /// <summary>
        /// Gets a webhook
        /// </summary>
        public static Hook GetWebhook(this DiscordClient client, long webhookId, string token = "")
        {
            var resp = client.HttpClient.Get($"/webhooks/{webhookId}/{token}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new WebhookNotFoundException(client, webhookId);

            return resp.Content.Json<Hook>().SetClient(client);
        }


        /// <summary>
        /// Gets all webhooks in a guild
        /// </summary>
        public static List<Hook> GetGuildWebhooks(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.Get($"/guilds/{guildId}/webhooks");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            return resp.Content.Json<List<Hook>>().SetClientsInList(client);
        }


        /// <summary>
        /// Gets all webhooks in a channel
        /// </summary>
        public static List<Hook> GetChannelWebhooks(this DiscordClient client, long channelId)
        {
            var resp = client.HttpClient.Get($"/channels/{channelId}/webhooks");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, channelId);

            return resp.Content.Json<List<Hook>>().SetClientsInList(client);
        }
    }
}
