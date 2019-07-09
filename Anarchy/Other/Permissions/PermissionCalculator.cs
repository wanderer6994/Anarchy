using System.Collections.Generic;
using System.Linq;

namespace Discord
{
    public static class PermissionCalculator
    {
        //if anyone knows how i'd loop through a 'someenumthing | someotherenumthing' tell me
        public static EditablePermissions Create(List<Permission> perms)
        {
            uint permissions = 512;
            perms = perms.Distinct().ToList();
            perms.ForEach(perm => permissions = Add(permissions, perm));
            return new EditablePermissions(permissions);
        }


        public static uint Add(uint permissions, Permission permission)
        {
            if (!Has(permissions, permission))
                permissions |= (uint)permission;

            return permissions;
        }


        public static uint Remove(uint permissions, Permission permission)
        {
            if (Has(permissions, permission))
                permissions ^= (uint)permission;

            return permissions;
        }


        public static bool Has(uint permissions, Permission permission)
        {
            return (permissions & (uint)permission) == (uint)permission;
        }
    }
}
