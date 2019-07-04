using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace Discord
{
    public static class UserExtensions
    {
        public static User GetUser(this DiscordClient client, long userId)
        {
            var resp = client.HttpClient.Get($"/users/{userId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new UserNotFoundException(client, userId);

            return resp.Deserialize<User>().SetClient(client);
        }


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

            client.User = resp.Deserialize<ClientUser>().SetClient(client);
            return client.User;
        }


        public static bool SetHypesquad(this DiscordClient client, HypesquadHouse house)
        {
            if (house == HypesquadHouse.None)
                return client.HttpClient.Delete("/hypesquad/online").StatusCode == HttpStatusCode.NoContent;

            return client.HttpClient.Post("/hypesquad/online", JsonConvert.SerializeObject(new Hypesquad() { House = house })).StatusCode == HttpStatusCode.NoContent;
        }


        public static bool ChangeSettings(this DiscordClient client, UserSettings settings)
        {
            if (settings.Email == null) settings.Email = client.User.Email;
            if (settings.Username == null) settings.Username = client.User.Username;

            if (client.HttpClient.Patch("/users/@me", JsonConvert.SerializeObject(settings)).StatusCode == HttpStatusCode.OK)
            {
                client.GetClientUser();
                return true;
            }
            else
                return false;
        }
    }
}