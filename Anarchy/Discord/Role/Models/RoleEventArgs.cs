namespace Discord
{
    public class RoleEventArgs
    {
        public Role Role { get; private set; }

        public RoleEventArgs(Role role)
        {
            Role = role;
        }
    }
}
