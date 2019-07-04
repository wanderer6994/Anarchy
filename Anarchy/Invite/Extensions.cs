using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class InviteExtensions
    {
        #region management
        public static Invite CreateInvite(this DiscordClient client, long channelId, InviteProperties properties = null)
        {
            if (properties == null) properties = new InviteProperties();

            return client.HttpClient.Post($"/channels/{channelId}/invites", JsonConvert.SerializeObject(properties))
                                .Deserialize<Invite>().SetClient(client);
        }


        public static Invite DeleteInvite(this DiscordClient client, string invCode)
        {
            return client.HttpClient.Delete($"/invites/{invCode}")
                                .Deserialize<Invite>().SetClient(client);
        }
        #endregion


        public static Invite GetInvite(this DiscordClient client, string invCode)
        {
            return client.HttpClient.Get($"/invite/{invCode}?with_counts=true")
                                .Deserialize<Invite>().SetClient(client);
        }


        public static IReadOnlyList<Invite> GetGuildInvites(this DiscordClient client, long guildId)
        {
            return client.HttpClient.Get($"/guilds/{guildId}/invites")
                                .Deserialize<IReadOnlyList<Invite>>().SetClientsInList(client);
        }
    }
}