using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public static class InviteExtensions
    {
        #region management
        /// <summary>
        /// Creates an invite for a channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="properties">Options for creating the invite</param>
        /// <returns>The created invite</returns>
        public static PartialInvite CreateInvite(this DiscordClient client, ulong channelId, InviteProperties properties = null)
        {
            if (properties == null) properties = new InviteProperties();

            return client.HttpClient.Post($"/channels/{channelId}/invites", JsonConvert.SerializeObject(properties))
                                .Deserialize<PartialInvite>().SetClient(client);
        }


        /// <summary>
        /// Deletes an invite
        /// </summary>
        /// <param name="invCode">The invite's code</param>
        /// <returns>The deleted invite</returns>
        public static PartialInvite DeleteInvite(this DiscordClient client, string invCode)
        {
            return client.HttpClient.Delete($"/invites/{invCode}")
                                .Deserialize<PartialInvite>().SetClient(client);
        }
        #endregion


        /// <summary>
        /// Gets an invite
        /// </summary>
        public static Invite GetInvite(this DiscordClient client, string invCode)
        {
            return client.HttpClient.Get($"/invite/{invCode}?with_counts=true")
                                .Deserialize<Invite>().SetClient(client);
        }


        /// <summary>
        /// Gets a guild's invites
        /// </summary>
        /// <param name="guildId">ID of the guild</param>
        public static IReadOnlyList<Invite> GetGuildInvites(this DiscordClient client, ulong guildId)
        {
            return client.HttpClient.Get($"/guilds/{guildId}/invites")
                                .Deserialize<IReadOnlyList<Invite>>().SetClientsInList(client);
        }
    }
}