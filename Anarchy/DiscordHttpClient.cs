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


        public HttpResponseMessage Send(HttpMethod method, string endpoint, string content = null)
        {
            HttpRequestMessage msg = new HttpRequestMessage
            {
                Method = method,
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
            return Send(HttpMethod.Get, endpoint);
        }


        public HttpResponseMessage Post(string endpoint, string content = "")
        {
            return Send(HttpMethod.Post, endpoint, content);
        }


        public HttpResponseMessage Delete(string endpoint)
        {
            return Send(HttpMethod.Delete, endpoint);
        }


        public HttpResponseMessage Put(string endpoint, string content = "")
        {
            return Send(HttpMethod.Put, endpoint, content);
        }


        public HttpResponseMessage Patch(string endpoint, string content = "")
        {
            return Send(new HttpMethod("PATCH"), endpoint, content);
        }
    }
}