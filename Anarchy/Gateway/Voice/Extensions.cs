using Discord.Gateway;

namespace Discord.Gateway
{
    public static class VoiceExtensions
    {
        private static void ChangeVoiceState(this DiscordSocketClient client, ulong guildId, ulong? channelId, bool muted = false, bool deafened = false)
        {
            var req = new GatewayRequest<VoiceStateChange>(GatewayOpcode.VoiceStateUpdate);
            req.Data.GuildId = guildId;
            req.Data.ChannelId = channelId;
            req.Data.Muted = muted;
            req.Data.Deafened = deafened;
            client.Socket.Send(req);
        }


        /// <summary>
        /// Joins a voice channel
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="muted">Whether the client will be muted or not</param>
        /// <param name="deafened">Whether the client will be deafened or not</param>
        public static void JoinVoiceChannel(this DiscordSocketClient client, ulong guildId, ulong channelId, bool muted = false, bool deafened = false)
        {
            client.ChangeVoiceState(guildId, channelId, muted, deafened);
        }


        /// <summary>
        /// Leaves a voice channel
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        public static void LeaveVoiceChannel(this DiscordSocketClient client, ulong guildId)
        {
            client.ChangeVoiceState(guildId, null);
        }
    }
}
