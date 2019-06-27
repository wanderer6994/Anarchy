namespace Discord
{
    public class TooManyRequestsException : DiscordException
    {
        public int RetryAfter { get; private set; }

        public TooManyRequestsException(DiscordClient client, int retryAfter) : base(client, "Too many requests are being sent")
        {
            RetryAfter = retryAfter;
        }


        public override string ToString()
        {
            return RetryAfter.ToString();
        }
    }
}