using Newtonsoft.Json;

namespace Discord
{
    public class IdContainer
    {
        [JsonProperty("id")]
        public long Id { get; private set; }


        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
