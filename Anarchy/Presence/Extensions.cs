namespace Discord.Gateway
{
    public static class PresenceExtensions
    {
        public static void ChangeStatus(this DiscordSocketClient client, UserStatus status)
        {
            var req = new GatewayRequest<Presence>(GatewayOpcode.PresenceChange);
            req.Data.Status = status;
            client.Socket.Send(req);
        }


        public static void SetActivity(this DiscordSocketClient client, Activity activity, uint since = 0)
        {
            var req = new GatewayRequest<Presence>(GatewayOpcode.PresenceChange);
            req.Data.Since = since;
            req.Data.Activity = activity;
            client.Socket.Send(req);
        }
    }
}