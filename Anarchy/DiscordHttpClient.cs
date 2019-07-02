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
        private class Experiments
        {
            [JsonProperty("fingerprint")]
            public string Fingerprint { get; set; }
        }


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
        

        public void UpdateFingerprint()
        {
            Headers.Remove("X-Fingerprint");
            Headers.Add("X-Fingerprint", Get("/experiments").Deserialize<Experiments>().Fingerprint);
        }


        private void CheckResponse(HttpResponseMessage resp)
        {
            switch (resp.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new InvalidParametersException(_discordClient, resp.Content.ReadAsStringAsync().Result);
                case HttpStatusCode.Forbidden:
                    throw new AccessDeniedException(_discordClient);
                case HttpStatusCode.Unauthorized:
                    throw new AccessDeniedException(_discordClient);
                case (HttpStatusCode)429:
                    throw new TooManyRequestsException(_discordClient, JsonConvert.DeserializeObject<RateLimit>(resp.Content.ReadAsStringAsync().Result).RetryAfter);
            }
        }


        public HttpResponseMessage Send(HttpMethod method, string endpoint, string content = null)
        {
            HttpRequestMessage msg = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri("https://discordapp.com/api/v6" + endpoint),
                Content = content != null ? new StringContent(content, Encoding.UTF8, "application/json") : null
            };

            var resp = _httpClient.SendAsync(msg).Result;
            CheckResponse(resp);
            return resp;
        }


        public HttpResponseMessage Get(string endpoint)
        {
            return Send(HttpMethod.Get, endpoint);
        }


        public HttpResponseMessage Post(string endpoint, string json = "")
        {
            return Send(HttpMethod.Post, endpoint, json);
        }


        public HttpResponseMessage Delete(string endpoint)
        {
            return Send(HttpMethod.Delete, endpoint);
        }


        public HttpResponseMessage Put(string endpoint, string json = "")
        {
            return Send(HttpMethod.Put, endpoint, json);
        }


        public HttpResponseMessage Patch(string endpoint, string json = "")
        {
            return Send(new HttpMethod("PATCH"), endpoint, json);
        }
    }
}