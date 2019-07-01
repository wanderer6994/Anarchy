namespace Discord
{
    public class BanNotFoundException : DiscordException
    {
        public long GuildId { get; private set; }

        public BanNotFoundException(DiscordClient client, long guildId) : base(client, "Unable to find ban")
        {
            GuildId = guildId;
        }


        public override string ToString()
        {
            return GuildId.ToString();
        }
    }
}
