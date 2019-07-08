using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public static class EmojiExtentions
    {
        #region management
        public static Emoji CreateGuildEmoji(this DiscordClient client, ulong guildId, EmojiCreationProperties properties)
        {
            Emoji emoji = client.HttpClient.Post($"/guilds/{guildId}/emojis", 
                            JsonConvert.SerializeObject(properties)).Deserialize<Emoji>().SetClient(client);
            emoji.GuildId = guildId;
            return emoji;
        }


        public static Emoji ModifyGuildEmoji(this DiscordClient client, ulong guildId, ulong reactionId, string name)
        {
            return client.HttpClient.Patch($"/guilds/{guildId}/emojis/{reactionId}", 
                JsonConvert.SerializeObject(new EmojiProperties() { Name = name })).Deserialize<Emoji>().SetClient(client);
        }


        public static void DeleteGuildEmoji(this DiscordClient client, ulong guildId, ulong reactionId)
        {
            client.HttpClient.Delete($"/guilds/{guildId}/emojis/{reactionId}");
        }
        #endregion


        public static IReadOnlyList<Emoji> GetGuildEmojis(this DiscordClient client, ulong guildId)
        {
            var reactions = client.HttpClient.Get($"/guilds/{guildId}/emojis")
                                        .Deserialize<IReadOnlyList<Emoji>>().SetClientsInList(client);
            foreach (var reaction in reactions) reaction.GuildId = guildId;
            return reactions;
        }


        public static Emoji GetGuildEmoji(this DiscordClient client, ulong guildId, ulong reactionId)
        {
            Emoji reaction = client.HttpClient.Get($"/guilds/{guildId}/emojis/{reactionId}")
                                        .Deserialize<Emoji>().SetClient(client);
            reaction.GuildId = guildId;
            return reaction;
        }


        public static IReadOnlyList<User> GetMessageReactions(this DiscordClient client, ulong channelId, ulong messageId, string reaction, uint limit = 25, ulong afterId = 0)
        {
            return client.HttpClient.Get($"/channels/{channelId}/messages/{messageId}/reactions/{reaction}?limit={limit}&after={afterId}")
                                .Deserialize<IReadOnlyList<User>>().SetClientsInList(client);
        }


        public static void AddMessageReaction(this DiscordClient client, ulong channelId, ulong messageId, string reaction)
        {
            client.HttpClient.Put($"/channels/{channelId}/messages/{messageId}/reactions/{reaction}/@me");
        }


        public static void RemoveMessageReaction(this DiscordClient client, ulong channelId, ulong messageId, string reaction)
        {
            client.HttpClient.Delete($"/channels/{channelId}/messages/{messageId}/reactions/{reaction}/@me");
        }
    }
}