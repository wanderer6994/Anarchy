using Newtonsoft.Json;
using System.Net;

namespace Discord
{
    public static class RelationsExtensions
    {
        public static bool AddFriend(this DiscordClient client, Recipient recipient)
        {
            return client.HttpClient.PostAsync("/users/@me/relationships",
                JsonConvert.SerializeObject(recipient)).Result.StatusCode == HttpStatusCode.NoContent;
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
    }
}