using Newtonsoft.Json;

namespace Discord.Gateway
{
    internal class BanContainer : Controllable
    {
        public BanContainer()
        {
            OnClientUpdated += (sender, e) => User.SetClient(Client);
        }

        [JsonProperty("guild_id")]
        public long GuildId { get; private set; }

        [JsonProperty("user")]
        public User User { get; private set; }


        public override string ToString()
        {
            return User.ToString();
        }
    }
}
