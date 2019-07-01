using Newtonsoft.Json;
using System.Drawing;
using System.Net;
using System.Net.Http;

namespace Discord
{
    public class User : Controllable
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("username")]
        public string Username { get; protected set; }

        [JsonProperty("discriminator")]
        public int Discriminator { get; protected set; }

        [JsonProperty("avatar")]
        public string AvatarId { get; protected set; }

        [JsonProperty("bot")]
        private bool _bot;


        public UserType Type
        {
            get
            {
                if (Discriminator == 0)
                    return UserType.Webhook;

                return _bot ? UserType.Bot : UserType.User;
            }
        }


        public virtual void Update()
        {
            User user = Client.GetUser(Id);
            Username = user.Username;
            Discriminator = user.Discriminator;
            AvatarId = user.AvatarId;
        }


        public Image GetAvatar()
        {
            var resp = new HttpClient().GetAsync($"https://cdn.discordapp.com/avatars/{Id}/{AvatarId}.png").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ImageNotFoundException(AvatarId);

            return (Bitmap)new ImageConverter().ConvertFrom(resp.Content.ReadAsByteArrayAsync().Result);
        }


        public override string ToString()
        {
            return $"{Username}#{Discriminator} ({Id})";
        }
    }
}