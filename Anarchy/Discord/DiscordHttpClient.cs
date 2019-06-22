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
            if (resp.StatusCode == HttpStatusCode.Forbidden || resp.StatusCode == HttpStatusCode.Unauthorized)
                throw new AccessDeniedException(_discordClient);
            else if (resp.StatusCode.ToString() == "429")
                throw new TooManyRequestsException(_discordClient, JsonConvert.DeserializeObject<RateLimit>(resp.Content.ReadAsStringAsync().Result).RetryAfter);
        }

        public async Task<HttpResponseMessage> SendAsync(string httpMethod, string url, string content = "")
        {
            HttpRequestMessage msg = new HttpRequestMessage
            {
                Method = new HttpMethod(httpMethod),
                RequestUri = new Uri("https://discordapp.com/api/v6" + url)
            };

            if (!string.IsNullOrEmpty(content))
                msg.Content = new StringContent(content, Encoding.UTF8, "application/json");

            var resp = await _httpClient.SendAsync(msg);
            CheckResponse(resp);
            return resp;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await SendAsync("GET", url);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, string content = "")
        {
            return await SendAsync("POST", url, content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await SendAsync("DELETE", url);
        }

        public async Task<HttpResponseMessage> PutAsync(string url, string content = "")
        {
            return await SendAsync("PUT", url);
        }

        public async Task<HttpResponseMessage> PatchAsync(string url, string content = "")
        {
            return await SendAsync("PATCH", url);
        }
    }
}