using Newtonsoft.Json;
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
    }
}
