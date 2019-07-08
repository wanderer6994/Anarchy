using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Discord.Gateway
{
    public class PresenceUpdate
    {
        [JsonProperty("user")]
        private readonly JObject _user;
        public ulong UserId
        {
            get { return ulong.Parse(_user.GetValue("id").ToString()); }
        }


        [JsonProperty("nick")]
        public string Nickname { get; private set; }

        [JsonProperty("roles")]
        public IReadOnlyList<ulong> Roles { get; private set; }

        [JsonProperty("game")]
        public Activity Activity { get; private set; }

        [JsonProperty("guild_id")]
        public long GuildId { get; private set; }

        [JsonProperty("status")]
        private string _status;
        public UserStatus Status
        {
            get
            {
                UserStatus status = UserStatus.Offline;

                switch (_status)
                {
                    case "online":
                        status = UserStatus.Online;
                        break;
                    case "idle":
                        status = UserStatus.Idle;
                        break;
                    case "dnd":
                        status = UserStatus.DoNotDisturb;
                        break;
                }

                return status;
            }
            set
            {
                _status = value != UserStatus.DoNotDisturb ? value.ToString().ToLower() : "dnd";
            }
        }

        public bool InGuild
        {
            get { return GuildId == 0; }
        }


        public override string ToString()
        {
            return UserId.ToString();
        }
    }
}
