namespace Discord
{
    /// <summary>
    /// Fired when a message is not found
    /// </summary>
    public class MessageNotFoundException : DiscordException
    {
        public long MessageId { get; private set; }

        public MessageNotFoundException(DiscordClient client, long messageId) : base(client, "Unable to find message")
        {
            MessageId = messageId;
        }


        public override string ToString()
        {
            return MessageId.ToString();
        }
    }
}
