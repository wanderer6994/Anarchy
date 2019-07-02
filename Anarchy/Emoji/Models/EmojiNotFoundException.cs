namespace Discord
{
    public class EmojiNotFoundException : DiscordException
    {
        public long GuildId { get; private set; }
        public long EmojiId { get; private set; }

        public EmojiNotFoundException(DiscordClient client, long guildId, long reactionId) : base(client, "Unable to find reaction")
        {
            GuildId = guildId;
            EmojiId = reactionId;
        }


        public override string ToString()
        {
            return EmojiId.ToString();
        }
    }
}