using Newtonsoft.Json;

namespace Discord
{
    public class Invite : Controllable
    {
        public Invite()
        {
            OnClientUpdated += (sender, e) =>
            {
                Inviter.SetClient(Client);
            };
        }


        [JsonProperty("code")]
        public string Code { get; private set; }


        [JsonProperty("inviter")]
        public User Inviter { get; private set; }


        [JsonProperty("guild")]
#pragma warning disable CS0649
        private readonly Guild _guild;
#pragma warning restore CS0649


        public InviteType Type
        {
            get { return _guild != null ? InviteType.Guild : InviteType.Group; }
        }


        /// <summary>
        /// Deletes the invite
        /// </summary>
        /// <returns></returns>
        public void Delete()
        {
            Inviter = Client.DeleteInvite(Code).Inviter;
        }
        

        public override string ToString()
        {
            return Code;
        }


        public static implicit operator string(Invite instance)
        {
            return instance.Code;
        }
    }
}
