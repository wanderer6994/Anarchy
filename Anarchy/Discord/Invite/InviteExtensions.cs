using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class InviteExtensions
    {
        public static List<Invite> GetGuildInvites(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.Get($"/guilds/{guildId}/invites");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            List<Invite> invites = JsonConvert.DeserializeObject<List<Invite>>(resp.Content.ReadAsStringAsync().Result);
            foreach (var invite in invites) invite.Client = client;
            return invites;
        }


        public static Invite GetInvite(this DiscordClient client, string invCode)
        {
            var resp = client.HttpClient.Get($"/invite/{invCode}?with_counts=true");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new InvalidInviteException(client, invCode);

            Invite invite = JsonConvert.DeserializeObject<Invite>(resp.Content.ReadAsStringAsync().Result);
            invite.Client = client;
            return invite;
        }
        

        public static Invite CreateInvite(this DiscordClient client, long channelId, InviteProperties properties)
        {
            var resp = client.HttpClient.Post($"/channels/{channelId}/invites", JsonConvert.SerializeObject(properties));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            Invite invite = JsonConvert.DeserializeObject<Invite>(resp.Content.ReadAsStringAsync().Result);
            invite.Client = client;
            return invite;
        }


        public static Invite DeleteInvite(this DiscordClient client, string invCode)
        {
            var resp = client.HttpClient.Delete($"/invites/{invCode}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new InvalidInviteException(client, invCode);

            Invite deleted = JsonConvert.DeserializeObject<Invite>(resp.Content.ReadAsStringAsync().Result);
            deleted.Client = client;
            return deleted;
        }
    }
}
