using Newtonsoft.Json;

namespace Discord
{
    //Some properties might not have been set here. i haven't found any so far tho /shrug
    public class GuildMemberProperties
    {
        internal Property<string> NickProperty = new Property<string>();
        [JsonProperty("nick")]
        public string Nickname
        {
            get { return NickProperty; }
            set { NickProperty.Value = value; }
        }


        public bool ShouldSerializeNickname()
        {
            return NickProperty.Set;
        }


        internal Property<bool> MuteProperty = new Property<bool>();
        [JsonProperty("mute")]
        public bool Muted
        {
            get { return MuteProperty; }
            set { MuteProperty.Value = value; }
        }


        public bool ShouldSerializeMuted()
        {
            return MuteProperty.Set;
        }


        internal Property<bool> DeafProperty = new Property<bool>();
        [JsonProperty("deaf")]
        public bool Deafened
        {
            get { return DeafProperty; }
            set { DeafProperty.Value = value; }
        }


        public bool ShouldSerializeDeafened()
        {
            return DeafProperty.Set;
        }
    }
}
