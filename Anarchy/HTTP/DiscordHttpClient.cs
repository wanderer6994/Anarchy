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
        

        public void UpdateFingerprint()
        {
            Headers.Remove("X-Fingerprint");
            Headers.Add("X-Fingerprint", Get("/experiments").Deserialize<Experiments>().Fingerprint);
        }


        private void CheckResponse(HttpResponseMessage resp)
        {
            string content = resp.Content.ReadAsStringAsync().Result;

            if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new InvalidParametersException(_discordClient, content);
            else if (resp.StatusCode == (HttpStatusCode)429)
                throw new TooManyRequestsException(_discordClient, content.Deserialize<RateLimit>().RetryAfter);
            else if (resp.StatusCode > HttpStatusCode.NoContent)
                throw new DiscordHttpErrorException(_discordClient, content);
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