using Newtonsoft.Json;
using System.Drawing;

namespace Discord
{
    public class UserSettings
    {
        [JsonProperty("username")]
        public string Username { get; internal set; }

        [JsonProperty("discriminator")]
        public int Discriminator { get; internal set; }

        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("new_password")]
        public string NewPassword { get; set; }

        #region avatar
        private DiscordImage _image = new DiscordImage();

        [JsonProperty("avatar")]
        private string _avatar
        {
            get { return _image.ImageBase64; }
        }

        [JsonIgnore]
        public Image Avatar
        {
            get
            {
                return _image.Image;
            }
            set
            {
                _image.Image = value;
            }
        }
        #endregion
    }
}