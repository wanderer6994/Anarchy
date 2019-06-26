using Newtonsoft.Json;

namespace Discord
{
    public class RoleProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("permissions")]
        private int _permissions { get; set; }

        [JsonProperty("color")]
        public int? Color { get; set; }

        [JsonProperty("hoist")]
        public bool? Seperated { get; set; }

        [JsonProperty("mentionable")]
        public bool? Mentionable { get; set; }


        public void AddPermission(GuildPermission permission)
        {
            PermissionCalculator.AddPermission(_permissions, permission);
        }


        public void RemovePermission(GuildPermission permission)
        {
            PermissionCalculator.RemovePermission(_permissions, permission);
        }


        public bool HasPermission(GuildPermission permission)
        {
            return PermissionCalculator.HasPermission(_permissions, permission);
        }


        public override string ToString()
        {
            return Name;
        }
    }
}