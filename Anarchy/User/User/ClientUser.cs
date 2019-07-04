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


        public bool ChangeSettings(UserSettings settings)
        {
            if (settings.Email == null) settings.Email = Email;
            if (!settings.DiscriminatorProperty.Set)
                settings.Discriminator = Discriminator;
            if (settings.Username == null) settings.Username = Username;

            if (Client.HttpClient.Patch("/users/@me", JsonConvert.SerializeObject(settings)).StatusCode == HttpStatusCode.OK)
            {
                Client.GetClientUser();
                return true;
            }
            else
                return false;
        }


        public void SetHypesquad(HypesquadHouse house)
        {
            Client.SetHypesquad(house);
        }
    }
}
