using Newtonsoft.Json;
using System.Drawing;

namespace Discord
{
    /// <summary>
    /// Options for modifying a <see cref="Guild"/>
    /// </summary>
    public class GuildProperties
    {
        internal Property<string> NameProperty = new Property<string>();
        [JsonProperty("name")]
        public string Name
        {
            get { return NameProperty; }
            set { NameProperty.Value = value; }
        }


        public bool ShouldSerializeName()
        {
            return NameProperty.Set;
        }


        internal Property<string> RegionProperty = new Property<string>();
        [JsonProperty("region")]
        public string Region
        {
            get { return RegionProperty; }
            set { RegionProperty.Value = value; }
        }


        public bool ShouldSerializeRegion()
        {
            return RegionProperty.Set;
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


        public bool ShouldSerializeIcon()
        {
            return IconSet;
        }
        #endregion 


        internal Property<ulong> OwnerProperty = new Property<ulong>();
        [JsonProperty("owner_id")]
        public ulong OwnerId
        {
            get { return OwnerProperty; }
            set { OwnerProperty.Value = value; }
        }

        
        public bool ShouldSerializeOwnerId()
        {
            return OwnerProperty.Set;
        }


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
