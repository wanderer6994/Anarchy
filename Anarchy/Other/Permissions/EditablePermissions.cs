namespace Discord
{
    public class EditablePermissions : Permissions
    {
        public EditablePermissions() : base()
        { }

        public EditablePermissions(int permissions) : base(permissions)
        { }

        public void Add(Permission permission)
        {
            _perms = PermissionCalculator.Add(_perms, permission);
        }

        public void Remove(Permission permission)
        {
            _perms = PermissionCalculator.Remove(_perms, permission);
        }
    }
}
