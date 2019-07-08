using System.Collections.Generic;
using Newtonsoft.Json;

namespace Discord.Webhook
{
    public static class WebhookExtensions
    {
        #region management
        public static Hook CreateChannelWebhook(this DiscordClient client, ulong channelId, WebhookProperties properties)
        {
            properties.ChannelId = channelId;
            Hook hook = client.HttpClient.Post($"/channels/{channelId}/webhooks", 
                                    JsonConvert.SerializeObject(properties)).Deserialize<Hook>().SetClient(client);
            if (!properties.ChannelProperty.Set) hook.Modify(properties);
            return hook;
        }


        public static Hook ModifyChannelWebhook(this DiscordClient client, ulong webhookId, WebhookProperties properties)
        {
            return client.HttpClient.Patch($"/webhooks/{webhookId}", 
                                JsonConvert.SerializeObject(properties)).Deserialize<Hook>().SetClient(client);
        }


        public static void DeleteChannelWebhook(this DiscordClient client, ulong webhookId)
        {
            client.HttpClient.Delete($"/webhooks/{webhookId}");
        }
        #endregion


        public static Hook GetWebhook(this DiscordClient client, ulong webhookId, string token = "")
        {
            return client.HttpClient.Get($"/webhooks/{webhookId}/{token}")
                            .Deserialize<Hook>().SetClient(client);
        }


        public static IReadOnlyList<Hook> GetGuildWebhooks(this DiscordClient client, ulong guildId)
        {
            return client.HttpClient.Get($"/guilds/{guildId}/webhooks")
                                .Deserialize<IReadOnlyList<Hook>>().SetClientsInList(client);
        }


        public static IReadOnlyList<Hook> GetChannelWebhooks(this DiscordClient client, ulong channelId)
        {
            return client.HttpClient.Get($"/channels/{channelId}/webhooks")
                                .Deserialize<IReadOnlyList<Hook>>().SetClientsInList(client);
        }
    }
}
