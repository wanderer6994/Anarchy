namespace Discord.Gateway
{
    public static class GatewayAuthExtensions
    {
        /// <summary>
        /// Logs intot he gateway
        /// </summary>
        internal static void LoginToGateway(this DiscordSocketClient client)
        {
            var identification = new GatewayIdentification()
            {
                Token = client.Token,
                Properties = client.SuperPropertiesInfo
            };

            client.Socket.Send(GatewayOpcode.Identify, identification);
        }


        internal static void Resume(this DiscordSocketClient client)
        {
            var resume = new GatewayResume()
            {
                Token = client.Token,
                SessionId = client.SessionId,
                Sequence = client.Sequence
            };

            client.Socket.Send(GatewayOpcode.Resume, resume);
        }
    }
}
