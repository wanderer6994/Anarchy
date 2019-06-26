namespace Discord
{
    public static class PermissionCalculator
    {
        public static int AddPermission(int permissions, GuildPermission permission)
        {
            if (!HasPermission(permissions, permission))
                permissions |= (int)permission;

            return permissions;
        }

        public static int RemovePermission(int permissions, GuildPermission permission)
        {
            if (HasPermission(permissions, permission))
                permissions ^= (int)permission;

            return permissions;
        }

        public static bool HasPermission(int permissions, GuildPermission permission)
        {
            return (permissions & (int)permission) == (int)permission;
        }
    }
}
