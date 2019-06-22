using System;

namespace Discord.Webhook
{
    public class WebhookException : Exception
    {
        public DiscordWebhookClient Client { get; private set; }

        public WebhookException(DiscordWebhookClient client)
        {
            Client = client;
        }

        public WebhookException(DiscordWebhookClient client, string message) : base(message)
        {
            Client = client;
        }
    }
}
