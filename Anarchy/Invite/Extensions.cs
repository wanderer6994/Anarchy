using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class InviteExtensions
    {
        /// <summary>
        /// Gets an invite
        /// </summary>
        public static Invite GetInvite(this DiscordClient client, string invCode)
        {
            var resp = client.HttpClient.Get($"/invite/{invCode}?with_counts=true");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new InvalidInviteException(client, invCode);

            return resp.Content.Json<Invite>().SetClient(client);
        }


        /// <summary>
        /// Gets all invites in a guild
        /// </summary>
        public static List<Invite> GetGuildInvites(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.Get($"/guilds/{guildId}/invites");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            return resp.Content.Json<List<Invite>>().SetClientsInList(client);
        }


        /// <summary>
        /// Creates a channel invite
        /// </summary>
        public static Invite CreateInvite(this DiscordClient client, long channelId, InviteProperties properties)
        {
            var resp = client.HttpClient.Post($"/channels/{channelId}/invites", JsonConvert.SerializeObject(properties));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            return resp.Content.Json<Invite>().SetClient(client);
        }


        /// <summary>
        /// Deletes a channel invite
        /// </summary>
        public static Invite DeleteInvite(this DiscordClient client, string invCode)
        {
            var resp = client.HttpClient.Delete($"/invites/{invCode}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new InvalidInviteException(client, invCode);

            return resp.Content.Json<Invite>().SetClient(client);
        }
    }
}
