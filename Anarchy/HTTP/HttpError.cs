using Newtonsoft.Json;

namespace Discord
{
    public class HttpError
    {
        [JsonProperty("code")]
        public int Code { get; private set; }

        [JsonProperty("message")]
        public string Message { get; private set; }
    }
}
