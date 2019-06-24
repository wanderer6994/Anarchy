using System;
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

        public HttpResponseMessage Send(string httpMethod, string endpoint, string content = null)
        {
            HttpRequestMessage msg = new HttpRequestMessage
            {
                Method = new HttpMethod(httpMethod),
                RequestUri = new Uri("https://discordapp.com/api/v6" + endpoint)
            };

            if (content != null)
                msg.Content = new StringContent(content, Encoding.UTF8, "application/json");

            var resp =  _httpClient.SendAsync(msg).Result;
            CheckResponse(resp);
            return resp;
        }

        public HttpResponseMessage Get(string endpoint)
        {
            return Send("GET", endpoint);
        }

        public HttpResponseMessage Post(string endpoint, string content = "")
        {
            return Send("POST", endpoint, content);
        }

        public HttpResponseMessage Delete(string endpoint)
        {
            return Send("DELETE", endpoint);
        }

        public HttpResponseMessage Put(string endpoint, string content = "")
        {
            return Send("PUT", endpoint, content);
        }

        public HttpResponseMessage Patch(string endpoint, string content = "")
        {
            return Send("PATCH", endpoint, content);
        }
    }
}