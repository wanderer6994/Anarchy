using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// Account user
    /// </summary>
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


        /// <summary>
        /// Updates the user's info
        /// </summary>
        public override void Update()
        {
            Client.GetClientUser();
        }


        /// <summary>
        /// Changes the user's profile
        /// </summary>
        /// <param name="settings">Options for changing the profile</param>
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


        public void SetHypesquad(Hypesquad house)
        {
            if (house == Hypesquad.None)
                Client.HttpClient.Delete("/hypesquad/online");

            Client.HttpClient.Post("/hypesquad/online", $"{{\"house_id\":{(int)house}}}");
        }
    }
}
