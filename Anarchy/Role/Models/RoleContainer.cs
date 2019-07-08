using Newtonsoft.Json;

namespace Discord.Gateway
{
    internal class RoleContainer
    {
        private ulong _guildId;
        [JsonProperty("guild_id")]
        public ulong GuildId
        {
            get { return _guildId; }
            set
            {
                _guildId = value;
                Role.GuildId = _guildId;
            }
        }

        [JsonProperty("role")]
        public Role Role { get; private set; }


        public override string ToString()
        {
            return Role.ToString();
        }
    }
}
