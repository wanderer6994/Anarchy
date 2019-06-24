namespace Discord
{
    public class InvalidInviteException : DiscordException
    {
        public string InviteCode { get; private set; }

        public InvalidInviteException(DiscordClient client, string inviteCode) : base (client, "Invite is invalid")
        {
            InviteCode = inviteCode;
        }
    }
}
