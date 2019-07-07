using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord.Gateway
{
    internal class EmojiContainer : Controllable
    {
        public EmojiContainer()
        {
            OnClientUpdated += (sender, e) => Emojis.SetClientsInList(Client);
        }

        [JsonProperty("guild_id")]
        public long GuildId { get; private set; }

        [JsonProperty("emojis")]
        public List<Emoji> Emojis { get; private set; }
    }
}
