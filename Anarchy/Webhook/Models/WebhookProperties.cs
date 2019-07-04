using Newtonsoft.Json;
using System.Drawing;

namespace Discord.Webhook
{
    public class WebhookProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }


        #region avatar
        private readonly DiscordImage _image = new DiscordImage();

        [JsonProperty("avatar")]
        private string _avatar
        {
            get { return _image.ImageBase64; }
        }

        public bool AvatarSet { get; private set; }
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
                AvatarSet = true;
            }
        }
        #endregion


        internal Property<long> ChannelProperty = new Property<long>();
        [JsonProperty("channel_id")]
        public long ChannelId
        {
            get { return ChannelProperty; }
            set { ChannelProperty.Value = value; }
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
