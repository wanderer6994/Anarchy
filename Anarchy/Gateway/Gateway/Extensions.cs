using Newtonsoft.Json;
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
    }
}