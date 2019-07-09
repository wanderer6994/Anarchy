using Newtonsoft.Json;
using System.Net;

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
            Client.GetClientUser();
        }


        public void ChangeProfile(UserSettings settings)
        {
            if (settings.Email == null)
                settings.Email = Email;
            if (!settings.DiscriminatorProperty.Set)
                settings.Discriminator = Discriminator;
            if (settings.Username == null)
                settings.Username = Username;

            ClientUser user = Client.HttpClient.Patch("/users/@me", JsonConvert.SerializeObject(settings)).Deserialize<ClientUser>();
            Email = user.Email;
            EmailVerified = user.EmailVerified;
            Username = user.Username;
            Discriminator = user.Discriminator;
            TwoFactorAuth = user.TwoFactorAuth;
            Language = user.Language;
            Nitro = user.Nitro;
        }


        public void SetHypesquad(HypesquadHouse house)
        {
            if (house == HypesquadHouse.None)
                Client.HttpClient.Delete("/hypesquad/online");

            Client.HttpClient.Post("/hypesquad/online",
                        JsonConvert.SerializeObject(new HypesquadContainer() { House = house }));
        }
    }
}
