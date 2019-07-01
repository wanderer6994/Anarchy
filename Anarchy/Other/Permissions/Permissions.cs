namespace Discord
{
    public class Permissions
    {
        protected int _perms;

        public Permissions()
        {
            _perms = 512;
        }

        public Permissions(int permissions)
        {
            _perms = permissions;
        }


        public bool Has(Permission permission)
        {
            return PermissionCalculator.Has(_perms, permission);
        }


        public static implicit operator int(Permissions instance)
        {
            return instance._perms;
        }


        public static bool operator ==(Permissions instance, int permissions)
        {
            return instance._perms == permissions;
        }


        public static bool operator !=(Permissions instance, int permissions)
        {
            return instance._perms != permissions;
        }
    }
}