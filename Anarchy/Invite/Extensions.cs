using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class InviteExtensions
    {
        #region management
        public static Invite CreateInvite(this DiscordClient client, long channelId, InviteProperties properties)
        {
            var resp = client.HttpClient.Post($"/channels/{channelId}/invites", JsonConvert.SerializeObject(properties));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            return resp.Content.Deserialize<Invite>().SetClient(client);
        }


        public static Invite DeleteInvite(this DiscordClient client, string invCode)
        {
            var resp = client.HttpClient.Delete($"/invites/{invCode}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new InvalidInviteException(client, invCode);

            return resp.Content.Deserialize<Invite>().SetClient(client);
        }
        #endregion

        public static Invite GetInvite(this DiscordClient client, string invCode)
        {
            var resp = client.HttpClient.Get($"/invite/{invCode}?with_counts=true");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new InvalidInviteException(client, invCode);

            return resp.Content.Deserialize<Invite>().SetClient(client);
        }


        public static List<Invite> GetGuildInvites(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.Get($"/guilds/{guildId}/invites");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            return resp.Content.Deserialize<List<Invite>>().SetClientsInList(client);
        }
    }
}
