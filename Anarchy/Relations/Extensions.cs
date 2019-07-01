using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class RelationsExtensions
    {
        #region friending
        public static bool AddFriend(this DiscordClient client, string username, int discriminator)
        {
            return client.HttpClient.Post("/users/@me/relationships",
                                JsonConvert.SerializeObject(new NameDiscriminator { Username = username, Discriminator = discriminator })).StatusCode == HttpStatusCode.NoContent;
        }


        public static bool AddFriend(this DiscordClient client, User user)
        {
            return client.AddFriend(user.Username, user.Discriminator);
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


        #region blocking
        public static bool BlockUser(this DiscordClient client, long userId)
        {
            var resp = client.HttpClient.Put($"/users/@me/relationships/{userId}", "{\"type\": 2}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new UserNotFoundException(client, userId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }


        public static bool BlockUser(this DiscordClient client, User user)
        {
            return client.BlockUser(user.Id);
        }


        public static bool UnblockUser(this DiscordClient client, long userId)
        {
            return client.RemoveFriend(userId);
        }


        public static bool UnblockUser(this DiscordClient client, User user)
        {
            return client.UnblockUser(user.Id);
        }
        #endregion


        #region DMs
        public static IReadOnlyList<Channel> GetClientDMs(this DiscordClient client)
        {
            return client.HttpClient.Get($"/users/@me/channels").Deserialize<IReadOnlyList<Channel>>().SetClientsInList(client);
        }


        public static Channel CreateDM(this DiscordClient client, long recipientId)
        {
            return client.HttpClient.Post("/users/@me/channels", "{\"recipient_id\":\"" + recipientId + "\"}").Deserialize<Channel>().SetClient(client);
        }

        public static Channel CloseDM(this DiscordClient client, long channelId)
        {
            return client.DeleteChannel(channelId);
        }
        #endregion
    }
}