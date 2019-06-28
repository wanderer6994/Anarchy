using System.Net.Http;
using Newtonsoft.Json;
using Discord.Gateway;

namespace Discord
{
    internal static class JsonExtensions
    {
        private static T Deserialize<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static T Deserialize<T>(this HttpContent httpContent)
        {
            return Deserialize<T>(httpContent.ReadAsStringAsync().Result);
        }

        public static T Deserialize<T>(this GatewayResponse response)
        {
            return Deserialize<T>(response.Data.ToString());
        }
    }
}