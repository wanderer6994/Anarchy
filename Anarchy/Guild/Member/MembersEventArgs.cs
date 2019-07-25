using System;
using System.Collections.Generic;

namespace Discord
{
    public class GuildMembersEventArgs : EventArgs
    {
        public IReadOnlyList<GuildMember> Members { get; private set; }

        internal GuildMembersEventArgs(IReadOnlyList<GuildMember> members)
        {
            Members = members;
        }
    }
}
