namespace Discord
{
    public class EditablePermissions : Permissions
    {
        public EditablePermissions() : base() { }
        public EditablePermissions(uint permissions) : base(permissions) { }


        public void Add(Permission permission)
        {
            _value = PermissionCalculator.Add(_value, permission);
        }


        public void Remove(Permission permission)
        {
            _value = PermissionCalculator.Remove(_value, permission);
        }
    }
}
