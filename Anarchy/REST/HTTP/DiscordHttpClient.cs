using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Linq;

namespace Discord
{
    internal class DiscordHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly DiscordClient _discordClient;
        private static JSchema _errorSchema = new JSchemaGenerator().Generate(typeof(DiscordHttpError));

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
            Headers.Add("X-Fingerprint", Get("/experiments").Deserialize<JObject>().GetValue("fingerprint").ToString());
        }


        /// <summary>
        /// Checks an HTTP response for errors
        /// </summary>
        private void CheckResponse(HttpResponseMessage resp)
        {
            if (resp.StatusCode <= HttpStatusCode.NoContent)
                return;

            if (resp.StatusCode == (HttpStatusCode)429)
                throw new RateLimitException(_discordClient, resp.Deserialize<RateLimit>().RetryAfter);

            if (resp.StatusCode == HttpStatusCode.BadRequest)
            {
                if (!resp.Deserialize<JObject>().IsValid(_errorSchema))
                    throw new InvalidParametersException(_discordClient, resp.Content.ReadAsStringAsync().Result);
            }

            throw new DiscordHttpException(_discordClient, resp.Deserialize<DiscordHttpError>());
        }


        /// <summary>
        /// Sends an HTTP request and checks for errors
        /// </summary>
        /// <param name="method">HTTP method to use</param>
        /// <param name="endpoint">API endpoint (fx. /users/@me)</param>
        /// <param name="json">JSON content</param>
        private HttpResponseMessage Send(HttpMethod method, string endpoint, string json = null)
        {
            HttpRequestMessage msg = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri("https://discordapp.com/api/v6" + endpoint),
                Content = json != null ? new StringContent(json, Encoding.UTF8, "application/json") : null
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