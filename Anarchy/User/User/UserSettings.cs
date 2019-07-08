using Newtonsoft.Json;
using System.Drawing;

namespace Discord
{
    public class UserSettings
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        internal Property<uint> DiscriminatorProperty = new Property<uint>();
        [JsonProperty("discriminator")]
        public uint Discriminator
        {
            get { return DiscriminatorProperty; }
            set { DiscriminatorProperty.Value = value; }
        }

        [JsonProperty("email")]
        public string Email { get; set; }

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

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("new_password")]
        public string NewPassword { get; set; }
    }
}