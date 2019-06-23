/*
   An HTTP client that makes it easier to check for common errors
*/

using System;
using System.Threading.Tasks;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Discord
{
    internal class DiscordHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly DiscordClient _discordClient;

        public HttpRequestHeaders Headers
        {
            get { return _httpClient.DefaultRequestHeaders; }
        }

        public DiscordHttpClient(DiscordClient discordClient)
        {
            _httpClient = new HttpClient();
            _discordClient = discordClient;
        }
        
        private void CheckResponse(HttpResponseMessage resp)
        {
            switch (resp.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new InvalidParametersException(_discordClient, resp.Content.ReadAsStringAsync().Result);
                case HttpStatusCode.Unauthorized:
                    throw new AccessDeniedException(_discordClient);
                case HttpStatusCode.Forbidden:
                    throw new AccessDeniedException(_discordClient);
            }

            if (resp.StatusCode.ToString() == "429")
                throw new TooManyRequestsException(_discordClient, JsonConvert.DeserializeObject<RateLimit>(resp.Content.ReadAsStringAsync().Result).RetryAfter);
        }

        public HttpResponseMessage Send(string httpMethod, string url, string content = null)
        {
            HttpRequestMessage msg = new HttpRequestMessage
            {
                Method = new HttpMethod(httpMethod),
                RequestUri = new Uri("https://discordapp.com/api/v6" + url)
            };

            if (content != null)
                msg.Content = new StringContent(content, Encoding.UTF8, "application/json");

            var resp =  _httpClient.SendAsync(msg).Result;
            CheckResponse(resp);
            return resp;
        }

        public HttpResponseMessage Get(string url)
        {
            return Send("GET", url);
        }

        public HttpResponseMessage Post(string url, string content = "")
        {
            return Send("POST", url, content);
        }

        public HttpResponseMessage Delete(string url)
        {
            return Send("DELETE", url);
        }

        public HttpResponseMessage Put(string url, string content = "")
        {
            return Send("PUT", url, content);
        }

        public HttpResponseMessage Patch(string url, string content = "")
        {
            return Send("PATCH", url, content);
        }
    }
}