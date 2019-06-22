using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class ReactionExtentions
    {
        public static List<Reaction> GetGuildReactions(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.GetAsync($"/guilds/{guildId}/emojis").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);
            
            List<Reaction> reactions = JsonConvert.DeserializeObject<List<Reaction>>(resp.Content.ReadAsStringAsync().Result);
            foreach (var reaction in reactions)
            {
                reaction.Client = client;
                reaction.GuildId = guildId;
            }
            return reactions;
        }

        public static Reaction GetGuildReaction(this DiscordClient client, long guildId, long reactionId)
        {
            var resp = client.HttpClient.GetAsync($"/guilds/{guildId}/emojis/{reactionId}").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ReactionNotFoundException(client, guildId, reactionId);

            Reaction reaction = JsonConvert.DeserializeObject<Reaction>(resp.Content.ReadAsStringAsync().Result);
            reaction.Client = client;
            reaction.GuildId = guildId;
            return reaction;
        }

        public static Reaction CreateReaction(this DiscordClient client, long guildId, ReactionProperties properties)
        {
            var resp = client.HttpClient.PostAsync($"/guilds/{guildId}/emojis", JsonConvert.SerializeObject(properties)).Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            Reaction reaction = JsonConvert.DeserializeObject<Reaction>(resp.Content.ReadAsStringAsync().Result);
            reaction.Client = client;
            reaction.GuildId = guildId;
            return reaction;
        }
        
        public static bool DeleteReaction(this DiscordClient client, long guildId, long reactionId)
        {
            var resp = client.HttpClient.DeleteAsync($"/guilds/{guildId}/emojis/{reactionId}").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ReactionNotFoundException(client, guildId, reactionId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
        
        public static bool AddMessageReaction(this DiscordClient client, long channelId, long messageId, string reaction)
        {
            var resp = client.HttpClient.PutAsync($"/channels/{channelId}/messages/{messageId}/reactions/{reaction}/@me").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new MessageNotFoundException(client, messageId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }

        public static bool RemoveMessageReaction(this DiscordClient client, long channelId, long messageId, string reaction)
        {
            var resp = client.HttpClient.DeleteAsync($"/channels/{channelId}/messages/{messageId}/reactions/{reaction}/@me").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new MessageNotFoundException(client, messageId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
    }
}