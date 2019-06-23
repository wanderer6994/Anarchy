using Newtonsoft.Json;
using System.Net;

namespace Discord
{
    public static class RelationsExtensions
    {
        #region add friend
        public static bool AddFriend(this DiscordClient client, Recipient recipient)
        {
            return client.HttpClient.Post("/users/@me/relationships",
                JsonConvert.SerializeObject(recipient)).StatusCode == HttpStatusCode.NoContent;
        }


        public static bool AddFriend(this DiscordClient client, string username, int discriminator)
        {
            Recipient recipient = new Recipient
            {
                Username = username,
                Discriminator = discriminator
            };

            return client.AddFriend(recipient);
        }
        #endregion


        #region remove friend
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
    }
}