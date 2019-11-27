using System.Net.Http;
using Newtonsoft.Json;
using Discord.Gateway;
using Leaf.xNet;
using Newtonsoft.Json.Linq;

namespace Discord
{
    public static class JsonExtensions
    {
        internal static T Deserialize<T>(this string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }


        public static T Deserialize<T>(this HttpResponse response)
        {
            return response.ToString().Deserialize<T>();
        }


        public static T DeserializeEx<T>(this HttpResponse response) where T : ControllableEx
        {
            JObject json = JObject.Parse(response.ToString());
            return ((T)json.ToObject(typeof(T))).SetJson(json);
        }



        internal static T Deserialize<T>(this GatewayResponse response)
        {
            return response.Data.ToString().Deserialize<T>();
        }
    }
}