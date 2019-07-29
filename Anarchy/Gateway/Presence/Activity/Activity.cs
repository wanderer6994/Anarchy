using Newtonsoft.Json;

namespace Discord.Gateway
{
    public class Activity
    {
        public Activity()
        {
            _timestamps = new ActivityTimestamps();
        }


        public Activity(string name, ActivityType type) : this()
        {
            Name = name;
            Type = type;
        }


        [JsonProperty("name")]
        public string Name { get; set; }


        [JsonProperty("type")]
        public ActivityType Type { get; set; }


        [JsonProperty("url")]
        public string Url { get; set; }


        [JsonProperty("details")]
        public string Details { get; set; }


        [JsonProperty("state")]
        public string State { get; set; }


        [JsonProperty("timestamps")]
        private ActivityTimestamps _timestamps;
        internal uint? Since
        {
            get { return _timestamps.Start; }
            set { _timestamps.Start = value; }
        }


        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
