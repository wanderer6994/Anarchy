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


        internal static void LoginToGateway(this DiscordSocketClient client)
        {
            var req = new GatewayRequest<GatewayIdentification>(GatewayOpcode.Identify);
            req.Data.Token = client.Token;
            req.Data.Properties = client.SuperPropertiesInfo.Properties;

            client.Socket.Send(req);
        }
        

        internal static async Task StartHeartbeatHandlersAsync(this DiscordSocketClient client, int interval)
        {
            while (true)
            {
                await Task.Delay(interval);

                client.Socket.Send(new GatewayRequest<int?>(GatewayOpcode.Heartbeat) { Data = client.Sequence });
            }
        }


        public static void GetGuildMembers(this DiscordSocketClient client, long guildId, int limit = 100)
        {
            var req = new GatewayRequest<GatewayMembers>(GatewayOpcode.RequestGuildMembers);
            req.Data.GuildId = guildId;
            req.Data.Limit = limit;
            client.Socket.Send(req);
        }


        public static IReadOnlyList<User> GetAllGuildMembers(this DiscordSocketClient client, long guildId)
        {
            List<User> members = new List<User>();
            IReadOnlyList<User> newMembers = new List<User>();
            client.OnGuildMembersReceived += (c, args) =>
            {
                newMembers = args.Users;
                members.AddRange(newMembers);
            };

            client.GetGuildMembers(guildId, 0);

            while (newMembers.Count == 1000 || newMembers.Count == 0) Thread.Sleep(15);

            return members;
        }
    }
}