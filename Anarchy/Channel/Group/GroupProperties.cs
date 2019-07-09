using System.Drawing;
using Newtonsoft.Json;

namespace Discord
{
    public class GroupProperties : ChannelProperties
    {
        private readonly DiscordImage _image = new DiscordImage();

        [JsonProperty("icon")]
        private string _icon
        {
            get { return _image.ImageBase64; }
        }


        internal bool IconSet { get; private set; }
        [JsonIgnore]
        public Image Icon
        {
            get { return _image.Image; }
            set
            {
                _image.Image = value;
                IconSet = true;
            }
        }
    }
}
