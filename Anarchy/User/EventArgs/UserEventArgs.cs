namespace Discord
{
    public class UserEventArgs
    {
        public User User { get; private set; }

        public UserEventArgs(User user)
        {
            User = user;
        }


        public override string ToString()
        {
            return User.ToString();
        }
    }
}
