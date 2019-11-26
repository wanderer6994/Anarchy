using System.Net.Http;
using Newtonsoft.Json;
using Discord.Gateway;
using Leaf.xNet;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Discord
{
    internal static class JsonExtensions
    {
        public static T Deserialize<T>(this string content)
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



        public static T Deserialize<T>(this GatewayResponse response)
        {
            return response.Data.ToString().Deserialize<T>();
        }
    }
}