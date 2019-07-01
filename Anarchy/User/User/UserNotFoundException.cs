namespace Discord
{
    public class UserNotFoundException : DiscordException
    {
        public long UserId { get; private set; }

        public UserNotFoundException(DiscordClient client, long userId) : base(client, "Unable to find user")
        {
            UserId = userId;
        }


        public override string ToString()
        {
            return UserId.ToString();
        }
    }
}
