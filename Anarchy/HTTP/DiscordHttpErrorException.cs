using System;
using Newtonsoft.Json;

namespace Discord
{
    public class DiscordHttpErrorException : DiscordException
    {
        public HttpError Error { get; private set; }

        public DiscordHttpErrorException(DiscordClient client, string errorJson) : base(client)
        {
            Error = errorJson.Deserialize<HttpError>();
        }
    }
}
