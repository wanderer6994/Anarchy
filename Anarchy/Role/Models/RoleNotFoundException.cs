namespace Discord
{
    public class RoleNotFoundException : DiscordException
    {
        public long RoleId { get; private set; }

        public RoleNotFoundException(DiscordClient client, long roleId) : base(client, "Unable to find role")
        {
            RoleId = roleId;
        }


        public override string ToString()
        {
            return RoleId.ToString();
        }
    }
}
