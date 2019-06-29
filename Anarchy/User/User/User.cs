using Newtonsoft.Json;

namespace Discord
{
    public class User : Controllable
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("username")]
        public string Username { get; internal set; }

        [JsonProperty("discriminator")]
        public int Discriminator { get; internal set; }

        [JsonProperty("avatar")]
        public string AvatarId { get; private set; }

        [JsonProperty("bot")]
        public bool Bot { get; private set; }


        public void Update()
        {
            User user = Client.GetUser(Id);
            Username = user.Username;
            Discriminator = user.Discriminator;
            AvatarId = user.AvatarId;
        }


        public override string ToString()
        {
            return $"{Username}#{Discriminator} ({Id})";
        }
    }
}