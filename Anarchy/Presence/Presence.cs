using Newtonsoft.Json;

namespace Discord.Gateway
{
    internal class Presence
    {
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

        [JsonProperty("game")]
        public Activity Activity { get; set; }

        [JsonProperty("since")]
        public int Since { get; set; }

        [JsonProperty("afk")]
        public bool Afk { get; set; }


        public override string ToString()
        {
            return Status.ToString();
        }
    }
}
