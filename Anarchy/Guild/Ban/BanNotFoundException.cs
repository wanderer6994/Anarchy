namespace Discord
{
    public class BanNotFoundException : DiscordException
    {
        public BanNotFoundException(DiscordClient client, long guildId) : base(client, "Unable to find ban")
        { }
    }
}
