using System.Collections.Generic;
using System.Linq;

namespace Discord
{
    public static class PermissionCalculator
    {
        //no point in making one for Permissions cuz you can't even input to that
        //also if anyone knows how i'd loop through a 'someenumthing | someotherenumthing' tell me
        public static EditablePermissions Create(List<Permission> perms)
        {
            int permissions = 512;
            perms = perms.Distinct().ToList();
            perms.ForEach(perm => permissions = Add(permissions, perm));
            return new EditablePermissions(permissions);
        }

        public static int Add(int permissions, Permission permission)
        {
            if (!Has(permissions, permission))
                permissions |= (int)permission;

            return permissions;
        }

        public static int Remove(int permissions, Permission permission)
        {
            if (Has(permissions, permission))
                permissions ^= (int)permission;

            return permissions;
        }

        public static bool Has(int permissions, Permission permission)
        {
            return (permissions & (int)permission) == (int)permission;
        }
    }
}
