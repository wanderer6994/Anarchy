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
    }
}
