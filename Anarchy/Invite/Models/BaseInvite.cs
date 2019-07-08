using Newtonsoft.Json;

namespace Discord
{
    public abstract class BaseInvite : Controllable
    {
        public BaseInvite()
        {
            OnClientUpdated += (sender, e) =>
            {
                Guild.SetClient(Client);
                Channel.SetClient(Client);
                Inviter.SetClient(Client);
            };
        }

        [JsonProperty("code")]
        public string Code { get; private set; }

        [JsonProperty("guild")]
        public PartialGuild Guild { get; private set; }

        [JsonProperty("channel")]
        public Channel Channel { get; private set; }

        [JsonProperty("inviter")]
        public User Inviter { get; private set; }

        [JsonProperty("temporary")]
        public bool Temporary { get; private set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; private set; }

        [JsonProperty("uses")]
        public uint Uses { get; private set; }

        [JsonProperty("max_uses")]
        public uint MaxUses { get; private set; }


        public PartialInvite Join()
        {
            return Guild == null ? Client.JoinGroup(Code) : Client.JoinGuild(Code);
        }


        public PartialInvite Delete()
        {
            return Client.DeleteInvite(Code);
        }


        public override string ToString()
        {
            return Code;
        }
    }
}
