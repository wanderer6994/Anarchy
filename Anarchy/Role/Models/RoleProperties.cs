using Newtonsoft.Json;

namespace Discord
{
    public class RoleProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("permissions")]
        public int? Permissions { get; set; }

        [JsonProperty("color")]
        public int? Color { get; set; }

        [JsonProperty("hoist")]
        public bool? Seperated { get; set; }

        [JsonProperty("mentionable")]
        public bool? Mentionable { get; set; }


        public void AddPermission(GuildPermission permission)
        {
            //if you have not set Permissions to anything it will default it to 'no permissions'
            if (Permissions == null)
                Permissions = 512;

            Permissions = PermissionCalculator.AddPermission((int)Permissions, permission);
        }


        public void RemovePermission(GuildPermission permission)
        {
            if (Permissions == null)
                return;

            Permissions = PermissionCalculator.RemovePermission((int)Permissions, permission);
        }


        public bool HasPermission(GuildPermission permission)
        {
            if (Permissions == null)
                return false;

            return PermissionCalculator.HasPermission((int)Permissions, permission);
        }


        public override string ToString()
        {
            return Name;
        }
    }
}