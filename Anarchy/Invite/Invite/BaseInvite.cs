using Newtonsoft.Json;

namespace Discord
{
    public class BaseInvite : Controllable
    {
        public BaseInvite()
        {
            OnClientUpdated += (sender, e) =>
            {
                Guild.SetClient(Client);
                Channel.SetClient(Client);
                Creator.SetClient(Client);
            };
        }

        [JsonProperty("code")]
        public string Code { get; private set; }

        [JsonProperty("guild")]
        public BaseGuild Guild { get; private set; }

        [JsonProperty("channel")]
        public Channel Channel { get; private set; }

        [JsonProperty("inviter")]
        public User Creator { get; private set; }

        [JsonProperty("temporary")]
        public bool Temporary { get; private set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; private set; }

        [JsonProperty("uses")]
        public int Uses { get; private set; }

        [JsonProperty("max_uses")]
        public int MaxUses { get; private set; }


        public Invite Delete()
        {
            return Client.DeleteInvite(Code);
        }


        public override string ToString()
        {
            return Code;
        }
    }
}
