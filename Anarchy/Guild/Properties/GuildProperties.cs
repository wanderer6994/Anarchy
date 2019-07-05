using Newtonsoft.Json;
using System.Drawing;

namespace Discord
{
    public class GuildProperties
    {
        internal Property<string> NameProperty = new Property<string>();
        [JsonProperty("name")]
        public string Name
        {
            get { return NameProperty; }
            set { NameProperty.Value = value; }
        }


        internal Property<string> RegionProperty = new Property<string>();
        [JsonProperty("region")]
        public string Region
        {
            get { return RegionProperty; }
            set { RegionProperty.Value = value; }
        }


        #region icon
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
        #endregion


        internal Property<GuildVerificationLevel> VerificationProperty = new Property<GuildVerificationLevel>();
        [JsonProperty("verification_level")]
        public GuildVerificationLevel VerificationLevel
        {
            get { return VerificationProperty; }
            set { VerificationProperty.Value = value; }
        }


        internal Property<GuildDefaultNotifications> NotificationsProperty = new Property<GuildDefaultNotifications>();
        [JsonProperty("default_message_notifications")]
        public GuildDefaultNotifications DefaultNotifications
        {
            get { return NotificationsProperty; }
            set { NotificationsProperty.Value = value; }
        }
    }
}
