namespace Discord
{
    public class AccessDeniedException : DiscordException
    {
        public AccessDeniedException(DiscordClient client) : base(client, "Access was denied")
        { }
    }
}