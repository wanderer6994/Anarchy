using Newtonsoft.Json;
using System.Net.Http;

namespace Discord
{
    public static class UserExtensions
    {
        public static User GetUser(this DiscordClient client, long userId)
        {
            return client.HttpClient.Get($"/users/{userId}")
                                .Deserialize<User>().SetClient(client);
        }


        public static ClientUser GetClientUser(this DiscordClient client)
        {
            HttpResponseMessage resp;

            try
            {
                resp = client.HttpClient.Get("/users/@me");
            }
            catch
            {
                client.User = null;
                throw;
            }

            client.User = resp.Deserialize<ClientUser>().SetClient(client);
            return client.User;
        }


        public static void SetHypesquad(this DiscordClient client, HypesquadHouse house)
        {
            if (house == HypesquadHouse.None)
                client.HttpClient.Delete("/hypesquad/online");

            client.HttpClient.Post("/hypesquad/online", 
                        JsonConvert.SerializeObject(new Hypesquad() { House = house }));
        }
    }
}