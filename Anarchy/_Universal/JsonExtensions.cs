using System.Net.Http;
using Newtonsoft.Json;
using Discord.Gateway;

namespace Discord
{
    internal static class JsonExtensions
    {
        private static T Json<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static T Json<T>(this HttpContent httpContent)
        {
            return Json<T>(httpContent.ReadAsStringAsync().Result);
        }

        public static T Json<T>(this GatewayResponse response)
        {
            return Json<T>(response.Data.ToString());
        }
    }
}