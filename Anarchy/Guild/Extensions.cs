using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Discord
{
    public static class GuildExtensions
    {
        #region management
        public static Guild CreateGuild(this DiscordClient client, GuildCreationProperties properties)
        {
            return client.HttpClient.Post("/guilds", JsonConvert.SerializeObject(properties))
                                .Deserialize<Guild>().SetClient(client);
        }


        public static Guild ModifyGuild(this DiscordClient client, ulong guildId, GuildProperties properties)
        {
            return client.HttpClient.Patch($"/guilds/{guildId}", JsonConvert.SerializeObject(properties))
                                .Deserialize<Guild>().SetClient(client);
        }


        public static void DeleteGuild(this DiscordClient client, ulong guildId)
        {
            client.HttpClient.Delete($"/guilds/{guildId}");
        }


        public static void KickGuildMember(this DiscordClient client, ulong guildId, ulong userId)
        {
            client.HttpClient.Delete($"/guilds/{guildId}/members/{userId}");
        }


        public static IReadOnlyList<Ban> GetGuildBans(this DiscordClient client, ulong guildId)
        {
            return client.HttpClient.Get($"/guilds/{guildId}/bans").Deserialize<IReadOnlyList<Ban>>().SetClientsInList(client);
        }


        public static Ban GetGuildBan(this DiscordClient client, ulong guildId, ulong userId)
        {
            return client.HttpClient.Get($"/guilds/{guildId}/bans/{userId}")
                                .Deserialize<Ban>().SetClient(client);
        }


        public static void BanGuildMember(this DiscordClient client, ulong guildId, ulong userId, string reason = null, uint deleteMessageDays = 0)
        {
            client.HttpClient.Put($"/guilds/{guildId}/bans/{userId}?delete-message-days={deleteMessageDays}&reason={reason}");
        }


        public static void UnbanGuildMember(this DiscordClient client, ulong guildId, ulong userId)
        {
            client.HttpClient.Delete($"/guilds/{guildId}/bans/{userId}");
        }
        #endregion


        public static IReadOnlyList<PartialGuild> GetClientGuilds(this DiscordClient client, uint limit = 100, ulong afterId = 0)
        {
            return client.HttpClient.Get($"/users/@me/guilds?limit={limit}&after={afterId}").Deserialize<IReadOnlyList<PartialGuild>>().SetClientsInList(client);
        }


        public static Guild GetGuild(this DiscordClient client, ulong guildId)
        {
            return client.HttpClient.Get("/guilds/" + guildId)
                                .Deserialize<Guild>().SetClient(client);
        }


        public static IReadOnlyList<GuildChannel> GetGuildChannels(this DiscordClient client, ulong guildId)
        {
            return client.HttpClient.Get($"/guilds/{guildId}/channels")
                                .Deserialize<IReadOnlyList<GuildChannel>>().SetClientsInList(client);
        }


        #region members
        public static GuildMember GetGuildMember(this DiscordClient client, ulong guildId, ulong memberId)
        {
            GuildMember member = client.HttpClient.Get($"/guilds/{guildId}/members/{memberId}")
                                            .Deserialize<GuildMember>();
            member.GuildId = guildId;
            return member;
        }


        public static IReadOnlyList<GuildMember> GetGuildMembers(this DiscordClient client, ulong guildId, uint limit, ulong afterId = 0)
        {
            IReadOnlyList<GuildMember> members = client.HttpClient.Get($"/guilds/{guildId}/members?limit={limit}&after={afterId}")
                                                            .Deserialize<IReadOnlyList<GuildMember>>().SetClientsInList(client);
            foreach (var member in members) member.GuildId = guildId;
            return members;
        }


        public static IReadOnlyList<GuildMember> GetAllGuildMembers(this DiscordClient client, ulong guildId)
        {
            List<GuildMember> members = client.GetGuildMembers(guildId, 1000).ToList();

            while (true)
            {
                IReadOnlyList<GuildMember> newMembers = client.GetGuildMembers(guildId, 1000, members[members.Count - 1].User.Id);
                if (newMembers.Count == 0) break;
                members.AddRange(newMembers);
            }

            return members;
        }
        #endregion


        public static PartialInvite JoinGuild(this DiscordClient client, string invCode)
        {
            return client.HttpClient.Post($"/invite/{invCode}")
                                .Deserialize<PartialInvite>().SetClient(client);
        }


        public static void LeaveGuild(this DiscordClient client, ulong guildId)
        {
            client.HttpClient.Delete($"/users/@me/guilds/{guildId}");
        }


        public static void ChangeNickname(this DiscordClient client, ulong guildId, string nickname)
        {
            client.HttpClient.Patch($"/guilds/{guildId}/members/@me/nick", "{\"nick\":\"" + nickname + "\"}");
        }
    }
}