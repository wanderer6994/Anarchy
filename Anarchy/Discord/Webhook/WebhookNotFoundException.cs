namespace Discord.Webhook
{
    public class WebhookNotFoundException : DiscordException
    {
        public long WebhookId { get; private set; }

        public WebhookNotFoundException(DiscordClient client, long webhookId) : base(client, "Unable to find webhook")
        {
            WebhookId = webhookId;
        }
    }
}
