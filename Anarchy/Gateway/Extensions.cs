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
        

        internal static async void StartHeartbeatHandlersAsync(this DiscordSocketClient client, uint interval)
        {
            while (true)
            {
                await Task.Delay((int)interval);
                client.Socket.Send(new GatewayRequest<uint?>(GatewayOpcode.Heartbeat) { Data = client.Sequence });
            }
        }


        public static void GetGuildMembers(this DiscordSocketClient client, ulong guildId, uint limit = 100)
        {
            var req = new GatewayRequest<GatewayMemberQuery>(GatewayOpcode.RequestGuildMembers);
            req.Data.GuildId = guildId;
            req.Data.Limit = limit;
            client.Socket.Send(req);
        }


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