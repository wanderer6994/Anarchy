using Newtonsoft.Json;
using System.Drawing;

namespace Discord
{
    public class ReactionProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        #region image
        private Base64Image _image = new Base64Image();

        [JsonProperty("image")]
        private string _img
        {
            get { return _image.ImageBase64; }
        }

        [JsonIgnore]
        public Image Image
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