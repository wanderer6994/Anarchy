using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class RoleExtensions
    {
        public static List<Role> GetGuildRoles(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.Get($"/guilds/{guildId}/roles");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);
            
            List<Role> roles = JsonConvert.DeserializeObject<List<Role>>(resp.Content.ReadAsStringAsync().Result).SetClientsInList(client);
            foreach (var role in roles) role.GuildId = guildId;
            return roles;
        }

        #region management
        public static Role CreateGuildRole(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.Post($"/guilds/{guildId}/roles");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);
            
            Role role = JsonConvert.DeserializeObject<Role>(resp.Content.ReadAsStringAsync().Result).SetClient(client);
            role.GuildId = guildId;
            return role;
        }
        

        public static Role ModifyGuildRole(this DiscordClient client, long guildId, long roleId, RoleProperties properties)
        {
            var resp = client.HttpClient.Patch($"/guilds/{guildId}/roles/{roleId}", JsonConvert.SerializeObject(properties));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new RoleNotFoundException(client, roleId);
            
            Role changed = JsonConvert.DeserializeObject<Role>(resp.Content.ReadAsStringAsync().Result).SetClient(client);
            changed.GuildId = guildId;
            return changed;
        }
        

        public static bool DeleteGuildRole(this DiscordClient client, long guildId, long roleId)
        {
            var resp = client.HttpClient.Delete($"/guilds/{guildId}/roles/{roleId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new RoleNotFoundException(client, roleId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
        #endregion
    }
}