namespace Discord.Webhook
{
    public class InvalidWebhookException : WebhookException
    {
        public string Url { get; private set; }

        public InvalidWebhookException(DiscordWebhookClient client, string url) : base(client, "Invalid webhook")
        {
            Url = url;
        }
    }
}
