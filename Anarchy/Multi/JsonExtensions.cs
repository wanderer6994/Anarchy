using System.Net.Http;
using Newtonsoft.Json;
using Discord.Gateway;

namespace Discord
{
    internal static class JsonExtensions
    {
        public static T Deserialize<T>(this string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }


        public static T Deserialize<T>(this HttpResponseMessage httpResponse)
        {
            return httpResponse.Content.ReadAsStringAsync().Result.Deserialize<T>();
        }


        public static T Deserialize<T>(this GatewayResponse response)
        {
            return response.Data.ToString().Deserialize<T>();
        }
    }
}