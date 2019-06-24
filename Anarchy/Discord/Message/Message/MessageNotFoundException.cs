namespace Discord
{
    public class MessageNotFoundException : DiscordException
    {
        public long MessageId { get; private set; }

        public MessageNotFoundException(DiscordClient client, long messageId) : base(client, "Unable to find message")
        {
            MessageId = messageId;
        }
    }
}
