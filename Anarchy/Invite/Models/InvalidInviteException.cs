namespace Discord
{
    /// <summary>
    /// Fired when an invalid invite is requested
    /// </summary>
    public class InvalidInviteException : DiscordException
    {
        public string InviteCode { get; private set; }

        public InvalidInviteException(DiscordClient client, string inviteCode) : base (client, "Invite is invalid")
        {
            InviteCode = inviteCode;
        }


        public override string ToString()
        {
            return InviteCode;
        }
    }
}
