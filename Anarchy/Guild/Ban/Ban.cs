using Newtonsoft.Json;

namespace Discord
{
    public class Ban
    {
        [JsonProperty("reason")]
        public string Reason { get; private set; }

        [JsonProperty("user")]
        public User User { get; private set; }


        public override string ToString()
        {
            return User.ToString();
        }
    }
}
