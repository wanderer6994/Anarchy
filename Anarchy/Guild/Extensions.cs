using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Discord
{
    public static class GuildExtensions
    {
        #region management
        /// <summary>
        /// Creates a guild
        /// </summary>
        /// <param name="properties">Options for creating the guild</param>
        /// <returns>The created <see cref="Guild"/></returns>
        public static Guild CreateGuild(this DiscordClient client, GuildCreationProperties properties)
        {
            return client.HttpClient.Post("/guilds", JsonConvert.SerializeObject(properties))
                                .Deserialize<Guild>().SetClient(client);
        }


        /// <summary>
        /// Modifies a guild
        /// </summary>
        /// <param name="guildId">ID of the group</param>
        /// <param name="properties">Options for modifying the guild</param>
        /// <returns>The modified <see cref="Guild"/></returns>
        public static Guild ModifyGuild(this DiscordClient client, ulong guildId, GuildProperties properties)
        {
            return client.HttpClient.Patch($"/guilds/{guildId}", JsonConvert.SerializeObject(properties))
                                .Deserialize<Guild>().SetClient(client);
        }


        /// <summary>
        /// Deletes a guild
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        public static void DeleteGuild(this DiscordClient client, ulong guildId)
        {
            client.HttpClient.Delete($"/guilds/{guildId}");
        }


        /// <summary>
        /// Kicks a member from a guild
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        /// <param name="userId">ID of the member</param>
        public static void KickGuildMember(this DiscordClient client, ulong guildId, ulong userId)
        {
            client.HttpClient.Delete($"/guilds/{guildId}/members/{userId}");
        }


        /// <summary>
        /// Gets the guild's banned users
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        public static IReadOnlyList<Ban> GetGuildBans(this DiscordClient client, ulong guildId)
        {
            IReadOnlyList<Ban> bans = client.HttpClient.Get($"/guilds/{guildId}/bans")
                                                    .Deserialize<IReadOnlyList<Ban>>().SetClientsInList(client);
            foreach (var ban in bans)
                ban.GuildId = guildId;
            return bans;
        }


        /// <summary>
        /// Gets a guild's banned user
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        /// <param name="userId">ID of the user</param>
        /// <returns></returns>
        public static Ban GetGuildBan(this DiscordClient client, ulong guildId, ulong userId)
        {
            Ban ban = client.HttpClient.Get($"/guilds/{guildId}/bans/{userId}")
                                   .Deserialize<Ban>().SetClient(client);
            ban.GuildId = guildId;
            return ban;
        }


        /// <summary>
        /// Bans a member from a guild
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        /// <param name="userId">ID of the member</param>
        /// <param name="reason">Reason for banning the member</param>
        /// <param name="deleteMessageDays">Amount of days to purge messages for</param>
        public static void BanGuildMember(this DiscordClient client, ulong guildId, ulong userId, string reason = null, uint deleteMessageDays = 0)
        {
            client.HttpClient.Put($"/guilds/{guildId}/bans/{userId}?delete-message-days={deleteMessageDays}&reason={reason}");
        }


        /// <summary>
        /// Unbans a user from a guild
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        /// <param name="userId">ID of the user</param>
        public static void UnbanGuildMember(this DiscordClient client, ulong guildId, ulong userId)
        {
            client.HttpClient.Delete($"/guilds/{guildId}/bans/{userId}");
        }
        #endregion


        /// <summary>
        /// Gets the guilds the account is in
        /// </summary>
        /// <param name="limit">Max amount of guild to receive</param>
        /// <param name="afterId">Guild ID to offset from</param>
        public static IReadOnlyList<PartialGuild> GetClientGuilds(this DiscordClient client, uint limit = 100, ulong afterId = 0)
        {
            return client.HttpClient.Get($"/users/@me/guilds?limit={limit}&after={afterId}")
                                .Deserialize<IReadOnlyList<PartialGuild>>().SetClientsInList(client);
        }


        /// <summary>
        /// Gets a guild
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        public static Guild GetGuild(this DiscordClient client, ulong guildId)
        {
            return client.HttpClient.Get("/guilds/" + guildId)
                                .Deserialize<Guild>().SetClient(client);
        }


        /// <summary>
        /// Gets a guild's channels
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        public static IReadOnlyList<GuildChannel> GetGuildChannels(this DiscordClient client, ulong guildId)
        {
            return client.HttpClient.Get($"/guilds/{guildId}/channels")
                                .Deserialize<IReadOnlyList<GuildChannel>>().SetClientsInList(client);
        }


        #region members
        /// <summary>
        /// Gets a member of a guild
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        /// <param name="userId">ID of the user</param>
        public static GuildMember GetGuildMember(this DiscordClient client, ulong guildId, ulong userId)
        {
            GuildMember member = client.HttpClient.Get($"/guilds/{guildId}/members/{userId}")
                                            .Deserialize<GuildMember>().SetClient(client);
            member.GuildId = guildId;
            return member;
        }


        /// <summary>
        /// Gets a list of members in a guild
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        /// <param name="limit">Max amount of members to receive</param>
        /// <param name="afterId">User ID to offset from</param>
        public static IReadOnlyList<GuildMember> GetGuildMembers(this DiscordClient client, ulong guildId, uint limit, ulong afterId = 0)
        {
            IReadOnlyList<GuildMember> members = client.HttpClient.Get($"/guilds/{guildId}/members?limit={limit}&after={afterId}")
                                                            .Deserialize<IReadOnlyList<GuildMember>>().SetClientsInList(client);
            foreach (var member in members) member.GuildId = guildId;
            return members;
        }


        /// <summary>
        /// Gets all members in a guld
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
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


        /// <summary>
        /// Joins a guild
        /// </summary>
        /// <returns>The invite used to join the guild</returns>
        public static PartialInvite JoinGuild(this DiscordClient client, string invCode)
        {
            return client.HttpClient.Post($"/invite/{invCode}")
                                .Deserialize<PartialInvite>().SetClient(client);
        }


        /// <summary>
        /// Leaves a guild
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        public static void LeaveGuild(this DiscordClient client, ulong guildId)
        {
            client.HttpClient.Delete($"/users/@me/guilds/{guildId}");
        }


        /// <summary>
        /// Changes the account's nickname in a guild
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        /// <param name="nickname">New nickname</param>
        public static void ChangeNickname(this DiscordClient client, ulong guildId, string nickname)
        {
            client.HttpClient.Patch($"/guilds/{guildId}/members/@me/nick", "{\"nick\":\"" + nickname + "\"}");
        }
    }
}