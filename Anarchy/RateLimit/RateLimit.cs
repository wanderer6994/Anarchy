using Newtonsoft.Json;

namespace Discord
{
    class RateLimit
    {
        [JsonProperty("global")]
        public bool Global { get; private set; }


        [JsonProperty("message")]
        public string Message { get; private set; }


        [JsonProperty("retry_after")]
        public uint RetryAfter { get; private set; }


        public override string ToString()
        {
            return RetryAfter.ToString();
        }
    }
}