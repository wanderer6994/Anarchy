using System.Collections.Generic;

namespace Discord
{
    public static class VoiceExtensions
    {
        /// <summary>
        /// Gets all available voice regions
        /// </summary>
        public static IReadOnlyList<VoiceRegion> GetVoiceRegions(this DiscordClient client)
        {
            return client.HttpClient.Get("/voice/regions")
                                .Deserialize<IReadOnlyList<VoiceRegion>>();
        }


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
    }
}
