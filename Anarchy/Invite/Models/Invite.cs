﻿using Newtonsoft.Json;

namespace Discord
{
    public class Invite : ClientMember
    {
        [JsonProperty("code")]
        public string Code { get; private set; }

        [JsonProperty("temporary")]
        public bool Temporary { get; private set; }

        [JsonProperty("guild")]
        public Guild Guild { get; private set; }

        [JsonProperty("channel")]
        public Channel Channel { get; private set; }

        [JsonProperty("inviter")]
        public User Creator { get; private set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; private set; }

        [JsonProperty("uses")]
        public int Uses { get; private set; }

        [JsonProperty("max_uses")]
        public int MaxUses { get; private set; }

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
