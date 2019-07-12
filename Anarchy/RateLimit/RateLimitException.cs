using System;

namespace Discord
{
    public class RateLimitException : DiscordException
    {
        public uint RetryAfter { get; private set; }

        public RateLimitException(DiscordClient client, uint retryAfter) : base(client, $"Please wait {retryAfter} milliseconds")
        {
            RetryAfter = retryAfter;
        }


        public override string ToString()
        {
            return RetryAfter.ToString();
        }
    }
}