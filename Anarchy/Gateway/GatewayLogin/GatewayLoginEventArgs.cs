using Discord.Gateway;

namespace Discord
{
    public class GatewayLoginEventArgs
    {
        public GatewayLogin Login { get; private set; }

        public GatewayLoginEventArgs(GatewayLogin login)
        {
            Login = login;
        }
    }
}
