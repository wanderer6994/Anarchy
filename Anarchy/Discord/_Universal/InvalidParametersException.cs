using Newtonsoft.Json;

namespace Discord
{
    public class InvalidParametersException : DiscordException
    {
        // Currently a dynamic object since there isn't really any keys that are persistent across requests
        public dynamic ErrorJsonContent { get; private set; }

        public InvalidParametersException(DiscordClient client, string errorJson) : base(client, "Invalid parameters were passed")
        {
            ErrorJsonContent = JsonConvert.DeserializeObject<dynamic>(errorJson);
        }
    }
}
