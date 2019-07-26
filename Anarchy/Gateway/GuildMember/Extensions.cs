using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Discord.Gateway
{
    public static class GuildMemberExtensions
    {
        /// <summary>
        /// Requests a member chunk from a guild
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        /// <param name="limit">Max amount of members to receive (set to 0 for all)</param>
        public static void GetGuildMembers(this DiscordSocketClient client, ulong guildId, uint limit = 100)
        {
            var req = new GatewayRequest<GatewayMemberQuery>(GatewayOpcode.RequestGuildMembers);
            req.Data.GuildId = guildId;
            req.Data.Limit = limit;
            client.Socket.Send(req);
        }


        /// <summary>
        /// Gets all memebers in a guild
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        public static IReadOnlyList<GuildMember> GetAllGuildMembers(this DiscordSocketClient client, ulong guildId)
        {
            List<GuildMember> members = new List<GuildMember>();
            IReadOnlyList<GuildMember> newMembers = new List<GuildMember>();
            client.OnGuildMembersReceived += (c, args) =>
            {
                newMembers = args.Members;
                members.AddRange(newMembers);
            };

            client.GetGuildMembers(guildId, 0);

            while (newMembers.Count == 1000 || newMembers.Count == 0) Thread.Sleep(20);
            return members;
        }
    }
}
