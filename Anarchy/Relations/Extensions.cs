using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class RelationsExtensions
    {
        #region add/remove friend
        public static bool AddFriend(this DiscordClient client, string username, int discriminator)
        {
            return client.HttpClient.Post("/users/@me/relationships",
                                JsonConvert.SerializeObject(new NameDiscriminator { Username = username, Discriminator = discriminator })).StatusCode == HttpStatusCode.NoContent;
        }


        public static bool RemoveFriend(this DiscordClient client, long userId)
        {
            var resp = client.HttpClient.Delete($"/users/@me/relationships/{userId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new UserNotFoundException(client, userId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }


        public static bool RemoveFriend(this DiscordClient client, User user)
        {
            return client.RemoveFriend(user.Id);
        }
        #endregion


        #region DMs
        public static List<Channel> GetClientDMs(this DiscordClient client)
        {
            var resp = client.HttpClient.Get($"/users/@me/channels");

            return resp.Deserialize<List<Channel>>().SetClientsInList(client);
        }


        public static Channel CreateDM(this DiscordClient client, long recipientId)
        {
            var resp = client.HttpClient.Post("/users/@me/channels", "{\"recipient_id\":\"" + recipientId + "\"}");

            return resp.Deserialize<Channel>().SetClient(client);
        }
        #endregion
    }
}