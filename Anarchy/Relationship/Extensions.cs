using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public static class RelationshipsExtensions
    {
        public static IReadOnlyList<Relationship> GetClientRelationships(this DiscordClient client)
        {
            return client.HttpClient.Get($"/users/@me/relationships")
                                .Deserialize<IReadOnlyList<Relationship>>().SetClientsInList(client);
        }


        public static void SendFriendRequest(this DiscordClient client, string username, uint discriminator)
        {
            client.HttpClient.Post("/users/@me/relationships", 
                        "{\"username\":\"" + $"{username}\",\"discriminator\":{discriminator}" + "}");
        }


        public static void BlockUser(this DiscordClient client, ulong userId)
        {
            client.HttpClient.Put($"/users/@me/relationships/{userId}", 
                        JsonConvert.SerializeObject(new Relationship() { Type = RelationshipType.Blocked }));
        }


        //this is used for removing friends, blocking users and cancelling friend requests
        public static void RemoveRelationship(this DiscordClient client, ulong userId)
        {
            client.HttpClient.Delete($"/users/@me/relationships/{userId}");
        }


        #region DMs
        public static IReadOnlyList<Channel> GetClientDMs(this DiscordClient client)
        {
            return client.HttpClient.Get($"/users/@me/channels").Deserialize<IReadOnlyList<Group>>().SetClientsInList(client);
        }


        public static Channel CreateDM(this DiscordClient client, ulong recipientId)
        {
            return client.HttpClient.Post("/users/@me/channels", "{\"recipient_id\":\"" + recipientId + "\"}").Deserialize<Group>().SetClient(client);
        }
        #endregion
    }
}