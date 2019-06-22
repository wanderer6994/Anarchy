namespace Discord
{
    public class UserEventArgs
    {
        public ClientUser User { get; private set; }

        public UserEventArgs(ClientUser user)
        {
            User = user;
        }
    }
}
