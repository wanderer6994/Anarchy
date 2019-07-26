using System.Drawing;
using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// Options for modifying a <see cref="Group"/>
    /// </summary>
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


        public bool ShouldSerialize_icon()
        {
            return IconSet;
        }
    }
}
