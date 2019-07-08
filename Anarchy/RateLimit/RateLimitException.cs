namespace Discord
{
    public class RateLimitException : DiscordException
    {
        public uint RetryAfter { get; private set; }

        public RateLimitException(DiscordClient client, uint retryAfter) : base(client, "Too many requests are being sent")
        {
            RetryAfter = retryAfter;
        }


        public override string ToString()
        {
            return RetryAfter.ToString();
        }
    }
}