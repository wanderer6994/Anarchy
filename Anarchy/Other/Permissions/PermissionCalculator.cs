namespace Discord
{
    public static class PermissionCalculator
    {
        public static int AddPermission(int permissions, Permission permission)
        {
            if (!HasPermission(permissions, permission))
                permissions |= (int)permission;

            return permissions;
        }

        public static int RemovePermission(int permissions, Permission permission)
        {
            if (HasPermission(permissions, permission))
                permissions ^= (int)permission;

            return permissions;
        }

        public static bool HasPermission(int permissions, Permission permission)
        {
            return (permissions & (int)permission) == (int)permission;
        }
    }
}
