using Newtonsoft.Json;

namespace Discord
{
    public class Ban : Controllable
    {
        public Ban()
        {
            OnClientUpdated += (sender, e) => User.SetClient(Client);
        }


        [JsonProperty("reason")]
        public string Reason { get; private set; }


        [JsonProperty("user")]
        public User User { get; private set; }


        public ulong GuildId { get; internal set; }


        /// <summary>
        /// Unbans the user
        /// </summary>
        public void Unban()
        {
            Client.UnbanGuildMember(GuildId, User.Id);
        }


        public override string ToString()
        {
            return User.ToString();
        }
    }
}
