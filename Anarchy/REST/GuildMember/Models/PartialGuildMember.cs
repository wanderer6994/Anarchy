using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Discord
{
    public class PartialGuildMember : Controllable
    {
        [JsonProperty("nick")]
        public string Nickname { get; internal set; }


        [JsonProperty("roles")]
        public IReadOnlyList<ulong> Roles { get; internal set; }


        [JsonProperty("joined_at")]
#pragma warning disable CS0649
        private string _joinedAt;
        public DateTime? JoinedAt
        {
            get
            {
                if (_joinedAt == null)
                    return null;

                return DiscordTimestamp.FromString(_joinedAt);
            }
            internal set
            {
                if (value == null)
                    _joinedAt = null;
                else
                    _joinedAt = DiscordTimestamp.ToString(value.Value);
            }
        }
#pragma warning restore CS0649
    }
}
