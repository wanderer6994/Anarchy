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
            _perms = PermissionCalculator.AddPermission(_perms, permission);
        }

        public void Remove(Permission permission)
        {
            _perms = PermissionCalculator.RemovePermission(_perms, permission);
        }
    }
}
