using System;

namespace Discord.Gateway
{
    public static class PresenceExtensions
    {
        /// <summary>
        /// Changes the client's status (online, idle, dnd or invisible)
        /// </summary>
        /// <param name="status">The new status</param>
        public static void ChangeStatus(this DiscordSocketClient client, UserStatus status)
        {
            var req = new GatewayRequest<Presence>(GatewayOpcode.PresenceChange);
            req.Data.Status = status;
            client.Socket.Send(req);
        }


        /// <summary>
        /// Sets the client's activity
        /// </summary>
        /// <param name="since">How long the client has been playing for</param>
        public static void SetActivity(this DiscordSocketClient client, Activity activity, uint? since = null)
        {
            var req = new GatewayRequest<Presence>(GatewayOpcode.PresenceChange);
            req.Data.Activity = activity;
            req.Data.Activity.Since = since;
            client.Socket.Send(req);
        }
    }
}