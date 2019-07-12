using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// Options for modifying a <see cref="VoiceChannel"/>
    /// </summary>
    public class VoiceChannelProperties : GuildChannelProperties
    {
        internal Property<uint> BitrateProperty = new Property<uint>();
        [JsonProperty("bitrate")]
        public uint Bitrate
        {
            get { return BitrateProperty; }
            set { BitrateProperty.Value = value; }
        }


        internal Property<uint> UserLimitProperty = new Property<uint>();
        [JsonProperty("user_limit")]
        public uint UserLimit
        {
            get { return UserLimitProperty; }
            set { UserLimitProperty.Value = value; }
        }
    }
}
