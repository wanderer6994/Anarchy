using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Discord
{
    public static class GuildDiscoveryExtensions
    {
        public static IReadOnlyList<DiscoveryGuild> QueryGuilds(this DiscordClient client, string query, int limit = 20, int offset = 0)
        {
            return ((IReadOnlyList<DiscoveryGuild>)JObject.Parse(client.HttpClient.Get($"/discoverable-guilds?query={query}&offset={offset}&limit={limit}").ToString())["guilds"]
                                                            .ToObject(typeof(IReadOnlyList<DiscoveryGuild>))).SetClientsInList(client);
        }


        public static IReadOnlyList<DiscoveryGuild> QueryGuilds(this DiscordClient client, int limit = 20, int offset = 0)
        {
            return client.QueryGuilds("", limit, offset);
        }


        public static Guild LurkGuild(this DiscordClient client, ulong guildId, string sessionId = null)
        {
            return client.HttpClient.Put($"https://discordapp.com/api/v6/guilds/{guildId}/members/@me?lurker=true&session_id={sessionId}")
                                .Deserialize<Guild>().SetClient(client);
        }
    }
}
