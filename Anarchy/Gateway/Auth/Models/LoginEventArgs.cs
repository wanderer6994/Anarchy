using System.Collections.Generic;

namespace Discord.Gateway
{
    public class LoginEventArgs
    {
        public ClientUser User { get; private set; }
        public IReadOnlyList<SocketGuild> Guilds { get; private set; }
        public IReadOnlyList<Channel> PrivateChannels { get; private set; }
        public IReadOnlyList<Relationship> Relationships { get; private set; }


        internal LoginEventArgs(Login login)
        {
            User = login.User;
            Guilds = login.Guilds;
            PrivateChannels = login.PrivateChannels;
            Relationships = login.Relationships;
        }


        public override string ToString()
        {
            return User.ToString();
        }
    }
}
