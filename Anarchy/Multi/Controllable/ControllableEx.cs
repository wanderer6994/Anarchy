using Newtonsoft.Json.Linq;

namespace Discord
{
    public class ControllableEx : Controllable
    {
        internal JObject Json { get; set; }
    }
}
