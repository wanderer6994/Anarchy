using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public static class RelationshipsExtensions
    {
        public static List<Relationship> GetRelationships(this DiscordClient client)
        {
            return client.HttpClient.Get($"/users/@me/relationships").Deserialize<List<Relationship>>();
        }


        public static void SendFriendRequest(this DiscordClient client, string username, int discriminator)
        {
            client.HttpClient.Post("/users/@me/relationships",
                        JsonConvert.SerializeObject(new NameDiscriminator { Username = username, Discriminator = discriminator }));
        }


        public static void BlockUser(this DiscordClient client, long userId)
        {
            client.HttpClient.Put($"/users/@me/relationships/{userId}", 
                        JsonConvert.SerializeObject(new Relationship() { Type = RelationshipType.Blocked }));
        }


        //this is used for removing friends, blocking users and cancelling friend requests
        public static void RemoveRelationship(this DiscordClient client, long userId)
        {
            client.HttpClient.Delete($"/users/@me/relationships/{userId}");
        }


        #region DMs
        public static IReadOnlyList<Channel> GetClientDMs(this DiscordClient client)
        {
            return client.HttpClient.Get($"/users/@me/channels").Deserialize<IReadOnlyList<Channel>>().SetClientsInList(client);
        }


        public static Channel CreateDM(this DiscordClient client, long recipientId)
        {
            return client.HttpClient.Post("/users/@me/channels", "{\"recipient_id\":\"" + recipientId + "\"}").Deserialize<Channel>().SetClient(client);
        }
        #endregion
    }
}