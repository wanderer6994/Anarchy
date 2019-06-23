using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Discord.Gateway
{
    public static class GatewayExtensions
    {
        internal static void LoginToGateway(this DiscordSocketClient client)
        {
            var req = new GatewayRequest<GatewayIdentification>(GatewayOpcode.Login, client.Token);
            req.Data.Properties = client.SuperPropertiesInfo.Properties;
            client.Socket.Send(JsonConvert.SerializeObject(req));
        }
        

        internal static async Task StartHeartbeatHandlersAsync(this DiscordSocketClient client, int interval)
        {
            while (true)
            {
                await Task.Delay(interval);

                client.Socket.Send($"\"op\":1,\"d\":{client.Sequence}");
            }
        }
        

        public static void ChangeStatus(this DiscordSocketClient client, UserStatus status)
        {
            string converted = "";

            switch (status)
            {
                case UserStatus.Online:
                    converted = "online";
                    break;
                case UserStatus.Idle:
                    converted = "idle";
                    break;
                case UserStatus.DoNotDisturb:
                    converted = "dnd";
                    break;
                case UserStatus.Offline:
                    converted = "offline";
                    break;
            }

            var req = new GatewayRequest<GatewayPresence>(GatewayOpcode.StatusChange, client.Token);
            req.Data.Status = converted;
            client.Socket.Send(JsonConvert.SerializeObject(req));
        }

        //Set limit to 0 to receive all members in chunks of 1000 members
        public static void GetGuildMembers(this DiscordSocketClient client, long guildId, int limit = 100)
        {
            var req = new GatewayRequest<GatewayGuildRequestMembers>(GatewayOpcode.RequestGuildMembers, client.Token);
            req.Data.GuildId = guildId;
            req.Data.Limit = limit;
            client.Socket.Send(JsonConvert.SerializeObject(req));
        }

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

            while (newMembers.Count == 1000 || newMembers.Count == 0) { Thread.Sleep(20); }

            return members;
        }
    }
}
