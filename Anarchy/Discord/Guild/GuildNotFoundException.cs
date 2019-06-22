namespace Discord
{
    public class GuildNotFoundException : DiscordException
    {
        public long GuildId { get; private set; }

        public GuildNotFoundException(DiscordClient client, long guildId) : base(client, "Unable to find guild")
        {
            GuildId = guildId;
        }
    }
}
