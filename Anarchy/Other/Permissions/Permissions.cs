namespace Discord
{
    public class Permissions
    {
        protected int _value;

        public Permissions()
        {
            _value = 512;
        }

        public Permissions(int permissions)
        {
            _value = permissions;
        }


        public bool Has(Permission permission)
        {
            return PermissionCalculator.Has(_value, permission);
        }


        public static implicit operator int(Permissions instance)
        {
            return instance._value;
        }


        public override bool Equals(object obj)
        {
            return obj is Permissions permissions &&
                   _value == permissions._value;
        }


        public override int GetHashCode()
        {
            return -1939223833 + _value.GetHashCode();
        }


        public static bool operator ==(Permissions instance, int permissions)
        {
            return instance._value == permissions;
        }


        public static bool operator !=(Permissions instance, int permissions)
        {
            return instance._value != permissions;
        }
    }
}