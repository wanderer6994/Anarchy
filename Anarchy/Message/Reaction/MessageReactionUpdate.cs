using Newtonsoft.Json;

namespace Discord
{
    public class MessageReactionUpdate : Controllable
    {
        public MessageReactionUpdate()
        {
            OnClientUpdated += (sender, e) => Emoji.SetClient(Client);
        }


        [JsonProperty("emoji")]
        public PartialEmoji Emoji { get; private set; }


        [JsonProperty("message_id")]
        public long MessageId { get; private set; }


        [JsonProperty("channel_id")]
        public ulong ChannelId { get; private set; }


        private ulong _guildId;
        [JsonProperty("guild_id")]
        public ulong GuildId
        {
            get { return _guildId; }
            set
            {
                _guildId = value;
                Emoji.GuildId = _guildId;
            }
        }


        [JsonProperty("user_id")]
        public ulong UserId { get; private set; }


        public override string ToString()
        {
            return Emoji.ToString();
        }
    }
}
