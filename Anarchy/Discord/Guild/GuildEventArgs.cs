using System;

namespace Discord
{
    public class GuildEventArgs : EventArgs
    {
        public Guild Guild { get; private set; }

        public GuildEventArgs(Guild guild)
        {
            Guild = guild;
        }
    }
}
