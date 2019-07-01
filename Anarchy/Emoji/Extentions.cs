using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class EmojiExtentions
    {
        #region management
        public static Emoji CreateGuildEmoji(this DiscordClient client, long guildId, EmojiCreationProperties properties)
        {
            var resp = client.HttpClient.Post($"/guilds/{guildId}/emojis", JsonConvert.SerializeObject(properties));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            Emoji emoji = resp.Deserialize<Emoji>().SetClient(client);
            emoji.GuildId = guildId;
            return emoji;
        }


        public static Emoji ModifyGuildEmoji(this DiscordClient client, long guildId, long reactionId, string name)
        {
            var resp = client.HttpClient.Patch($"/guilds/{guildId}/emojis/{reactionId}", JsonConvert.SerializeObject(new EmojiModProperties() { Name = name }));

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new EmojiNotFoundException(client, guildId, reactionId);

            return resp.Deserialize<Emoji>().SetClient(client);
        }


        public static bool DeleteGuildEmoji(this DiscordClient client, long guildId, long reactionId)
        {
            var resp = client.HttpClient.Delete($"/guilds/{guildId}/emojis/{reactionId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new EmojiNotFoundException(client, guildId, reactionId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
        #endregion


        public static IReadOnlyList<Emoji> GetGuildEmojis(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.Get($"/guilds/{guildId}/emojis");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            var reactions = resp.Deserialize<List<Emoji>>().SetClientsInList(client);
            foreach (var reaction in reactions) reaction.GuildId = guildId;
            return reactions;
        }


        public static Emoji GetGuildEmoji(this DiscordClient client, long guildId, long reactionId)
        {
            var resp = client.HttpClient.Get($"/guilds/{guildId}/emojis/{reactionId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new EmojiNotFoundException(client, guildId, reactionId);

            Emoji reaction = resp.Deserialize<Emoji>().SetClient(client);
            reaction.GuildId = guildId;
            return reaction;
        }


        public static IReadOnlyList<User> GetMessageReactions(this DiscordClient client, long channelId, long messageId, string reaction, int limit = 25, int afterId = 0)
        {
            var resp = client.HttpClient.Get($"/channels/{channelId}/messages/{messageId}/reactions/{reaction}?limit={limit}&after={afterId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ChannelNotFoundException(client, channelId);

            return resp.Deserialize<IReadOnlyList<User>>().SetClientsInList(client);
        }


        public static bool AddMessageReaction(this DiscordClient client, long channelId, long messageId, string reaction)
        {
            var resp = client.HttpClient.Put($"/channels/{channelId}/messages/{messageId}/reactions/{reaction}/@me");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new MessageNotFoundException(client, messageId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }


        public static bool RemoveMessageReaction(this DiscordClient client, long channelId, long messageId, string reaction)
        {
            var resp = client.HttpClient.Delete($"/channels/{channelId}/messages/{messageId}/reactions/{reaction}/@me");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new MessageNotFoundException(client, messageId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
    }
}