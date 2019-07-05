namespace Discord.Gateway
{
    public class LoginEventArgs
    {
        public Login Login { get; private set; }

        public LoginEventArgs(Login login)
        {
            Login = login;
        }


        public override string ToString()
        {
            return Login.ToString();
        }
    }
}
