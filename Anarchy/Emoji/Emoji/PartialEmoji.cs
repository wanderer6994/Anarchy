using Newtonsoft.Json;

namespace Discord
{
    public class PartialEmoji : Controllable
    {
        [JsonProperty("id")]
        public ulong? Id { get; private set; }


        [JsonProperty("name")]
        public string Name { get; protected set; }


        [JsonProperty("animated")]
        public bool Animated { get; private set; }


        public bool IsCustom
        {
            get { return Id != null; }
        }


        public string GetMessegable()
        {
            return IsCustom ? $"<:{Name}:{Id}>" : Name;
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
