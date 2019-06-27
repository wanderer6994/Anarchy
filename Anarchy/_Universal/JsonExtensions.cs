using System.Net.Http;
using Newtonsoft.Json;
using Discord.Gateway;

namespace Discord
{
    internal static class JsonExtensions
    {
        /// <summary>
        /// Deserializes <see cref="string"/> into json
        /// </summary>
        private static T Json<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        /// <summary>
        /// Deserializes <see cref="HttpContent"/> into json
        /// </summary>
        public static T Json<T>(this HttpContent httpContent)
        {
            return Json<T>(httpContent.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Deserializes <see cref="GatewayResponse"/> into json
        /// </summary>
        public static T Json<T>(this GatewayResponse response)
        {
            return Json<T>(response.Data.ToString());
        }
    }
}