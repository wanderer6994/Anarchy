using System.Collections.Generic;
using Newtonsoft.Json;

namespace Discord
{
    public class Message : Controllable
    {
        public Message()
        {
            OnClientUpdated += (sender, e) =>
            {
                Reactions.SetClientsInList(Client);
                Author.SetClient(Client);
            };
        }


        [JsonProperty("id")]
        public ulong Id { get; private set; }


        [JsonProperty("content")]
        public string Content { get; private set; }


        [JsonProperty("tts")]
        public bool Tts { get; private set; }


        [JsonProperty("author")]
        public User Author { get; private set; }


        [JsonProperty("type")]
        public MessageType Type { get; private set; }


        [JsonProperty("attachments")]
        public IReadOnlyList<Attachment> Attachments { get; private set; }


        [JsonProperty("embeds")]
        public IReadOnlyList<Embed> Embeds { get; private set; }


        [JsonProperty("reactions")]
        public IReadOnlyList<MessageReaction> Reactions { get; private set; }


        [JsonProperty("mentions")]
        public IReadOnlyList<User> Mentions { get; private set; }


        [JsonProperty("mention_roles")]
        public IReadOnlyList<ulong> MentionedRoles { get; private set; }


        [JsonProperty("mention_everyone")]
        public bool MentionedEveryone { get; private set; }


        [JsonProperty("timestamp")]
        public string Timestamp { get; private set; }
        

        [JsonProperty("pinned")]
        public bool Pinned { get; private set; }
      

        [JsonProperty("channel_id")]
        public ulong ChannelId { get; private set; }


        public void Edit(string message)
        {
            if (Type != MessageType.Default)
                return;

            Message msg = Client.EditMessage(ChannelId, Id, message);
            Content = msg.Content;
            Pinned = msg.Pinned;
            Mentions = msg.Mentions;
            MentionedRoles = msg.MentionedRoles;
            MentionedEveryone = msg.MentionedEveryone;
            Embeds = msg.Embeds;
        }


        public void Delete()
        {
            Client.DeleteMessage(ChannelId, Id);
        }


        public IReadOnlyList<User> GetReactions(string reaction, uint limit = 25, ulong afterId = 0)
        {
            return Client.GetMessageReactions(ChannelId, Id, reaction, limit, afterId);
        }


        public void AddReaction(string reaction)
        {
            Client.AddMessageReaction(ChannelId, Id, reaction);
        }


        public void RemoveReaction(string reaction)
        {
            Client.RemoveMessageReaction(ChannelId, Id, reaction);
        }


        public override string ToString()
        {
            return Author.ToString();
        }
    }
}