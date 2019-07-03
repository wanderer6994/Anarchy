using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class RelationshipsExtensions
    {
        public static List<Relationship> GetRelationships(this DiscordClient client)
        {
            return client.HttpClient.Get($"/users/@me/relationships").Deserialize<List<Relationship>>();
        }


        public static bool AddFriend(this DiscordClient client, string username, int discriminator)
        {
            return client.HttpClient.Post("/users/@me/relationships",
                                JsonConvert.SerializeObject(new NameDiscriminator { Username = username, Discriminator = discriminator })).StatusCode == HttpStatusCode.NoContent;
        }


        public static bool AddFriend(this DiscordClient client, User user)
        {
            return client.AddFriend(user.Username, user.Discriminator);
        }


        public static bool BlockUser(this DiscordClient client, long userId)
        {
            var resp = client.HttpClient.Put($"/users/@me/relationships/{userId}", JsonConvert.SerializeObject(new Relationship() { Type = RelationshipType.Blocked }));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new UserNotFoundException(client, userId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }


        public static bool BlockUser(this DiscordClient client, User user)
        {
            return client.BlockUser(user.Id);
        }


        //this is used for removing a friend, blocking a user, and cancelling a friend request
        public static bool RemoveRelationship(this DiscordClient client, long userId)
        {
            var resp = client.HttpClient.Delete($"/users/@me/relationships/{userId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new UserNotFoundException(client, userId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }


        public static bool RemoveRelationship(this DiscordClient client, User user)
        {
            return client.RemoveRelationship(user.Id);
        }


        #region DMs
        public static IReadOnlyList<Channel> GetClientDMs(this DiscordClient client)
        {
            return client.HttpClient.Get($"/users/@me/channels").Deserialize<IReadOnlyList<GuildChannel>>().SetClientsInList(client);
        }


        public static Channel CreateDM(this DiscordClient client, long recipientId)
        {
            return client.HttpClient.Post("/users/@me/channels", "{\"recipient_id\":\"" + recipientId + "\"}").Deserialize<GuildChannel>().SetClient(client);
        }

        public static Channel CloseDM(this DiscordClient client, long channelId)
        {
            return client.DeleteChannel(channelId);
        }
        #endregion
    }
}