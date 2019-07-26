using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord.Gateway
{
    public static class GatewayAuthExtensions
    {
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
            client.Socket.Send(req);
        }
    }
}
