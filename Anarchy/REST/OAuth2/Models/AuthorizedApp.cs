using System.Collections.Generic;
using Newtonsoft.Json;

namespace Discord
{
    public class AuthorizedApp : Controllable
    {
        [JsonProperty("scopes")]
        public IReadOnlyList<string> Scopes { get; private set; }


        [JsonProperty("application")]
        public OAuth2Application Application { get; private set; }

        /// <summary>
        /// Removes the authorized app from the current user
        /// </summary>
        public void Remove()
        {
            Client.RemoveAuthorizedApp(Application.Id);
        }
    }
}
