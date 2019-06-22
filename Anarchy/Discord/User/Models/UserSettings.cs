using Newtonsoft.Json;
using System.Drawing;

namespace Discord
{
    public class UserSettings : Recipient
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("new_password")]
        public string NewPassword { get; set; }

        #region avatar
        private Base64Image _image = new Base64Image();

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
