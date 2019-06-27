using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Discord.Gateway
{
    public static class GatewayExtensions
    {
        /// <summary>
        /// Logs into the gateway
        /// </summary>
        internal static void LoginToGateway(this DiscordSocketClient client)
        {
            var req = new GatewayRequest<GatewayIdentification>(GatewayOpcode.Identify);
            req.Data.Token = client.Token;
            req.Data.Properties = client.SuperPropertiesInfo.Properties;
            client.Socket.Send(JsonConvert.SerializeObject(req));
        }
        

        /// <summary>
        /// Sends a heartbeat every x seconds
        /// </summary>
        internal static async Task StartHeartbeatHandlersAsync(this DiscordSocketClient client, int interval)
        {
            while (true)
            {
                await Task.Delay(interval);

                client.Socket.Send($"\"op\":{GatewayOpcode.Heartbeat},\"d\":{client.Sequence}");
            }
        }
        

        /// <summary>
        /// Changes the client's <see cref="UserStatus"/>
        /// </summary>
        public static void ChangeStatus(this DiscordSocketClient client, UserStatus status)
        {
            var req = new GatewayRequest<GatewayPresence>(GatewayOpcode.StatusChange);
            req.Data.Status = status != UserStatus.DoNotDisturb ? status.ToString().ToLower() : "dnd";
            client.Socket.Send(JsonConvert.SerializeObject(req));
        }


        /// <summary>
        /// Gets a list of guild members (subscribe to OnGuildMembersReceived to get them)
        /// </summary>
        public static void GetGuildMembers(this DiscordSocketClient client, long guildId, int limit = 100)
        {
            var req = new GatewayRequest<GatewayMembers>(GatewayOpcode.RequestGuildMembers);
            req.Data.GuildId = guildId;
            req.Data.Limit = limit;
            client.Socket.Send(JsonConvert.SerializeObject(req));
        }


        /// <summary>
        /// Gets all members in a guild
        /// </summary>
        public static List<User> GetAllGuildMembers(this DiscordSocketClient client, long guildId)
        {
            List<User> members = new List<User>();
            List<User> newMembers = new List<User>();
            client.OnGuildMembersReceived += (c, args) =>
            {
                newMembers = args.Users;
                members.AddRange(newMembers);
            };

            client.GetGuildMembers(guildId, 0);

            while (newMembers.Count == 1000 || newMembers.Count == 0) Thread.Sleep(10);

            return members;
        }
    }
}