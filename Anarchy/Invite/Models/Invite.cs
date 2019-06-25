using Newtonsoft.Json;

namespace Discord
{
    public class Invite : ClientClassBase
    {
        [JsonProperty("code")]
        public string Code { get; private set; }

        [JsonProperty("guild")]
        public Guild Guild { get; private set; }

        [JsonProperty("channel")]
        public Channel Channel { get; private set; }

        [JsonProperty("approximate_presence_count")]
        public int OnlineMembers { get; private set; }

        [JsonProperty("approximate_member_count")]
        public int TotalMembers { get; private set; }


        public Invite Delete()
        {
            return Client.DeleteInvite(Code);
        }
    }
}
