using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Discord
{
    public class Message : Controllable
    {
#pragma warning disable CS0649
        public Message()
        {
            Reactions = new List<MessageReaction>();

            OnClientUpdated += (sender, e) =>
            {
                Reactions.SetClientsInList(Client);
                Mentions.SetClientsInList(Client);
            };
        }


        [JsonProperty("id")]
        public ulong Id { get; private set; }


        [JsonProperty("content")]
        public string Content { get; private set; }


        [JsonProperty("tts")]
        public bool Tts { get; private set; }


        [JsonProperty("author")]
        private User _authorUser;


        [JsonProperty("member")]
        private PartialGuildMember _authorMember;


        public GuildMember Author
        {
            get
            {
                return GuildMember.FromInformation(_authorUser, GuildId.Value, _authorMember);
            }
        }


        [JsonProperty("attachments")]
        private readonly IReadOnlyList<Attachment> _attachments;
        public Attachment Attachment
        {
            get { return _attachments == null || _attachments.Count == 0 ? null : _attachments[0]; }
        }


        [JsonProperty("embeds")]
        private IReadOnlyList<Embed> _embeds;
        public Embed Embed
        {
            get { return _embeds == null || _embeds.Count == 0 ? null : _embeds[0]; }
            private set { _embeds = new List<Embed>() { value }; }
        }


        [JsonProperty("reactions")]
        public IReadOnlyList<MessageReaction> Reactions { get; private set; }


        [JsonProperty("mentions")]
        public IReadOnlyList<User> Mentions { get; private set; }


        [JsonProperty("mention_roles")]
        public IReadOnlyList<ulong> MentionedRoles { get; private set; }


        [JsonProperty("mention_everyone")]
        public bool MentionedEveryone { get; private set; }


        [JsonProperty("timestamp")]
        private readonly string _timestamp;
        public DateTime Timestamp
        {
            get { return DiscordTimestamp.FromString(_timestamp); }
        }


        [JsonProperty("pinned")]
        public bool Pinned { get; private set; }
      

        [JsonProperty("channel_id")]
        public ulong ChannelId { get; private set; }


        /// <summary>
        /// This will only be set if the message is received through the gateway
        /// </summary>
        [JsonProperty("guild_id")]
        public ulong? GuildId { get; private set; }


        [JsonProperty("type")]
        public MessageType Type { get; private set; }


        /// <summary>
        /// Edits the message
        /// </summary>
        /// <param name="message">The new contents of the message</param>
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
            Embed = msg.Embed;
        }


        /// <summary>
        /// Deletes the message
        /// </summary>
        public void Delete()
        {
            Client.DeleteMessage(ChannelId, Id);
        }


        /// <summary>
        /// Gets instances of a reaction to a message
        /// </summary>
        /// <param name="reaction">The reaction</param>
        /// <param name="limit">Max amount of reactions to receive</param>
        /// <param name="afterId">The reaction ID to offset from</param>
        public IReadOnlyList<User> GetReactions(string reaction, uint limit = 25, ulong afterId = 0)
        {
            return Client.GetMessageReactions(ChannelId, Id, reaction, limit, afterId);
        }


        /// <summary>
        /// Adds a reaction to the message
        /// </summary>
        public void AddReaction(string reaction)
        {
            Client.AddMessageReaction(ChannelId, Id, reaction);
        }


        /// <summary>
        /// Adds a reaction to the message
        /// </summary>
        public void AddReaction(PartialEmoji emoji)
        {
            AddReaction(emoji.GetMessegable());
        }


        /// <summary>
        /// Removes a reaction from the message
        /// </summary>
        public void RemoveReaction(string reaction)
        {
            Client.RemoveMessageReaction(ChannelId, Id, reaction);
        }


        /// <summary>
        /// Removes a reaction from the message
        /// </summary>
        public void RemoveReaction(PartialEmoji emoji)
        {
            AddReaction(emoji.GetMessegable());
        }


        public override string ToString()
        {
            return Author.ToString();
        }
#pragma warning restore CS0649
    }
}