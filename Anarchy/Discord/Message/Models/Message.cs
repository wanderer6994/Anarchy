using System.Collections.Generic;
using Newtonsoft.Json;

namespace Discord
{
    public class Message
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("channel_id")]
        public long ChannelId { get; set; }

        [JsonProperty("author")]
        public User Author { get; set; }

        [JsonProperty("tts")]
        public bool Tts { get; set; }

        //if anyone knows how tf you'd differanciate between images and other files do a pull request
        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }

        [JsonProperty("guild_id")]
        public long? GuildId { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
        
        [JsonProperty("pinned")]
        public bool Pinned { get; set; }
        
        [JsonProperty("mentions")]
        public List<User> Mentions { get; set; }

        [JsonProperty("mention_roles")]
        public List<long> MentionedRoles { get; set; }

        [JsonProperty("mention_everyone")]
        public bool MentionedEveryone { get; set; }
        
        [JsonProperty("embeds")]
        public List<Embed> Embeds { get; set; }

        [JsonIgnore]
        internal DiscordClient Client { get; set; }

        public bool AddReaction(string reaction)
        {
            return Client.AddMessageReaction(ChannelId, Id, reaction);
        }

        public bool RemoveReaction(string reaction)
        {
            return Client.RemoveMessageReaction(ChannelId, Id, reaction);
        }

        public Message Edit(string message)
        {
            Message msg = Client.EditMessage(ChannelId, Id, message);
            Content = msg.Content;
            Pinned = msg.Pinned;
            Mentions = msg.Mentions;
            MentionedRoles = msg.MentionedRoles;
            MentionedEveryone = msg.MentionedEveryone;
            Embeds = msg.Embeds;
            return msg;
        }

        public bool Delete()
        {
            return Client.DeleteMessage(ChannelId, Id);
        }
    }
}
