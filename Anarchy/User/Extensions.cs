using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Discord
{
    public static class UserExtensions
    {
        /// <summary>
        /// Gets a user
        /// </summary>
        public static User GetUser(this DiscordClient client, long userId)
        {
            var resp = client.HttpClient.Get($"/users/{userId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new UserNotFoundException(client, userId);

            return resp.Content.Json<User>();
        }


        /// <summary>
        /// Gets the client's local user
        /// </summary>
        public static ClientUser GetClientUser(this DiscordClient client)
        {
            HttpResponseMessage resp;

            try
            {
                resp = client.HttpClient.Get("/users/@me");
            }
            catch (AccessDeniedException)
            {
                client.User = null;
                throw;
            }

            client.User = resp.Content.Json<ClientUser>();
            return client.User;
        }


        /// <summary>
        /// Gets all guilds the client is in
        /// </summary>
        public static List<Guild> GetClientGuilds(this DiscordClient client)
        {
            var resp = client.HttpClient.Get("/users/@me/guilds");

            return resp.Content.Json<List<Guild>>().SetClientsInList(client);
        }


        /// <summary>
        /// Joins a guild
        /// </summary>
        public static Invite JoinGuild(this DiscordClient client, string invCode)
        {
            var resp = client.HttpClient.Post($"/invite/{invCode}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new InvalidInviteException(client, invCode);

            return resp.Content.Json<Invite>().SetClient(client);
        }


        /// <summary>
        /// Leaves a guild
        /// </summary>
        public static bool LeaveGuild(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.Delete($"/users/@me/guilds/{guildId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }


        /// <summary>
        /// Sets the client's hypesquad
        /// </summary>
        public static bool SetHypesquad(this DiscordClient client, HypesquadHouse house)
        {
            //the request protocol is different if we don't want any hypesquad
            if (house == HypesquadHouse.None)
                return client.HttpClient.Delete("/hypesquad/online").StatusCode == HttpStatusCode.NoContent;

            return client.HttpClient.Post("/hypesquad/online", JsonConvert.SerializeObject(new Hypesquad() { House = house })).StatusCode == HttpStatusCode.NoContent;
        }


        /// <summary>
        /// Changes the client's user settings
        /// </summary>
        public static bool ChangeSettings(this DiscordClient client, UserSettings settings)
        {
            if (client.User != null)
            {
                if (settings.Email == null)
                    settings.Email = client.User.Email;
                if (settings.Username == null)
                    settings.Username = client.User.Username;
            }

            if (client.HttpClient.Patch("/users/@me", JsonConvert.SerializeObject(settings)).StatusCode == HttpStatusCode.OK)
            {
                client.GetClientUser();
                return true;
            }
            else
                return false;
        }


        /// <summary>
        /// Gets a fingerprint
        /// </summary>
        internal static string GetFingerprint(this DiscordClient client)
        {
            return JsonConvert.DeserializeObject<Experiments>(client.HttpClient.Get("/experiments").Content.ReadAsStringAsync().Result).Fingerprint;
        }
    }
}