using Newtonsoft.Json;
using System.Drawing;

namespace Discord
{
    public class GuildCreationProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        #region icon
        private Base64Image _image = new Base64Image();

        [JsonProperty("icon")]
        private string _icon
        {
            get { return _image.ImageBase64; }
        }

        [JsonIgnore]
        public Image Icon
        {
            get { return _image.Image; }
            set { _image.Image = value; }
        }
        #endregion
    }
}