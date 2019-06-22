using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace Discord
{
    public static class GuildExtensions
    {
        public static Guild CreateGuild(this DiscordClient client, GuildCreationProperties properties)
        {
            var resp = client.HttpClient.PostAsync("/guilds", JsonConvert.SerializeObject(properties)).Result;
            
            Guild guild = JsonConvert.DeserializeObject<Guild>(resp.Content.ReadAsStringAsync().Result);
            guild.Client = client;
            return guild;
        }

        public static Guild ModifyGuild(this DiscordClient client, long guildId, GuildModProperties properties)
        {
            var resp = client.HttpClient.PatchAsync($"/guilds/{guildId}", JsonConvert.SerializeObject(properties)).Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            Guild guild = JsonConvert.DeserializeObject<Guild>(resp.Content.ReadAsStringAsync().Result);
            guild.Client = client;
            return guild;
        }

        public static bool DeleteGuild(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.PostAsync($"/guilds/{guildId}/delete").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
        
        public static Guild GetGuild(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.GetAsync("/guilds/" + guildId).Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            Guild guild = JsonConvert.DeserializeObject<Guild>(resp.Content.ReadAsStringAsync().Result);
            guild.Client = client;
            return guild;
        }

        public static bool KickGuildMember(this DiscordClient client, long guildId, long userId)
        {
            var resp = client.HttpClient.DeleteAsync($"/guilds/{guildId}/members/{userId}").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new UserNotFoundException(client, userId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }

        #region channels
        public static List<Channel> GetGuildChannels(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.GetAsync($"/guilds/{guildId}/channels").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            List<Channel> channels = JsonConvert.DeserializeObject<List<Channel>>(resp.Content.ReadAsStringAsync().Result);
            foreach (var channel in channels) channel.Client = client;
            return channels;
        }

        public static OrganizedChannelList Organize(this List<Channel> channels)
        {
            return new OrganizedChannelList(channels);
        }
        #endregion

        #region get guild members
        public static List<GuildMember> GetGuildMembers(this DiscordClient client, long guildId, int limit, long afterId = 0)
        {
            var resp = client.HttpClient.GetAsync($"/guilds/{guildId}/members?limit={limit}&after={afterId}").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            return JsonConvert.DeserializeObject<List<GuildMember>>(resp.Content.ReadAsStringAsync().Result);
        }

        //would be great to have this thing optimized if possible, cuz it's hella slow
        public static List<GuildMember> GetAllGuildMembers(this DiscordClient client, long guildId)
        {
            List<GuildMember> members = client.GetGuildMembers(guildId, 1000);

            while (true)
            {
                List<GuildMember> newMembers = client.GetGuildMembers(guildId, 1000, members[members.Count - 1].User.Id);

                if (newMembers.Count == 0) break;

                members.AddRange(newMembers);
            }

            return members;
        }
        #endregion
        
        public static bool ChangeNickname(this DiscordClient client, long guildId, string nickname)
        {
            var resp = client.HttpClient.PatchAsync($"/guilds/{guildId}/members/@me/nick", "{\"nick\":\"" + nickname + "\"}").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            return resp.StatusCode == HttpStatusCode.OK;
        }
    }
}