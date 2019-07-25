using Newtonsoft.Json;

namespace Discord
{
    public class InvalidParametersException : DiscordException
    {
        public string ErrorJson { get; private set; }
        public dynamic ErrorJsonObject { get; private set; }

        internal InvalidParametersException(DiscordClient client, string errorJson) : base(client, errorJson)
        {
            ErrorJson = errorJson;
            ErrorJsonObject = JsonConvert.DeserializeObject<dynamic>(ErrorJson);
        }


        public override string ToString()
        {
            return ErrorJson;
        }
    }
}
