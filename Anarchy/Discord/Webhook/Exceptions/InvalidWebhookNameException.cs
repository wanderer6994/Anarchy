namespace Discord.Webhook
{
    public class InvalidWebhookNameException : WebhookException
    {
        public string Name { get; private set; }

        public InvalidWebhookNameException(DiscordWebhookClient client, string name) : base(client, $"Name '{name}' is invalid")
        {
            Name = name;
        }
    }
}
