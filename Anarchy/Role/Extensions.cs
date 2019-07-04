using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class RoleExtensions
    {
        #region management
        public static Role CreateGuildRole(this DiscordClient client, long guildId, RoleProperties properties = null)
        {
            Role role = client.HttpClient.Post($"/guilds/{guildId}/roles")
                                    .Deserialize<Role>().SetClient(client);
            role.GuildId = guildId;
            if (properties != null) role.Modify(properties);
            return role;
        }


        public static Role ModifyGuildRole(this DiscordClient client, long guildId, long roleId, RoleProperties properties)
        {
            Role changed = client.HttpClient.Patch($"/guilds/{guildId}/roles/{roleId}", 
                                        JsonConvert.SerializeObject(properties)).Deserialize<Role>().SetClient(client);
            changed.GuildId = guildId;
            return changed;
        }


        public static void DeleteGuildRole(this DiscordClient client, long guildId, long roleId)
        {
            client.HttpClient.Delete($"/guilds/{guildId}/roles/{roleId}");
        }
        #endregion


        public static IReadOnlyList<Role> GetGuildRoles(this DiscordClient client, long guildId)
        {
            IReadOnlyList<Role> roles = client.HttpClient.Get($"/guilds/{guildId}/roles")
                                                    .Deserialize<IReadOnlyList<Role>>().SetClientsInList(client);
            foreach (var role in roles) role.GuildId = guildId;
            return roles;
        }
    }
}