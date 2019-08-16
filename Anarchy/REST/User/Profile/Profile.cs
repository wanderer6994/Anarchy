using System.Collections.Generic;
using Newtonsoft.Json;

namespace Discord
{
    public class Profile : Controllable
    {
        public Profile()
        {
            OnClientUpdated += (sender, e) => User.SetClient(Client);
        }


        /// <summary>
        /// Updates the profile's info
        /// </summary>
        public void Update()
        {
            Profile profile = Client.GetProfile(User.Id);
            User = profile.User;
            MutualGuilds = profile.MutualGuilds;
            ConnectedAccounts = profile.ConnectedAccounts;
        }


        [JsonProperty("user")]
        public User User { get; private set; }


        [JsonProperty("premium_since")]
        private string _premiumSince;

        public Nitro Nitro
        {
            get { return new Nitro(_premiumSince); }
        }


        [JsonProperty("mutual_guilds")]
        public IReadOnlyList<MutualGuild> MutualGuilds { get; private set; }

        [JsonProperty("connected_accounts")]
        public IReadOnlyList<ConnectedAccount> ConnectedAccounts { get; private set; }


        public override string ToString()
        {
            return User.ToString();
        }
    }
}
