using Newtonsoft.Json;
using System.Net;

namespace Discord
{
    public static class RelationsExtensions
    {
        public static bool AddFriend(this DiscordClient client, NameDiscriminator recipient)
        {
            return client.HttpClient.Post("/users/@me/relationships",
                JsonConvert.SerializeObject(recipient)).StatusCode == HttpStatusCode.NoContent;
        }


        public static bool AddFriend(this DiscordClient client, string username, int discriminator)
        {
            NameDiscriminator recipient = new NameDiscriminator
            {
                Username = username,
                Discriminator = discriminator
            };

            return client.AddFriend(recipient);
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

        public static Channel CreateDM(this DiscordClient client, long recipientId)
        {
            var resp = client.HttpClient.Post("/users/@me/channels", "{\"recipient_id\":\"" + recipientId + "\"}");

            return JsonConvert.DeserializeObject<Channel>(resp.Content.ReadAsStringAsync().Result).SetClient(client);
        }
    }
}