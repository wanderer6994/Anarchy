namespace Discord
{
    /// <summary>
    /// Fired when too many HTTP requests are being sent at once
    /// </summary>
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