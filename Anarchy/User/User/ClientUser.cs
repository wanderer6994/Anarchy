using Newtonsoft.Json;

namespace Discord
{
    public class ClientUser : User
    {
        [JsonProperty("email")]
        public string Email { get; private set; }

        [JsonProperty("verified")]
        public bool EmailVerified { get; private set; }

        [JsonProperty("mfa_enabled")]
        public bool TwoFactorAuth { get; private set; }

        [JsonProperty("locale")]
        public string Language { get; private set; }

        [JsonProperty("premium_type")]
        public NitroType Nitro { get; private set; }


        public override void Update()
        {
            ClientUser user = Client.GetClientUser();
            Username = user.Username;
            Discriminator = user.Discriminator;
            AvatarId = user.AvatarId;
            Email = user.Email;
            EmailVerified = user.EmailVerified;
            TwoFactorAuth = user.TwoFactorAuth;
            Language = user.Language;
            Nitro = user.Nitro;
        }
    }
}
