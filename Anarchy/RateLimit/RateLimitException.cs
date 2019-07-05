namespace Discord
{
    public class RateLimitException : DiscordException
    {
        public int RetryAfter { get; private set; }

        public RateLimitException(DiscordClient client, int retryAfter) : base(client, "Too many requests are being sent")
        {
            RetryAfter = retryAfter;
        }


        public override string ToString()
        {
            return RetryAfter.ToString();
        }
    }
}