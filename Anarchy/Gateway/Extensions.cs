using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace Discord.Gateway
{
    public static class GatewayExtensions
    {
        internal static void Send(this WebSocket socket, object json)
        {
            socket.Send(JsonConvert.SerializeObject(json));
        }


        /// <summary>
        /// Logs intot he gateway
        /// </summary>
        internal static void LoginToGateway(this DiscordSocketClient client)
        {
            var req = new GatewayRequest<GatewayIdentification>(GatewayOpcode.Identify);
            req.Data.Token = client.Token;
            req.Data.Properties = client.SuperPropertiesInfo.Properties;
            client.Socket.Send(req);
        }


        internal static void Resume(this DiscordSocketClient client)
        {
            var req = new GatewayRequest<GatewayResume>(GatewayOpcode.Resume);
            req.Data.Token = client.Token;
            req.Data.SessionId = client.SessionId;
            req.Data.Sequence = client.Sequence;

            System.Console.WriteLine(JsonConvert.SerializeObject(req));

            client.Socket.Send(req);
        }


        /// <summary>
        /// Starts a process that will continuously send heartbeats every <paramref name="interval"/> milliseconds
        /// </summary>
        /// <param name="interval">Amount of time (in milliseconds) to wait between sending heartbeats</param>
        internal static async void StartHeartbeatHandlersAsync(this DiscordSocketClient client, uint interval)
        {
            while (true)
            {
                await Task.Delay((int)interval);
                client.Socket.Send(new GatewayRequest<uint?>(GatewayOpcode.Heartbeat) { Data = client.Sequence });
            }
        }


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