using System.Collections.Generic;

namespace Discord
{
    public static class ConnectedAccountsExtensions
    {
        public static IReadOnlyList<ConnectedAccount> GetConnectedAccounts(this DiscordClient client)
        {
            return client.HttpClient.Get($"/users/@me/connections")
                                .Deserialize<IReadOnlyList<ConnectedAccount>>();
        }


        //there might be some new JSON error codes to get here :)
        public static void RemoveConnectedAccount(this DiscordClient client, AccountType type, string id)
        {
            client.HttpClient.Delete($"/users/@me/connections/{type.ToString()}/{id}");
        }
    }
}
