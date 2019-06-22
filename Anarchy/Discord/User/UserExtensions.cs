using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Discord
{
    public static class UserExtensions
    {
        #region get user
        public static User GetUser(this DiscordClient client, long userId)
        {
            var resp = client.HttpClient.GetAsync($"/users/{userId}").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new UserNotFoundException(client, userId);

            return JsonConvert.DeserializeObject<User>(resp.Content.ReadAsStringAsync().Result);
        }

        public static ClientUser GetClientUser(this DiscordClient client)
        {
            HttpResponseMessage resp;

            try
            {
                resp = client.HttpClient.GetAsync("/users/@me").Result;
            }
            catch (AccessDeniedException)
            {
                client.User = null;
                throw;
            }

            client.User = JsonConvert.DeserializeObject<ClientUser>(resp.Content.ReadAsStringAsync().Result);

            return client.User;
        }
        #endregion

        public static List<BasicGuild> GetGuilds(this DiscordClient client)
        {
            var resp = client.HttpClient.GetAsync("/users/@me/guilds").Result;

            List<BasicGuild> guilds = JsonConvert.DeserializeObject<List<BasicGuild>>(resp.Content.ReadAsStringAsync().Result);
            foreach (var guild in guilds) guild.Client = client;
            return guilds;
        }

        #region join/leave guild
        public static Invite JoinGuild(this DiscordClient client, string invCode)
        {
            var resp = client.HttpClient.PostAsync($"/invite/{invCode}").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new InvalidInviteException(client, invCode);

            Invite invite = JsonConvert.DeserializeObject<Invite>(resp.Content.ReadAsStringAsync().Result);
            invite.Client = client;
            return invite;
        }

        public static bool LeaveGuild(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.DeleteAsync($"/users/@me/guilds/{guildId}").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
        #endregion

        public static bool SetHypesquad(this DiscordClient client, HypesquadHouse house)
        {
            //the request protocol is different if we don't want any hypesquad
            if (house == HypesquadHouse.None)
                return client.HttpClient.PostAsync("/hypesquad/online", JsonConvert.SerializeObject(new Hypesquad() { House = house })).Result.StatusCode == HttpStatusCode.NoContent;

            return client.HttpClient.PostAsync("/hypesquad/online", JsonConvert.SerializeObject(new Hypesquad() { House = house })).Result.StatusCode == HttpStatusCode.NoContent;
        }

        public static bool ChangeSettings(this DiscordClient client, UserSettings settings)
        {
            if (client.HttpClient.PatchAsync("/users/@me", JsonConvert.SerializeObject(settings)).Result.StatusCode == HttpStatusCode.OK)
            {
                client.GetClientUser();
                return true;
            }
            else
                return false;
        }

        internal static string GetFingerprint(this DiscordClient client)
        {
            return JsonConvert.DeserializeObject<Experiments>(client.HttpClient.GetAsync("/experiments").Result.Content.ReadAsStringAsync().Result).Fingerprint;
        }
    }
}