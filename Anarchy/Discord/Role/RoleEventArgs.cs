using System;

namespace Discord
{
    public class RoleEventArgs : EventArgs
    {
        public Role Role { get; private set; }

        public RoleEventArgs(Role role)
        {
            Role = role;
        }
    }
}
