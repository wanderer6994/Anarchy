namespace Discord
{
    public static class UserExtensions
    {
        public static User GetUser(this DiscordClient client, ulong userId)
        {
            return client.HttpClient.Get($"/users/{userId}")
                                .Deserialize<User>().SetClient(client);
        }


        public static ClientUser GetClientUser(this DiscordClient client)
        {
            try
            {
                client.User = client.HttpClient.Get("/users/@me")
                                        .Deserialize<ClientUser>().SetClient(client);
                return client.User;
            }
            catch (DiscordHttpException)
            {
                client.User = null;
                throw;
            }
        }
    }
}