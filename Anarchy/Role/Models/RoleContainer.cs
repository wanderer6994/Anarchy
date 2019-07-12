using Newtonsoft.Json;

namespace Discord.Gateway
{
    internal class RoleContainer : Controllable
    {
        private ulong _guildId;
        [JsonProperty("guild_id")]
        public ulong GuildId
        {
            get { return _guildId; }
            set { Role.GuildId = _guildId = value; }
        }


        [JsonProperty("role")]
        public Role Role { get; private set; }


        public override string ToString()
        {
            return Role.ToString();
        }
    }
}
