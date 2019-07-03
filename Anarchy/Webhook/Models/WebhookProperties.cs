using Newtonsoft.Json;
using System.Drawing;

namespace Discord.Webhook
{
    public class WebhookProperties
    {
        [JsonProperty("channel_id")]
        public long? ChannelId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        #region avatar
        private readonly DiscordImage _image = new DiscordImage();

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


        public override string ToString()
        {
            return Name;
        }
    }
}
