using Newtonsoft.Json;
using Discord.Gateway;

namespace Discord
{
    public static class VoiceExtensions
    {
        /// <summary>
        /// Changes a user's voice state in the specified guild
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        /// <param name="userId">ID of the user</param>
        /// <param name="muted">Whether the member should be muted or not (null for 'don't change')</param>
        /// <param name="deafened">Whether the member should be deafened or not (null for 'don't change')</param>
        public static void ChangeGuildMemberVoiceState(this DiscordClient client, ulong guildId, ulong userId, bool? muted = null, bool? deafened = null)
        {
            GuildMemberProperties properties = new GuildMemberProperties();
            if (muted != null)
                properties.Muted = muted.Value;
            if (deafened != null)
                properties.Deafened = deafened.Value;
            client.ModifyGuildMember(guildId, userId, properties);
        }


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
