using System;

namespace Discord
{
    public class DiscordException : Exception
    {
        public DiscordClient Client { get; private set; }

        public DiscordException(DiscordClient client, string message) : base(message)
        {
            Client = client;
        }
    }
}