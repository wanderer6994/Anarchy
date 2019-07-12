using Newtonsoft.Json;
using System.Drawing;

namespace Discord
{
    /// <summary>
    /// Options for creating a <see cref="Emoji"/>
    /// </summary>
    public class EmojiProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }


        #region image
        private readonly DiscordImage _image = new DiscordImage();

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


        public override string ToString()
        {
            return Name;
        }
    }
}