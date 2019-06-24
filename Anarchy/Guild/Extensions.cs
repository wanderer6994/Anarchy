using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace Discord
{
    public static class GuildExtensions
    {
        #region manage
        public static Guild CreateGuild(this DiscordClient client, GuildCreationProperties properties)
        {
            var resp = client.HttpClient.Post("/guilds", JsonConvert.SerializeObject(properties));
            
            Guild guild = JsonConvert.DeserializeObject<Guild>(resp.Content.ReadAsStringAsync().Result);
            guild.Client = client;
            return guild;
        }


        public static Guild ModifyGuild(this DiscordClient client, long guildId, GuildModProperties properties)
        {
            var resp = client.HttpClient.Patch($"/guilds/{guildId}", JsonConvert.SerializeObject(properties));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            Guild guild = JsonConvert.DeserializeObject<Guild>(resp.Content.ReadAsStringAsync().Result);
            guild.Client = client;
            return guild;
        }


        public static bool DeleteGuild(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.Post($"/guilds/{guildId}/delete");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
        #endregion


        public static Guild GetGuild(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.Get("/guilds/" + guildId);

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            Guild guild = JsonConvert.DeserializeObject<Guild>(resp.Content.ReadAsStringAsync().Result);
            guild.Client = client;
            return guild;
        }


        public static bool KickGuildMember(this DiscordClient client, long guildId, long userId)
        {
            var resp = client.HttpClient.Delete($"/guilds/{guildId}/members/{userId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new UserNotFoundException(client, userId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }


        public static List<Channel> GetGuildChannels(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.Get($"/guilds/{guildId}/channels");

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


        public static List<GuildMember> GetGuildMembers(this DiscordClient client, long guildId, int limit, long afterId = 0)
        {
            var resp = client.HttpClient.Get($"/guilds/{guildId}/members?limit={limit}&after={afterId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            return JsonConvert.DeserializeObject<List<GuildMember>>(resp.Content.ReadAsStringAsync().Result);
        }


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
        

        public static bool ChangeNickname(this DiscordClient client, long guildId, string nickname)
        {
            var resp = client.HttpClient.Patch($"/guilds/{guildId}/members/@me/nick", "{\"nick\":\"" + nickname + "\"}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            return resp.StatusCode == HttpStatusCode.OK;
        }
    }
}