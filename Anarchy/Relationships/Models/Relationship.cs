using Newtonsoft.Json;

namespace Discord
{
    public class Relationship
    {
        [JsonProperty("user")]
        public User User { get; private set; }

        [JsonProperty("type")]
        public RelationshipType Type { get; internal set; }


        public override string ToString()
        {
            return $"{Type} {User}";
        }
    }
}