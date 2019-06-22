namespace Discord
{
    public class ChannelNotFoundException : DiscordException
    {
        public long ChannelId { get; private set; }

        public ChannelNotFoundException(DiscordClient client, long channelId) : base (client)
        {
            ChannelId = channelId;
        }
    }
}
