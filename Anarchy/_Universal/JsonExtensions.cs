using System.Net.Http;
using Newtonsoft.Json;

namespace Discord
{
    internal static class JsonExtensions
    {
        public static T Json<T>(this HttpContent httpContent)
        {
            return JsonConvert.DeserializeObject<T>(httpContent.ReadAsStringAsync().Result);
        }
    }
}