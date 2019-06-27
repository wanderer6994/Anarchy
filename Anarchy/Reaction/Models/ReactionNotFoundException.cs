namespace Discord
{
    public class ReactionNotFoundException : DiscordException
    {
        public long GuildId { get; private set; }
        public long ReactionId { get; private set; }

        public ReactionNotFoundException(DiscordClient client, long guildId, long reactionId) : base(client, "Unable to find reaction")
        {
            GuildId = guildId;
            ReactionId = reactionId;
        }


        public override string ToString()
        {
            return $"Guild: {GuildId} Reaction: {ReactionId}";
        }
    }
}
