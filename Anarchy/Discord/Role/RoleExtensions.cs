using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class RoleExtensions
    {
        public static List<Role> GetGuildRoles(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.GetAsync($"/guilds/{guildId}/roles").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);
            
            List<Role> roles = JsonConvert.DeserializeObject<List<Role>>(resp.Content.ReadAsStringAsync().Result);
            foreach (var role in roles)
            {
                role.Client = client;
                role.GuildId = guildId;
            }
            return roles;
        }
        
        public static Role CreateGuildRole(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.PostAsync($"/guilds/{guildId}/roles").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);
            
            Role role = JsonConvert.DeserializeObject<Role>(resp.Content.ReadAsStringAsync().Result);
            role.Client = client;
            role.GuildId = guildId;
            return role;
        }
        
        public static Role ChangeGuildRole(this DiscordClient client, long guildId, long roleId, RoleProperties properties)
        {
            var resp = client.HttpClient.PatchAsync($"/guilds/{guildId}/roles/{roleId}", JsonConvert.SerializeObject(properties)).Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new RoleNotFoundException(client, roleId);
            
            Role changed = JsonConvert.DeserializeObject<Role>(resp.Content.ReadAsStringAsync().Result);
            changed.Client = client;
            changed.GuildId = guildId;
            return changed;
        }
        
        public static bool DeleteGuildRole(this DiscordClient client, long guildId, long roleId)
        {
            var resp = client.HttpClient.DeleteAsync($"/guilds/{guildId}/roles/{roleId}").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new RoleNotFoundException(client, roleId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
    }
}